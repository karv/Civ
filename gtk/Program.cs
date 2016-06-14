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
		const float MultiplicadorVelocidad = 120;
		public static Civilización MyCiv;

		static FrmCiv win;

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

			Juego.Instancia.Autoguardado = TimeSpan.FromSeconds (10);

			Juego.Instancia.EntreCiclos += delegate
			{
				while (Application.EventsPending ())
					Application.RunIteration ();
			};

			// Ciclo principal
			Juego.Instancia.Ejecutar ();
		}
	}
}