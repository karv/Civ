using System;
using System.Collections.Generic;

namespace Civ
{
	public interface IRequerimiento
	{
        /// <summary>
        /// Un método que a cada IRequerimiento le (debe) asocia un único string
        /// </summary>
        string ObtenerId();

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

	/// <summary>
	/// Representa el estado de un juego.
	/// </summary>
	public class g_State
	{

	}
}

namespace Basic
{
	[Serializable()]
	public struct Par <S,T>
	{
		public S x;
		public T y;

		public Par (S s, T t)
		{
			x = s;
			y = t;
		}

        public override string ToString()
        {
            return string.Format("({0}, {1})", x, y);
        }
	}


    public static class Covertidor<S, T>
    {
        /// <summary>
        /// Convierte una lista de objetos S en la equivalente lista de objetos T, mediante un Convertidos
        /// </summary>
        /// <param name="Entrada"></param>
        /// <param name="Conver"></param>
        /// <returns></returns>
        public static List<T> ConvertirLista (List<S> Entrada, Func<S,T> Conver)
        {
            List<T> ret = new List<T>();

            foreach (S x in Entrada)
	        {
                ret.Add (Conver(x));		 
	        }
            return ret;
        }
    }

    public static class ExtRandom
    {
        /// <summary>
        /// Elije aleatoriamente $a_0, \dots, a_{Partes - 1}$ de tal forma que su suma sea Suma.
        /// </summary>
        /// <param name="r">Clase random que se usará</param>
        /// <param name="Suma">La suma que debe de tener el conjunto.</param>
        /// <param name="Partes">Valor máximo </param>
        /// <returns>Devuelve un arreglo pseudoaleatoriamente generado de flotantes cuya suma es 1.</returns>
        public static float[] Separadores (this Random r, int Partes, float Suma = 1)
        {
            List<float> ret = new List<float>();
            for (int i = 0; i < Partes - 1; i++)
            {
                ret.Add((float)r.NextDouble() * Suma);
            }
            ret.Sort();
            ret.Add(Suma);

            for (int i = 0; i < Partes - 1; i++)
            {
                ret[i] = ret[i + 1] - ret[i];
            }
            return ret.ToArray();
        }
    }

}

namespace Global
{
	/// <summary>
	/// Los objetos globales.
	/// </summary>
	[Serializable()]
	public static class g_
	{
		public static g_Data Data = new g_Data();
		public static g_State State = new g_State();

		private const string archivo = "Data.xml";

		/// <summary>
		/// Carga del archivo predeterminado.
		/// </summary>
		public static void CargaData ()
		{
			Data = Store.Store<g_Data>.Deserialize (archivo);
		}

		public static void GuardaData() 
		{
			Store.Store<g_Data>.Serialize (archivo, Data);
		}

		public static void GuardaData(string f)
		{
			Store.Store<g_Data>.Serialize (f, Data);
		}

	}
}