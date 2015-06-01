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
	[Obsolete()]
	public partial class frmCiudad : Gtk.Window
	{
		public readonly Ciudad ciudad;
		public Gtk.NodeStore RecStore = new NodeStore(typeof(RecursoListEntry));

		public frmCiudad (Ciudad c) :
			base (Gtk.WindowType.Toplevel)
		{
			ciudad = c;

			this.Build ();

			lbCityInfo.Text =
			// textview1.Buffer.Text = 
				string.Format ("Población: {0}/{1}/{2}",
				c.getPoblacionPreProductiva,
				c.getPoblacionProductiva,
				c.getPoblacionPostProductiva);

			CellRendererText rtEdif = new CellRendererText ();
			CellRendererText rtNumTrab = new CellRendererText ();
			CellRendererText rtTrab = new CellRendererText ();

			TreeViewColumn EdifCol = new TreeViewColumn ();
			EdifCol.Title = "Edificio";
			EdifCol.PackStart (rtEdif, true);

			TreeViewColumn NumTrabCol = new TreeViewColumn ();
			NumTrabCol.Title = "Trabajadores";
			NumTrabCol.PackStart (rtNumTrab, true);

			TreeViewColumn TrabCol = new TreeViewColumn ();
			TrabCol.Title = "Trabajo";
			TrabCol.PackStart (rtTrab, true);

			treeview1.AppendColumn (EdifCol);
			treeview1.AppendColumn (NumTrabCol);
			treeview1.AppendColumn (TrabCol);

			EdifCol.AddAttribute (rtEdif, "text", 0);
			NumTrabCol.AddAttribute (rtNumTrab, "text", 1);
			TrabCol.AddAttribute (rtTrab, "text", 2);

			// Hacer el árbol de trabajos
			TreeStore store = new TreeStore (typeof(string), typeof(ulong), typeof(string));
			foreach (var x in ciudad.Edificios) {
				// TreeIter Iterx = store.AppendValues (x, x.getEspaciosTrabajadoresCiudad);
				TreeIter Iterx = store.AppendValues (x.Nombre, x.getTrabajadores);

				foreach (var y in x.Trabajos)
				{	
					store.AppendValues (Iterx, x.Nombre, y.Trabajadores, y.RAW.Nombre);
				}
			}
			treeview1.Model = store;

			// Los recursos:
			foreach (var x in ciudad.Almacen)
			{
				RecStore.AddNode(new RecursoListEntry(x));
			}

			nvRec.AppendColumn("Recurso", new Gtk.CellRendererText(), "text", 0);
			nvRec.AppendColumn("Valor", new Gtk.CellRendererText(), "text", 1);
			nvRec.Model = (Gtk.TreeModel)RecStore;

			ShowAll();
		}
	}
}