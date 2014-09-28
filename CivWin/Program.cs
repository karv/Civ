using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Global;
using Civ;

namespace CivWin
{
    public static class Program
    {

        private static void DoRead()
        {
            g_.Data = new g_Data();
            g_.Data = Store.Store<g_Data>.Deserialize("Data.xml");

        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            DoRead();

            Civilización C = new Civilización();
            C.Nombre = "E:3";
            Ciudad Cd = new Ciudad("MyCity", C);

            Cd.PoblaciónProductiva = 10;
            Cd.Almacén[Ciudad.RecursoAlimento] = 100;
            //g_.Data.RecursoAlimento = "";
            Cd.AgregaEdificio(g_.Data.EncuentraEdificio("Palacio"));

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

public interface IDibujable
{
    void Draw();
}