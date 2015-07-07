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

namespace gtk
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
				return _data.Count > 0 ? _current.Value : "";
			}
		}

		public void Add(string data)
		{
			_data.AddLast(data);
		}

		public void MoveNext()
		{
			_current = _current.Next ?? _data.First;
		}

		public void MovePrev()
		{
			_current = _current.Previous ?? _data.Last;
		}

		public void RemoveCurrent()
		{
			LinkedListNode<string> del = _current;
			MoveNext();
			_data.Remove(del);
		}

		public MensView()
		{
			_current = _data.First;
			this.Build();
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

