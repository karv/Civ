using System;
using System.Collections.Generic;

namespace Civ
{
	public class Civilización
	{
		/// <summary>
		/// Nombre de la <see cref="Str.Civilización"/>.
		/// </summary>
		public string Nombre;

		/// <summary>
		/// Lista de ciudades.
		/// </summary>
		List<Ciudad> Ciudades = new List<Ciudad> ();

		/// <summary>
		/// Devuelve la lista de ciudades que pertenecen a esta <see cref="Civ.Civilización"/>.
		/// </summary>
		/// <value>The get ciudades.</value>
		public List<Ciudad> getCiudades {
			get {
				return Ciudades;
			}
		}
		/// <summary>
		/// Agrega una ciudad a esta civ.
		/// </summary>
		/// <param name="C">C.</param>
		public void addCiudad(Ciudad C)
		{
			if (C.CivDueño != this)
				C.CivDueño = this;
		}

		/// <summary>
		/// Agrega una nueva ciudad a esta civ.
		/// </summary>
		/// <returns>Devuelve la ciudad que se agregó.</returns>
		/// <param name="Nom">Nombre de la ciudad.</param>
		public Ciudad addCiudad (string Nom)
		{
			Ciudad C = new Ciudad (Nom, this);
			return C;
		}

			// Ticks
		public void doTick()
		{
			foreach (var x in Ciudades) {
				x.FullTick ();
			}

				// Las ciencias.
			List<Ciencia> Investigado = new List<Ciencia> ();
			foreach (var x in Investigando.Keys) {
				Recurso Rec = Global.g_.Data.EncuentraRecurso (x.RecursoReq);
				Investigando [x] += ObtenerGlobalRecurso (Rec);

				// Si Tiene lo suficiente para terminar investigación
				if (Investigando[x] >= x.CantidadReq) {
					Investigado.Add (x);
				}

				// TODO: Que funcione así: Para cada recurso científico, revisa qué ciencias abiertas lo necesitan, listarlos, y dividir la producción de
				// tal ciencia, aleatoriamente (con Random.Divide) sobra cada uno de éstas.

			}
			foreach (var x in Investigado) {
				Avances.Add(x);
				Investigando.Data.Remove (x);
			}
		}
			// Avances
		public List<Ciencia> Avances = new List<Ciencia>();
		public ListasExtra.ListaPeso<Ciencia> Investigando = new ListasExtra.ListaPeso<Ciencia>();

			// Economía
		/// <summary>
		/// Devuelve la cantidad que existe en la civilización de un cierto recurso.
		/// </summary>
		/// <returns>Devuelve la suma de la cantidad que existe de algún recurso sobre cada ciudad.</returns>
		/// <param name="R">Recurso que se quiere contar</param>
		public float ObtenerGlobalRecurso(Recurso R)
		{
			float ret = 0;
			foreach (var x in Ciudades) {
				ret += x.Almacén [R];
			}
			return ret;
		}
	}
}

