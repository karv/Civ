using System;
using ListasExtra;
using System.Collections.Generic;

namespace Civ
{
	/// <summary>
	/// Representa una instancia de ciudad.
	/// </summary>
	public class Ciudad
	{
		public string Nombre;
		Civilización _CivDueño;
		/// <summary>
		/// Devuelve o establece la civilización a la cual pertecene esta ciudad.
		/// </summary>
		/// <value>The civ dueño.</value>
		public Civilización CivDueño {
			get {
				return _CivDueño;
			}
			set	{
				if (_CivDueño != null) _CivDueño.getCiudades.Remove (this);
				_CivDueño = value;
				_CivDueño.getCiudades.Add (this);
			}
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Civ.Ciudad"/> class.
		/// </summary>
		/// <param name="Nombre">Nombre de la ciudad</param>
		/// <param name="Dueño">Civ a la que pertenece esta ciudad</param>
		public Ciudad (string Nom, Civilización Dueño)
		{
			Nombre = Nom;
			CivDueño = Dueño;
		}

			//Población y crecimiento.

		/// <summary>
		/// Recurso que será el alimento
		/// </summary>
		public static Recurso RecursoAlimento;
		/// <summary>
		/// Número de infantes que nacen por (PoblaciónProductiva*Tick) Base.
		/// </summary>
		public static float TasaNatalidadBase = 0.2f;
		/// <summary>
		/// Probabilidad base de un infante arbitrario de morir en cada tick.
		/// </summary>
		public static float TasaMortalidadInfantilBase = 0.01f;
		/// <summary>
		/// Probabilidad base de un habitante productivo arbitrario de morir en cada tick.
		/// </summary>
		public static float TasaMortalidadProductivaBase = 0.02f;
		/// <summary>
		/// Probabilidad base de un adulto de la tercera edad arbitrario de morir en cada tick.
		/// </summary>
		public static float TasaMortalidadVejezBase = 0.1f;
		/// <summary>
		/// Probabilidad de que un infante se convierta en productivo
		/// </summary>
		public static float TasaDesarrolloBase = 0.2f;
		/// <summary>
		/// Probabilidad de que un Productivo envejezca
		/// </summary>
		public static float TasaVejezBase = 0.05f;
		/// <summary>
		/// Consumo base de alimento por ciudadano.
		/// </summary>
		public static float ConsumoAlimentoPorCiudadanoBase = 1f;

				//Población
		float PoblaciónProductiva = 10f;
		float PoblaciónPreProductiva = 0;
		float PoblaciónPostProductiva = 0;


		/// <summary>
		/// Devuelve la población real y total de la ciudad.
		/// </summary>
		/// <value>The get real población.</value>
		public float getRealPoblación {
			get {
				return PoblaciónProductiva + PoblaciónPreProductiva+ PoblaciónPostProductiva;
			}
		}
		/// <summary>
		/// Devuelve la población de la ciudad.
		/// </summary>
		/// <value>The get poplación.</value>
		public ulong getPoblación {
			get {
				return getPoblaciónProductiva + getPoblaciónPreProductiva + getPoblaciónPostProductiva;
			}
		}

		/// <summary>
		/// Devuelve la población productiva.
		/// </summary>
		/// <value></value>
		public ulong getPoblaciónProductiva {
			get {
				return (ulong)Math.Floor (PoblaciónProductiva);
			}
		}

		/// <summary>
		/// Devuelve la población pre productiva.
		/// </summary>
		/// <value></value>
		public ulong getPoblaciónPreProductiva {
			get {
				return (ulong)Math.Floor (PoblaciónPreProductiva);
			}
		}

		/// <summary>
		/// Devuelve la población post productiva.
		/// </summary>
		/// <value></value>
		public ulong getPoblaciónPostProductiva {
			get {
				return (ulong)Math.Floor (PoblaciónPostProductiva);
			}
		}

				//Tick poblacional
		public void PopTick()
		{
			//Crecimiento prometido por sector de edad.
			float[] Crecimiento = new float[3];
			float Consumo = getPoblación * ConsumoAlimentoPorCiudadanoBase;
			//Que coman
				//Si tienen qué comer
			if (Consumo <=  AlimentoAlmacén) {
				AlimentoAlmacén = AlimentoAlmacén - Consumo;
			}
			else {
				//El porcentage de muertes
				float pctMuerte = 1 - (AlimentoAlmacén / getPoblación);
				AlimentoAlmacén = 0;
				//Promesas de muerte por sector.
				Crecimiento[0] -= getPoblaciónPreProductiva * pctMuerte;
				Crecimiento[1] -= getPoblaciónProductiva * pctMuerte;
				Crecimiento[2] -= getPoblaciónPostProductiva * pctMuerte;
			}

			//Crecimiento poblacional
				//Infantil a productivo.
			float Desarrollo = TasaDesarrolloBase * getPoblaciónPreProductiva;
			Crecimiento [0] -= Desarrollo;
			Crecimiento [1] += Desarrollo;
				//Productivo a viejo
			float Envejecer = TasaVejezBase * getPoblaciónProductiva;
			Crecimiento [1] -= Envejecer;
			Crecimiento [2] += Envejecer;
				//Nuevos infantes
			float Natalidad = TasaNatalidadBase * getPoblaciónProductiva;
			Crecimiento [0] += Natalidad;
				//Mortalidad
			Crecimiento [0] -= getPoblaciónPreProductiva * TasaMortalidadInfantilBase;
			Crecimiento [1] -= getPoblaciónProductiva * TasaMortalidadProductivaBase;
			Crecimiento [2] -= getPoblaciónPostProductiva * TasaMortalidadVejezBase;

			//Aplicar cambios.
			//TODO: Un crecimiento negativo en el sector productivo causaría problemas con los trabajos. Arreglarlo.
			PoblaciónPreProductiva = Math.Max (PoblaciónPreProductiva + Crecimiento [0], 0);
			PoblaciónPreProductiva = Math.Max (PoblaciónProductiva + Crecimiento [1], 0);
			PoblaciónPreProductiva = Math.Max (PoblaciónPostProductiva + Crecimiento [2], 0);

			//TODO: Probar el popTick.
		}

			//Almacén
		//TODO Hacer una clase que controle bien esto; luego veo.
		/// <summary>
		/// Almacén de recursos.
		/// </summary>
		public ListaPeso<Recurso> Almacén = new ListaPeso<Recurso>();
		/// <summary>
		/// Devuelve el alimento existente en la ciudad.
		/// </summary>
		/// <value>The alimento almacén.</value>
		float AlimentoAlmacén {
			get {
				return Almacén [RecursoAlimento];
			}
			set {
				Almacén [RecursoAlimento] = value;
			}

		}

			//Edificios
		System.Collections.Generic.List <Edificio> _Edif = new System.Collections.Generic.List<Edificio>();
		/// <summary>
		/// Devuelve la lista de instancias de edicio de la ciudad.
		/// </summary>
		/// <value></value>
		public List <Edificio> Edificios {
			get {
				return _Edif;
			}
		}

		public void AgregarEdificioExistente(Edificio Edif)
		{

		}

		/// <summary>
		/// Agrega una instancia de edicifio a la ciudad.
		/// </summary>
		/// <returns>La instancia de edificio que se agregó.</returns>
		/// <param name="Edif">RAW dek edificio a agregar.</param>
		public Edificio AgregaEdificio(EdificioRAW Edif)
		{
			Edificio ret = new Edificio (Edif, this);

			return ret;
		}

			// Workers
		/// <summary>
		/// Devuelve en número de trabajadores ocupados en algún edificio.
		/// </summary>
		/// <value>The get población ocupada.</value>
		public ulong getNumTrabajadores
		{
			get
			{
				ulong ret = 0;
				foreach (var x in Edificios) {
					ret += x.getTrabajadores;
				}
				return ret;
			}
		}
		public ulong getTrabajadoresDesocupados
		{
			get
			{
				return getPoblaciónProductiva - getNumTrabajadores;
			}
		}
	}
}

