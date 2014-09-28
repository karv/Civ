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
		public override string ToString ()
		{
			return string.Format ("{0}: {1}//{2}//{3}", Nombre, getPoblaciónPreProductiva, PoblaciónProductiva, getPoblaciónPostProductiva);
		}

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
		public static Recurso RecursoAlimento {
			get {
				return Global.g_.Data.RecAlimento;
			}
		}
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
		float _PoblaciónProductiva = 10f;
		float _PoblaciónPreProductiva = 0;
		float _PoblaciónPostProductiva = 0;


		/// <summary>
		/// Devuelve la población real y total de la ciudad.
		/// </summary>
		/// <value>The get real población.</value>
		public float getRealPoblación {
			get {
				return _PoblaciónProductiva + _PoblaciónPreProductiva+ _PoblaciónPostProductiva;
			}
		}
		/// <summary>
		/// Devuelve la población de la ciudad.
		/// </summary>
		/// <value>The get poplación.</value>
		public ulong getPoblación {
			get {
				return PoblaciónProductiva + getPoblaciónPreProductiva + getPoblaciónPostProductiva;
			}
		}

		/// <summary>
		/// Devuelve la población productiva.
		/// </summary>
		/// <value></value>
		public ulong PoblaciónProductiva {
			get {
				return (ulong)Math.Floor (_PoblaciónProductiva);
			}
			set	{
				_PoblaciónProductiva = value;
			}
		}

		/// <summary>
		/// Devuelve la población pre productiva.
		/// </summary>
		/// <value></value>
		public ulong getPoblaciónPreProductiva {
			get {
				return (ulong)Math.Floor (_PoblaciónPreProductiva);
			}
		}

		/// <summary>
		/// Devuelve la población post productiva.
		/// </summary>
		/// <value></value>
		public ulong getPoblaciónPostProductiva {
			get {
				return (ulong)Math.Floor (_PoblaciónPostProductiva);
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
				Crecimiento[1] -= PoblaciónProductiva * pctMuerte;
				Crecimiento[2] -= getPoblaciónPostProductiva * pctMuerte;
			}

			//Crecimiento poblacional
				//Infantil a productivo.
			float Desarrollo = TasaDesarrolloBase * getPoblaciónPreProductiva;
			Crecimiento [0] -= Desarrollo;
			Crecimiento [1] += Desarrollo;
				//Productivo a viejo
			float Envejecer = TasaVejezBase * PoblaciónProductiva;
			Crecimiento [1] -= Envejecer;
			Crecimiento [2] += Envejecer;
				//Nuevos infantes
			float Natalidad = TasaNatalidadBase * PoblaciónProductiva;
			Crecimiento [0] += Natalidad;
				//Mortalidad
			Crecimiento [0] -= getPoblaciónPreProductiva * TasaMortalidadInfantilBase;
			Crecimiento [1] -= PoblaciónProductiva * TasaMortalidadProductivaBase;
			Crecimiento [2] -= getPoblaciónPostProductiva * TasaMortalidadVejezBase;

			// Aplicar cambios.
			// TODO: Un crecimiento negativo en el sector productivo causaría problemas con los trabajos. Arreglarlo.
            // Agregar una propiedad a los trabajos, que controle su prioridad, los de menor prioridad pierden trabajadores en este caso.
            // Los de mayor prioridad reclutan trabajadores en descanso. (¿opcional?)
            
			_PoblaciónPreProductiva = Math.Max (_PoblaciónPreProductiva + Crecimiento [0], 0);
			_PoblaciónProductiva = Math.Max (_PoblaciónProductiva + Crecimiento [1], 0);
			_PoblaciónPostProductiva = Math.Max (_PoblaciónPostProductiva + Crecimiento [2], 0);
		}
			// Tick
		/// <summary>
		/// Da un tick hereditario.
		/// </summary>
		public void Tick (){
			foreach (var x in Edificios) {
				x.Tick ();
			}
		}

		/// <summary>
		/// Ejecuta ambos: Tick () y PopTick ().
		/// En ese orden.
		/// </summary>
		public void FullTick (){
			Tick ();
			PopTick ();
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
		public float AlimentoAlmacén {
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

		public bool ExisteEdificio (EdificioRAW Edif)
		{
			foreach (var x in Edificios) {
				if (x.RAW == Edif)
					return true;
			}
			return false;
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
				return PoblaciónProductiva - getNumTrabajadores;
			}
		}
	}
}

