using System;
using Gtk;
using Global;

namespace Civ
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			TestClass.DoRead ();

			Application.Init ();

			//MainWindow win = new MainWindow ();
			//win.Show ();
			Application.Run ();
		}
	}

	public static class TestClass
	{
		public static void DoRead()
		{
			g_.Data = new g_Data ();
			g_.Data= Store.Store<g_Data>.Deserialize ("Data.xml");
		}
		[Obsolete]
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

			T1.SalidaBase [Ciudad.RecursoAlimento] = 5;
			T2.SalidaBase [Ciudad.RecursoAlimento] = 1.5f;
			T3.SalidaBase [Ciudad.RecursoAlimento] = 0.3f;

			e1 [T1].Trabajadores = 3;
			e1 [T2].Trabajadores = 10;
			e2 [T3].Trabajadores = 3;

			ulong n = C.getTrabajadoresDesocupados;

			// Hacer un poco de Data
			Ciencia NC = new Ciencia ();
			Recurso Rec = new Recurso ("Ciencia");


			NC.Nombre = "Lenguaje";
			NC.RecursoReq = "Ciencia";
			NC.CantidadReq = 500;

			//Store.Store<Recurso>.Serialize ("text.dat", Rec);
			//Store.Store<Recurso>.Save ("text.txt", Rec);

			g_.Data.Recursos.Add (Rec);
			g_.Data.Recursos.Add (Ciudad.RecursoAlimento);
			g_.Data.Ciencias.Add (NC);

			g_.Data.RecursoAlimento = "Alimento";

			g_.GuardaData ();


			while (true) {
				C.FullTick ();
				Console.WriteLine (C.getPoblaciónPreProductiva + "\t" + C.getPoblaciónProductiva + "\t" + C.getPoblaciónPostProductiva);				                    
			}
		}
	}
}

namespace Global
{
	/// <summary>
	/// Los objetos globales.
	/// </summary>
	[Serializable()]
	public static class g_
	{
		public static g_Data Data = new g_Data();
		public static g_State State = new g_State();

		private const string archivo = "Data2.xml";

		/// <summary>
		/// Carga del archivo predeterminado.
		/// </summary>
		public static void CargaData ()
		{
			Data = Store.Store<g_Data>.Deserialize (archivo);
		}

		public static void GuardaData() 
		{
			Store.Store<g_Data>.Serialize (archivo, Data);
		}

	}

}