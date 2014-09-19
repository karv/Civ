using System;
using Gtk;

namespace Civ
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			TestClass.Do ();
			Application.Init ();
			MainWindow win = new MainWindow ();
			win.Show ();
			Application.Run ();
		}
	}
	public static class TestClass
	{
		public static void Do()
		{
			Civilización T = new Civilización ();
			Ciudad C = new Ciudad ("Alejandría", T);


			Ciudad.RecursoAlimento = new Recurso ("Alimento");
			C.Almacén [Ciudad.RecursoAlimento] = 100f;

				// Algunos edificios
			EdificioRAW E1 = new EdificioRAW ();
			E1.Nombre = "Laboratorio";
			E1.MaxWorkers = 4;

			EdificioRAW E2 = new EdificioRAW ();
			E2.Nombre = "Granja";
			E2.MaxWorkers = 100;

			Edificio e1 = C.AgregaEdificio (E1);
			Edificio e2 = C.AgregaEdificio (E2);

			TrabajoRAW T1 = new TrabajoRAW ();
			TrabajoRAW T2 = new TrabajoRAW ();
			TrabajoRAW T3 = new TrabajoRAW ();

			T1.Nombre = "T1";
			T2.Nombre = "T2";
			T3.Nombre = "T3";

			T1.SalidaBase [Ciudad.RecursoAlimento] = 1;
			T2.SalidaBase [Ciudad.RecursoAlimento] = 1.5f;
			T3.SalidaBase [Ciudad.RecursoAlimento] = 0.3f;

			e1 [T1].Trabajadores = 3;
			e1 [T2].Trabajadores = 10;
			e2 [T3].Trabajadores = 3;

			ulong n = C.getTrabajadoresDesocupados;

			for (int i = 0; i < 5; i++) {
				C.PopTick ();
			}

			Console.Write ("");
		}
	}
}
