using System;
using ListasExtra;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Civ
{

	/// <summary>
	/// Representa un trabajo en un edificioRAW
	/// </summary>
	public class TrabajoRAW
	{
		/// <summary>
		/// Nombre
		/// </summary>
		public string Nombre;

        /// <summary>
        /// Recursos consumidos por trabajador*turno (Base)
        /// </summary>
        public List<Basic.Par<string, float>> EntradaStr = new List<Basic.Par<string, float>>();
        
        [XmlIgnore()]
		/// <summary>
		/// Recursos consumidos por trabajador*turno (Base)
		/// </summary>
		public ListaPeso<Recurso> EntradaBase 
        {
            get
            {
                ListaPeso<Recurso> ret = new ListaPeso<Recurso>();
                foreach (var x in EntradaStr)
                {
                    ret[Global.g_.Data.EncuentraRecurso(x.x)] = x.y;
                }
                return ret;
            }
        }

        /// <summary>
        /// Recursos producidos por trabajador*turno (Base)
        /// </summary>
        public List<Basic.Par<string, float>> SalidaStr = new List<Basic.Par<string, float>>();

        [XmlIgnore()]
		/// <summary>
		/// Recursos producidos por trabajador*turno (Base)
		/// </summary>
		public ListaPeso<Recurso> SalidaBase
        {
            get
            {
                ListaPeso<Recurso> ret = new ListaPeso<Recurso>();
                foreach (var x in SalidaStr)
                {
                    ret[Global.g_.Data.EncuentraRecurso(x.x)] = x.y;
                }
                return ret;
            }
        }

        // Requiere
        /// <summary>
        /// IRequerimientos necesarios para construir.
        /// No se requiere listar al edificio vinculado. Su necesidad es implícita.
        /// </summary>
        public List<String> Requiere = new List<string>();

        /// <summary>
        /// Devuelve la lista de requerimientos
        /// </summary>
        /// <value>El IRequerimiento</value> 
        public List<IRequerimiento> Reqs()
        {
            // TODO: Que funcione, debería revisar la lista de cada Edificio, Ciencias y los demás IRequerimientos
            // y convertirlos a su respectivo objeto. Devolver esa lista.
            throw new NotImplementedException();
            //return null;
        }

        /// <summary>
        /// EdificioRAW vinculado a este trabajo.
        /// </summary>
        public string Edificio;


        
	}

	/// <summary>
	/// Representa una instancia trabajo en una instancia de edificio.
	/// </summary>
	public class Trabajo
	{
		public Trabajo (TrabajoRAW nRAW, Edificio EBase)
		{
			_RAW = nRAW;
			_EdificioBase = EBase;
			_EdificioBase.Trabajos.Add (this);
		}

		Edificio _EdificioBase;
		TrabajoRAW _RAW;

		/// <summary>
		/// Devuelve el edificio base de esta instancia.
		/// </summary>
		/// <value>The edificio base.</value>
		public Edificio EdificioBase {
			get {
				return _EdificioBase;
			}
		}

		/// <summary>
		/// Devuelve la ciudad que posee esta instancia de trabajo.
		/// </summary>
		/// <value>The ciudad dueño.</value>
		public Ciudad CiudadDueño {
			get {
				return EdificioBase.CiudadDueño;
			}
		}

		/// <summary>
		/// Devuelve la civilización que posee este trabajo.
		/// </summary>
		/// <value>The civ dueño.</value>
		public Civilización CivDueño {
			get {
				return CiudadDueño.CivDueño;
			}
		}

		/// <summary>
		/// Devuelve la lista de recursos de la ciudad.
		/// </summary>
		/// <value>The almacén.</value>
		public ListaPeso<Recurso> Almacén {
			get {
				return CiudadDueño.Almacén;
			}
		}



		/// <summary>
		/// Devuelve el tipo de trabajo de esta instancia.
		/// </summary>
		/// <value>The RA.</value>
		public TrabajoRAW RAW {
			get {
				return _RAW;
			}
		}

			//Cosas sobre la ciudad/edificio
		ulong _Trabajadores;

		public ulong Trabajadores {
			get {
				return _Trabajadores;
			}
			set {
				ulong realValue;
			
				_Trabajadores = 0;
				realValue = (ulong)Math.Min (value, EdificioBase.getEspaciosTrabajadoresCiudad);
				_Trabajadores = realValue;
			}
		}

		/// <summary>
		/// Ejecuta un tick de tiempo
		/// </summary>
		public void Tick ()
		{
			float PctProd = 1;
			foreach (var x in RAW.EntradaBase.Keys) {
				PctProd = Math.Min (PctProd, Almacén [x] / (RAW.EntradaBase[x] * Trabajadores));
			}

			// Consumir recursos
			foreach (var x in RAW.EntradaBase.Keys) {
				Almacén[x] -= RAW.EntradaBase[x] * Trabajadores * PctProd;
			}


			// Producir recursos
			foreach (var x in RAW.SalidaBase.Keys) {
				Almacén[x] += RAW.SalidaBase[x] * Trabajadores * PctProd;
			}
		}
	}
}