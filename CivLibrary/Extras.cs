using System;
using System.Collections.Generic;

namespace Civ
{
	public interface IRequerimiento
	{
		/// <summary>
		/// Si una ciudad satisface este requerimiento.
		/// </summary>
		/// <returns><c>true</c>, Si la ciudad <c>C</c> lo satisface , <c>false</c> si no.</returns>
		/// <param name="C">La ciudad que intenta satisfacer este requerimiento.</param>
		bool LoSatisface (Ciudad C);
	}
}

namespace Global
{
	/// <summary>
	/// Representa las opciones del juego.
	/// </summary>
	[Serializable()]
	public class g_Data
	{
		public List<Civ.Ciencia> Ciencias = new List<Civ.Ciencia> ();
		public List<Civ.Recurso> Recursos = new List<Civ.Recurso> ();

		/// <summary>
		/// El recurso que sirve como alimento en una ciudad.
		/// </summary>
		public Civ.Recurso RecursoAlimento;


		/// <summary>
		/// Revisa si existe una ciencia con un nombre específico.
		/// </summary>
		/// <returns><c>true</c>, si existe una ciencia con ese nombre, <c>false</c> otherwise.</returns>
		/// <param name="NombreRecurso">Nombre del recurso.</param>
		public bool ExisteRecurso (string NombreRecurso)
		{
			foreach (var x in Recursos) {
				if (x.Nombre == NombreRecurso)
					return true;
			}
			return false;
		}

		/// <summary>
		/// Revisa si existe una ciencia con un nombre específico.
		/// </summary>
		/// <returns><c>true</c>, si existe una ciencia con ese nombre, <c>false</c> otherwise.</returns>
		/// <param name="NombreCiencia">Nombre ciencia.</param>
		public bool ExisteCiencia (string NombreCiencia)
		{
			foreach (var x in Ciencias) {
				if (x.Nombre == NombreCiencia)
					return true;
			}
			return false;
		}

		/// <summary>
		/// Devuelve el recurso con un nombre específico.
		/// </summary>
		/// <returns>The recurso.</returns>
		/// <param name="nombre">Nombre del recurso a buscar.</param>
		public Civ.Recurso EncuentraRecurso (string nombre)
		{
			foreach (var x in Recursos) {
				if (x.Nombre == nombre) {
					return x;
				}
			}
			return null;
		}

		/// <summary>
		/// Devuelve la ciencia con un nombre específico.
		/// </summary>
		/// <returns>Ciencia.</returns>
		/// <param name="nombre">Nombre de la ciencia a buscar.</param>
		public Civ.Ciencia EncuentraCiencia (string nombre)
		{
			foreach (var x in Ciencias) {
				if (x.Nombre == nombre) {
					return x;
				}
			}
			return null;
		}
	
	}

	/// <summary>
	/// Representa el estado de un juego.
	/// </summary>
	public class g_State
	{

	}
}

namespace Basic
{
	public struct Par <S,T>
	{
		public S x;
		public T y;
	}
}

