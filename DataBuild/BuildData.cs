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
			var r_Alimento = new Recurso
			{
				Nombre = "Alimento",
				Valor = 2,
				Img = "Comida.jpg"
			};
			data.Recursos.Add (r_Alimento);
			data.RecursoAlimento = r_Alimento;

			var r_Semilla = new Recurso
			{
				Nombre = "Semillas",
				EsEcológico = true
			};
			data.Recursos.Add (r_Semilla);
			#region Construcción
			var r_Martillo = new Recurso
			{
				Nombre = "Martillos",
				Desaparece = true
			};
			data.Recursos.Add (r_Martillo);

			var r_Piedra = new Recurso ("Piedra");
			data.Recursos.Add (r_Piedra);

			var r_Madera = new Recurso ("Madera");
			data.Recursos.Add (r_Madera);
			#endregion

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
			p_Alimento.Salida.Add (new TasaProdConstante
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

			var e_Montañoso = new Ecosistema { Nombre = "Montaña" };
			e_Montañoso.Nombres.Add ("Montaña");
			e_Montañoso.Nombres.Add ("Monte");
			e_Montañoso.Nombres.Add ("Incario");
			e_Montañoso.PropPropiedad.Add (p_Alimento, 0.3f);

			var e_Desierto = new Ecosistema { Nombre = "Desierto" };
			e_Desierto.Nombres.Add ("Sahara");
			e_Desierto.Nombres.Add ("Sahuaro");
			e_Desierto.PropPropiedad.Add (p_Alimento, 0.2f);
			#endregion

			#region Edificios
			var b_Palacio = new EdificioRAW
			{
				EsAutoConstruible = true,
				Nombre = "Palacio",
				MaxPorCivilizacion = 1,
				MaxWorkers = 4
			};
			b_Palacio.ReqRecursos.Add (r_Madera, 50);
			b_Palacio.ReqRecursos.Add (r_Piedra, 30);
			b_Palacio.ReqRecursos.Add (r_Martillo, 15);
			b_Palacio.Salida.Add (r_Martillo, 1);
			data.Edificios.Add (b_Palacio);

			var b_Granja = new EdificioRAW
			{
				MaxWorkers = 20,
				Nombre = "Granaja"
			};
			b_Granja.ReqRecursos.Add (r_Martillo, 5);
			b_Granja.Requiere.Ciencias.Add (c_Agricultura);
			#endregion

			#region Trabajos
			var t_CienciaPalacio = new TrabajoRAW
			{
				Edificio = b_Palacio,
				Nombre = "Ciencia"
			};
			t_CienciaPalacio.SalidaBase [r_c_Ciencia] = 1;
			data.Trabajos.Add (t_CienciaPalacio);

			var t_AlimentoGranja = new TrabajoRAW
			{
				Nombre = "Producir alimento",
				Edificio = b_Granja
			};
			t_AlimentoGranja.EntradaBase.Add (r_Semilla, 1);
			t_AlimentoGranja.SalidaBase.Add (r_Alimento, 1.4f);
			data.Trabajos.Add (t_AlimentoGranja);

			var t_SemillasGranja = new TrabajoRAW
			{
				Nombre = "Producir semillas",
				Edificio = b_Granja
			};
			t_SemillasGranja.EntradaBase.Add (r_Alimento, 0.1f);
			t_SemillasGranja.SalidaBase.Add (r_Semilla, 0.5f);
			#endregion

			#region Unidades
			var u_LanzaPiedras = new UnidadRAWCombate
			{
				Velocidad = 1,
				Peso = 1,
				MaxCarga = 0.3f,
				Defensa = 1,
				CostePoblación = 1,
				Dispersión = 0,
				Nombre = "Lanza piedras",
				Ataque = 1
			};
			u_LanzaPiedras.Flags.Add ("Pie");
			u_LanzaPiedras.Flags.Add ("Humano");
			u_LanzaPiedras.Flags.Add ("Rango");
			u_LanzaPiedras.Reqs [r_Piedra] = 3.5f;
			data.Unidades.Add (u_LanzaPiedras);
			#endregion

			Store.BinarySerialization.WriteToBinaryFile (@"../../../gtk/Data.bin", data);

		}
	}
}