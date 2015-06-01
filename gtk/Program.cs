//
//  Program.cs
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
using Global;
using System.Threading;
using System.Threading.Tasks;


namespace CivGTK
{
	class MainClass
	{
		static DateTime timer = DateTime.Now;
		const float MultiplicadorVelocidad = 360;
		public static Civilizacion MyCiv;

		private static void DoRead (string f = "Data.xml")
		{
			Global.g_.Data = Store.Store<Global.g_Data>.Deserialize (f);
		}


		public static void Main (string[] args)
		{
			DoRead ();
			Global.g_.InicializarJuego ();


			Civilizacion C = g_.State.Civs [0];
			MyCiv = C;
			Ciudad cd = C.getCiudades [0];

			cd.AlimentoAlmacen = 100;
			cd.AutoReclutar = false;
			EdificioRAW eraw = g_.Data.Trabajos [0].Edificio;
			cd.AgregaEdificio (eraw);

			new Trabajo (g_.Data.Trabajos [0], cd);

			C.OnNuevoMensaje += MuestraMensajes;

			Thread emu = new Thread (new ThreadStart (Ticker));


			emu.Priority = ThreadPriority.Lowest;
			emu.Start ();

			Application.Init ();
			frmCiv win = new frmCiv (MyCiv);
			win.Show ();
			Application.Run ();

			emu.Abort ();

		}

		static void MuestraMensajes ()
		{
			while (MyCiv.ExisteMensaje) {
				IU.Mensaje m = MyCiv.SiguitenteMensaje ();
				if (m != null) {
					Gtk.Dialog d = new Dialog ();
					d.Title = m.ToString ();
					d.Show ();
				}
			}
		}

		static void Ticker ()
		{
			timer = DateTime.Now;
			while (true) {
				TimeSpan tiempo = DateTime.Now - timer;
				timer = DateTime.Now;
				float t = (float)tiempo.TotalHours * MultiplicadorVelocidad;

				// Console.WriteLine (t);
				Global.g_.Tick (t);

				if (Global.g_.State.Civs.Count == 0)
					throw new Exception ("Ya se acabó el juego :3");
			}
		}

	}
}