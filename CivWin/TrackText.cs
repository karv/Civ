using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CivWin
{
	public partial class TrackText : UserControl
	{
		/// <summary>
		/// Se ejecuta cuando Valor cambia.
		/// </summary>
		public event Action OnCambio;

		/// <summary>
		/// Devuelve o establece el valor del control.
		/// </summary>
		public int Valor
		{
			get { return tbNumTrab.Value; }
			set 
			{
				tbNumTrab.Value = Valor;
				txNumTrab.Text = Valor.ToString();
			}
		}

		public string texto
		{
			get
			{
				return gbTexto.Text;
			}
			set
			{
				gbTexto.Text = value;
			}
		}

		/// <summary>
		/// Devuelve o establece en máximo valor de este control.
		/// </summary>
		public int Máximo
		{
			get { return tbNumTrab.Maximum; }
			set { tbNumTrab.Maximum = value; }
		}

		public TrackText()
		{
			InitializeComponent();
		}

		void tbNumTrab_ValueChanged(object sender, EventArgs e)
		{
			if (OnCambio != null) OnCambio.Invoke();
			txNumTrab.Text = Valor.ToString();
		}
	}
}
