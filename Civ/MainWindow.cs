using System;
using Gtk;

public partial class MainWindow: Gtk.Window
{	
	Global.g_Data EditingData = new Global.g_Data ();

	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();

		foreach (Civ.Recurso x in EditingData.Recursos) {
			cbRecursos.AppendText(x.Nombre);
		}
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected void OnCmdNuevoRecursoClicked (object sender, EventArgs e)
	{

		Dial.AskStr Nom = new Dial.AskStr ("Nombre del recurso", "Mi Recurso");
		Basic.Par<string, bool> Resp;
		Resp = Nom.Run ();
		if (Resp.y) {
			//TODO Hacer que no deje nombres repetidos. (¿O sí?)
			EditingData.Recursos.Add (new Civ.Recurso (Resp.x));

			cbRecursos.Clear ();

			foreach (Civ.Recurso x in EditingData.Recursos) {
				cbRecursos.AppendText(x.Nombre);
			}

			this.QueueDraw ();

		}
	}
}
