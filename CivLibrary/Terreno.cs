using System;
using ListasExtra;
using System.Collections.Generic;

namespace Civ
{
    /// <summary>
    /// Representa el terreno donde se construye una ciudad.
    /// </summary>
    public class Terreno: IPosición
    {
        /// <summary>
        /// Edificios que se contruyen al construir una ciudad aquí.
        /// </summary>
        public List<EdificioRAW> EdificiosIniciales;


            // Ecología
        /// <summary>
        /// Representa la ecología del terreno.
        /// </summary>
        public Ecología Eco;

        /// <summary>
        /// Representa la ecología del terreno.
        /// </summary>
        public class Ecología
        {
            public struct RecursoEstado
	        {
                public float Cant;
                public float Max;
                public float Crec;		
	        }
            public Dictionary<Recurso, RecursoEstado> RecursoEcológico;
        }

        /// <summary>
        /// Ciudad que está contruida en este terreno.
        /// </summary>
        public Ciudad CiudadConstruida;
    }
}
