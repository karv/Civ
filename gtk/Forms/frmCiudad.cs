using System;
using Civ;
using Gtk;
using Civ.Data;

namespace Gtk
{
	#region TreeNodes
	class TrabajoListEntry : TreeNode
	{
		public readonly Trabajo Trabajo;

		[Gtk.TreeNodeValue (Column = 0)]
		public string Nombre
		{
			get
			{
				return Trabajo.RAW.Nombre;
			}
		}

		[Gtk.TreeNodeValue (Column = 1)]
		public ulong Trabajadores
		{
			get
			{
				return Trabajo.Trabajadores;
			}
			set
			{
				Trabajo.Trabajadores = value;
			}
		}

		[Gtk.TreeNodeValue (Column = 2)]
		public ulong MaxTrabajadores
		{
			get
			{
				return Trabajo.MaxTrabajadores;
			}
		}

		[Gtk.TreeNodeValue (Column = 3)]
		public float Prioridad
		{
			get
			{
				return Trabajo.Prioridad;
			}
			set
			{
				Trabajo.Prioridad = value;
			}
		}

		[Gtk.TreeNodeValue (Column = 4)]
		public string Edificio
		{
			get
			{
				return Trabajo.EdificioBase.Nombre;
			}
		}

		public TrabajoListEntry (Trabajo trabajo)
		{
			Trabajo = trabajo;
		}
	}

	class RecursoListEntry : TreeNode
	{
		const string iconDir = "img//";
		const string nullIconFile = "Comida.jpg";
		const int iconSize_x = 24;
		const int iconSize_y = 24;

		readonly IAlmacénRead almacén;
		readonly Recurso recurso;

		//public readonly ListasExtra.ReadonlyPair<Recurso, float> data;
		readonly Gdk.Pixbuf _icon;

		public RecursoListEntry (IAlmacénRead almacén, Recurso recurso)
		{
			this.almacén = almacén;
			this.recurso = recurso;
			_icon = buildIcon ();
		}

		Gdk.Pixbuf buildIcon ()
		{
			string IconName = recurso.Img;
			if (IconName == null)
			{
				System.Diagnostics.Debug.WriteLine (string.Format ("Recurso {0} con enlace a icono roto a {1}. Usando icono genérico.", 
					Nombre, Cantidad));
				return new Gdk.Pixbuf (iconDir + nullIconFile, iconSize_x, iconSize_y);
			}
			return new Gdk.Pixbuf (iconDir + IconName, iconSize_x, iconSize_y);
		}

		[Gtk.TreeNodeValue (Column = 1)]
		public string Nombre { get { return recurso.Nombre; } }

		[Gtk.TreeNodeValue (Column = 2)]
		public float Cantidad { get { return almacén [recurso]; } }

		[Gtk.TreeNodeValue (Column = 0)]
		public Gdk.Pixbuf Icono
		{ 
			get
			{ 
				return _icon;
			} 
		}

		[Gtk.TreeNodeValue (Column = 3)]
		public float Delta
		{ 
			get
			{ 
				var AlmCiudad = almacén as AlmacénCiudad;
				return AlmCiudad == null ? 0 : AlmCiudad.CiudadDueño.CalculaDeltaRecurso (recurso);
			} 
		}
	}
	#endregion

	public partial class FrmCiudad : Window, IActualizable
	{
		public readonly ICiudad Ciudad;
		public readonly frmCiv MainWindow;

		#region IActualizable implementation

