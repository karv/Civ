using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Civ;
using CivWin;

namespace CivWin
{
	public partial class FrmCiudad : Form, IDibujable
	{
		/// <summary>
		/// Devuelve la ciudad vinculada a este formulario.
		/// </summary>
		public readonly Ciudad ciudad;
		/// <summary>
		/// Crea una instancia de este formulario.
		/// </summary>
		/// <param name="C">La ciudad vinculada a este formulario.</param>
		public FrmCiudad(Ciudad C)
		{
			ciudad = C;
			InitializeComponent();
			chkAutoReclutar.Checked = ciudad.AutoReclutar;
			Draw();
		}

		public void Draw()
		{
			Text = ciudad.Nombre;

			// Recursos
			listRecursos.Items.Clear();
			foreach (var x in ciudad.Almacén.Keys.ToArray())
			{
				listRecursos.Items.Add(string.Format("{0} - {1}", x, ciudad.Almacén[x]));
			}

			// textInfo            
			List<string> strInfo = new List<string>();
			strInfo.Add("Población: " + ciudad.getPoblación.ToString());
			strInfo.Add(string.Format("Distribución por edad: {0} / {1} / {2}", ciudad.getPoblaciónPreProductiva, ciudad.PoblaciónProductiva, ciudad.getPoblaciónPostProductiva));
			textInfo.Lines = strInfo.ToArray();

			// Edificios
			listEdificios.Items.Clear();
			foreach (var x in ciudad.Edificios)
			{
				listEdificios.Items.Add(x.Nombre);
			}

			// Trabajos y trabajadores
			listTrabajos.Items.Clear();
			foreach (var x in ciudad.ObtenerListaTrabajosRAW)
			{
				listTrabajos.Items.Add(x);
			}

			// El "construyendo".
			comboConstruir.Items.Clear();
			foreach (var x in ciudad.Construibles())
			{
				comboConstruir.Items.Add(x);
			}
			comboConstruir.SelectedItem = ciudad.RAWConstruyendo;
			if (ciudad.EdifConstruyendo != null)
				pbEdif.Value = (int)(ciudad.EdifConstruyendo.Porcentageconstruccion() * 100);
		}

		/// <summary>
		/// Devuele el trabajo seleccionado en listTrabajos. Si no existe, lo crea.
		/// </summary>
		/// <returns></returns>
		private Trabajo TrabajoSeleccionado()
		{
			TrabajoRAW Selec = (TrabajoRAW)listTrabajos.SelectedItem;
			if (Selec == null) return null;
			Trabajo Sel = ciudad.EncuentraInstanciaTrabajo(Selec);
			if (Sel == null)    // Si no existen instancia, se crea
			{
				Trabajo NTrab = new Trabajo(Selec, ciudad.EncuentraInstanciaEdificio(Selec.Edificio));
				Sel = NTrab;
			}
			return Sel;
		}

		private void listTrabajos_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listTrabajos.SelectedItem != null)
			{
				Trabajo Sel = TrabajoSeleccionado();
				numTrabajador.Minimum = 0;
				numTrabajador.Maximum = Sel.MaxTrabajadores;
				numTrabajador.Value = Sel.Trabajadores;
				numTrabajador.Enabled = true;
			}
			else
			{
				numTrabajador.Value = 0;
				numTrabajador.Enabled = false;
			}

		}

		private void numTrabajador_ValueChanged(object sender, EventArgs e)
		{
			Trabajo T = TrabajoSeleccionado();
			if (T != null)
			{
				T.Trabajadores = (ulong)numTrabajador.Value;
				numTrabajador.Value = T.Trabajadores;
			}
		}

		private void comboConstruir_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboConstruir.SelectedItem != ciudad.RAWConstruyendo)
			{
				ciudad.RAWConstruyendo = (EdificioRAW)comboConstruir.SelectedItem;
			}
		}

		private void toolStripButton2_Click(object sender, EventArgs e)
		{
			Program.DibujaTodo();
		}

		private void chkAutoReclutar_CheckedChanged(object sender, EventArgs e)
		{
			ciudad.AutoReclutar = chkAutoReclutar.Checked;
		}

		private void listTrabajos_DoubleClick(object sender, EventArgs e)
		{
			Trabajo T = TrabajoSeleccionado();
			if (T != null)
			{
				frmTrabajo frmT = new frmTrabajo(T);
				frmT.Show();
			}
		}
	}
}
