using Civ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

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
				ListViewItem Agrega = new ListViewItem();
				Agrega.Tag = x;
				Agrega.Text = x.ToString();
				listTrabajos.Items.Add(Agrega);
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

			// Unidades / Defensa
			listUnidades.Items.Clear();
			ListasExtra.ListaPeso<UnidadRAW, ListViewGroup> Dict = new ListasExtra.ListaPeso<UnidadRAW,ListViewGroup>(null, null);
			foreach (var x in ciudad.Defensa.Unidades)
			{
				ListViewGroup grp = Dict[x.RAW];
				if (grp == null)
				{
					grp = new ListViewGroup();
					Dict[x.RAW] = grp;
				}
				//grp.Name = "n: " + x.RAW.ToString();
				grp.Header = x.RAW.ToString();
				grp.Tag = x.RAW;
				listUnidades.Groups.Add(grp);

				ListViewItem It = new ListViewItem(grp);
				It.Text = x.ToString();
				It.Tag = x;
				listUnidades.Items.Add(It);
			}
		}

		/// <summary>
		/// Devuele el trabajo seleccionado en listTrabajos. Si no existe, lo crea.
		/// </summary>
		/// <returns></returns>
		private Trabajo TrabajoSeleccionado()
		{
			//listTrabajos.SelectedItems[0].Tag
			TrabajoRAW Selec = listTrabajos.SelectedItems.Count > 0 ? (TrabajoRAW)listTrabajos.SelectedItems[0].Tag : null;
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
			if (listTrabajos.SelectedItems.Count > 0)
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

		private void cmdReclutar_Click(object sender, EventArgs e)
		{
			frmRecluta Rec = new frmRecluta(ciudad);
			frmRecluta.Regresa Res = Rec.ShowDialog();
			if (Res.Unidad != null && Res.Cantidad > 0)
			{
				ciudad.EntrenarUnidades(Res.Unidad, Res.Cantidad);
			}
			Draw();
		}
	}
}
