﻿using System;
using Gtk;
using System.Diagnostics;
using System.IO;
using Civ.ObjetosEstado;
using Civ.Global;

namespace CivGTK
{
	
	public static class MainClass
	{
		static DateTime timer = DateTime.Now;
		const float MultiplicadorVelocidad = 600;
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
			Juego.Guardar ();

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
				Civ.IU.Mensaje m = MyCiv.SiguienteMensaje ();
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
			Juego.Instancia.Tick (modTiempo);

			if (Juego.State.Civs.Count == 0)
				throw new Exception ("Ya se acabó el juego :3");
		}
	}
}