//
//  Program.cs
//
//  Author:
//       Edgar Carballo <karvayoEdgar@gmail.com>
//
//  Copyright (c) 2014 edgar
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
using Global;

namespace GtkLinux
{
	class MainClass
{

		private static void DoRead()
		{

			g_.Data = new g_Data ();
			g_.Data = Store.Store<g_Data>.Deserialize ("Data.xml");

		}
		public static void Main (string[] args)
		{
			DoRead ();


			Civilización C = new Civilización ();
			Ciudad Cd = new Ciudad ("MyCity", C);

			Cd.PoblaciónProductiva = 10;
			//g_.Data.RecursoAlimento = "";


			//Application.Init ();

			GtkLinux.CivWindow win = new GtkLinux.CivWindow (C);
			win.Show ();
			Application.Run ();

			while (true) {
				C.doTick ();
				//C.FullTick ();
				Console.WriteLine (Cd.getPoblaciónPreProductiva + "\t" + Cd.PoblaciónProductiva + "\t" + Cd.getPoblaciónPostProductiva);				                    
			}
			//Console.WriteLine ();
		}
	}
}
