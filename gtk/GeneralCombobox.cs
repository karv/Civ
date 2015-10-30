//
//  GeneralCombobox.cs
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
using System.Collections.Generic;

namespace Gtk
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class GeneralCombobox : Gtk.Bin
	{
		Dictionary <string, object> model = new Dictionary<string, object>();

		public GeneralCombobox()
		{
			this.Build();
			combobox.Model = new Gtk.ListStore(typeof(string));
			Gtk.CellRendererText text = new Gtk.CellRendererText();
			combobox.PackStart(text, false);
		}

		/// <summary>
		/// Agrega un elemento al combobox
		/// </summary>
		/// <param name="obj">Objeto vinculado</param>
		/// <param name="s">String a mostrar</param>
		public void Add(object obj, string s)
		{
			combobox.AppendText(s);
			model.Add(s, obj);
		}

		public void Clear()
		{
			model.Clear();
			combobox.Model = new Gtk.ListStore(typeof(string));
			Gtk.CellRendererText text = new Gtk.CellRendererText();
			combobox.PackStart(text, false);
		}

		/// <summary>
		/// Devuelve la armada seleccionada.
		/// </summary>
		/// <returns>The selected.</returns>
		public object getSelected()
		{
			if (combobox.ActiveText == null || !model.ContainsKey(combobox.ActiveText))
				return null;
			return model[combobox.ActiveText];
		}

		public event EventHandler onSelectionChanged;

		protected void OnComboBoxChanged(object sender, EventArgs e)
		{
			if (onSelectionChanged != null)
			{
				onSelectionChanged.Invoke(sender, e);
			}
		}

		public void LlenarCon(System.Collections.IEnumerable list)
		{
			Clear();
			foreach (var x in list)
			{
				Add(x, x.ToString());
			}
		}

		public void LlenarCon(System.Collections.Generic.IDictionary<string, object> list)
		{
			Clear();
			foreach (var x in list)
			{
				Add(x.Value, x.Key);
			}
		}

		public void LlenarCon<T>(System.Collections.Generic.IEnumerable<T> list, Func<T, string> stringSelector)
		{
			Clear();
			foreach (var x in list)
			{
				//TODO Forzar que no se repitan
				string s = stringSelector(x);
				int i = 0;
				while (this.model.ContainsKey(s))
				{
					s = stringSelector(x) + (++i).ToString();
				}
				Add(x, s);
			}
		}
	}
}

