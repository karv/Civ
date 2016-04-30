using System;
using Civ;
using Global;
using Gtk;
using System.Diagnostics;
using System.IO;

namespace CivGTK
{
	
	public static class MainClass
	{
		static DateTime timer = DateTime.Now;
		const float MultiplicadorVelocidad = 360;
		public static Civilización MyCiv;

		static FrmCiv win;
		public static bool EndGame;

		public static void Main ()
		{
			Juego.CargaData ();

			if (File.Exists (Juego.ArchivoState))
				Juego.State = GameState.Cargar (Juego.ArchivoState);
			else
				Juego.InicializarJuego ();
			
			var i = 0;
			//Store.BinarySerialization.WriteToBinaryFile ("MyCiv", Juego.State.Civs [0]);
			foreach (var a in Juego.State.Civs[0].Ciudades)
			{
				Store.BinarySerialization.WriteToBinaryFile ("ciudad " + i++, a.Edificios);
			}

			MyCiv = Juego.State.Civs [0] as Civilización;
			Juego.State.Guardar (Juego.ArchivoState);

			return;
			//var cd = MyCiv.Ciudades [0] as Ciudad;


			//EdificioRAW eraw = Juego.Data.Trabajos[0].Edificio;
			//cd.AgregaEdificio(eraw);

			//new Trabajo(Juego.Data.Trabajos[0], cd);

			MyCiv.AlNuevoMensaje += MuestraMensajes;

			Application.Init ();
			win = new FrmCiv (MyCiv);
			win.Show ();
			timer = DateTime.Now;

			// Ciclo principal
			while (!EndGame)
			{
				Tick ();
				while (Application.EventsPending ())
					Application.RunIteration ();
			}
			//Application.Run();

		}

		static void MuestraMensajes ()
		{
			while (MyCiv.ExisteMensaje)
			{
				IU.Mensaje m = MyCiv.SiguienteMensaje ();
				if (m != null)
				{
					Debug.WriteLine (m.ToString ());
					win.AddMens (m.ToString ());
					win.Actualizar ();
				}
			}
		}

		/// <summary>
		/// Da un sólo tick global
		/// </summary>
		static void Tick ()
		{
			TimeSpan tiempo = DateTime.Now - timer;
			timer = DateTime.Now;
			var modTiempo = new TimeSpan ((long)(tiempo.Ticks * MultiplicadorVelocidad));

			// Console.WriteLine (t);
			Juego.Tick (modTiempo);

			if (Juego.State.Civs.Count == 0)
				throw new Exception ("Ya se acabó el juego :3");
		}
	}
}