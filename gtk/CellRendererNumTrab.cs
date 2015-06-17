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
		
		public CellRendererNumTrab()
		{
			Editable = true;

			//base.OnEdited += onEdt;
		}

		/// <summary>
		/// Raises the edited event.
		/// </summary>
		/// <param name="path">Path.</param>
		/// <param name="new_text">New text.</param>
		protected void OnEdited2(string path, string new_text)
		{
			int res;
			string outstr;
			if (int.TryParse(new_text, out res))
			{
				outstr = res.ToString();
				new_text = outstr;
				Text = outstr;
				base.OnEdited(path, outstr);
			}
		}

		void onEdt()
		{
			
		}
	}
}

