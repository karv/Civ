using System;
using Gtk;
using System.IO;
using Civ.ObjetosEstado;
using Civ.Global;
using Civ;

namespace CivGTK
{
	
	public static class MainClass
	{
		static DateTime timer = DateTime.Now;
		const float MultiplicadorVelocidad = 120;
		public static Civilización MyCiv;

		static FrmCiv win;
		public static bool EndGame;

		public static void Main ()
		{
			if (File.Exists (Juego.ArchivoState))
				Juego.Cargar ();
			else
				Juego.Instancia.Inicializar ();

			MyCiv = Juego.State.Civs [0] as Civilización;
			foreach (var x in Juego.Data.Unidades)
				MyCiv.Ciudades [0].Defensa.AgregaUnidad (x, 3);

			Console.WriteLine (((IPuntuado)MyCiv).Puntuación);
			Juego.Guardar ();

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

		/// <summary>
		/// Da un sólo tick global
		/// </summary>
		static void Tick ()
		{
			TimeSpan tiempo = DateTime.Now - timer;
			timer = DateTime.Now;
			var modTiempo = new TimeSpan ((long)(tiempo.Ticks * MultiplicadorVelocidad));

			// Console.WriteLine (t);
			Juego.Instancia.Tick (modTiempo);

			if (Juego.State.Civs.Count == 0)
				throw new Exception ("Ya se acabó el juego :3");
		}
	}
}