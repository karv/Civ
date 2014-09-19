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
	}
}

