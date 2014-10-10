using System;
using System.Collections.Generic;
using ListasExtra;

namespace Civ
{
    /// <summary>
    /// Representa una clase de unidad
    /// </summary>
    public class UnidadRAW
    {
        /// <summary>
        /// El nombre de la clase de unidad.
        /// </summary>
        public string Nombre;

        /// <summary>
        /// Lista de modificadores de combate de la unidad.
        /// </summary>
        public ListaPeso<string> Mods;

        /// <summary>
        /// Fuerza de la unidad.
        /// </summary>
        public float Fuerza;

        /// <summary>
        /// Flags.
        /// </summary>
        public List<string> Flags;

        // Reqs
        /// <summary>
        /// Requerimientos para crearse.
        /// </summary>
        public List<IRequerimiento> Reqs;

        /// <summary>
        /// Población productiva que requiere para entrenar.
        /// </summary>
        public ulong CostePoblación;
    }
}
