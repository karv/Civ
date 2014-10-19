using System;
using System.Collections.Generic;
using ListasExtra;

namespace Gráficas
{
    /// <summary>
    /// Representa una gráfica, en el sentido abstracto.
    /// Los nodos serán del tipo <c>T</c>.
    /// </summary>
    public class Gráfica<T>
    {
        /// <summary>
        /// Representa un nodo de la gráfica.
        /// </summary>
        public class Nodo
        {
            /// <summary>
            /// El objeto asociado a este nodo.
            /// </summary>
            public T Objeto;

            /// <summary>
            /// La vecindad de este nodo.
            /// </summary>
            public ListaPeso<Nodo> Vecinos = new ListaPeso<Nodo>();

            public Nodo(T Obj)
            {
                Objeto = Obj;
                Vecinos.Nulo = float.PositiveInfinity;
            }

            public int CantidadVecinos
            {
                get
                {
                    return Vecinos.Data.Count;
                }
            }
        }

        /// <summary>
        /// Representa una ruta en un árbol.
        /// </summary>
        public class Ruta
        {
            public List<Nodo> Paso;

            public static bool operator == (Ruta left, Ruta right)
            {
                if (left.Paso.Count!=right.Paso.Count) return false;

                for (int i = 0; i < left.Paso.Count; i++)
			    {
			        if (!left.Paso[i].Equals(right.Paso[i])) return false;
			    }
                return true;
            }

            public static bool operator !=(Ruta left, Ruta right)
            {
                return !(left == right);
            }

            /// <summary>
            /// Devuelve la longitud de la ruta.
            /// </summary>
            public float Longitud
            {
                get
                {
                    float ret = 0f;
                    for (int i = 0; i < Paso.Count - 1; i++)
			        {
			            ret += Paso[i].Vecinos[Paso[i+1]];
			        }
                    return ret;
                }
            }

            public override bool Equals(object obj)
            {
                if (obj.GetType() is Gráfica<T>.Ruta)
                {
                    Gráfica<T>.Ruta Obj = (Gráfica<T>.Ruta)obj;
                    return this == Obj;
                }
                else return false;                    
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        }
        /// <summary>
        /// Lista de nodos.
        /// </summary>
        private List<Nodo> _Nodos = new List<Nodo>();

        /// <summary>
        /// Devuelve un clon de la lista de nodos.
        /// </summary>
        public Nodo[] Nodos
        {
            get
            {
                return _Nodos.ToArray();
            }
        }

        /// <summary>
        /// Agrega un nodo al árbol.
        /// </summary>
        /// <param name="nodo"></param>
        public void AgregaNodo (T nodo)
        {
            _Nodos.Add(new Nodo(nodo));
        }

        /// <summary>
        /// Devuelve o establece el peso de la arista que une dos vértices.
        /// </summary>
        /// <param name="x">Un vértice.</param>
        /// <param name="y">Otro vértice.</param>
        /// <returns>Devuelve el peso de la arista que une estos nodos. <see cref="float.PositiveInfinity"/> si no existe arista.</returns>
        public float this[Nodo x, Nodo y]
        {
            get
            {
                return x.Vecinos[y];
            }
            set
            {
                x.Vecinos[y] = value;
                y.Vecinos[x] = value;                
            }
        }

        /// <summary>
        /// Agrega un vértice entre dos nodos existentes a la gráfica.
        /// </summary>
        /// <param name="x">Un nodo.</param>
        /// <param name="y">Otro nodo.</param>
        /// <param name="Peso">El peso de la arista entre los nodos</param>
        public void AgregaVértice(Nodo x, Nodo y, float Peso)
        {
            if (_Nodos.Contains(x) && _Nodos.Contains(y))
            {
                x.Vecinos[y] = Peso;
                y.Vecinos[x] = Peso;
            }
        }

        /// <summary>
        /// Devuelve el número de nodos de esta gráfica.
        /// </summary>
        public int NumNodos
        {
            get
            {
                return _Nodos.Count;
            }
        }

