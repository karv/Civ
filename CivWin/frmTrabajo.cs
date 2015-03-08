using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CivWin
{
	public partial class frmTrabajo : Form, IDibujable
	{
		public readonly Civ.Trabajo Trab;

		/// <summary>
		/// Crea una instancia de este formulario.
		/// </summary>
		/// <param name="Tr">Trabajo vinculado a este formulario.</param>
		public frmTrabajo(Civ.Trabajo Tr)
		{
			InitializeComponent();
			Trab = Tr;

			Draw();
		}

		public void Draw ()
		{
			Text = string.Format("{0} en {1}", Trab.RAW.Nombre, Trab.EdificioBase.Nombre);

			ttNumTrab.Máximo = (int)Trab.MaxTrabajadores;		
			ttNumTrab.Valor = (int)Trab.Trabajadores;
			 
		}

		private void ttNumTrab_OnCambio()
		{
			Trab.Trabajadores = (ulong)ttNumTrab.Valor;
		}

		private void trackText1_OnCambio()
		{
			Trab.Prioridad = ttPrior.Valor;
		}
	}
}
