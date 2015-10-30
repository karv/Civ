namespace Gtk
{

	[System.ComponentModel.ToolboxItem (true)]
	public partial class PopDisplay : Bin, IActualizable
	{
		#region IActualizable implementation

		public void Actualizar ()
		{
			Refresh ();
		}

		#endregion

		Civ.ICiudad ciudad;

		public Civ.ICiudad Ciudad
		{
			get
			{
				return ciudad;
			}
			set
			{
				ciudad = value;
				Refresh ();
			}
		}

		public PopDisplay ()
		{
			Build ();
		}

		public void Refresh ()
		{
			var pop = ciudad.GetPoblacionInfo;
			label3.Text = pop.Productiva.ToString ();
			label4.Text = pop.PreProductiva.ToString ();
			label5.Text = pop.PostProductiva.ToString ();
		}
	}
}