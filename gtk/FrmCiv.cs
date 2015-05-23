//
//  frmCiv.cs
//
//  Author:
//       edgar <>
//
//  Copyright (c) 2015 edgar
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using Civ;

namespace CivGTK
{
	class CityListEntry : Gtk.TreeNode
	{
		public readonly Ciudad ciudad;

		[Gtk.TreeNodeValue (Column = 0)]
		public string nombre {
			get {
				return ciudad.Nombre;
			}
		}

		[Gtk.TreeNodeValue (Column = 1)]
		public ulong población {
			get {
				return ciudad.getPoblacion;
			}
		}

		[Gtk.TreeNodeValue (Column = 2)]
		public float Ocupación { get { return ciudad.getNumTrabajadores / ciudad.getPoblacionProductiva; } }

		public CityListEntry (Ciudad ciudad)
		{
			this.ciudad = ciudad;
		}
	}

	public class frmCiv : Gtk.Window
	{
		#region Controles

		private global::Gtk.Notebook notebook1;

		private global::Gtk.VBox vbox1;

		private global::Gtk.HButtonBox hbuttonbox2;

		private global::Gtk.Button cmdIr;

		private global::Gtk.Label label1;

		Gtk.NodeView nv;

		#endregion

		public readonly Civilizacion Civ;
		Gtk.NodeStore store = new Gtk.NodeStore (typeof(CityListEntry));

		public frmCiv (Civilizacion nCiv) :
			base (Gtk.WindowType.Toplevel)
		{

			Civ = nCiv;
			foreach (var x in Civ.getCiudades) {
				//store.AppendValues (new CityListEntry (x));
				store.AddNode (new CityListEntry (x));
			}

			Build ();
			nv.Columns [0].Reorderable = false;
			nv.Columns [1].Reorderable = true;
			nv.Columns [2].Reorderable = true;

			ShowAll ();
		}

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget CivGTK.frmCiv
			this.Name = "CivGTK.frmCiv";
			this.Title = global::Mono.Unix.Catalog.GetString ("frmCiv");
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			// Container child CivGTK.frmCiv.Gtk.Container+ContainerChild
			this.notebook1 = new global::Gtk.Notebook ();
			this.notebook1.CanFocus = true;
			this.notebook1.Name = "notebook1";
			this.notebook1.CurrentPage = 0;
			// Container child notebook1.Gtk.Notebook+NotebookChild
			this.vbox1 = new global::Gtk.VBox ();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbuttonbox2 = new global::Gtk.HButtonBox ();
			this.hbuttonbox2.Name = "hbuttonbox2";
			// Nodeview
			nv = new Gtk.NodeView (store);
			nv.AppendColumn ("Nombre", new Gtk.CellRendererText (), "text", 0);
			nv.AppendColumn ("Población", new Gtk.CellRendererText (), "text", 1);
			nv.AppendColumn ("Ocupación", new Gtk.CellRendererText (), "text", 2);
			// Container child hbuttonbox2.Gtk.ButtonBox+ButtonBoxChild
			this.cmdIr = new global::Gtk.Button ();
			this.cmdIr.CanFocus = true;
			this.cmdIr.Name = "cmdIr";
			this.cmdIr.UseUnderline = true;
			this.cmdIr.Label = global::Mono.Unix.Catalog.GetString ("_Ir");
			this.hbuttonbox2.Add (this.cmdIr);
			global::Gtk.ButtonBox.ButtonBoxChild w1 = ((global::Gtk.ButtonBox.ButtonBoxChild)(this.hbuttonbox2 [this.cmdIr]));
			w1.Expand = false;
			w1.Fill = false;
			this.vbox1.Add (this.hbuttonbox2);
			this.vbox1.Add (this.nv);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.hbuttonbox2]));
			w2.Position = 1;
			w2.Expand = false;
			w2.Fill = false;
			this.notebook1.Add (this.vbox1);
			// Notebook tab
			this.label1 = new global::Gtk.Label ();
			this.label1.Name = "label1";
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString ("Ciudades");
			this.notebook1.SetTabLabel (this.vbox1, this.label1);
			this.label1.ShowAll ();
			this.Add (this.notebook1);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 400;
			this.DefaultHeight = 300;
			this.Show ();
			this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
			this.cmdIr.Clicked += OnCmdIrActivated;
		}


		protected void OnCmdIrActivated (object sender, EventArgs e)
		{
			Gtk.NodeSelection r = nv.NodeSelection;
			Ciudad c = ((CityListEntry)r.SelectedNode).ciudad;

			frmCiudad wind = new frmCiudad (c);
			wind.Show ();
			//throw new NotImplementedException ();

		}

		protected void OnDeleteEvent (object sender, Gtk.DeleteEventArgs a)
		{
			Gtk.Application.Quit ();
			a.RetVal = true;
		}

		protected override bool OnDeleteEvent (Gdk.Event evnt)
		{
			Gtk.Application.Quit ();
			return true;
		}

	}
}

