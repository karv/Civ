
// This file has been generated by the GUI designer. Do not modify.
namespace CivGTK
{
	public partial class frmCiudad
	{
		private global::Gtk.Notebook notebook1;
		
		private global::Gtk.HBox hbox1;
		
		private global::Gtk.VBox vbox1;
		
		private global::Gtk.ScrolledWindow GtkScrolledWindow;
		
		private global::Gtk.TextView textview1;
		
		private global::Gtk.ScrolledWindow GtkScrolledWindow1;
		
		private global::Gtk.TreeView treeview1;
		
		private global::Gtk.Label label1;
		
		private global::Gtk.ScrolledWindow GtkScrolledWindow2;
		
		private global::Gtk.NodeView nvRec;
		
		private global::Gtk.Label label2;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget CivGTK.frmCiudad
			this.Name = "CivGTK.frmCiudad";
			this.Title = global::Mono.Unix.Catalog.GetString ("frmCiudad");
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			// Container child CivGTK.frmCiudad.Gtk.Container+ContainerChild
			this.notebook1 = new global::Gtk.Notebook ();
			this.notebook1.CanFocus = true;
			this.notebook1.Name = "notebook1";
			this.notebook1.CurrentPage = 1;
			// Container child notebook1.Gtk.Notebook+NotebookChild
			this.hbox1 = new global::Gtk.HBox ();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.vbox1 = new global::Gtk.VBox ();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			// Container child vbox1.Gtk.Box+BoxChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.textview1 = new global::Gtk.TextView ();
			this.textview1.Buffer.Text = "Uno";
			this.textview1.CanFocus = true;
			this.textview1.Name = "textview1";
			this.textview1.Editable = false;
			this.textview1.AcceptsTab = false;
			this.GtkScrolledWindow.Add (this.textview1);
			this.vbox1.Add (this.GtkScrolledWindow);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.GtkScrolledWindow]));
			w2.Position = 0;
			this.hbox1.Add (this.vbox1);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.vbox1]));
			w3.Position = 0;
			// Container child hbox1.Gtk.Box+BoxChild
			this.GtkScrolledWindow1 = new global::Gtk.ScrolledWindow ();
			this.GtkScrolledWindow1.Name = "GtkScrolledWindow1";
			this.GtkScrolledWindow1.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow1.Gtk.Container+ContainerChild
			this.treeview1 = new global::Gtk.TreeView ();
			this.treeview1.CanFocus = true;
			this.treeview1.Name = "treeview1";
			this.GtkScrolledWindow1.Add (this.treeview1);
			this.hbox1.Add (this.GtkScrolledWindow1);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.GtkScrolledWindow1]));
			w5.Position = 1;
			this.notebook1.Add (this.hbox1);
			// Notebook tab
			this.label1 = new global::Gtk.Label ();
			this.label1.Name = "label1";
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString ("_Población");
			this.label1.UseUnderline = true;
			this.notebook1.SetTabLabel (this.hbox1, this.label1);
			this.label1.ShowAll ();
			// Container child notebook1.Gtk.Notebook+NotebookChild
			this.GtkScrolledWindow2 = new global::Gtk.ScrolledWindow ();
			this.GtkScrolledWindow2.Name = "GtkScrolledWindow2";
			this.GtkScrolledWindow2.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow2.Gtk.Container+ContainerChild
			this.nvRec = new global::Gtk.NodeView ();
			this.nvRec.CanFocus = true;
			this.nvRec.Name = "nvRec";
			this.nvRec.Reorderable = true;
			this.GtkScrolledWindow2.Add (this.nvRec);
			this.notebook1.Add (this.GtkScrolledWindow2);
			global::Gtk.Notebook.NotebookChild w8 = ((global::Gtk.Notebook.NotebookChild)(this.notebook1 [this.GtkScrolledWindow2]));
			w8.Position = 1;
			// Notebook tab
			this.label2 = new global::Gtk.Label ();
			this.label2.Name = "label2";
			this.label2.LabelProp = global::Mono.Unix.Catalog.GetString ("Recursos");
			this.notebook1.SetTabLabel (this.GtkScrolledWindow2, this.label2);
			this.label2.ShowAll ();
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