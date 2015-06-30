//
//  frmCiudad.cs
//
//  Author:
//       Edgar Carballo <karvayoEdgar@gmail.com>
//
//  Copyright (c) 2015 edgar
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using Civ;
using Gtk;

namespace gtk
{
	#region TreeNodes
	class TrabajoListEntry : Gtk.TreeNode
	{
		public readonly Trabajo trabajo;

		[Gtk.TreeNodeValue(Column = 0)]
		public string nombre
		{
			get
			{
				return trabajo.RAW.Nombre;
			}
		}

		[Gtk.TreeNodeValue(Column = 1)]
		public ulong Trabajadores
		{
			get
			{
				return trabajo.Trabajadores;
			}
			set
			{
				trabajo.Trabajadores = value;
			}
		}

		[Gtk.TreeNodeValue(Column = 2)]
		public ulong MaxTrabajadores
		{
			get
			{
				return trabajo.MaxTrabajadores;
			}
		}

		[Gtk.TreeNodeValue(Column = 3)]
		public float Prioridad
		{
			get
			{
				return trabajo.Prioridad;
			}
			set
			{
				trabajo.Prioridad = value;
			}
		}

		[Gtk.TreeNodeValue(Column = 4)]
		public string edificio
		{
			get
			{
				return trabajo.EdificioBase.Nombre;
			}
		}

		public TrabajoListEntry(Trabajo trabajo)
		{
			this.trabajo = trabajo;
		}
	}

	class RecursoListEntry : Gtk.TreeNode
	{
		const string iconDir = "img//";
		const string nullIconFile = "Comida.jpg";
		const int iconSize_x = 24;
		const int iconSize_y = 24;

		IAlmacenante almacen;
		Recurso recurso;

		//public readonly ListasExtra.ReadonlyPair<Recurso, float> data;
		readonly Gdk.Pixbuf _icon;

		public RecursoListEntry(IAlmacenante almacén, Recurso recurso)
		{
			this.almacen = almacén;
			this.recurso = recurso;
			_icon = buildIcon();
		}

		Gdk.Pixbuf buildIcon()
		{
			string IconName = recurso.Img;
			if (IconName == null)
			{
				System.Diagnostics.Debug.WriteLine(string.Format("Recurso {0} con enlace a icono roto a {1}. Usando icono genérico.", 
					nombre, cant));
				return new Gdk.Pixbuf(iconDir + nullIconFile, iconSize_x, iconSize_y);
			}
			return new Gdk.Pixbuf(iconDir + IconName, iconSize_x, iconSize_y);
		}

		[Gtk.TreeNodeValue(Column = 1)]
		public string nombre { get { return recurso.Nombre; } }

		[Gtk.TreeNodeValue(Column = 2)]
		public float cant { get { return almacen.obtenerRecurso(recurso); } }

		[Gtk.TreeNodeValue(Column = 0)]
		public Gdk.Pixbuf icon
		{ 
			get
			{ 
				return _icon;
			} 
		}
	}
	#endregion

	public partial class frmCiudad : Gtk.Window, IActualizable
	{
		public readonly Ciudad ciudad;
		public readonly CivGTK.frmCiv mainWindow;

		#region IActualizable implementation

		public void Actualizar()
		{
			CivGTK.ThreadManager.Pausar();

			// Construir recStore
			stRecurso.Clear();
			foreach (System.Collections.Generic.KeyValuePair<Recurso, float> x in ciudad.Almacen)
			{
				stRecurso.AddNode(new RecursoListEntry(ciudad, x.Key));
			}
			// Construir lista de trabajos
			stTrabajo.Clear();
			foreach (var x in ciudad.obtenerTrabajosAbiertos())
			{
				stTrabajo.AddNode(new TrabajoListEntry(ciudad.EncuentraInstanciaTrabajo(x)));
			}

			armDefensa.Actualizar();

			rcReclutar.ConstruirModelo();

			CivGTK.ThreadManager.Continuar();

			//Llenar etiquetas
			Title = ciudad.Nombre;
			popdisplay1.Refresh();
		}

		#endregion

		NodeStore stRecurso = new NodeStore(typeof(RecursoListEntry));
		NodeStore stTrabajo = new NodeStore(typeof(TrabajoListEntry));

		public frmCiudad(Ciudad ciudad, CivGTK.frmCiv main) :
			base(Gtk.WindowType.Toplevel)
		{
			this.mainWindow = main;
			this.ciudad = ciudad;
			this.Build();

			armDefensa.Armada = ciudad.Defensa;
			rcReclutar.ciudad = ciudad;
			popdisplay1.Ciudad = ciudad;

			rcReclutar.ConstruirModelo();

			Actualizar();

			nvTrabajos.NodeStore = stTrabajo;
			nvTrabajos.AppendColumn("Nombre", new Gtk.CellRendererText(), "text", 0);
			nvTrabajos.AppendColumn("Trabajadores", new CellRendererNumTrab(stTrabajo), "text", 1);
			nvTrabajos.AppendColumn("Máx. trab", new Gtk.CellRendererText(), "text", 2);
			nvTrabajos.AppendColumn("Prioridad", new CellRendererPrioridadTrab(stTrabajo), "text", 3);
			nvTrabajos.AppendColumn("Edificio", new CellRendererText(), "text", 4);

			nvRecursos.NodeStore = stRecurso;
			nvRecursos.AppendColumn("Icono", new Gtk.CellRendererPixbuf(), "pixbuf", 0);
			nvRecursos.AppendColumn("Nombre", new Gtk.CellRendererText(), "text", 1);
			nvRecursos.AppendColumn("Cantidad", new Gtk.CellRendererText(), "text", 2);
		}

		protected void OnCmdRenombrarCiudadClicked(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		protected override void OnDestroyed()
		{
			mainWindow.formsActualizables.Remove(this);
			base.OnDestroyed();
		}

		protected void OnNotebook1SwitchPage(object o, SwitchPageArgs args)
		{
			Actualizar();
		}
	}

}