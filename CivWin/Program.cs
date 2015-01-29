using Civ;
using Global;
using Graficas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CivWin
{
    public static class Program
    {

        private static void DoRead(string f = "Data.xml")
        {
            g_.Data = Store.Store<g_Data>.Deserialize(f);
            
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            DoRead();
            g_.InicializarJuego();

            Civilizacion C = g_.State.Civs[0];
			C.getCiudades[0].AlimentoAlmacén = 100;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmCiv(C));
        }

        public static void MuestraCiudad(Ciudad C, Form Owner)
        {
            FrmCiudad frm = new FrmCiudad(C);
            frm.Show(Owner);
           
        }
    }
}

/// <summary>
/// Para un Windows.form, obliga tener una función de redibujar/actualizar.
/// En partícular, para actualizar todo después de un tick.
/// </summary>
public interface IDibujable
{
    void Draw();
}