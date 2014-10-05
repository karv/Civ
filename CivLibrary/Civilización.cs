using System;
using System.Collections.Generic;
using Basic;

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
            Random r = new Random();
			foreach (var x in Ciudades) {
				x.FullTick ();
			}

			// Las ciencias.
			List<Ciencia> Investigado = new List<Ciencia> ();

            foreach (Recurso x in Global.g_.Data.ObtenerRecursosCientíficos())
            {
                List<Ciencia> SemiListaCiencias = CienciasAbiertas().FindAll(z => (z.RecursoReq == x.Nombre));  // Lista de ciencias abiertas que usan el recurso x.
                float[] sep = r.Separadores(SemiListaCiencias.Count, ObtenerGlobalRecurso(x));

                int i = 0;
                foreach (var y in SemiListaCiencias)
                {
                    // En este momento, se está investigando "y" con el recurso "x".
                    Investigando[y] += sep[i];
                    i++;

                    // Si Tiene lo suficiente para terminar investigación
                    if (Investigando[y] >= y.CantidadReq) Investigado.Add(y);
                }
            }

			foreach (var x in Investigado) {
				Avances.Add(x);
				Investigando.Data.Remove (x);
			}

            // Fase final, desaparecer recursos.
            foreach (Ciudad x in Ciudades)
            {
                x.DestruirRecursosTemporales();
            }
		}


			// Avances
        /// <summary>
        /// Lista de avances de la civilización
        /// </summary>
		public List<Ciencia> Avances = new List<Ciencia>();

        /// <summary>
        /// Ciencias que han sido parcialmente investigadas.
        /// </summary>
		public ListasExtra.ListaPeso<Ciencia> Investigando = new ListasExtra.ListaPeso<Ciencia>();

        public List<Ciencia> CienciasAbiertas ()
        {
            List<Ciencia> ret = new List<Ciencia>();
            foreach (Ciencia x in Global.g_.Data.Ciencias)
            {
                if (EsCienciaAbierta(x))
                {
                    ret.Add(x);
                }
            }
            return ret;
        }

        /// <summary>
        /// Revisa si una ciencia se puede investigar.
        /// </summary>
        /// <param name="C">Una ciencia</param>
        /// <returns><c>true</c> si la ciencia se puede investigar; <c>false</c> si no.</returns>
        bool EsCienciaAbierta(Ciencia C)
        {
            return !Avances.Contains(C) && C.ReqCiencia.TrueForAll(z => Avances.Exists(w => (w.Nombre == z)));
        }


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

