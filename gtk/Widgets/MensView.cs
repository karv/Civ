using System;
using System.Collections.Generic;

namespace Gtk
{
	[System.ComponentModel.ToolboxItem (true)]
	public partial class MensView : Bin
	{
		LinkedList<string> _data = new LinkedList<string> ();
		LinkedListNode<string> _current;

		/// <summary>
		/// Devuelve o establece el texto que aparece
		/// </summary>
		/// <value>The texto.</value>
		public string Texto
		{
			get
			{
				return _current.Value;
			}
		}

		public int Count
		{
			get{ return _data.Count; }
		}

		public void Add (string data)
		{
			_data.AddLast (data);
			if (Count == 1)
				_current = _data.First;
			UpdateLabel ();
			UpdateControls ();
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
			_current = _current.Next ?? _data.First;
			UpdateLabel ();
		}

		public void MovePrev ()
		{
			_current = _current.Previous ?? _data.Last;
			UpdateLabel ();
		}

		public void RemoveCurrent ()
		{
			LinkedListNode<string> del = _current;
			MoveNext ();
			_data.Remove (del);
			UpdateLabel ();
			UpdateControls ();
		}

		public MensView ()
		{
			_current = _data.First;
			Build ();
			UpdateLabel ();
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