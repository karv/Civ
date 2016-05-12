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
			#region Ecológicos
			var r_Semilla = new Recurso
			{
				Nombre = "Semillas",
				EsEcológico = true,
				Valor = 0.1f
			};
			data.Recursos.Add (r_Semilla);

			var r_Bestias = new Recurso
			{
				Nombre = "Bestias de casa",
				EsEcológico = true,
				Valor = 0.01f
			};
			data.Recursos.Add (r_Bestias);

			var r_Frutas = new Recurso
			{
				Nombre = "Frutas",
				Valor = 0.01f,
				EsEcológico = true
			};
			data.Recursos.Add (r_Frutas);

			var r_Árbol = new Recurso
			{
				Nombre = "Árboles",
				EsEcológico = true
			};
			data.Recursos.Add (r_Árbol);

			var r_PerroSalvage = new Recurso
			{
				Nombre = "Perro salvage",
				EsEcológico = true
			};
			data.Recursos.Add (r_PerroSalvage);

			#endregion
		
			#region Comunes
			var r_Alimento = new Recurso
			{
				Nombre = "Alimento",
				Valor = 2,
				Img = "Comida.jpg"
			};
			data.Recursos.Add (r_Alimento);
			data.RecursoAlimento = r_Alimento;

			var r_PerroDomesticado = new Recurso
			{
				Valor = 1.3f,
				Nombre = "Perro"
			};
			data.Recursos.Add (r_PerroDomesticado);

			#endregion

			#region Construcción
			var r_Martillo = new Recurso
			{
				Nombre = "Martillos",
				Desaparece = true
			};
			data.Recursos.Add (r_Martillo);

			var r_Piedra = new Recurso ("Piedra")
			{
				Valor = 1.1f
			};
			data.Recursos.Add (r_Piedra);

			var r_Madera = new Recurso ("Madera")
			{
				Valor = 1
			};
			data.Recursos.Add (r_Madera);
			#endregion

			#region Científicos
			var r_c_Ciencia = new Recurso ("Ciencia")
			{
				Desaparece = true,
				EsCientifico = true,
				EsGlobal = true,
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
				Nombre = "Palos de madera",
				Valor = 5
			};
			data.Recursos.Add (r_m_Palos);

			var r_m_PerroGuerra = new Recurso
			{
				Nombre = "Perro de guerra",
				Valor = 4.1f
			};
			data.Recursos.Add (r_m_PerroGuerra);
			#endregion
			#endregion

			#region Ciencia
			var c_Caza = new Ciencia { Nombre = "Caza" };
			c_Caza.Reqs.Recursos.Add (r_c_Ciencia, 5);
			data.Ciencias.Add (c_Caza);

			var c_Recolección = new Ciencia { Nombre = "Recolección" };
			c_Recolección.Reqs.Recursos.Add (r_c_Ciencia, 5);
			data.Ciencias.Add (c_Recolección);

			var c_Agricultura = new Ciencia  { Nombre = "Agricultura" };
			c_Agricultura.Reqs.Recursos.Add (r_c_Ciencia, 10);
			c_Agricultura.Reqs.Ciencias.Add (c_Recolección);
			data.Ciencias.Add (c_Agricultura);

			var c_FabricarPalos = new Ciencia { Nombre = "Fabricación de palos" };
			c_FabricarPalos.Reqs.Recursos.Add (r_c_Cacería, 5);
			c_FabricarPalos.Reqs.Ciencias.Add (c_Caza);
			data.Ciencias.Add (c_FabricarPalos);

			var c_Lenguaje = new Ciencia { Nombre = "Lenguaje" };
			c_Lenguaje.Reqs.Ciencias.Add (c_Recolección);
			c_Lenguaje.Reqs.Recursos.Add (r_c_Ciencia, 15);
			data.Ciencias.Add (c_Lenguaje);

			var c_DomesticaciónPerro = new Ciencia { Nombre = "Domesticación del perro" };
			c_DomesticaciónPerro.Reqs.Ciencias.Add (c_Lenguaje);
			c_DomesticaciónPerro.Reqs.Ciencias.Add (c_Caza);
			c_DomesticaciónPerro.Reqs.Recursos.Add (r_c_Ciencia, 5);
			c_DomesticaciónPerro.Reqs.Recursos.Add (r_c_Cacería, 7);
			data.Ciencias.Add (c_DomesticaciónPerro);
			#endregion

			#region Propiedades
			var p_Bestias = new Propiedad { Nombre = "Bestias de caza" };
			p_Bestias.Salida.Add (new TasaProdExp
			{
				Recurso = r_Bestias,
				CrecimientoBase = 3,
				Max = 5000
			});
			p_Bestias.Iniciales.Add (r_Bestias, 10);

			var p_Frutas = new Propiedad{ Nombre = "Frutas" };
			p_Frutas.Salida.Add (new TasaProdConstante
			{
				Recurso = r_Frutas,
				Crecimiento = 50,
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

			var p_Perros = new Propiedad { Nombre = "Perros" };
			p_Perros.Salida.Add (new TasaProdConstante
			{
				Recurso = r_PerroSalvage,
				Crecimiento = 3,
				Max = 1000
			});
			#endregion

			#region Ecosistemas
			var e_Llanura = new Ecosistema { Nombre = "Llanura" };
			e_Llanura.Nombres.Add ("Planicie");
			e_Llanura.PropPropiedad.Add (p_Bestias, 0.7f);
			e_Llanura.PropPropiedad.Add (p_Frutas, 0.85f);
			e_Llanura.PropPropiedad.Add (p_Arboleda, 0.7f);
			e_Llanura.PropPropiedad.Add (p_minArbol, 1);
			e_Llanura.PropPropiedad.Add (p_Perros, 0.9f);
			data.Ecosistemas.Add (e_Llanura);

			var e_Montañoso = new Ecosistema { Nombre = "Montaña" };
			e_Montañoso.Nombres.Add ("Montaña");
			e_Montañoso.Nombres.Add ("Monte");
			e_Montañoso.Nombres.Add ("Incario");
			e_Montañoso.PropPropiedad.Add (p_Bestias, 0.4f);
			e_Montañoso.PropPropiedad.Add (p_Frutas, 0.4f);
			e_Montañoso.PropPropiedad.Add (p_Arboleda, 0.6f);
			e_Montañoso.PropPropiedad.Add (p_minArbol, 1);
			e_Montañoso.PropPropiedad.Add (p_Perros, 0.3f);
			data.Ecosistemas.Add (e_Montañoso);

			var e_Desierto = new Ecosistema { Nombre = "Desierto" };
			e_Desierto.Nombres.Add ("Sahara");
			e_Desierto.Nombres.Add ("Sahuaro");
			e_Desierto.PropPropiedad.Add (p_Bestias, 0.2f);
			e_Desierto.PropPropiedad.Add (p_Arboleda, 0.3f);
			e_Desierto.PropPropiedad.Add (p_minArbol, 1);
			data.Ecosistemas.Add (e_Desierto);

			var e_bosque = new Ecosistema { Nombre = "Bosque" };
			e_bosque.Nombres.Add ("Amazonas");
			e_bosque.PropPropiedad.Add (p_Bestias, 0.85f);
			e_bosque.PropPropiedad.Add (p_Arboleda, 1f);
			e_bosque.PropPropiedad.Add (p_Frutas, 0.4f);
			e_bosque.PropPropiedad.Add (p_Boscoso, 0.8f);
			e_bosque.PropPropiedad.Add (p_minArbol, 1);
			e_bosque.PropPropiedad.Add (p_Perros, 0.4f);
			data.Ecosistemas.Add (e_bosque);

			var e_selva = new Ecosistema { Nombre = "Selva" };
			e_selva.Nombres.Add ("Selva (?)");
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

			var b_ReuniónCacería = new EdificioRAW
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

			var b_e_ComunicaciónI = new EdificioRAW
			{
				MaxWorkers = 0,
				Nombre = "Comunicación social I",
				EsAutoConstruible = true
			};
			b_e_ComunicaciónI.Salida.Add (r_c_Ciencia, 0.3f);
			b_e_ComunicaciónI.Requiere.Ciencias.Add (c_Lenguaje);
			data.Edificios.Add (b_e_ComunicaciónI);

			var b_EntrenaPerros = new EdificioRAW
			{
				MaxWorkers = 10,
				Nombre = "Casa de domesticación de perros"
			};
			b_EntrenaPerros.Requiere.Ciencias.Add (c_DomesticaciónPerro);
			b_EntrenaPerros.ReqRecursos.Add (r_Piedra, 5);
			b_EntrenaPerros.ReqRecursos.Add (r_Madera, 5);
			b_EntrenaPerros.ReqRecursos.Add (r_Martillo, 3);
			data.Edificios.Add (b_EntrenaPerros);
			#endregion

			#region Trabajos
			var t_CienciaPalacio = new TrabajoRAW
			{
				Edificio = b_Palacio,
				Nombre = "Ciencia"
			};
			t_CienciaPalacio.SalidaBase.Add (r_c_Ciencia, 1);

			var t_AlimentoGranja = new TrabajoRAW
			{
				Nombre = "Producir alimento",
				Edificio = b_Granja
			};
			t_AlimentoGranja.EntradaBase.Add (r_Semilla, 1);
			t_AlimentoGranja.SalidaBase.Add (r_Alimento, 1.4f);

			var t_SemillasGranja = new TrabajoRAW
			{
				Nombre = "Producir semillas",
				Edificio = b_Granja
			};
			t_SemillasGranja.EntradaBase.Add (r_Alimento, 0.1f);
			t_SemillasGranja.SalidaBase.Add (r_Semilla, 0.5f);

			var t_Cazar = new TrabajoRAW
			{
				Nombre = "Cacería",
				Edificio = b_ReuniónCacería
			};
			t_Cazar.EntradaBase.Add (r_Bestias, 1);
			t_Cazar.SalidaBase.Add (r_Alimento, 1.2f);
			t_Cazar.SalidaBase.Add (r_c_Cacería, 1);

			var t_RecolectarMadera = new TrabajoRAW
			{
				Nombre = "Recolectar madera",
				Edificio = b_RecolRecursos
			};
			t_RecolectarMadera.SalidaBase.Add (r_Madera, 2);
			t_RecolectarMadera.EntradaBase.Add (r_Árbol, 1);

			var t_RecolectarPiedra = new TrabajoRAW
			{
				Nombre = "Recolectar piedra",
				Edificio = b_RecolRecursos
			};
			t_RecolectarPiedra.SalidaBase.Add (r_Piedra, 1.5f);

			var t_RecolectarFruta = new TrabajoRAW
			{
				Nombre = "Recolectar fruta",
				Edificio = b_RecolFrutas
			};
			t_RecolectarFruta.SalidaBase.Add (r_Alimento, 3);
			t_RecolectarFruta.SalidaBase.Add (r_c_Cacería, 1);
			t_RecolectarFruta.EntradaBase.Add (r_Frutas, 3);

			var t_FabricarPalos = new TrabajoRAW
			{
				Nombre = "Construir palos",
				Edificio = b_FábricaArmasI
			};
			t_FabricarPalos.EntradaBase.Add (r_Madera, 0.1f);
			t_FabricarPalos.SalidaBase.Add (r_m_Palos, 0.2f);
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
				Ataque = 1,
				Puntuación = 30
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
				Ataque = 1.5f,
				Puntuación = 40
			};
			u_GuerreroPalo.Flags.Add ("Pie");
			u_GuerreroPalo.Flags.Add ("Humano");
			u_GuerreroPalo.Flags.Add ("Melee");
			u_GuerreroPalo.Reqs.Add (r_m_Palos, 1);
			data.Unidades.Add (u_GuerreroPalo);

			var u_perro = new UnidadRAWCombate
			{
				Velocidad = 1.6f,
				Peso = 0.7f,
				MaxCarga = 0,
				Defensa = 0.6f,
				CostePoblación = 0,
				Dispersión = 0.2f,
				Nombre = "Perro de guerra",
				Ataque = 1.4f,
				Puntuación = 37
			};
			u_perro.Flags.Add ("Pie");
			u_perro.Flags.Add ("Animal");
			u_perro.Flags.Add ("Melee");
			u_perro.Mods.Add ("Humano", 1.1f);
			u_perro.Reqs.Add (r_m_PerroGuerra, 1);
			u_perro.ReqCiencia = c_DomesticaciónPerro;
			data.Unidades.Add (u_perro);
			#endregion

			// Pasar el bin al proyecto principal
			Store.BinarySerialization.WriteToBinaryFile (@"../../../gtk/Data.bin", data);

			// Probar

			// Dejar un bin para probar su lectura
			Store.BinarySerialization.WriteToBinaryFile ("test.bin", data);

			var r = Store.BinarySerialization.ReadFromBinaryFile<GameData> ("test.bin");
			if (data.Recursos.Count != r.Recursos.Count)
				System.Console.WriteLine ("Error");
			if (data.Ciencias.Count != r.Ciencias.Count)
				System.Console.WriteLine ("Error");
			if (data.Ecosistemas.Count != r.Ecosistemas.Count)
				System.Console.WriteLine ("Error");
			if (data.Edificios.Count != r.Edificios.Count)
				System.Console.WriteLine ("Error");
			if (data.Unidades.Count != r.Unidades.Count)
				System.Console.WriteLine ("Error");
			if (data.RecursoAlimento.Nombre != r.RecursoAlimento.Nombre)
				System.Console.WriteLine ("Error");

			// Eliminar el archivo de pruebas
			System.IO.File.Delete ("test.bin");
		}
	}
}