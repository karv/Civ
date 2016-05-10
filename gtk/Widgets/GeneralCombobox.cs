using System;
using System.Collections.Generic;
using System.Linq;

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
				TreeIter iter;
				combobox.Model.GetIterFirst (out iter);
				do
				{
					var thisRow = new GLib.Value ();
					combobox.Model.GetValue (iter, 0, ref thisRow);
					if ((thisRow.Val as string).Equals (value))
					{
						combobox.SetActiveIter (iter);
						return;
					}
				}
				while (combobox.Model.IterNext (ref iter));

				throw new Exception (string.Format (
					"No se encuentra objeto con nombre {0} en el combobox {1}",
					value,
					this));
			}
		}

		public int ÍndiceSelección
		{
			set
			{
				combobox.Active = value;
			}
		}

		public event EventHandler AlCambiarSelección;

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
				string s = stringSelector (x);
				int i = 0;
				while (model.ContainsKey (s))
				{
					s = stringSelector (x) + (++i);
				}
				Add (x, s);
			}
		}

		protected void OnComboboxChanged (object sender, EventArgs e)
		{
			AlCambiarSelección?.Invoke (sender, e);
		}
	}
}