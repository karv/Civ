using System;
using ListasExtra;
using System.Collections.Generic;

namespace Civ
{
	/// <summary>
	/// Representa una instancia de ciudad.
	/// </summary>
	public partial class Ciudad
	{
		public override string ToString ()
		{
            return Nombre;
		}

		/// <summary>
		/// Nombre de la ciudad.
		/// </summary>
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
	        
	}
}