        /// <summary>
        /// Calcula la ruta óptima de un nodo a otro.
        /// </summary>
        /// <param name="x">Nodo inicial.</param>
        /// <param name="y">Nodo final.</param>
        /// <param name="Ignorar">Lista de nodos a evitar.</param>
        /// <returns>Devuelve la ruta de menor <c>Longitud</c>.</returns>
        /// <remarks>Puede ciclar si no existe ruta de x a y.</remarks> // TODO: Arreglar esto.
        public Ruta CaminoÓptimo(Nodo x, Nodo y, List<Nodo> Ignorar)
        {
            Ruta ret = new Ruta();
            Ruta RutaBuscar;
            List<Nodo> Ignora2;
            Nodo[] tmp = {};


            if (x.Equals(y)) {
                ret.Paso.Add(x);
                return ret;            
            }
            // else
            foreach (var n in y.Vecinos.Keys)
	        {
		        if (!Ignorar.Contains(n))
                {
                    Ignorar.CopyTo(tmp);
                    Ignora2 = new List<Nodo> (tmp);

                    RutaBuscar = CaminoÓptimo(x, n, Ignora2);
                    RutaBuscar.Paso.Add(y);

                    if (ret.Paso.Count > 0 && ret.Longitud > RutaBuscar.Longitud) ret = RutaBuscar;
                }
	        }
            return ret;           
        }

        /// <summary>
        /// Calcula la ruta óptima de un nodo a otro.
        /// </summary>
        /// <param name="x">Nodo inicial.</param>
        /// <param name="y">Nodo final.</param>
        /// <returns>Devuelve la ruta de menor <c>Longitud</c>.</returns>
        /// <remarks>Puede ciclar si no existe ruta de x a y.</remarks> // TODO: Arreglar esto.
        public Ruta CaminoÓptimo (Nodo x, Nodo y)
        {
            return CaminoÓptimo (x,y, new List<Nodo>());
        }

        /// <summary>
        /// Genera una gráfica aleatoria.
        /// </summary>
        /// <param name="Nodos">El conjunto de nodos que se usarán</param>
        /// <param name="r">El generador aleatorio.</param>
        /// <returns></returns>
        public static Gráfica<T> GeneraGráficaAleatoria(T[] Nods, Random r)
        {
            Gráfica<T> ret = new Gráfica<T>();
            foreach (var x in Nods)
            {
                ret.AgregaNodo(x);
                if (ret._Nodos.Count > 1)
                {

                }
            }
            return ret;
        }

        /* // TODO: Esto...

        /// <summary>
        /// Selecciona al azar algunos nodos T.
        /// Con peso según su número de vecinos.
        /// </summary>
        /// <param name="NumSel">Cantidad de nodos a seleccionar.</param>
        /// <param name="r">Generador.</param>
        /// <returns>La lista de nodos seleccionados.</returns>
        List<T> SeleccionaAleatorio (int NumSel, Random r, List<T> lst)
        {
            ListaPeso<T> Pesos = new ListaPeso<T>();
            List<T> ret = new List<T>();

            foreach (var x in Nodos)
	            {
		            Pesos[x.Objeto] = 1/(float)Math.Pow(2, x.CantidadVecinos);
	            }

            

            if (NumSel > NumNodos)
            {
                throw new Exception("No se pueden seleccionar más nodos de los que existen.");
            }

            List<T> m = new List<T>();
            

            foreach (var x in Nodos)
            {
                float prob = (NumSel - ret.Count) / num
            }
            while (ret.Count < NumNodos)
            {

            }

            return ret;
        }
         */

        /// <summary>
        /// Selecciona pseudoaleatoriamente una sublista de tamaño fijo de una lista dada.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="n">Número de elementos a seleccionar.</param>
        /// <param name="Lista">Lista de dónde seleccionar la sublista.</param>
        /// <returns>Devuelve una lista con los elementos seleccionados.</returns>
        List<object> SeleccionaPeso (Random r, int n, ListasExtra.ListaPeso<object> Lista)
        {
            List<object> ret;
            float Suma = 0;
            float rn;
            if (n == 0) return new List<object>(); else {
                ret = SeleccionaPeso(r, n - 1, Lista);

                foreach (var x in ret)
                {
                    Lista[x] = 0;
                }

                    // Ahora seleecionar uno.
                Suma = 0;
                rn = (float)r.NextDouble() * Lista.SumaTotal();

                foreach (var x in Lista.Keys)
                {
                    Suma += Lista[x];
                    if (Suma >= rn)
                    {
                        ret.Add(x);
                        return ret;
                    }
                }
                return null;
            }
        }
    }

}
