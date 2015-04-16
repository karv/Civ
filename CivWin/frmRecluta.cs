using System.Windows.Forms;
using Civ;
using Global;

namespace CivWin
{
	public partial class frmRecluta : Form
	{
		/// <summary>
		/// Ciudad vinculada a esta forma.
		/// </summary>
		public readonly Civ.Ciudad Ciudad;

		/// <summary>
		/// Inicializa instancia de esta clase.
		/// </summary>
		/// <param name="C">Ciudad vinculada.</param>
		public frmRecluta(Civ.Ciudad C)
		{			
			Ciudad = C;
			InitializeComponent();
			Actualiza();
		}

		public void Actualiza()
		{
			// Llenar la lista
			lbUnidades.Items.Clear();
			foreach (var x in g_.Data.Unidades)
			{
				if (Ciudad.SatisfaceReq(x.ReqCiencia) && x.Reqs <= Ciudad.Almacen)				
					lbUnidades.Items.Add(x);				
			}
		}

		private void lbUnidades_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			UnidadRAW Unid = (UnidadRAW)lbUnidades.SelectedItem;
			if (Unid == null) return;
			// Poner máximo reclutable
			float maxf = Ciudad.getTrabajadoresDesocupados / Unid.CostePoblacion;
			foreach (var rec in Unid.Reqs.Keys)
			{
				maxf = System.Math.Min(maxf, Ciudad.Almacen[rec] / Unid.Reqs[rec]);
			}

			ttCantidad.Máximo = (int)maxf;
		}

		/// <summary>
		/// Ejecuta esta forma como un díalogo.
		/// </summary>
		/// <returns>Devuelve la unidad y cantidad que el usuario especifica.
		/// Si el usuario no acepta, regresa la unidad "null" con cantidad 0.</returns>
		public new Regresa ShowDialog ()
		{
			Regresa ret = new Regresa();
			DialogResult res = base.ShowDialog();
			if (res == DialogResult.OK)
			{
				ret.Unidad = (UnidadRAW)lbUnidades.SelectedItem;
				ret.Cantidad = (ulong)ttCantidad.Valor;
			}
			else
			{
				ret.Unidad = null;
				ret.Cantidad = 0;
			}
			return ret;
		}
		
		// Return
		public struct Regresa
		{
			/// <summary>
			/// Unidad.
			/// </summary>
			public UnidadRAW Unidad;
			/// <summary>
			/// Cantidad.
			/// </summary>
			public ulong Cantidad;
		}


	}
}
