using System;
using System.Collections.Generic;

namespace Gtk
{
	[System.ComponentModel.ToolboxItem (true)]
	public partial class GeneralCombobox : Bin
	{
		Dictionary <string, object> model = new Dictionary<string, object> ();

		public GeneralCombobox ()
		{
			Build ();
			combobox.Model = new ListStore (typeof (string));
			var text = new CellRendererText ();
			combobox.PackStart (text, false);
		}

		/// <summary>
		/// Agrega un elemento al combobox
		/// </summary>
		/// <param name="obj">Objeto vinculado</param>
		/// <param name="s">String a mostrar</param>
		public void Add (object obj, string s)
		{
			combobox.AppendText (s);
			model.Add (s, obj);
		}

		public void Clear ()
		{
			model.Clear ();
			combobox.Model = new ListStore (typeof (string));
			var text = new CellRendererText ();
			combobox.PackStart (text, false);
		}

		/// <summary>
		/// Devuelve la armada seleccionada.
		/// </summary>
		/// <returns>The selected.</returns>
		public object Selected
		{
			get
			{
				if (combobox.ActiveText == null || !model.ContainsKey (combobox.ActiveText))
					return null;
				return model [combobox.ActiveText];
			}
		}

		/// <summary>
		/// Devuelve o establece el texto del control.
		/// </summary>
		public string Texto
		{
			get
			{
				return combobox.ActiveText;
			}
			set
			{
				combobox.ActiveText = value;
			}
		}

		public event EventHandler OnSelectionChanged;

		protected void OnComboBoxChanged (object sender, EventArgs e)
		{
			OnSelectionChanged?.Invoke (sender, e);
		}

		public void LlenarCon (System.Collections.IEnumerable list)
		{
			Clear ();
			foreach (var x in list)
			{
				Add (x, x.ToString ());
			}
		}

		public void LlenarCon (IDictionary<string, object> list)
		{
			Clear ();
			foreach (var x in list)
			{
				Add (x.Value, x.Key);
			}
		}

		public void LlenarCon<T> (IEnumerable<T> list,
		                          Func<T, string> stringSelector)
		{
			Clear ();
			foreach (var x in list)
			{
				//TODO Forzar que no se repitan
				string s = stringSelector (x);
				int i = 0;
				while (model.ContainsKey (s))
				{
					s = stringSelector (x) + (++i);
				}
				Add (x, s);
			}
		}
	}
}