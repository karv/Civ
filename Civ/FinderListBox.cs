using System;

namespace Civ
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class FinderListBox : Gtk.Bin
	{
		public FinderListBox ()
		{
			this.Build ();

			cbLista.PrependText ("-.-");
		}
		public void AddItem (object Obj)
		{
			cbLista.PrependText ("Nyah");
		}
	}

}

