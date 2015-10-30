using System;
using System.Collections.Generic;
using Civ;
using Gtk;

namespace Gtk
{
	[System.ComponentModel.ToolboxItem (true)]
	public partial class ArmadaComboBoxWidget : Bin, IEnumerable<Armada>
	{
		Dictionary<string, Armada> armadas = new Dictionary<string, Armada> ();

		public ArmadaComboBoxWidget ()
		{
			Build ();
			comboBox.Model = new ListStore (typeof (string));
			var text = new CellRendererText ();
			comboBox.PackStart (text, false);
		}

		public void Add (Armada arm)
		{
			string nombre = string.Format ("Peso {0}/{1}", arm.Peso, arm.MaxPeso);

			// Evita repeticiones.
			int i = 0;
			string nom = nombre;

			while (armadas.ContainsKey (nom))
			{
				nom = nombre + " - " + (i++);
			}
			Add (arm, nom);

			comboBox.CheckResize ();
		}

		public void Add (Armada arm, string nombre)
		{
			comboBox.AppendText (nombre);
			armadas.Add (nombre, arm);
		}

		public void Clear ()
		{
			armadas.Clear ();
			//comboBox.Clear();
			comboBox.Model = new ListStore (typeof (string));
			var text = new CellRendererText ();
			comboBox.PackStart (text, false);
		}

		/// <summary>
		/// Devuelve la armada seleccionada.
		/// </summary>
		/// <returns>The selected.</returns>
		public Armada Selected
		{
			get
			{
				if (comboBox.ActiveText == null || !armadas.ContainsKey (comboBox.ActiveText))
					return null;
				return armadas [comboBox.ActiveText];
			}
		}

		IEnumerator<Armada> IEnumerable<Armada>.GetEnumerator ()
		{
			IEnumerable<Armada> enu;
			enu = armadas.Values;
			return enu.GetEnumerator ();
		}

		public event EventHandler OnSelectionChanged;

		protected void OnComboBoxChanged (object sender, EventArgs e)
		{
			OnSelectionChanged?.Invoke (sender, e);
		}
	}
}