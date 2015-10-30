namespace Gtk
{
	public class CellRendererNumRecluta: CellRendererText
	{
		public NodeStore Store;

		public CellRendererNumRecluta (NodeStore store)
			: this ()
		{
			Store = store;
		}

		CellRendererNumRecluta ()
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
			if (ulong.TryParse (new_text, out res) && res > 0)
			{
				var nodo = Store.GetNode (new TreePath (path)) as wgReclutar.ReclutarListEntry;
				nodo.ciudad.Reclutar (nodo.unidad, res);
				System.Diagnostics.Debug.WriteLine (string.Format (
					"Se reclutaron {0} unidad(es) del tipo {1}",
					res,
					nodo.unidad));
			}
		}
	}
}