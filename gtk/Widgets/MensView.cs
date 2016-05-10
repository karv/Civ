using System;
using Civ.IU;

namespace Gtk
{
	[System.ComponentModel.ToolboxItem (true)]
	public partial class MensView : Bin
	{
		ManejadorMensajes _data;

		public ManejadorMensajes Manejador
		{
			set
			{
				_data = value;
				_data.AlAgregar += x => UpdateAll ();
				UpdateLabel ();
			}
		}

		public void UpdateAll ()
		{
			UpdateControls ();
			UpdateLabel ();
		}

		/// <summary>
		/// Devuelve o establece el texto que aparece
		/// </summary>
		/// <value>The texto.</value>
		public string Texto
		{
			get
			{
				return _data [0].ToString ();
			}
		}

		public int Count
		{
			get{ return _data.Count; }
		}

		void UpdateLabel ()
		{
			label.Text = _data.Count > 0 ? Texto : null;
		}

		void UpdateControls ()
		{
			bool Sensib = (Count > 0);
			cmdAdel.Sensitive = Sensib;
			cmdAtras.Sensitive = Sensib;
			cmdBorrar.Sensitive = Sensib;
		}

		public void MoveNext ()
		{
			_data.Move (1);
			UpdateLabel ();
		}

		public void MovePrev ()
		{
			_data.Move (-1);
			UpdateLabel ();
		}

		public void RemoveCurrent ()
		{
			_data.RemoveAt (0);
			UpdateLabel ();
			UpdateControls ();
		}

		public MensView ()
		{
			Build ();
		}


		protected void OnCmdBorrarClicked (object sender, EventArgs e)
		{
			RemoveCurrent ();
		}

		protected void OnButton19Clicked (object sender, EventArgs e)
		{
			MovePrev ();
		}

		protected void OnCmdAdelClicked (object sender, EventArgs e)
		{
			MoveNext ();
		}
	}
}