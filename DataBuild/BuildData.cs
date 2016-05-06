using Civ.RAW;
using Civ.Global;
using Civ.Ciencias;
using Civ.Topología;

namespace DataBuild
{
	public static class BuildData
	{
		public static void Main ()
		{
			var data = new GameData ();

			#region Recursos
			var r_Alimento = new Recurso ("Alimento");
			r_Alimento.Valor = 2;
			data.Recursos.Add (r_Alimento);
			data.RecursoAlimento = r_Alimento;

			#region Científicos
			var r_c_Ciencia = new Recurso ("Ciencia")
			{
				Desaparece = true,
				EsCientifico = true,
				EsGlobal = true
			};
			data.Recursos.Add (r_c_Ciencia);

			#endregion

			#endregion

			#region Ciencia
			var c_Agricultura = new Ciencia  { Nombre = "Agricultura" };
			c_Agricultura.Reqs.Recursos [r_c_Ciencia] = 10;
			data.Ciencias.Add (c_Agricultura);
			#endregion

			#region Propiedades
			var p_Alimento = new Propiedad { Nombre = "Alimento" };
			p_Alimento.Salida.Add (new TasaProdConstante ()
			{
				Recurso = r_Alimento,
				Crecimiento = 1,
				Max = 1000
			});
			#endregion
			#region Ecosistemas
			var e_Llanura = new Ecosistema { Nombre = "Llanura" };
			e_Llanura.Nombres.Add ("Planicie");
			e_Llanura.PropPropiedad.Add (p_Alimento, 0.8f);
			data.Ecosistemas.Add (e_Llanura);
			#endregion


			Store.BinarySerialization.WriteToBinaryFile ("Data.bin", data);
		}
	}
}