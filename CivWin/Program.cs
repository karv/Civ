﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Civ;
using Global;
using Graficas;


namespace CivWin
{
	public static class Program
	{
		public static ImageList Imágenes = new ImageList ();

		private static void DoRead (string f = "Data.xml")
		{
			g_.Data = Store.Store<g_Data>.Deserialize (f);

		}

		static Civilizacion MyCiv;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[MTAThread]
		static void Main ()
		{
			DoRead ();
			g_.InicializarJuego ();

			// Inicializar imágenes
			Imágenes.ImageSize = new System.Drawing.Size (24, 24);

			foreach (var x in g_.Data.Recursos) {
				if (x.Img != null)
					Imágenes.Images.Add (x.Img, System.Drawing.Image.FromFile ("img//" + x.Img));
			}

			Civilizacion C = g_.State.Civs [0];
			C.getCiudades [0].AlimentoAlmacen = 10;
			MyCiv = C;
			C.OnNuevoMensaje += MuestraMensajes;

			Thread emu = new Thread (new ThreadStart (Ticker));

			Application.EnableVisualStyles ();
			Application.SetCompatibleTextRenderingDefault (false);
			FrmCiv frmCiv = new FrmCiv (C);



			emu.Priority = ThreadPriority.Lowest;
			emu.Start ();
			//helper.Start();


			Application.Run (frmCiv);

			emu.Abort ();
		}

		public static void MuestraCiudad (Ciudad C, Form Owner)
		{
			FrmCiudad frm = new FrmCiudad (C);
			frm.Show (Owner);

		}

		static DateTime timer = DateTime.Now;
		const float MultiplicadorVelocidad = 360;

		public static void Ticker ()
		{
			while (true) {
				TimeSpan tiempo = DateTime.Now - timer;
				timer = DateTime.Now;
				float t = (float)tiempo.TotalHours * MultiplicadorVelocidad;

				Console.WriteLine (t);
				Global.g_.Tick (t);

				if (Global.g_.State.Civs.Count == 0)
					throw new Exception ("Ya se acabó el juego :3");
			}
		}

		public static void DibujaTodo ()
		{
			foreach (var x in Application.OpenForms) {
				if (x is IDibujable) {
					IDibujable y = (IDibujable)x;
					y.Draw ();
				}
			}

		}

		static void MuestraMensajes ()
		{
			while (MyCiv.ExisteMensaje) {
				IU.Mensaje m = MyCiv.SiguitenteMensaje ();
				if (m != null) {
					MessageBox.Show (m.ToString ());
					//listMensajes.Items.Add(m.ToString());
				}
			}
		}


	}
}

/// <summary>
/// Para un Windows.form, obliga tener una función de redibujar/actualizar.
/// </summary>
public interface IDibujable
{
	void Draw ();
}

