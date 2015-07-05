//
//  ArmadaWidget.cs
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

namespace gtk
{
	class UnidadListEntry : Gtk.TreeNode
	{
		public readonly Stack unidad;

		public UnidadListEntry(Stack unidad)
		{
			this.unidad = unidad;
		}

		[Gtk.TreeNodeValue(Column = 0)]
		public string Tipo
		{
			get
			{
				return unidad.RAW.Nombre;
			}
		}

		[Gtk.TreeNodeValue(Column = 1)]
		public ulong Cantidad { get { return unidad.cantidad; } }

		[Gtk.TreeNodeValue(Column = 2)]
		public float Entrenamiento { get { return unidad.Entrenamiento; } }
	}

	[System.ComponentModel.ToolboxItem(true)]
	public partial class ArmadaWidget : Gtk.Bin, IActualizable
	{
		public Gtk.NodeStore store = new Gtk.NodeStore(typeof(UnidadListEntry));
		public Civ.Armada Armada;

		public ArmadaWidget()
		{
			this.Build();

			nodeview2.AppendColumn("Tipo", new Gtk.CellRendererText(), "text", 0);
			nodeview2.AppendColumn("Cantidad", new Gtk.CellRendererText(), "text", 1);
			nodeview2.AppendColumn("Entrenamiento", new Gtk.CellRendererText(), "text", 2);
			nodeview2.NodeStore = store;
		}

		/// <summary>
		/// Devuelve la unidad seleccionada.
		/// </summary>
		/// <returns>The selected.</returns>
		public Civ.Stack getSelected()
		{
			Gtk.NodeSelection r = nodeview2.NodeSelection;
			if (r.SelectedNode == null)
				return null;
			Stack c = ((UnidadListEntry)r.SelectedNode).unidad;

			return c;
		}

		#region IActualizable implementation

		public void Actualizar()
		{
			store.Clear();
			foreach (var x in Armada.Unidades)
			{
				store.AddNode(new UnidadListEntry(x));
			}
/*
			System.Collections.Generic.Dictionary <UnidadRAW, System.Collections.Generic.List <Unidad>> unid = Armada.ToDictionary();
			foreach (var x in unid)
			{
				Gtk.TreeIter iter = store.AppendValues(x.Key);
				foreach (var y in x.Value)
				{
					store.AppendValues(iter, new UnidadListEntry(y));
				}
			}
			*/
		}

		#endregion
	}
}

