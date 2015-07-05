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
	
	public static class ThreadManager
	{
		public static Thread emu;
		/// <summary>
		/// Devuelve o establece el deseo de pausar el thread.
		/// </summary>
		public static bool libThreadPaused = false;
		/// <summary>
		/// Devuelve o establece si el thread esta pausado en un lugar seguro.
		/// </summary>
		public static bool isLibThreadPaused = false;

		/// <summary>
		/// Pausa el thread servidor en lugar seguro.
		/// </summary>
		public static void Pausar()
		{
			return;
			CivGTK.ThreadManager.libThreadPaused = true;
			while (!CivGTK.ThreadManager.isLibThreadPaused)
			{
			}
		}

		/// <summary>
		/// Continua con el proceso servidor.
		/// </summary>
		public static void Continuar()
		{
			return;
			CivGTK.ThreadManager.libThreadPaused = false;
		}

	}

	class MainClass
	{
		static DateTime timer = DateTime.Now;
		const float MultiplicadorVelocidad = 360;
		public static Civilizacion MyCiv;

		private static void DoRead(string f = "Data.xml")
		{
			Global.g_.Data = Store.Store<Global.g_Data>.Deserialize(f);
		}

		static gtk.frmCiv win;

		public static void Main(string[] args)
		{
			DoRead();
			Global.g_.InicializarJuego();


			Civilizacion C = g_.State.Civs[0];
			MyCiv = C;
			Ciudad cd = C.getCiudades[0];

			cd.AlimentoAlmacen = 100;
			cd.AutoReclutar = false;
			EdificioRAW eraw = g_.Data.Trabajos[0].Edificio;
			cd.AgregaEdificio(eraw);

			new Trabajo(g_.Data.Trabajos[0], cd);

			C.OnNuevoMensaje += MuestraMensajes;

			ThreadManager.emu = new Thread(new ThreadStart(Ticker));


			ThreadManager.emu.Priority = ThreadPriority.Lowest;
			ThreadManager.emu.Start();

			Application.Init();
			win = new gtk.frmCiv(MyCiv);
			win.Show();
			Application.Run();

			ThreadManager.emu.Abort();

		}

		static void MuestraMensajes()
		{
			while (MyCiv.ExisteMensaje)
			{
				IU.Mensaje m = MyCiv.SiguitenteMensaje();
				if (m != null)
				{
					System.Diagnostics.Debug.WriteLine(m.ToString());
					win.Actualizar();
				}

				//Thread thrRefresh = new Thread(new ThreadStart(win.Actualizar));
				//thrRefresh.Start();
				// win.Actualizar();
			}
		}

		static void Ticker()
		{
			timer = DateTime.Now;
			while (true)
			{
				//Pausar en el momento menos peligroso, si se requiere.
				while (ThreadManager.libThreadPaused)
				{
					ThreadManager.isLibThreadPaused = true;
				}
				ThreadManager.isLibThreadPaused = false;

				TimeSpan tiempo = DateTime.Now - timer;
				timer = DateTime.Now;
				float t = (float)tiempo.TotalHours * MultiplicadorVelocidad;

				// Console.WriteLine (t);
				Global.g_.Tick(t);

				if (Global.g_.State.Civs.Count == 0)
					throw new Exception("Ya se acabó el juego :3");
			}
		}

	}
}