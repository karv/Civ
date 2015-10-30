//
//  MensView.cs
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
	public partial class MensView : Gtk.Bin
	{
		LinkedList<string> _data = new LinkedList<string>();
		LinkedListNode<string> _current;
		public string texto;

		/// <summary>
		/// Devuelve o establece el texto que aparece
		/// </summary>
		/// <value>The texto.</value>
		public string Texto
		{
			get
			{
				return _current.Value;
			}
		}

		public int Count
		{
			get{ return _data.Count; }
		}

		public void Add(string data)
		{
			_data.AddLast(data);
			if (Count == 1)
				_current = _data.First;
			UpdateLabel();
			UpdateControls();
		}

		void UpdateLabel()
		{
			if (_data.Count > 0)
				label.Text = Texto;
			else
				label.Text = null;
		}

		void UpdateControls()
		{
			bool Sensib = (Count > 0);
			cmdAdel.Sensitive = Sensib;
			cmdAtras.Sensitive = Sensib;
			cmdBorrar.Sensitive = Sensib;
		}

		public void MoveNext()
		{
			_current = _current.Next ?? _data.First;
			UpdateLabel();
		}

		public void MovePrev()
		{
			_current = _current.Previous ?? _data.Last;
			UpdateLabel();
		}

		public void RemoveCurrent()
		{
			LinkedListNode<string> del = _current;
			MoveNext();
			_data.Remove(del);
			UpdateLabel();
			UpdateControls();
		}

		public MensView()
		{
			_current = _data.First;
			this.Build();
			UpdateLabel();
		}

		protected void OnCmdBorrarClicked(object sender, EventArgs e)
		{
			RemoveCurrent();
		}

		protected void OnButton19Clicked(object sender, EventArgs e)
		{
			MovePrev();
		}

		protected void OnCmdAdelClicked(object sender, EventArgs e)
		{
			MoveNext();
		}
	}
}

