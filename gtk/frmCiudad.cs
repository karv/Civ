﻿//
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


		public readonly ListasExtra.ReadonlyPair<Recurso, float> data;
		readonly Gdk.Pixbuf _icon;

		public RecursoListEntry(System.Collections.Generic.KeyValuePair<Recurso, float> entry)
		{
			data = new ListasExtra.ReadonlyPair<Recurso, float>(entry);
			_icon = buildIcon();
		}

		public RecursoListEntry(ListasExtra.ReadonlyPair<Recurso, float> entry)
		{
			data = entry;
			_icon = buildIcon();
		}

		Gdk.Pixbuf buildIcon()
		{
			string IconName = data.Key.Img;
			if (IconName == null)
			{
				System.Diagnostics.Debug.WriteLine(string.Format("Recurso {0} con enlace a icono roto a {1}. Usando icono genérico.", data.Key.Nombre, data.Key.Img));
				return new Gdk.Pixbuf(iconDir + nullIconFile, iconSize_x, iconSize_y);
			}
			return new Gdk.Pixbuf(iconDir + IconName, iconSize_x, iconSize_y);
		}

		[Gtk.TreeNodeValue(Column = 1)]
		public string nombre { get { return data.Key.Nombre; } }

		[Gtk.TreeNodeValue(Column = 2)]
		public float cant { get { return data.Value; } }

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

	public partial class frmCiudad : Gtk.Window
	{
		public readonly Ciudad ciudad;

		NodeStore stRecurso = new NodeStore(typeof(RecursoListEntry));
		NodeStore stTrabajo = new NodeStore(typeof(TrabajoListEntry));

		public frmCiudad(Ciudad ciudad) :
			base(Gtk.WindowType.Toplevel)
		{
			this.ciudad = ciudad;

			CivGTK.ThreadManager.Pausar();

			// Construir recStore
			foreach (System.Collections.Generic.KeyValuePair<Recurso, float> x in ciudad.Almacen)
			{
				stRecurso.AddNode(new RecursoListEntry(x));
			}

			// Construir lista de trabajos
			foreach (var x in ciudad.ObtenerListaTrabajos())
			{
				
				stTrabajo.AddNode(new TrabajoListEntry(x));
			}

			CivGTK.ThreadManager.Continuar();

			this.Build();

			nvTrabajos.NodeStore = stTrabajo;

			nvTrabajos.AppendColumn("Nombre", new Gtk.CellRendererText(), "text", 0);
			nvTrabajos.AppendColumn("Trabajadores", new CellRendererNumTrab(stTrabajo), "text", 1);
			nvTrabajos.AppendColumn("Máx. trab", new Gtk.CellRendererText(), "text", 2);

			nvRecursos.NodeStore = stRecurso;
			nvRecursos.AppendColumn("Icono", new Gtk.CellRendererPixbuf(), "pixbuf", 0);
			nvRecursos.AppendColumn("Nombre", new Gtk.CellRendererText(), "text", 1);
			nvRecursos.AppendColumn("Cantidad", new Gtk.CellRendererText(), "text", 2);


			//Llenar etiquetas
			lbCityInfo.Angle = 90;
			Title = ciudad.Nombre;
			lbCityInfo.Text = string.Format 
				("Población:\n{0}/{1}/{2}",
				ciudad.getPoblacionPreProductiva,
				ciudad.getPoblacionProductiva,
				ciudad.getPoblacionPostProductiva);
		}

		protected void OnCmdRenombrarCiudadClicked(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}
	}
}