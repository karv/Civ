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
        public readonly Civilización Civ;
        /// <summary>
        /// Crea una instancia de esta forma, para una civilización
        /// </summary>
        /// <param name="C">La civilización que estará vinculada a esta forma.</param>
        public FrmCiv(Civilización C)
        {
            Civ = C;
            InitializeComponent();
            Draw();
        }

        /// <summary>
        /// Ejecuta un turno.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void acTurno(object sender, EventArgs e)
        {
            Civ.doTick();

            // Actualizar otros formularios abiertos.
            foreach (var x in Application.OpenForms)
            {
                if (x is IDibujable)
                {
                    IDibujable y = (IDibujable)x;
                    y.Draw();
                }
            }

            // Actualizar este formulario
            Draw();

            // Mensajes
            foreach (var x in Civ.Msj)
            {
                MessageBox.Show(x);
            }
            Civ.Msj.Clear();
        }

        public void Draw ()
        {
            this.Text = Civ.Nombre;
            // Lista de ciudades
            listCiudades.Items.Clear();
            foreach (var x in Civ.getCiudades)
            {
                listCiudades.Items.Add(x);
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

    }
}