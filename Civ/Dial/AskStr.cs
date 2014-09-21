using System;

namespace Dial
{
	public partial class AskStr : Gtk.Dialog
	{

		Basic.Par<string,bool> ret;
		public AskStr (string Text, string Default = "")
		{
			this.Build ();
			lbtext.Text = Text;
			entText.Text = Default;
		}
		/// <summary>
		/// Run this instance.
		/// </summary>
		/// <returns>Una pareja: El string que devolvió el usuario y si presionó aceptar.</returns>
		override public Basic.Par<String, bool> Run()
		{
			Show ();

			ret.x = entText.Text;

			return ret;
		}

		protected void OnButtonOkClicked (object sender, EventArgs e)
		{
			ret.y = true;
			this.Destroy ();
		}

		protected void OnButtonCancelClicked (object sender, EventArgs e)
		{
			ret.y = false;
			this.Destroy ();
		}
	}
}
