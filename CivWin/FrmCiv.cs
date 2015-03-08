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
	public partial class FrmCiv : Form, IDibujable
	{
		public readonly Civilizacion Civ;
		/// <summary>
		/// Crea una instancia de esta forma, para una civilización
		/// </summary>
		/// <param name="C">La civilización que estará vinculada a esta forma.</param>
		public FrmCiv(Civilizacion C)
		{
			Civ = C;
			InitializeComponent();
			Draw();
		}

		public void Draw()
		{
			this.Text = Civ.Nombre;
			// Lista de ciudades
			listCiudades.Items.Clear();
			foreach (var x in Civ.getCiudades)
			{
				listCiudades.Items.Add(x);
			}

			lstCiencias.Items.Clear();
			foreach (var x in Civ.Investigando.ToArray())	//Crear copia e iterar.
			{
				lstCiencias.Items.Add(string.Format("{0} - {1:p}", x.Ciencia.Nombre, x.ObtPct ()));	// TODO: Que se muestre más o menos cuánto falta
			}
			lstCiencias.Items.Add("");
			foreach (var x in Civ.Avances)
			{
				lstCiencias.Items.Add(x);
			}
		}

		private void listCiudades_DoubleClick(object sender, EventArgs e)
		{
			Ciudad C = (Ciudad)listCiudades.SelectedItem;
			if (C != null)
			{
				Program.MuestraCiudad(C, this);
			}
		}

		private void toolStripButton2_Click(object sender, EventArgs e)
		{
			Program.DibujaTodo();
		}

		
	}
}