using System;
using System.Collections.Generic;



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
