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
		public readonly Unidad unidad;

		public UnidadListEntry(Unidad unidad)
		{
			this.unidad = unidad;
		}

		[Gtk.TreeNodeValue(Column = 0)]
		public UnidadRAW Tipo
		{
			get
			{
				return unidad.RAW;
			}
		}
	}

	[System.ComponentModel.ToolboxItem(true)]
	public partial class ArmadaWidget : Gtk.Bin, IActualizable
	{
		public Gtk.TreeStore store = new Gtk.TreeStore(typeof(UnidadListEntry));
		public Civ.Armada Armada;

		public ArmadaWidget()
		{
			this.Build();
		}

		#region IActualizable implementation

		public void Actualizar()
		{
			store.Clear();
			foreach (var x in Armada.Unidades)
			{
				Gtk.TreeIter iter = store.AppendValues(x.RAW);
				store.AppendValues(iter, x);
			}
		}

		#endregion
	}
}

