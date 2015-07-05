//
//  wgReclutar.cs
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
using Civ;
using System;

namespace gtk
{
	/// <summary>
	/// Widget para reclutar
	/// </summary>
	[System.ComponentModel.ToolboxItem(true)]
	public partial class wgReclutar : Gtk.Bin, IActualizable
	{
		/// <summary>
		/// La ciudad anclada a este widget.
		/// </summary>
		public Ciudad ciudad;
		//Gtk.ListStore store = new Gtk.ListStore(typeof(ReclutarListEntry));
		Gtk.NodeStore store = new Gtk.NodeStore(typeof(ReclutarListEntry));

		public wgReclutar()
		{
			this.Build();
		
			Nodeview.AppendColumn("Nombre", new Gtk.CellRendererText(), "text", 0);
			Nodeview.AppendColumn("Existentes", new Gtk.CellRendererText(), "text", 1);
			Nodeview.AppendColumn("Reclutar", new CellRendererNumRecluta(store), "text", 2);
			Nodeview.AppendColumn("Máximo", new Gtk.CellRendererText(), "text", 3);

			Nodeview.NodeStore = store;
		}

		public void ConstruirModelo()
		{
			CivGTK.ThreadManager.Pausar();

			store.Clear();
			foreach (var x in ciudad.UnidadesConstruibles())
			{
				store.AddNode(new ReclutarListEntry(x.Key, ciudad));
			}

			CivGTK.ThreadManager.Continuar();
		}

		#region IActualizable implementation

		void IActualizable.Actualizar()
		{
			ConstruirModelo();
		}

		#endregion

		/// <summary>
		/// Entrada de TreeView de lista de reclutamiento de unidades.
		/// </summary>
		public class ReclutarListEntry : Gtk.TreeNode
		{
			public readonly UnidadRAW unidad;
			public readonly Ciudad ciudad;

			public ReclutarListEntry(UnidadRAW unidad, Ciudad ciudad)
			{
				this.unidad = unidad;
				this.ciudad = ciudad;
			}

			[Gtk.TreeNodeValue(Column = 0)]
			public string Nombre
			{
				get
				{
					return unidad.Nombre;
				}
			}

			[Gtk.TreeNodeValue(Column = 1)]
			public ulong Existentes
			{
				get
				{
					Stack grupo = ciudad.Defensa.UnidadesAgrupadas(unidad);
					return grupo == null ? 0 : grupo.cantidad;
				}
			}

			[Gtk.TreeNodeValue(Column = 2)]
			public ulong MarcadoReclutar
			{
				get
				{
					return 0;
				}
			}

			[Gtk.TreeNodeValue(Column = 3)]
			public ulong MaxRecluta
			{
				get
				{
					return ciudad.UnidadesConstruibles(unidad);
				}
			}
		}
	}
}