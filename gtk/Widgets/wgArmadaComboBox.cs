//
//  wgArmadaComboBox.cs
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
using Civ;
using Gtk;

namespace Gtk
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class wgArmadaComboBox : Gtk.Bin, IEnumerable<Armada>
	{
		Dictionary<string, Armada> armadas = new Dictionary<string, Armada>();

		public wgArmadaComboBox()
		{
			this.Build();
			comboBox.Model = new Gtk.ListStore(typeof(string));
			CellRendererText text = new CellRendererText();
			comboBox.PackStart(text, false);
			//comboBox.AddAttribute(text
		}

		public void Add(Armada arm)
		{
			string nombre = string.Format("Peso {0}/{1}", arm.Peso, arm.MaxPeso);

			// Evita repeticiones.
			int i = 0;
			string nom = nombre;

			while (armadas.ContainsKey(nom))
			{
				nom = nombre + " - " + (i++).ToString();
			}
			Add(arm, nom);

			comboBox.CheckResize();
		}

		public void Add(Armada arm, string nombre)
		{
			comboBox.AppendText(nombre);
			armadas.Add(nombre, arm);
		}

		public void Clear()
		{
			armadas.Clear();
			//comboBox.Clear();
			comboBox.Model = new Gtk.ListStore(typeof(string));
			CellRendererText text = new CellRendererText();
			comboBox.PackStart(text, false);
		}

		/// <summary>
		/// Devuelve la armada seleccionada.
		/// </summary>
		/// <returns>The selected.</returns>
		public Armada getSelected()
		{
			if (comboBox.ActiveText == null || !armadas.ContainsKey(comboBox.ActiveText))
				return null;
			return armadas[comboBox.ActiveText];
		}

		System.Collections.Generic.IEnumerator<Civ.Armada> System.Collections.Generic.IEnumerable<Civ.Armada>.GetEnumerator()
		{
			IEnumerable<Armada> enu;
			enu = (IEnumerable<Armada>)(armadas.Values);
			return enu.GetEnumerator();
		}

		public event EventHandler onSelectionChanged;

		protected void OnComboBoxChanged(object sender, EventArgs e)
		{
			if (onSelectionChanged != null)
			{
				onSelectionChanged.Invoke(sender, e);
			}
		}
	}
}