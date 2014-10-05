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
            return Nombre;
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






            // Tick
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

		/// <summary>
		/// Da un tick hereditario.
		/// </summary>
		public void Tick (){
			foreach (var x in Edificios) {
				x.Tick ();
			}
            // Construir edificio.
            EdifConstruyendo.AbsorbeRecursos();
            if (EdifConstruyendo.EstáCompletado()) EdifConstruyendo.Completar();
            // Desaparecen algunos recursos
            foreach (Recurso x in Almacén.Keys)
            {
                if (x.Desaparece)
                {
                    Almacén[x] = 0;
                }
            }
		}

		/// <summary>
		/// Ejecuta ambos: Tick () y PopTick ().
		/// En ese orden.
		/// </summary>
		public void FullTick (){
            PopTick();
            Tick();
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

        /// <summary>
        /// Revisa si existe una clase de edificio en esta ciudad.
        /// </summary>
        /// <param name="Edif">La clase de edificio buscada</param>
        /// <returns><c>true</c> si existe el edificio, <c>false</c> si no.</returns>
		public bool ExisteEdificio (EdificioRAW Edif)
		{
			foreach (var x in Edificios) {
				if (x.RAW == Edif)
					return true;
			}
			return false;
		}

        /// <summary>
        /// Devuelve el edificio en la ciudad con un nombre específico.
        /// </summary>
        /// <param name="Ed">RAW del edificio.</param>
        /// <returns>La instancia de edificio en la ciudad; si no existe devuelve <c>null</c>.</returns>
        public Edificio EncuentraInstanciaEdificio(EdificioRAW Ed)
        {
            // TODO: Probar
            foreach (Edificio x in Edificios)
            {
                if (x.RAW == Ed)
                {
                    return x;
                }
            }
            return null;
        }

        /// <summary>
        /// Devuelve el edificio en la ciudad con un nombre específico.
        /// </summary>
        /// <param name="Ed">Nombre del edificio.</param>
        /// <returns>La instancia de edificio en la ciudad; si no existe devuelve <c>null</c>.</returns>
        public Edificio EncuentraInstanciaEdificio(string Ed)
        {
            if (!Global.g_.Data.ExisteEdificio(Ed)) return null;       //Si no existe el edificio, devuelve nulo
            EdificioRAW Edif = Global.g_.Data.EncuentraEdificio(Ed);   //La clase de edificio que puede contener este trabajo.
            return EncuentraInstanciaEdificio(Edif);
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

        /// <summary>
        /// Devuelve la lista de edificios contruibles por esta ciudad; los que se pueden hacer yno estpan hechos.
        /// </summary>
        /// <returns></returns>
        public List<EdificioRAW> Construibles()
        {
            List<EdificioRAW> ret = new List<EdificioRAW>();
            foreach (EdificioRAW x in Global.g_.Data.Edificios)
            {
                if (!ExisteEdificio(x) && SatisfaceReq(x.Reqs()))
                {
                    ret.Add(x);
                }
            }
            return ret;
        }

                // Edificio en construcción.
        /// <summary>
        /// Representa un edificio en construcción.
        /// </summary>
        internal class EdificioConstruyendo
        {
            public EdificioRAW RAW;

            /// <summary>
            /// Recursos ya usados en el edificio.
            /// </summary>
            public ListaPeso<Recurso> RecursosAcumulados = new ListaPeso<Recurso>();

            /// <summary>
            /// Devuelve la función de recursos faltantes.
            /// </summary>
            public ListaPeso<Recurso> RecursosRestantes
            {
                get
                {
                    ListaPeso<Recurso> ret = new ListaPeso<Recurso>();
                    foreach (var x in RAW.ReqRecursos)
                    {
                        Recurso r = Global.g_.Data.EncuentraRecurso(x.x);
                        ret[r] = x.y - RecursosAcumulados[r];
                    }
                    return ret;
                }
            }

            public Ciudad CiudadDueño;

            /// <summary>
            /// Crea una instancia.
            /// </summary>
            /// <param name="EdifRAW">El RAW de este edificio.</param>
            /// <param name="C">Ciudad dueño.</param>
            public EdificioConstruyendo(EdificioRAW EdifRAW, Ciudad C)
            {
                RAW = EdifRAW;
                CiudadDueño = C;
            }

            /// <summary>
            /// Absorbe los recursos de la ciudad para su construcción.
            /// </summary>
            public void AbsorbeRecursos()
            {
                foreach (Recurso x in RecursosRestantes.Keys)
                {
                    float abs = Math.Min(RecursosRestantes[x], CiudadDueño.Almacén[x]);
                    RecursosAcumulados[x] += abs;
                    CiudadDueño.Almacén[x] -= abs;
                }
            }

            /// <summary>
            /// Revisa si este edificio está completado.
            /// </summary>
            /// <returns><c>true</c> si ya no quedan recursos restantes; <c>false</c> en caso contrario.</returns>
            public bool EstáCompletado()
            {
                return RecursosRestantes.Keys.Count == 0;
            }

            /// <summary>
            /// Contruye una instancia de su RAW en la ciudad dueño.
            /// </summary>
            /// <returns>Devuelve su edificio completado.</returns>
            public Edificio Completar()
            {
                return CiudadDueño.AgregaEdificio(RAW);
            }
        }

        /// <summary>
        /// Devuelve o establece El edificio que se está contruyendo, y su progreso.
        /// </summary>
        private EdificioConstruyendo EdifConstruyendo;

        /// <summary>
        /// Devuelve el RAW del edificio que se está contruyendo.
        /// </summary>
        public EdificioRAW RAWConstruyendo
        {
            get
            {
                return EdifConstruyendo == null ? null : EdifConstruyendo.RAW;
            }
            set
            {
                // TODO: ¿Qué hacer con los recursos del edificio anterior? ¿Se pierden? (por ahora sí :3)
                EdifConstruyendo = new EdificioConstruyendo(value, this);
            }
        }




			// Trabajadores
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

        /// <summary>
        /// Devuelve la lista de trabajos que se pueden realizar en una ciudad.
        /// </summary>
        public List<TrabajoRAW> ObtenerListaTrabajosRAW
        {
            get
            {
                List<TrabajoRAW> ret = new List<TrabajoRAW>();
                foreach (var x in Global.g_.Data.Trabajos)
                {
                    // TODO: Revisar todos los trabajos, hacer función que determine si un trabajo es posible.
                    List<IRequerimiento> Req = Basic.Covertidor<string, IRequerimiento>.ConvertirLista(x.Requiere, (z => Global.g_.Data.EncuentraRequerimiento(z)));
                    if (SatisfaceReq(Req) && ExisteEdificio(Global.g_.Data.EncuentraEdificio(x.Edificio))) 
                    {
                        ret.Add(x);
                    }
                }
                return ret;
            }
        }

        /// <summary>
        /// Devuelve la lista de trabajos actuales en esta  <see cref="Civ.Ciudad"/>. 
        /// </summary>
        public List<Trabajo> ObtenerListaTrabajos
        {
            get
            {
                List<Trabajo> ret = new List<Trabajo>();
                foreach (var x in Edificios)
                {
                    foreach (var y in x.Trabajos)
                    {
                        ret.Add(y);
                    }
                }
                return ret;
            }

        }

        /// <summary>
        /// Revisa si esta ciudad satisface un Irequerimiento.
        /// </summary>
        /// <param name="Req">Un requerimiento</param>
        /// <returns>Devuelve <c>true</c> si esta ciudad satisface un Irequerimiento. <c>false</c> en caso contrario.</returns>
        public bool SatisfaceReq(IRequerimiento Req)
        {
            return Req.LoSatisface(this);
        }

        /// <summary>
        /// Revisa si esta ciudad satisface una lista de requerimientos.
        /// </summary>
        /// <param name="Req"></param>
        /// <returns>Devuelve <c>true</c> si esta ciudad satisface todos los Irequerimiento. <c>false</c> en caso contrario.</returns>
        public bool SatisfaceReq(List<IRequerimiento> Req)
        {
            return Req.TrueForAll(x => x.LoSatisface(this));
        }

        /// <summary>
        /// Devuelve la instancia de trabajo en esta ciudad, si existe. Si no, la crea y la devuelve cuando <c>CrearInstancia</c>.
        /// </summary>
        /// <param name="TRAW"></param>
        /// TrabajoRAW que se busca
        /// <param name="CrearInstancia">Si no existe tal instancia y <c>CrearInstancia</c>, la crea; si no, tira excepción.</param>
        /// <returns>Devuelve el trabajo en la ciudad correspondiente a este TrabajoRAW.</returns>
        public Trabajo EncuentraInstanciaTrabajo (TrabajoRAW TRAW)
        {
            // TODO: Probar.

            EdificioRAW Ed = Global.g_.Data.EncuentraEdificio(TRAW.Edificio);   // La clase de edificio que puede contener este trabajo.
            Edificio Edif = EncuentraInstanciaEdificio(Ed); // La instancia del edificio en esta ciudad.
            
            if (Edif == null) return null;    // Devuelve nulo si no existe el edificio donde se trabaja.
            foreach (Trabajo x in ObtenerListaTrabajos)
            {
                if (x.RAW == TRAW) return x;
            }
            return null;
        }

        /// <summary>
        /// Devuelve la instancia de trabajo en esta ciudad, si existe. Si no, la crea y la devuelve cuando <c>CrearInstancia</c>.
        /// </summary>
        /// <param name="TRAW"></param>
        /// Nombre del trabajo que se busca.
        /// <param name="CrearInstancia">Si no existe tal instancia y <c>CrearInstancia</c>, la crea; si no, tira excepción.</param>
        /// <returns>Devuelve el trabajo en la ciudad con el nombre buscado.</returns>
        public Trabajo EncuentraInstanciaTrabajo (string TRAW)
        {
            TrabajoRAW Tr = Global.g_.Data.EncuentraTrabajo(TRAW);
            if (Tr == null) return null;
            return EncuentraInstanciaTrabajo(Tr);
        }

        
        
	}
}

