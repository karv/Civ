//
//  EditableCell.cs
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

namespace gtk
{
	public class CellRendererNumTrab: Gtk.CellRendererText
	{

		public Gtk.NodeStore store;

		public CellRendererNumTrab(Gtk.NodeStore store) : this()
		{
			this.store = store;
		}

		CellRendererNumTrab()
		{
			Editable = true;
		}

		/// <summary>
		/// Valida valor ulong, y luego cambia los trabajadores del trabajo seleccionado.
		/// </summary>
		/// <param name="path">Path.</param>
		/// <param name="new_text">New text.</param>
		override protected void OnEdited(string path, string new_text)
		{
			ulong res;
			if (ulong.TryParse(new_text, out res))
			{
				TrabajoListEntry nodo = (TrabajoListEntry)store.GetNode(new Gtk.TreePath(path));
				nodo.Trabajadores = res;
			}
		}
	}
}

