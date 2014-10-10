using System;
using System.Collections.Generic;

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
		public List<Civ.EdificioRAW> Edificios = new List<Civ.EdificioRAW>();
		public List<Civ.TrabajoRAW> Trabajos = new List<Civ.TrabajoRAW>();

		/// <summary>
		/// El string del recurso que sirve como alimento en una ciudad.
		/// </summary>
		public string RecursoAlimento;

		/// <summary>
		/// El recurso que sirve como alimento en una ciudad.
		/// </summary>
		public Civ.Recurso RecAlimento
		{
			get
			{
				return EncuentraRecurso (RecursoAlimento);

			}
		}

		/// <summary>
		/// Revisa si existe una edificio con un nombre específico.
		/// </summary>
		/// <returns><c>true</c>, si existe un edificio con ese nombre, <c>false</c> otherwise.</returns>
		/// <param name="NombreRecurso">Nombre del eidficio.</param>
		public bool ExisteEdificio(string NombreEdificio)
		{
			foreach (var x in Edificios)
			{
				if (x.Nombre == NombreEdificio)
					return true;
			}
			return false;
		}

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
		/// Devuelve el edificio con un nombre específico.
		/// </summary>
		/// <returns>The recurso.</returns>
		/// <param name="nombre">Nombre del edificio a buscar.</param>
		public Civ.EdificioRAW EncuentraEdificio(string nombre)
		{
			foreach (var x in Edificios)
			{
				if (x.Nombre == nombre)
				{
					return x;
				}
			}
			return null;
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

		/// <summary>
		/// Devuelve el requerimiento con un id específico.
		/// </summary>
		/// <returns>IRequerimiento.</returns>
		/// <param name="Id">Nombre del IRequerimiento a buscar.</param>
		public Civ.IRequerimiento EncuentraRequerimiento (string Id)
		{
			foreach (var x in Reqs)
			{
				if (x.ObtenerId() == Id)
				{
					return x;
				}
			}
			return null;            
		}

		/// <summary>
		/// Devuelve el trabajo con un nombre específico.
		/// </summary>
		/// <returns>TrabajoRAW.</returns>
		/// <param name="nombre">Nombre del Trabajo a buscar.</param>
		public Civ.TrabajoRAW EncuentraTrabajo(string nombre)
		{
			return Trabajos.Find(x => x.Nombre == nombre);
		}

		/// <summary>
		/// Devuelve un arreglo de recursos que son científicos
		/// </summary>
		/// <returns>The lista recursos científicos.</returns>
		public Civ.Recurso[] ObtenerRecursosCientíficos()
		{
			return Recursos.FindAll (y => y.EsCientífico).ToArray();
		}

		/// <summary>
		/// Devuelve todos los <see cref="Civ.IRequerimiento"/>s.
		/// </summary>
		[System.Xml.Serialization.XmlIgnore()]
		public List<Civ.IRequerimiento> Reqs
		{
			get
			{
				List<Civ.IRequerimiento> ret = new List<Civ.IRequerimiento>();
				foreach (Civ.IRequerimiento x in Edificios)
				{
					ret.Add(x);
				}
				foreach (Civ.IRequerimiento x in Ciencias)
				{
					ret.Add(x);
				}
				return ret;
			}
		}
	}

}
