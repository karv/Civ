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

			#region Comunes
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

			var r_Bestias = new Recurso
			{
				Nombre = "Bestias de casa",
				EsEcológico = true
			};
			data.Recursos.Add (r_Bestias);

			var r_Frutas = new Recurso
			{
				Nombre = "Frutas",
				EsEcológico = true
			};
			data.Recursos.Add (r_Frutas);

			var r_Árbol = new Recurso
			{
				Nombre = "Árboles",
				EsEcológico = true
			};

			#endregion

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

			var r_c_Cacería = new Recurso ("Ciencia: cacería")
			{
				Desaparece = true,
				EsCientifico = true,
				EsGlobal = true
			};
			data.Recursos.Add (r_c_Cacería);
			#endregion

			#region Militar
			var r_m_Palos = new Recurso
			{
				Nombre = "Palos de madera"
			};
			data.Recursos.Add (r_m_Palos);
			#endregion
			#endregion

			#region Ciencia
			var c_Caza = new Ciencia { Nombre = "Caza" };
			c_Caza.Reqs.Recursos.Add (r_c_Ciencia, 5);
			data.Ciencias.Add (c_Caza);

			var c_Recolección = new Ciencia { Nombre = "Recolección" };
			c_Caza.Reqs.Recursos.Add (r_c_Ciencia, 5);
			data.Ciencias.Add (c_Recolección);

			var c_Agricultura = new Ciencia  { Nombre = "Agricultura" };
			c_Agricultura.Reqs.Recursos [r_c_Ciencia] = 10;
			c_Agricultura.Reqs.Ciencias.Add (c_Recolección);
			data.Ciencias.Add (c_Agricultura);

			var c_FabricarPalos = new Ciencia { Nombre = "Fabricación de palos" };
			c_FabricarPalos.Reqs.Recursos.Add (r_c_Cacería, 5);
			c_FabricarPalos.Reqs.Ciencias.Add (c_Caza);
			data.Ciencias.Add (c_FabricarPalos);
			#endregion

			#region Propiedades
			var p_Alimento = new Propiedad { Nombre = "Alimento" };
			p_Alimento.Salida.Add (new TasaProdConstante
			{
				Recurso = r_Alimento,
				Crecimiento = 1,
				Max = 1000
			});

			var p_Bestias = new Propiedad { Nombre = "Bestias de caza" };
			p_Bestias.Salida.Add (new TasaProdExp
			{
				Recurso = r_Bestias,
				CrecimientoBase = 3,
				Max = 5000
			});
			p_Bestias.Iniciales.Add (r_Bestias, 10);

			var p_Frutas = new Propiedad{ Nombre = "Frutas" };
			p_Frutas.Salida.Add (new TasaProdExp
			{
				Recurso = r_Frutas,
				CrecimientoBase = 1.4f,
				Max = 4500
			});
			p_Frutas.Iniciales.Add (r_Frutas, 20);

			var p_Arboleda = new Propiedad{ Nombre = "Arboleda" };
			p_Arboleda.Salida.Add (new TasaProdConstante
			{
				Recurso = r_Árbol,
				Crecimiento = 4,
				Max = 200
			});

			var p_Boscoso = new Propiedad{ Nombre = "Bosque" };
			p_Boscoso.Salida.Add (new TasaProdExp
			{
				Recurso = r_Árbol,
				CrecimientoBase = 2,
				Max = 2500
			});
			p_Boscoso.Iniciales.Add (r_Árbol, 1000);

			var p_minArbol = new Propiedad { Nombre = "Árboles dispersos" };
			p_minArbol.Salida.Add (new TasaProdConstante
			{
				Recurso = r_Árbol,
				Crecimiento = 10,
				Max = 200
			});
			#endregion

			#region Ecosistemas
			var e_Llanura = new Ecosistema { Nombre = "Llanura" };
			e_Llanura.Nombres.Add ("Planicie");
			e_Llanura.PropPropiedad.Add (p_Alimento, 0.8f);
			e_Llanura.PropPropiedad.Add (p_Bestias, 0.7f);
			e_Llanura.PropPropiedad.Add (p_Frutas, 0.85f);
			e_Llanura.PropPropiedad.Add (p_Arboleda, 0.7f);
			e_Llanura.PropPropiedad.Add (p_minArbol, 1);
			data.Ecosistemas.Add (e_Llanura);

			var e_Montañoso = new Ecosistema { Nombre = "Montaña" };
			e_Montañoso.Nombres.Add ("Montaña");
			e_Montañoso.Nombres.Add ("Monte");
			e_Montañoso.Nombres.Add ("Incario");
			e_Montañoso.PropPropiedad.Add (p_Alimento, 0.3f);
			e_Montañoso.PropPropiedad.Add (p_Bestias, 0.4f);
			e_Montañoso.PropPropiedad.Add (p_Frutas, 0.4f);
			e_Montañoso.PropPropiedad.Add (p_Arboleda, 0.6f);
			e_Montañoso.PropPropiedad.Add (p_minArbol, 1);
			data.Ecosistemas.Add (e_Montañoso);

			var e_Desierto = new Ecosistema { Nombre = "Desierto" };
			e_Desierto.Nombres.Add ("Sahara");
			e_Desierto.Nombres.Add ("Sahuaro");
			e_Desierto.PropPropiedad.Add (p_Alimento, 0.2f);
			e_Desierto.PropPropiedad.Add (p_Bestias, 0.2f);
			e_Desierto.PropPropiedad.Add (p_Arboleda, 0.3f);
			e_Desierto.PropPropiedad.Add (p_minArbol, 1);
			data.Ecosistemas.Add (e_Desierto);

			var e_bosque = new Ecosistema { Nombre = "Bosque" };
			e_bosque.Nombres.Add ("Amazonas");
			e_bosque.PropPropiedad.Add (p_Alimento, 0.3f);
			e_bosque.PropPropiedad.Add (p_Bestias, 0.85f);
			e_bosque.PropPropiedad.Add (p_Arboleda, 1f);
			e_bosque.PropPropiedad.Add (p_Frutas, 0.4f);
			e_bosque.PropPropiedad.Add (p_Boscoso, 0.8f);
			e_bosque.PropPropiedad.Add (p_minArbol, 1);
			data.Ecosistemas.Add (e_bosque);

			var e_selva = new Ecosistema { Nombre = "Selva" };
			e_selva.Nombres.Add ("Selva (?)");
			e_selva.PropPropiedad.Add (p_Alimento, 0.5f);
			e_selva.PropPropiedad.Add (p_Bestias, 0.6f);
			e_selva.PropPropiedad.Add (p_Arboleda, 0.7f);
			e_selva.PropPropiedad.Add (p_Frutas, 0.6f);
			e_selva.PropPropiedad.Add (p_Boscoso, 0.15f);
			e_selva.PropPropiedad.Add (p_minArbol, 1);
			data.Ecosistemas.Add (e_selva);
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
			data.Edificios.Add (b_Granja);

			var b_ReuniónCacería = new EdificioRAW ()
			{
				MaxWorkers = 20,
				Nombre = "Casa de cacería"
			};
			b_ReuniónCacería.ReqRecursos.Add (r_Piedra, 10);
			b_ReuniónCacería.ReqRecursos.Add (r_Madera, 5);
			b_ReuniónCacería.ReqRecursos.Add (r_Martillo, 3);
			b_ReuniónCacería.Requiere.Ciencias.Add (c_Caza);
			data.Edificios.Add (b_ReuniónCacería);

			var b_RecolRecursos = new EdificioRAW
			{
				MaxWorkers = 10,
				Nombre = "Reunión de recursos",
				EsAutoConstruible = true
			};
			data.Edificios.Add (b_RecolRecursos);

			var b_RecolFrutas = new EdificioRAW
			{
				MaxWorkers = 10,
				Nombre = "Recolección de frutas"
			};
			b_RecolFrutas.ReqRecursos.Add (r_Piedra, 2);
			b_RecolFrutas.ReqRecursos.Add (r_Madera, 1);
			b_RecolFrutas.ReqRecursos.Add (r_Martillo, 0.5f);
			data.Edificios.Add (b_RecolFrutas);

			var b_FábricaArmasI = new EdificioRAW
			{
				MaxWorkers = 50,
				Nombre = "Fábrica de armas primitivo"
			};
			b_FábricaArmasI.ReqRecursos.Add (r_Piedra, 10);
			b_FábricaArmasI.ReqRecursos.Add (r_Madera, 8);
			b_FábricaArmasI.ReqRecursos.Add (r_Martillo, 3);
			data.Edificios.Add (b_FábricaArmasI);
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
			data.Trabajos.Add (t_SemillasGranja);

			var t_Cazar = new TrabajoRAW
			{
				Nombre = "Cacería",
				Edificio = b_ReuniónCacería
			};
			t_Cazar.EntradaBase.Add (r_Bestias, 1);
			t_Cazar.SalidaBase.Add (r_Alimento, 1.2f);
			t_Cazar.SalidaBase.Add (r_c_Cacería, 1);
			data.Trabajos.Add (t_Cazar);

			var t_RecolectarMadera = new TrabajoRAW
			{
				Nombre = "Recolectar madera",
				Edificio = b_RecolRecursos
			};
			t_RecolectarMadera.SalidaBase.Add (r_Madera, 2);
			t_RecolectarMadera.EntradaBase.Add (r_Árbol, 1);
			data.Trabajos.Add (t_RecolectarMadera);

			var t_RecolectarPiedra = new TrabajoRAW
			{
				Nombre = "Recolectar piedra",
				Edificio = b_RecolRecursos
			};
			t_RecolectarPiedra.SalidaBase.Add (r_Piedra, 1.5f);
			data.Trabajos.Add (t_RecolectarPiedra);

			var t_RecolectarFruta = new TrabajoRAW
			{
				Nombre = "Recolectar fruta",
				Edificio = b_RecolFrutas
			};
			t_RecolectarFruta.SalidaBase.Add (r_Alimento, 2);
			t_RecolectarFruta.EntradaBase.Add (r_Frutas, 2);
			data.Trabajos.Add (t_RecolectarFruta);

			var t_FabricarPalos = new TrabajoRAW
			{
				Nombre = "Construir palos",
				Edificio = b_FábricaArmasI
			};
			t_FabricarPalos.EntradaBase.Add (r_Madera, 0.1f);
			t_FabricarPalos.SalidaBase.Add (r_m_Palos, 0.2f);
			data.Trabajos.Add (t_FabricarPalos);
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

			var u_GuerreroPalo = new UnidadRAWCombate
			{
				Velocidad = 1,
				Peso = 1,
				MaxCarga = 0.2f,
				Defensa = 1.5f,
				CostePoblación = 1,
				Dispersión = 0,
				Nombre = "Guerrero con palo",
				Ataque = 1.5f
			};
			u_GuerreroPalo.Flags.Add ("Pie");
			u_GuerreroPalo.Flags.Add ("Humano");
			u_GuerreroPalo.Flags.Add ("Melee");
			u_GuerreroPalo.Reqs [r_m_Palos] = 1;
			data.Unidades.Add (u_GuerreroPalo);
			#endregion

			Store.BinarySerialization.WriteToBinaryFile (@"../../../gtk/Data.bin", data);
		}
	}
}