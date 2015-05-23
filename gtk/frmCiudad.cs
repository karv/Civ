//
//  frmCiudad.cs
//
//  Author:
//       edgar <>
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
using Gtk;
using Civ;

namespace CivGTK
{
	class TrabajoListEntry : Gtk.TreeNode
	{
		public readonly Trabajo trabajo;

		[Gtk.TreeNodeValue (Column = 0)]
		public string nombre {
			get {
				return trabajo.RAW.Nombre;
			}
		}

		[Gtk.TreeNodeValue (Column = 1)]
		public ulong Trabajadores {
			get {
				return trabajo.Trabajadores;
			}
		}

		[Gtk.TreeNodeValue (Column = 2)]
		public ulong MaxTrabajadores {
			get {
				return trabajo.MaxTrabajadores;
			}
		}

		public TrabajoListEntry (Trabajo trabajo)
		{
			this.trabajo = trabajo;
		}
	}


	public partial class frmCiudad : Gtk.Window
	{
		public readonly Ciudad ciudad;

		public frmCiudad (Ciudad c) :
			base (Gtk.WindowType.Toplevel)
		{
			ciudad = c;

			this.Build ();

			textview1.Buffer.Text = 
				string.Format ("Población: {0}/{1}/{2}",
				c.getPoblacionPreProductiva,
				c.getPoblacionProductiva,
				c.getPoblacionPostProductiva);

			// Hacer el árbol de trabajos
			TreeStore store = new TreeStore (typeof(Edificio), typeof(uint), typeof(Trabajo));
			foreach (var x in ciudad.Edificios) {
				TreeIter Iterx = store.AppendValues (x, x.getEspaciosTrabajadoresCiudad);

				foreach (var y in x.Trabajos) {	
					store.AppendValues (Iterx, x, y.Trabajadores, y);
				}
			}
			treeview1.Model = store;

			ShowAll ();


		}
	}
}