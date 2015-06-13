//
//  FrmCiudad.cs
//
//  Author:
//       Edgar Carballo <karvayoEdgar@gmail.com>
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
using Gtk;
using Civ;

namespace CivGTK
{
	class TrabajoListEntry : Gtk.TreeNode
	{
		public readonly Trabajo trabajo;

		[Gtk.TreeNodeValue(Column = 0)]
		public string nombre
		{
			get
			{
				return trabajo.RAW.Nombre;
			}
		}

		[Gtk.TreeNodeValue(Column = 1)]
		public ulong Trabajadores
		{
			get
			{
				return trabajo.Trabajadores;
			}
		}

		[Gtk.TreeNodeValue(Column = 2)]
		public ulong MaxTrabajadores
		{
			get
			{
				return trabajo.MaxTrabajadores;
			}
		}

		public TrabajoListEntry(Trabajo trabajo)
		{
			this.trabajo = trabajo;
		}
	}

	class RecursoListEntry : Gtk.TreeNode
	{
		public readonly Recurso recurso;
		public readonly float cantidad;

		public RecursoListEntry(System.Collections.Generic.KeyValuePair<Recurso, float> entry)
		{
			recurso = entry.Key;
			cantidad = entry.Value;
		}

		public RecursoListEntry(Recurso recurso, float cap)
		{
			this.recurso = recurso;
			this.cantidad = cap;
		}

		[Gtk.TreeNodeValue(Column = 0)]
		public string nombre { get { return recurso.Nombre; } }

		[Gtk.TreeNodeValue(Column = 1)]
		public float cant { get { return cantidad; } }
	}



	public class FrmCiudad: Window
	{
		private Notebook notebook1;

		private HBox hbox1;

		private VBox vbox1;

		private ScrolledWindow GtkScrolledWindow;

		private TextView txCityInfo;

		private ScrolledWindow GtkScrolledWindow1;

		private TreeView tvTrabajos;

		private Label tab1Title;

		private ScrolledWindow GtkScrolledWindow2;

		NodeStore recStore = new NodeStore(typeof(RecursoListEntry));
		private NodeView nvRecursos;

		private Label tab2Title;

		public readonly Ciudad ciudad;

		public FrmCiudad(Ciudad ciudad) : base(ciudad.Nombre)
		{
			this.ciudad = ciudad;

			// Construir recStore
			lock (ciudad.Almacen)
			{
				foreach (var x in ciudad.Almacen)  //Clonar
				{
					recStore.AddNode(new RecursoListEntry(x));
				}
			}

			#region Build
			global::Stetic.Gui.Initialize(this);
			// Widget CivGTK.frmCiudad
			this.Name = "CivGTK.frmCiudad";
			this.Title = global::Mono.Unix.Catalog.GetString("frmCiudad");
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			// Container child CivGTK.frmCiudad.Gtk.Container+ContainerChild
			this.notebook1 = new global::Gtk.Notebook();
			this.notebook1.CanFocus = true;
			this.notebook1.Name = "notebook1";
			this.notebook1.CurrentPage = 1;
			// Container child notebook1.Gtk.Notebook+NotebookChild
			this.hbox1 = new global::Gtk.HBox();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.vbox1 = new global::Gtk.VBox();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			// Container child vbox1.Gtk.Box+BoxChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.txCityInfo = new global::Gtk.TextView();
			this.txCityInfo.Buffer.Text = "Uno";
			this.txCityInfo.CanFocus = true;
			this.txCityInfo.Name = "textview1";
			this.txCityInfo.Editable = false;
			this.txCityInfo.AcceptsTab = false;
			this.GtkScrolledWindow.Add(this.txCityInfo);
			this.vbox1.Add(this.GtkScrolledWindow);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.GtkScrolledWindow]));
			w2.Position = 0;
			this.hbox1.Add(this.vbox1);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.vbox1]));
			w3.Position = 0;
			// Container child hbox1.Gtk.Box+BoxChild
			this.GtkScrolledWindow1 = new global::Gtk.ScrolledWindow();
			this.GtkScrolledWindow1.Name = "GtkScrolledWindow1";
			this.GtkScrolledWindow1.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow1.Gtk.Container+ContainerChild
			this.tvTrabajos = new global::Gtk.TreeView();
			this.tvTrabajos.CanFocus = true;
			this.tvTrabajos.Name = "treeview1";
			this.GtkScrolledWindow1.Add(this.tvTrabajos);
			this.hbox1.Add(this.GtkScrolledWindow1);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.GtkScrolledWindow1]));
			w5.Position = 1;
			this.notebook1.Add(this.hbox1);
			// Notebook tab
			this.tab1Title = new global::Gtk.Label();
			this.tab1Title.Name = "label1";
			this.tab1Title.LabelProp = global::Mono.Unix.Catalog.GetString("_Población");
			this.tab1Title.UseUnderline = true;
			this.notebook1.SetTabLabel(this.hbox1, this.tab1Title);
			this.tab1Title.ShowAll();
			// Container child notebook1.Gtk.Notebook+NotebookChild
			this.GtkScrolledWindow2 = new global::Gtk.ScrolledWindow();
			this.GtkScrolledWindow2.Name = "GtkScrolledWindow2";
			this.GtkScrolledWindow2.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow2.Gtk.Container+ContainerChild
			this.nvRecursos = new NodeView(recStore);
			this.nvRecursos.CanFocus = true;
			this.nvRecursos.Name = "nvRec";
			this.nvRecursos.Reorderable = true;
			this.nvRecursos.AppendColumn("Recurso", new Gtk.CellRendererText(), "text", 0);
			this.nvRecursos.AppendColumn("Valor", new Gtk.CellRendererText(), "text", 1);
			this.GtkScrolledWindow2.Add(this.nvRecursos);
			this.notebook1.Add(this.GtkScrolledWindow2);
			global::Gtk.Notebook.NotebookChild w8 = ((global::Gtk.Notebook.NotebookChild)(this.notebook1[this.GtkScrolledWindow2]));
			w8.Position = 1;
			// Notebook tab
			this.tab2Title = new global::Gtk.Label();
			this.tab2Title.Name = "label2";
			this.tab2Title.LabelProp = global::Mono.Unix.Catalog.GetString("Recursos");
			this.notebook1.SetTabLabel(this.GtkScrolledWindow2, this.tab2Title);
			this.tab2Title.ShowAll();
			this.Add(this.notebook1);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.DefaultWidth = 400;
			this.DefaultHeight = 300;
			#endregion

			this.Show();

		}


	}
}

