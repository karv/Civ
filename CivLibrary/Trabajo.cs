using System;
using ListasExtra;

namespace Civ
{

	/// <summary>
	/// Representa un trabajo en un edificioRAW
	/// </summary>
	public class TrabajoRAW
	{
		//TODO Hacer todo esto readonly.
		/// <summary>
		/// Nombre
		/// </summary>
		public string Nombre;
		/// <summary>
		/// Recursos consumidos por trabajador*turno (Base)
		/// </summary>
		public ListaPeso<Recurso> EntradaBase = new ListaPeso<Recurso>();
		/// <summary>
		/// Recursos producidos por trabajador*turno (Base)
		/// </summary>
		public ListaPeso<Recurso> SalidaBase = new ListaPeso<Recurso>();
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
				PctProd = Math.Min (PctProd, Almacén [x] / (RAW.EntradaBase [x] * Trabajadores));
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

