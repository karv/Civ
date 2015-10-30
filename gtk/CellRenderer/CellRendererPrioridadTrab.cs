namespace Gtk
{
	public class CellRendererPrioridadTrab : CellRendererText
	{
		public NodeStore Store;

		public CellRendererPrioridadTrab (NodeStore store)
			: this ()
		{
			Store = store;
		}

		CellRendererPrioridadTrab ()
		{
			Editable = true;
		}

		/// <summary>
		/// Valida valor ulong, y luego cambia los trabajadores del trabajo seleccionado.
		/// </summary>
		/// <param name="path">Path.</param>
		/// <param name="new_text">New text.</param>
		override protected void OnEdited (string path, string new_text)
		{
			float res;
			if (float.TryParse (new_text, out res))
			{
				var nodo = Store.GetNode (new TreePath (path)) as TrabajoListEntry;
				nodo.Prioridad = res;
			}
		}
	}
}