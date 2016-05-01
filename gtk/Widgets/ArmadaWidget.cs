using Civ.ObjetosEstado;

namespace Gtk
{
	class UnidadListEntry : TreeNode
	{
		public readonly Stack Unidad;

		public UnidadListEntry (Stack unidad)
		{
			Unidad = unidad;
		}

		[Gtk.TreeNodeValue (Column = 0)]
		public string Tipo
		{
			get
			{
				return Unidad.RAW.Nombre;
			}
		}

		[Gtk.TreeNodeValue (Column = 1)]
		public ulong Cantidad { get { return Unidad.Cantidad; } }

		[Gtk.TreeNodeValue (Column = 2)]
		public float Entrenamiento { get { return Unidad.Entrenamiento; } }
	}

	[System.ComponentModel.ToolboxItem (true)]
	public partial class ArmadaWidget : Bin, IActualizable
	{
		public NodeStore Store = new NodeStore (typeof (UnidadListEntry));
		public Armada Armada;

		public ArmadaWidget ()
		{
			Build ();

			nodeview2.AppendColumn ("Tipo", new CellRendererText (), "text", 0);
			nodeview2.AppendColumn ("Cantidad", new CellRendererText (), "text", 1);
			nodeview2.AppendColumn ("Entrenamiento", new CellRendererText (), "text", 2);
			nodeview2.NodeStore = Store;
		}

		/// <summary>
		/// Devuelve la unidad seleccionada.
		/// </summary>
		/// <returns>The selected.</returns>
		public Stack Selected
		{
			get
			{
				NodeSelection r = nodeview2.NodeSelection;
				if (r.SelectedNode == null)
					return null;
				Stack c = ((UnidadListEntry)r.SelectedNode).Unidad;

				return c;
			}
		}

		#region IActualizable implementation

		public void Actualizar ()
		{
			Store.Clear ();
			foreach (var x in Armada.Unidades)
			{
				Store.AddNode (new UnidadListEntry (x));
			}
/*
			System.Collections.Generic.Dictionary <UnidadRAW, System.Collections.Generic.List <Unidad>> unid = Armada.ToDictionary();
			foreach (var x in unid)
			{
				Gtk.TreeIter iter = store.AppendValues(x.Key);
				foreach (var y in x.Value)
				{
					store.AppendValues(iter, new UnidadListEntry(y));
				}
			}
			*/
		}

		#endregion
	}
}