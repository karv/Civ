namespace Gtk
{
	public class CellRendererNumTrab: CellRendererText
	{

		public NodeStore Store;

		public CellRendererNumTrab (NodeStore store)
			: this ()
		{
			Store = store;
		}

		CellRendererNumTrab ()
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
			ulong res;
			if (ulong.TryParse (new_text, out res))
			{
				var nodo = Store.GetNode (new TreePath (path)) as TrabajoListEntry;
				nodo.Trabajadores = res;
			}
		}
	}
}

