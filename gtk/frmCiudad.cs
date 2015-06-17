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
		public readonly Recurso recurso;
		public readonly float cantidad;

		public RecursoListEntry(System.Collections.Generic.KeyValuePair<Recurso, float> entry)
		{
			recurso = entry.Key;
			cantidad = entry.Value;
		}

		public RecursoListEntry(Recurso recurso, float cap)
		{
			this.recurso = recurso;
			this.cantidad = cap;
		}

		[Gtk.TreeNodeValue(Column = 0)]
		public string nombre { get { return recurso.Nombre; } }

		[Gtk.TreeNodeValue(Column = 1)]
		public float cant { get { return cantidad; } }
	}



	public partial class frmCiudad : Gtk.Window
	{
		public readonly Ciudad ciudad;

		NodeStore stRecurso = new NodeStore(typeof(RecursoListEntry));
		NodeStore stTrabajo = new NodeStore(typeof(TrabajoListEntry));

		public frmCiudad(Ciudad ciudad) :
			base(Gtk.WindowType.Toplevel)
		{
			this.ciudad = ciudad;

			// Construir recStore
			foreach (var x in ciudad.Almacen.ToDictionary())  //Clonar
			{
				stRecurso.AddNode(new RecursoListEntry(x));
			}

			// Construir lista de trabajos
			foreach (var x in ciudad.ObtenerListaTrabajos.ToArray())
			{
				stTrabajo.AddNode(new TrabajoListEntry(x));
			}



			this.Build();

			nvTrabajos.NodeStore = stTrabajo;
			//TreeViewColumn tc = new TreeViewColumn();
			nvTrabajos.AppendColumn("Nombre", new Gtk.CellRendererText(), "text", 0);
			nvTrabajos.AppendColumn("Trabajadores", new gtk.CellRendererNumTrab(), "text", 1);
			nvTrabajos.AppendColumn("Máx. trab", new Gtk.CellRendererText(), "text", 2);

		}
	}
}

