using Civ.RAW;
using Civ.Global;
using System.Security.Cryptography.X509Certificates;

namespace DataBuild
{
	public static class BuildData
	{
		public static void Main ()
		{
			var data = new GameData ();
			var r_Alimento = new Recurso ("Alimento");
			r_Alimento.Valor = 2;
			data.Recursos.Add (r_Alimento);
			data.RecursoAlimento = r_Alimento;

			Store.BinarySerialization.WriteToBinaryFile ("Data.bin", data);
		}
	}
}