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

            Draw();
        }

        public void Draw()
        {
            Text = ciudad.Nombre;

            // Recursos
            listRecursos.Items.Clear();
            foreach (var x in ciudad.Almacén.Keys)
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
            foreach (var x in ciudad.ObtenerListaTrabajos)
            {
                listTrabajos.Items.Add(x);
            }
        }
    }
}
