//
//  PopDisplay.cs
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

	[System.ComponentModel.ToolboxItem(true)]
	public partial class PopDisplay : Gtk.Bin, IActualizable
	{
		#region IActualizable implementation

		public void Actualizar()
		{
			Refresh();
		}

		#endregion

		Civ.ICiudad ciudad;

		public Civ.ICiudad Ciudad
		{
			get
			{
				return ciudad;
			}
			set
			{
				ciudad = value;
				Refresh();
			}
		}

		public PopDisplay()
		{
			this.Build();
		}

		public void Refresh()
		{
			Civ.InfoPoblacion pop = ciudad.GetPoblacionInfo;
			label3.Text = pop.Productiva.ToString();
			label4.Text = pop.PreProductiva.ToString();
			label5.Text = pop.PostProductiva.ToString();
		}
			
	}
}

