using System;

namespace Civ
{
	public class Recurso
	{
		/// <summary>
		/// Nombre del recurso.
		/// </summary>
		public string Nombre;
		/// <summary>
		/// Initializes a new instance of the <see cref="Civ.Recurso"/> class.
		/// </summary>
		/// <param name="nom">Nombre del recurso.</param>
		public Recurso (string nom)
		{
			Nombre = nom;
		}
	}
}