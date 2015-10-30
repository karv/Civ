using Civ;

namespace Gtk
{
	/// <summary>
	/// Widget para reclutar
	/// </summary>
	[System.ComponentModel.ToolboxItem (true)]
	public partial class ReclutarWidget : Bin, IActualizable
	{
		/// <summary>
		/// La ciudad anclada a este widget.
		/// </summary>
		public ICiudad Ciudad;
		//Gtk.ListStore store = new Gtk.ListStore(typeof(ReclutarListEntry));
		NodeStore store = new NodeStore (typeof (ReclutarListEntry));

		public ReclutarWidget ()
		{
			Build ();
		
			Nodeview.AppendColumn ("Nombre", new CellRendererText (), "text", 0);
			Nodeview.AppendColumn ("Existentes", new CellRendererText (), "text", 1);
			Nodeview.AppendColumn (
				"Reclutar",
				new CellRendererNumRecluta (store),
				"text",
				2);
			Nodeview.AppendColumn ("Máximo", new CellRendererText (), "text", 3);

			Nodeview.NodeStore = store;
		}

		public void ConstruirModelo ()
		{
			store.Clear ();
			foreach (var x in Ciudad.UnidadesConstruibles())
			{
				store.AddNode (new ReclutarListEntry (x, Ciudad));
			}
		}

		#region IActualizable implementation

		void IActualizable.Actualizar ()
		{
			ConstruirModelo ();
		}

		#endregion

		/// <summary>
		/// Entrada de TreeView de lista de reclutamiento de unidades.
		/// </summary>
		public class ReclutarListEntry : TreeNode
		{
			public readonly IUnidadRAW Unidad;
			public readonly ICiudad Ciudad;

			public ReclutarListEntry (IUnidadRAW unidad, ICiudad ciudad)
			{
				Unidad = unidad;
				Ciudad = ciudad;
			}

			[Gtk.TreeNodeValue (Column = 0)]
			public string Nombre
			{
				get
				{
					return Unidad.Nombre;
				}
			}

			[Gtk.TreeNodeValue (Column = 1)]
			public ulong Existentes
			{
				get
				{
					Stack grupo = Ciudad.Defensa [Unidad];
					return grupo?.Cantidad ?? 0;
				}
			}

			[Gtk.TreeNodeValue (Column = 2)]
			public ulong MarcadoReclutar
			{
				get
				{
					return 0;
				}
			}

			[Gtk.TreeNodeValue (Column = 3)]
			public ulong MaxRecluta
			{
				get
				{
					return Ciudad.UnidadesConstruibles (Unidad);
				}
			}
		}
	}
}