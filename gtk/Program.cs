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
using Civ;
using Global;
using Gtk;
using System.Diagnostics;

namespace CivGTK
{
	
	public static class MainClass
	{
		static DateTime timer = DateTime.Now;
		const float MultiplicadorVelocidad = 360;
		public static Civilización MyCiv;

		static void DoRead(string f = "Data.xml")
		{
			Juego.Data = Store.Store<GameData>.Deserialize(f);
		}

		static gtk.frmCiv win;
		public static bool endGame;

		public static void Main()
		{
			Juego.CargaData();
			Juego.InicializarJuego();

			MyCiv = Juego.State.Civs[0] as Civilización;
			//var cd = MyCiv.Ciudades [0] as Ciudad;


			//EdificioRAW eraw = Juego.Data.Trabajos[0].Edificio;
			//cd.AgregaEdificio(eraw);

			//new Trabajo(Juego.Data.Trabajos[0], cd);

			MyCiv.AlNuevoMensaje += MuestraMensajes;

			Application.Init();
			win = new gtk.frmCiv(MyCiv);
			win.Show();
			timer = DateTime.Now;

			// Ciclo principal
			while (!endGame)
			{
				Tick();
				while (Application.EventsPending())
					Application.RunIteration();
			}
			//Application.Run();

		}

		static void MuestraMensajes()
		{
			while (MyCiv.ExisteMensaje)
			{
				IU.Mensaje m = MyCiv.SiguienteMensaje();
				if (m != null)
				{
					Debug.WriteLine(m.ToString());
					win.AddMens(m.ToString());
					win.Actualizar();
				}
			}
		}

		/// <summary>
		/// Da un sólo tick global
		/// </summary>
		static void Tick()
		{
			TimeSpan tiempo = DateTime.Now - timer;
			timer = DateTime.Now;
			var modTiempo = new TimeSpan((long)(tiempo.Ticks * MultiplicadorVelocidad));

			// Console.WriteLine (t);
			Juego.Tick(modTiempo);

			if (Juego.State.Civs.Count == 0)
				throw new Exception("Ya se acabó el juego :3");
		}
	}
}