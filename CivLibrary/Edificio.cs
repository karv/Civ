using System;
using System.Collections.Generic;

namespace Civ
{
	[Serializable()]
	/// <summary>
	/// Representa una clase de edificios. Para sólo lectura.
	/// </summary>
	public class EdificioRAW : IRequerimiento
	{
		public string Nombre;
		public ulong MaxWorkers;

		public EdificioRAW ()
		{
			
		}

		// IRequerieminto
		bool Civ.IRequerimiento.LoSatisface (Ciudad C){
			return C.ExisteEdificio(this);
		}

			// Construcción
		/// <summary>
		/// Recursos necesarios para construir.
		/// </summary>
		public List<String> Requiere = new List<string>();

			// Requiere
		/// <summary>
		/// Lista de nombres de sus IRequerimientos.
		/// </summary>
		//public ListasExtra.ListaPeso<string> Requiere = new ListasExtra.ListaPeso<string> ();
		//public List<string> Requiere = new List<string>();
		public List<Basic.Par<string, float>> ReqRecursos = new List<Basic.Par<string, float>>();

		// TODO: Mover esta propiedad a la clase que lo necesite.

		/// <summary>
		/// Devuelve la lista de requerimientos
		/// </summary>
		/// <value>El IRequerimiento</value> 
		public List<IRequerimiento> Reqs ()
		{
			// TODO: Que funcione, debería revisar la lista de cada Edificio, Ciencias y los demás IRequerimientos
			// y convertirlos a su respectivo objeto.
			return null;
		}

	}
	/// <summary>
	/// Representa una instancia de edificio en una ciudad.
	/// </summary>
	public class Edificio
	{
        /// <summary>
        /// Devuelve el nombre del (RAW del) edificio.
        /// </summary>
        public string Nombre
        {
            get
            {
                return RAW.Nombre;
            }

        }


		public override string ToString ()
		{
			return CiudadDueño.Nombre + " - " + RAW.Nombre;
		}
		/// <summary>
		/// El RAW del edificio.
		/// </summary>
		public readonly EdificioRAW RAW;
		Ciudad _Ciudad;

		public Edificio (EdificioRAW nRAW)
		{
			RAW = nRAW;
		}

		public Edificio (EdificioRAW nRAW, Ciudad nCiudad):this(nRAW)
		{
			_Ciudad = nCiudad;
			_Ciudad.Edificios.Add (this);
		}

		/// <summary>
		/// Devuelve o establece la ciudad a la que pertenece este edificio.
		/// </summary>
		/// <value></value>
		public Ciudad CiudadDueño {
			get {
				return _Ciudad;
			}
		}

		System.Collections.Generic.List<Trabajo> _Trabajo = new System.Collections.Generic.List<Trabajo>();

		/// <summary>
		/// Devuelve la lista de instancias de trabajo de este edificio
		/// </summary>
		/// <value>The _ trabajo.</value>
		public System.Collections.Generic.List<Trabajo> Trabajos {
			get {
				return _Trabajo;
			}
		}

		/// <summary>
		/// Devuelve el número de trabajadores ocupados en este edificio.
		/// </summary>
		/// <value>The get trabajadores.</value>
		public ulong getTrabajadores
		{
			get
			{
				ulong ret = 0;
				foreach (var x in _Trabajo) {
					ret += x.Trabajadores;
				}
				return ret;
			}
		}

		/// <summary>
		/// Devuelve en número de espacios para trabajadores restantes en este edificio.
		/// Ignora el estado de la ciudad.
		/// </summary>
		/// <value>The get espacios trabajadores.</value>
		public ulong getEspaciosTrabajadores
		{
			get
			{
				return RAW.MaxWorkers - getTrabajadores;
			}
		}

		/// <summary>
		/// Devuelve en número de espacios para trabajadores restantes en este edificio.
		/// Tomando en cuenta el estado de la ciudad.
		/// </summary>
		/// <value>The get espacios trabajadores.</value>
		public ulong getEspaciosTrabajadoresCiudad 
		{
			get 
			{
				return (ulong)Math.Min (getEspaciosTrabajadores, CiudadDueño.getTrabajadoresDesocupados);
			}
		}

			// Trabajos
		/// <summary>
		/// Devuelve o establece el número de trabajadores en un trabajo
		/// </summary>
		/// <param name="Trab">El trabajo</param>
		public ulong this [Trabajo Trab]
		{
			get {
				if (Trabajos.Contains (Trab)) {
					return Trab.Trabajadores;					
				} else
					return 0;
			}
			set {
				if (Trabajos.Contains (Trab)) {
					Trab.Trabajadores = value;
				}
			}
		}
		/// <summary>
		/// Devuelve la instancia de trabajo de un RAW de trabajo.
		/// Si no existe, la crea.
		/// </summary>
		/// <param name="Trab">El RAW del trabajo.</param>
		public Trabajo this [TrabajoRAW Trab]
		{
			get
			{
				return getInstanciaTrabajo (Trab);
			}
		}
		/// <summary>
		/// Devuelve la instancia de trabajo de un RAW de trabajo.
		/// Si no existe, la crea.
		/// </summary>
		/// <returns>The instancia trabajo.</returns>
		/// <param name="Trab">El RAW de trabajo.</param>
		public Trabajo getInstanciaTrabajo (TrabajoRAW Trab)
		{
			foreach (var x in Trabajos) {
				if (x.RAW == Trab)
					return x;
			}
			return new Trabajo (Trab, this);
		}

		/// <summary>
		/// Produce un tick productivo hereditario.
		/// </summary>
		public void Tick () {
			foreach (var x in Trabajos) {
				x.Tick ();
			}
		}
	}
}