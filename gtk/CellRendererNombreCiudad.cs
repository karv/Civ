namespace Gtk
{
	public class CellRendererNombreCiudad: CellRendererText
	{

		public NodeStore Store;

		public CellRendererNombreCiudad (NodeStore store)
			: this ()
		{
			Store = store;
		}

		CellRendererNombreCiudad ()
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
			var nodo = Store.GetNode (new TreePath (path)) as CityListEntry;
			nodo.Ciudad.Nombre = new_text;
		}
	}
}