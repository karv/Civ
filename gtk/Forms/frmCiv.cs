using System;
using Civ.Ciencias;
using Civ.ObjetosEstado;
using Civ.RAW;
using Civ.Global;
using Civ.Topología;

namespace Gtk
{
	#region EntryLists
	class CienciaConoListEntry : TreeNode
	{
		public readonly Ciencia Ciencia;

		public CienciaConoListEntry (Ciencia c)
		{
			Ciencia = c;
		}

		[Gtk.TreeNodeValue (Column = 0)]
		public string Nombre { get { return Ciencia.Nombre; } }
	}

	class CienciaAbtaListEntry : TreeNode
	{
		public readonly InvestigandoCiencia Ciencia;

		public CienciaAbtaListEntry (InvestigandoCiencia c)
		{
			Ciencia = c;
		}

		[Gtk.TreeNodeValue (Column = 1)]
		public string Nombre { get { return Ciencia.Ciencia.Nombre; } }

		[Gtk.TreeNodeValue (Column = 0)]
		public float Pct { get { return Ciencia.ObtPct () * 100; } }
	}

	class CityListEntry : TreeNode
	{
		public readonly ICiudad Ciudad;

		[Gtk.TreeNodeValue (Column = 0)]
		public string Nombre
		{
			get
			{
				return Ciudad.Nombre;
			}
		}

		[Gtk.TreeNodeValue (Column = 1)]
		public ulong Población
		{
			get
			{
				return Ciudad.GetPoblacionInfo.Total;
			}
		}

		[Gtk.TreeNodeValue (Column = 2)]
		public float Ocupación { get { return (float)Ciudad.NumTrabajadores * 100 / Ciudad.GetPoblacionInfo.Productiva; } }

		[Gtk.TreeNodeValue (Column = 3)]
		public string NombreTerreno { get { return Ciudad.Posición ().A.ToString (); } }

		public CityListEntry (ICiudad ciudad)
		{
			Ciudad = ciudad;
		}
	}

	class CienciaDetalleListEntry : TreeNode
	{
		readonly Recurso _recurso;
		readonly InvestigandoCiencia _invest;

		public CienciaDetalleListEntry (InvestigandoCiencia inv, Recurso recurso)
		{
			_invest = inv;
			_recurso = recurso;
		}

		[Gtk.TreeNodeValue (Column = 0)]
		public string Recurso
		{
			get { return _recurso.ToString (); }
		}

		[Gtk.TreeNodeValue (Column = 1)]
		public double Progreso
		{
			get { return (double)_invest [_recurso] * 100 / _invest.Ciencia.Reqs.Recursos [_recurso]; }
		}
	}
	#endregion

	public partial class FrmCiv : Window
	{
		public Civilización Civ;

		public readonly System.Collections.Generic.List<IActualizable> FormsActualizables = new System.Collections.Generic.List<IActualizable> ();

		#region IActualizable implementation

		/// <summary>
		/// Actualiza esta form y todas sus hijas.
		/// </summary>
		public void Actualizar ()
		{
			ActualizarDebil ();
			foreach (var x in FormsActualizables)
			{
				x.Actualizar ();
			}
		}

		/// <summary>
		/// Actualiza esta form
		/// </summary>
		public void ActualizarDebil ()
		{
			stCiudad.Clear ();
			foreach (var x in Civ.Ciudades)
			{
				stCiudad.AddNode (new CityListEntry (x));
			}

			stCienciasCono.Clear ();
			foreach (var x in Civ.Avances)
			{
				stCienciasCono.AddNode (new CienciaConoListEntry (x));
			}

			stCienciasAbtas.Clear ();
			foreach (var x in Civ.Investigando)
			{				
				stCienciasAbtas.AddNode (new CienciaAbtaListEntry (x));
			}

			ActualizaDetalle ();

			ArmadaSelector.Clear ();
			foreach (var x in Civ.Armadas)
			{
				if (!x.EsDefensa)
					ArmadaSelector.Add (x);
			}
		}

		/// <summary>
		/// Actualiza los detalles de investigación
		/// </summary>
		void ActualizaDetalle ()
		{
			// Obtener nodo seleccionado
			NodeSelection r = nvInvestigando.NodeSelection;
			if (r.SelectedNode != null)
			{
				InvestigandoCiencia c = ((CienciaAbtaListEntry)r.SelectedNode).Ciencia;

				stCienciaDetail.Clear ();
				foreach (var x in c.Keys)
				{
					stCienciaDetail.AddNode (new CienciaDetalleListEntry (c, x));
				}
			}
		}

		#endregion

