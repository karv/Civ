using System;
using System.Collections.Generic;
using Civ;
using ListasExtra;
using System.IO;

namespace CivEditor
{
	class MainClass
	{
		static Global.g_Data Data;
		public static int Main (string[] args)
		{
			if (args.Length < 2) {
				Console.WriteLine ("Especifique el archivo a compilar y el archivo salida.");
				return 1;
			}



			doCompile (args [0], args [1]);
			return 0;
		}
		/// <summary>
		/// Construye el archivo compilado de <see cref="Civ.g_Data"/> 
		/// </summary>
		/// <param name="From">Archivo a compilar.</param>
		/// <param name="To">Nombre del archivo salida.</param>
		static void doCompile (string From, string To)
		{
			//object Editing;

			Data = new Global.g_Data ();
			StreamReader sr = new StreamReader (From);
			string Line;

			while (!sr.EndOfStream) {
				Line = sr.ReadLine ().ToLower ();
				switch (Line) {
				case "ciencia":
				{
					Ciencia C = new Ciencia ();
					string nombre = sr.ReadLine ();
					if (Data.ExisteCiencia(nombre)) {
						throw new Exception ("Ciencia repetida: " + nombre);
					}
					
					
					C.Nombre = nombre;
					Data.Ciencias.Add (C);

					Recurso ReqRec = Data.EncuentraRecurso (sr.ReadLine ());
					C.RecursoReq = ReqRec;

					ulong cant = ulong.Parse (sr.ReadLine ());
					C.CantidadReq = cant;

					nombre = sr.ReadLine ();
					while (nombre != "\\") {
						C.ReqCiencia.Add (Data.EncuentraCiencia (nombre));
					}

					break;
				}

				case "recurso":
				{
					Recurso R;
					string nombre = sr.ReadLine ();
					if (Data.ExisteRecurso(nombre)) {
						throw new Exception ("Recurso repetida: " + nombre);
					}

					R = new Recurso (nombre);
					Data.Recursos.Add (R);

					break;
				}
				
				default:
					break;
				}
			}
			Store.Store<Global.g_Data>.Serialize (To, Data);
		}
	}
}
