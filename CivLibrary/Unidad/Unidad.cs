using System;
using ListasExtra;
using Basic;
using System.Collections.Generic;

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

    /// <summary>
    /// Representa a una instancia de unidad.
    /// </summary>
    public class Unidad
    {
        /// <summary>
        /// La clase a la que pertenece esta unidad.
        /// </summary>
        public readonly UnidadRAW RAW;

        /// <summary>
        /// Crea una instancia.
        /// </summary>
        /// <param name="uRAW">El RAW que tendrá esta unidad.</param>
        public Unidad(UnidadRAW uRAW)
        {
            RAW = uRAW;
            Nombre=uRAW.Nombre;
        }

        public override string ToString()
        {
            return Nombre;
        }

        /// <summary>
        /// Devuelve o establece el nombre de esta unidad.
        /// </summary>
        public string Nombre;

        float _Entrenamiento;

        /// <summary>
        /// Devuelve o establece el nivel de entrenamiento de esta unidad.
        /// Es un valor en [0, 1].
        /// </summary>
        public float Entrenamiento
        {
            get { return _Entrenamiento; }
            set { _Entrenamiento = Math.Max(Math.Min(1, value), 0); }
        }
    }
}
