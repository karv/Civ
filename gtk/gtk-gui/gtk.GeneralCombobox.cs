
// This file has been generated by the GUI designer. Do not modify.
namespace gtk
{
	public partial class GeneralCombobox
	{
		private global::Gtk.ComboBox combobox;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget gtk.GeneralCombobox
			global::Stetic.BinContainer.Attach (this);
			this.Name = "gtk.GeneralCombobox";
			// Container child gtk.GeneralCombobox.Gtk.Container+ContainerChild
			this.combobox = global::Gtk.ComboBox.NewText ();
			this.combobox.Name = "combobox";
			this.Add (this.combobox);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.Hide ();
		}
	}
}