		public void Actualizar ()
		{
			// Construir recStore
			stRecurso.Clear ();
			foreach (var x in Ciudad.Almacén.Recursos)
			{
				stRecurso.AddNode (new RecursoListEntry (Ciudad.Almacén, x));
			}
			// Construir lista de trabajos
			stTrabajo.Clear ();
			foreach (var x in Ciudad .ObtenerTrabajosAbiertos())
			{
				stTrabajo.AddNode (new TrabajoListEntry (Ciudad.EncuentraInstanciaTrabajo (x)));
			}

			ArmadaCombobox.Clear ();
			foreach (var x in Ciudad.ArmadasEnCiudad())
			{
				ArmadaCombobox.Add (x);
			}

			armSeleccionada.Visible = false;

			armDefensa.Actualizar ();

			rcReclutar.ConstruirModelo ();

			//Llenar etiquetas
			Title = Ciudad.Nombre;
			popdisplay1.Refresh ();

			ShowAll ();
		}

		#endregion

		NodeStore stRecurso = new NodeStore (typeof (RecursoListEntry));
		NodeStore stTrabajo = new NodeStore (typeof (TrabajoListEntry));

		public FrmCiudad (ICiudad ciudad, frmCiv main)
			: base (WindowType.Toplevel)
		{
			MainWindow = main;
			Ciudad = ciudad;
			Build ();

			//ArmadaCombobox.Add(ciudad.Defensa, "Defensa");

			armDefensa.Armada = ciudad.Defensa;
			rcReclutar.ciudad = ciudad;
			popdisplay1.Ciudad = ciudad;

			rcReclutar.ConstruirModelo ();

			Actualizar ();

			nvTrabajos.NodeStore = stTrabajo;
			nvTrabajos.AppendColumn ("Nombre", new CellRendererText (), "text", 0);
			nvTrabajos.AppendColumn (
				"Trabajadores",
				new CellRendererNumTrab (stTrabajo),
				"text",
				1);
			nvTrabajos.AppendColumn (
				"Máx. trab",
				new CellRendererText (),
				"text",
				2);
			nvTrabajos.AppendColumn (
				"Prioridad",
				new CellRendererPrioridadTrab (stTrabajo),
				"text",
				3);
			nvTrabajos.AppendColumn ("Edificio", new CellRendererText (), "text", 4);

			nvRecursos.NodeStore = stRecurso;
			nvRecursos.AppendColumn (
				"Icono",
				new CellRendererPixbuf (),
				"pixbuf",
				0);
			nvRecursos.AppendColumn ("Nombre", new CellRendererText (), "text", 1);
			nvRecursos.AppendColumn ("Cantidad", new CellRendererText (), "text", 2);
			nvRecursos.AppendColumn ("Delta/h", new CellRendererText (), "text", 3);
		}

		// Analysis disable UnusedParameter
		protected void OnCmdRenombrarCiudadClicked (object sender, EventArgs e)
		{
			throw new NotImplementedException ();
		}

		protected override void OnDestroyed ()
		{
			MainWindow.formsActualizables.Remove (this);
			base.OnDestroyed ();
		}

		protected void OnNotebook1SwitchPage (object o, SwitchPageArgs args)
		{
			Actualizar ();
		}

		protected void OnCmdAddArmadaClicked (object sender, EventArgs e)
		{
			new Armada (Ciudad);
			Actualizar ();
		}

		protected void OnArmadaComboboxonSelectionChanged (object sender,
		                                                   EventArgs e)
		{
			Armada selArmada = ArmadaCombobox.getSelected ();
			if (selArmada == null)
			{
				armSeleccionada.Visible = false;
			}
			else
			{
				armSeleccionada.Armada = selArmada;
				armSeleccionada.Actualizar ();
			}
		}

		protected void OnCmdAddClicked (object sender, EventArgs e)
		{
			Stack c = armDefensa.getSelected ();
			Armada selArmada = ArmadaCombobox.getSelected ();
			if (c == null || selArmada == null)
			{
				System.Diagnostics.Debug.WriteLine ("No se seleccionó unidad o armada.");
				return;
			}
			selArmada.AgregaUnidad (c.RAW, c.Cantidad);
			armDefensa.Armada.QuitarUnidad (c);
			armDefensa.Actualizar ();
			armSeleccionada.Actualizar ();
		}
		// Analysis restore UnusedParameter
	}
}