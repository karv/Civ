using System;

namespace Civ
{
	[Serializable()]
	public class Recurso
	{
		public override string ToString ()
		{
			return Nombre;
		}
		/// <summary>
		/// Desaparece al final del turno.
		/// </summary>
		public bool Desaparece;
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

		public Recurso ()
		{
			
		}
	}
}