		NodeStore stCiudad = new NodeStore (typeof (CityListEntry));
		NodeStore stCienciasCono = new NodeStore (typeof (CienciaConoListEntry));
		NodeStore stCienciasAbtas = new NodeStore (typeof (CienciaAbtaListEntry));
		NodeStore stCienciaDetail = new NodeStore (typeof (CienciaDetalleListEntry));

		public FrmCiv (Civilización civ)
			: base (WindowType.Toplevel)
		{
			Civ = civ;

			Build ();

			Mens.Manejador = civ.Mensajes;
			Title = civ.Nombre;

			ActualizarDebil ();

			nvCiudades.NodeStore = stCiudad;
			nvAvances.NodeStore = stCienciasCono;
			nvInvestigando.NodeStore = stCienciasAbtas;
			nvInvestDetalle.NodeStore = stCienciaDetail;

			nvCiudades.AppendColumn (
				"Nombre",
				new CellRendererNombreCiudad (stCiudad),
				"text",
				0);
			nvCiudades.AppendColumn ("Población", new CellRendererText (), "text", 1);
			nvCiudades.AppendColumn (
				"Ocupación",
				new CellRendererProgress (),
				"value",
				2);
			nvCiudades.AppendColumn ("Terreno", new CellRendererText (), "text", 3);

			nvAvances.AppendColumn ("Avance", new CellRendererText (), "text", 0);

			nvInvestigando.AppendColumn ("%", new CellRendererProgress (), "text", 0);
			nvInvestigando.AppendColumn ("Avance", new CellRendererText (), "text", 1);

			nvInvestDetalle.AppendColumn ("Recurso", new CellRendererText (), "text", 0);
			nvInvestDetalle.AppendColumn (
				"Progreso",
				new CellRendererProgress (),
				"value",
				1);


			nvCiudades.Columns [0].Reorderable = false;
			nvCiudades.Columns [1].Reorderable = true;
			nvCiudades.Columns [2].Reorderable = true;
			nvAvances.Columns [0].Reorderable = true;
			nvInvestigando.Columns [0].MaxWidth = 70;

			//nvCiudades.KeyPressEvent += OnNvCiudadesKeyPressEvent;
			//Default = cmdIrCiudad;
		}

		protected override bool OnDeleteEvent (Gdk.Event evnt)
		{
			Application.Quit ();
			Juego.Guardar ();
			CivGTK.MainClass.EndGame = true;
			return true;
		}

		/// <summary>
		/// Ir a la ciudad
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		protected void OnCmdIrActivated (object sender, EventArgs e)
		{
			Console.WriteLine (sender);
			Console.WriteLine (e);
			MuestraCiudad ();
		}

		protected void MuestraCiudad ()
		{
			NodeSelection r = nvCiudades.NodeSelection;
			if (r.SelectedNode != null)
			{
				var c = ((CityListEntry)r.SelectedNode).Ciudad as Ciudad;

				var wind = new FrmCiudad (c, this);
				FormsActualizables.Add (wind);
				wind.Show ();
			}
		}


		protected void OnNvInvestigandoCursorChanged (object sender, EventArgs e)
		{
			ActualizaDetalle ();
		}

		protected void OnArmadaSelectoronSelectionChanged (object sender,
		                                                   EventArgs e)
		{
			Armada selArmada = ArmadaSelector.Selected;
			if (selArmada == null)
			{
				ArmadaSeleccionadaInfo.Visible = false;
			}
			else
			{
				ArmadaSeleccionadaInfo.Armada = selArmada;
				ArmadaSeleccionadaInfo.Actualizar ();
				lbPos.Text = selArmada.Posición.ToString ();

				// Las órdenes
				// Orden Ir a
				IrACB.LlenarCon (selArmada.Posición.Vecindad (), (x => x.ToString ()));
			}
		}

		protected void OnCmdActualizaClicked (object sender, EventArgs e)
		{
			ActualizarDebil ();
		}

		/// <summary>
		/// Cuando le das OrdenIr a una Armada
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		protected void OnCmdIrAClicked (object sender, EventArgs e)
		{
			var destino = IrACB.Selected as Terreno;
			var selArmada = ArmadaSelector.Selected;

			selArmada.Orden = new Civ.Orden.OrdenIrALugar (selArmada, destino.Pos);
		}

		protected void OnCmdColonizarClicked (object sender, EventArgs e)
		{
			Armada selArmada = ArmadaSelector.Selected;
			Stack Colonizador = null;
			if (selArmada?.PuedeColonizar (out Colonizador) ?? false)
			{
				Colonizador.Colonizar ();
			}
		}

		protected void OnNotebook1SwitchPage (object sender, EventArgs e)
		{
			ActualizarDebil ();
		}

	}
}