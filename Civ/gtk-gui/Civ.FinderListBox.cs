
// This file has been generated by the GUI designer. Do not modify.
namespace Civ
{
	public partial class FinderListBox
	{
		private global::Gtk.VBox vbox1;
		private global::Gtk.Entry entry1;
		private global::Gtk.ComboBoxEntry cbLista;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget Civ.FinderListBox
			global::Stetic.BinContainer.Attach (this);
			this.Name = "Civ.FinderListBox";
			// Container child Civ.FinderListBox.Gtk.Container+ContainerChild
			this.vbox1 = new global::Gtk.VBox ();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			// Container child vbox1.Gtk.Box+BoxChild
			this.entry1 = new global::Gtk.Entry ();
			this.entry1.CanFocus = true;
			this.entry1.Name = "entry1";
			this.entry1.IsEditable = true;
			this.entry1.InvisibleChar = '•';
			this.vbox1.Add (this.entry1);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.entry1]));
			w1.Position = 0;
			w1.Expand = false;
			w1.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.cbLista = global::Gtk.ComboBoxEntry.NewText ();
			this.cbLista.Name = "cbLista";
			this.vbox1.Add (this.cbLista);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.cbLista]));
			w2.Position = 1;
			w2.Fill = false;
			this.Add (this.vbox1);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.Hide ();
		}
	}
}