
// This file has been generated by the GUI designer. Do not modify.
namespace gtk
{
	public partial class frmCiudad
	{
		private global::Gtk.Notebook notebook1;
		
		private global::Gtk.HBox hbox1;
		
		private global::Gtk.Label lbCityInfo;
		
		private global::Gtk.ScrolledWindow GtkScrolledWindow;
		
		private global::Gtk.NodeView nvTrabajos;
		
		private global::Gtk.Label label1;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget gtk.frmCiudad
			this.Name = "gtk.frmCiudad";
			this.Title = global::Mono.Unix.Catalog.GetString ("frmCiudad");
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			// Container child gtk.frmCiudad.Gtk.Container+ContainerChild
			this.notebook1 = new global::Gtk.Notebook ();
			this.notebook1.CanFocus = true;
			this.notebook1.Name = "notebook1";
			this.notebook1.CurrentPage = 0;
			// Container child notebook1.Gtk.Notebook+NotebookChild
			this.hbox1 = new global::Gtk.HBox ();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.lbCityInfo = new global::Gtk.Label ();
			this.lbCityInfo.Name = "lbCityInfo";
			this.lbCityInfo.LabelProp = global::Mono.Unix.Catalog.GetString ("label2");
			this.hbox1.Add (this.lbCityInfo);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.lbCityInfo]));
			w1.Position = 0;
			w1.Expand = false;
			w1.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.nvTrabajos = new global::Gtk.NodeView ();
			this.nvTrabajos.CanFocus = true;
			this.nvTrabajos.Name = "nvTrabajos";
			this.GtkScrolledWindow.Add (this.nvTrabajos);
			this.hbox1.Add (this.GtkScrolledWindow);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.GtkScrolledWindow]));
			w3.Position = 1;
			this.notebook1.Add (this.hbox1);
			// Notebook tab
			this.label1 = new global::Gtk.Label ();
			this.label1.Name = "label1";
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString ("General");
			this.notebook1.SetTabLabel (this.hbox1, this.label1);
			this.label1.ShowAll ();
			this.Add (this.notebook1);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 400;
			this.DefaultHeight = 300;
			this.Show ();
		}
	}
}
