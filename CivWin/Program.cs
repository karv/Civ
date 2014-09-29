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

        private static void DoRead(string f = "Data.xml")
        {
            g_.Data = new g_Data();
            g_.Data = Store.Store<g_Data>.Deserialize(f);

            
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            DoRead(); /*

            TrabajoRAW T = new TrabajoRAW();
            T.Nombre = "Trab";
            T.Edificio = "Granja";
            T.EntradaStr.Add(new Basic.Par<string, float>("Alimento", 0.3f));
            T.EntradaStr.Add(new Basic.Par<string, float>("Oro", 0.1f));
            T.SalidaStr.Add(new Basic.Par<string, float>("Salida", 12f));
            T.SalidaStr.Add(new Basic.Par<string, float>("Salida 2", 10.1234f));
            T.Requiere.Add("Una ciencia");
            T.Requiere.Add("Otra ciencia");
            g_.Data.Trabajos.Add(T);*/

            g_.GuardaData();

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

/// <summary>
/// Para un Windows.form, obliga tener una función de redibujar/actualizar.
/// En partícular, para actualizar todo después de un tick.
/// </summary>
public interface IDibujable
{
    void Draw();
}