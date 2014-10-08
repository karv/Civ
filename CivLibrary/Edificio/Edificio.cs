using System;
using System.Collections.Generic;

namespace Civ
{
	/// <summary>
	/// Representa una instancia de edificio en una ciudad.
	/// </summary>
	public partial class Edificio
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
			// TODO: Debe tirar error si ya existe esta clase de edificio en la ciudad.
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