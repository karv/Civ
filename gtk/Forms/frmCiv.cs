//
//  frmCiv.cs
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
using Civ;

namespace gtk
{
	#region EntryLists
	class CienciaConoListEntry : Gtk.TreeNode
	{
		public readonly Ciencia ciencia;

		public CienciaConoListEntry(Ciencia c)
		{
			ciencia = c;
		}

		[Gtk.TreeNodeValue(Column = 0)]
		public string nombre { get { return ciencia.Nombre; } }
	}

	class CienciaAbtaListEntry : Gtk.TreeNode
	{
		public readonly InvestigandoCiencia ciencia;

		public CienciaAbtaListEntry(InvestigandoCiencia c)
		{
			ciencia = c;
		}

		[Gtk.TreeNodeValue(Column = 1)]
		public string nombre { get { return ciencia.Ciencia.Nombre; } }

		[Gtk.TreeNodeValue(Column = 0)]
		public float getPct { get { return ciencia.ObtPct(); } }
	}

	class CityListEntry : Gtk.TreeNode
	{
		public readonly Ciudad ciudad;

		[Gtk.TreeNodeValue(Column = 0)]
		public string nombre
		{
			get
			{
				return ciudad.Nombre;
			}
		}

		[Gtk.TreeNodeValue(Column = 1)]
		public ulong población
		{
			get
			{
				return ciudad.getPoblacion;
			}
		}

		[Gtk.TreeNodeValue(Column = 2)]
		public float Ocupación { get { return (float)ciudad.getNumTrabajadores / ciudad.getPoblacionProductiva; } }

		public CityListEntry(Ciudad ciudad)
		{
			this.ciudad = ciudad;
		}
	}
	#endregion

	public partial class frmCiv : Gtk.Window
	{
		public Civilizacion civ;

		public readonly System.Collections.Generic.List<IActualizable> formsActualizables = new System.Collections.Generic.List<IActualizable>();

		#region IActualizable implementation

		/// <summary>
		/// Actualiza esta form y todas sus hijas.
		/// </summary>
		public void Actualizar()
		{
			ActualizarDebil();
			foreach (var x in formsActualizables)
			{
				x.Actualizar();
			}
		}

		/// <summary>
		/// Actualiza esta form
		/// </summary>
		public void ActualizarDebil()
		{
			stCiudad.Clear();
			foreach (var x in civ.getCiudades)
			{
				//store.AppendValues (new CityListEntry (x));
				stCiudad.AddNode(new CityListEntry(x));
			}

			stCienciasCono.Clear();
			foreach (var x in civ.Avances)
			{
				stCienciasCono.AddNode(new CienciaConoListEntry(x));
			}

			stCienciasAbtas.Clear();
			foreach (var x in civ.Investigando)
			{				
				stCienciasAbtas.AddNode(new CienciaAbtaListEntry(x));
			}

		}

		#endregion

		Gtk.NodeStore stCiudad = new Gtk.NodeStore(typeof(CityListEntry));
		Gtk.NodeStore stCienciasCono = new Gtk.NodeStore(typeof(CienciaConoListEntry));
		Gtk.NodeStore stCienciasAbtas = new Gtk.NodeStore(typeof(CienciaAbtaListEntry));

		public frmCiv(Civilizacion civ) :
			base(Gtk.WindowType.Toplevel)
		{
			this.civ = civ;

			this.Build();

			ActualizarDebil();

			nvCiudades.NodeStore = stCiudad;
			nvAvances.NodeStore = stCienciasCono;
			nvInvestigando.NodeStore = stCienciasAbtas;

			nvCiudades.AppendColumn("Nombre", new Gtk.CellRendererText(), "text", 0);
			nvCiudades.AppendColumn("Población", new Gtk.CellRendererText(), "text", 1);
			nvCiudades.AppendColumn("Ocupación", new Gtk.CellRendererText(), "text", 2);
			nvAvances.AppendColumn("Avance", new Gtk.CellRendererText(), "text", 0);
			nvInvestigando.AppendColumn("%", new Gtk.CellRendererText(), "text", 0);
			nvInvestigando.AppendColumn("Avance", new Gtk.CellRendererText(), "text", 1);

			nvCiudades.Columns[0].Reorderable = false;
			nvCiudades.Columns[1].Reorderable = true;
			nvCiudades.Columns[2].Reorderable = true;
			nvAvances.Columns[0].Reorderable = true;
			nvInvestigando.Columns[0].MaxWidth = 70;

		}

		protected override bool OnDeleteEvent(Gdk.Event evnt)
		{
			Gtk.Application.Quit();
			return true;
		}

		protected void OnCmdIrActivated(object sender, EventArgs e)
		{
			Gtk.NodeSelection r = nvCiudades.NodeSelection;
			Ciudad c = ((CityListEntry)r.SelectedNode).ciudad;

			frmCiudad wind = new frmCiudad(c, this);
			formsActualizables.Add(wind);
			wind.Show();
		}
	}
}

