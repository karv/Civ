﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;   //ref=System.Serialization....Soap.dll

namespace ListasExtra
{
	/// <summary>
	/// Representa una lista tipo Dictionary (o mejor aún una función de soporte finito) con operaciones de grupoide.
	/// </summary>
	/// <typeparam name="T">Dominio de la función.</typeparam>
	/// <typeparam name="V">Rango(co-dominio) de la función.</typeparam>
   [Serializable()]
   public class ListaPeso<T, V>
   {
		/// <summary>
		/// Representa una entrada (KeyValuePair) para esta clase.
		/// </summary>
      [Serializable()]
      public struct  Entrada
      {
         public T Key;
         public V Val;          
      }

      public bool ContainsKey(T key)
      {
          return Data.ContainsKey(key);
      }
      private Dictionary<T, V> _Data = new Dictionary<T, V>();
		/// <summary>
		/// Devuelve el tipo diccionario de la instancia.
		/// </summary>
      public Dictionary<T, V> Data
      {
         get
         {
            return _Data;
         }
      }
       /// <summary>
       /// Devuelve o establece el valor de este objeto en un valor específico.
       /// </summary>
       /// <param name="Key">Valor específico.</param>
       /// <returns>Devuelve el valor específico de este objeto en una entrada.</returns>
      public V this [T Key]
      {
         get
         {
            return Data.ContainsKey(Key) ? _Data[Key] : Nulo;
         }
         set
         {
            Set(Key, value);
         }
      }
      private V _NullV;
		/// <summary>
		/// Devuelve o establece cuál es el objeto nulo (cero) del grupoide; o bien, el velor prederminado de cada entrada T del dominio.
		/// </summary>
      public V Nulo
      {
         get
         {
            return _NullV;
         }
         set
         {
            _NullV = value;
         }
      }
		/// <summary>
		/// La operación suma.
		/// </summary>
      public Func<V, V, V> Suma;
		/// <summary>
		/// La operación inverso, si la tiene.
		/// </summary>
      public Func<V,V> Inv;

			//Estadísticos
		public V SumaTotal()
		{
			V tot = Nulo;
			foreach (T x in Data.Keys)
			{
				tot = Suma(tot, this[x]);
			}
			return tot;
		}

			//Ordenación y máximización
		/// <summary>
		/// Obtiene la entrda cuyo valor es máximo.
		/// </summary>
		/// <returns></returns>		
		public T ObtenerMáximo(Func<V,V,bool> Comparador)
		{
			if (!Data.Any()) return default(T);
			else
			{
				T tmp = Data.Keys.ToArray()[0];
				foreach (T x in Data.Keys)
				{
					if (Comparador(this[x], this[tmp])) tmp = x;
				}
				return tmp;
			}
		}

			//Eventos
		/// <summary>
		/// Se llama cuando se cambia algún valor (creo que no sirve aún Dx).
		/// </summary>
      public event EventHandler CambioValor; 

      
			//Constructor
		/// <summary>
		/// Inicializa una instancia de la clase.
		/// </summary>
		/// <param name="OperSuma">Operador suma inicial.</param>
		/// <param name="ObjetoNulo">Objeto cero inicial.</param>
      public ListaPeso(Func<V, V, V> OperSuma, V ObjetoNulo)
      {
         Suma = OperSuma;
         Nulo = ObjetoNulo;
      }
       /// <summary>
       /// Devuelve el soporte de esta instancia.
       /// </summary>
       public System.Collections.Generic.Dictionary<T,V>.KeyCollection Keys
      {
           get
          {
              return Data.Keys;
          }
      }
		/// <summary>
		/// Inicializa una instancia de la clase a partir de un valor inicial dado.
		/// </summary>
		/// <param name="OperSuma">Operador suma inicial.</param>
		/// <param name="ObjetoNulo">Objeto cero inicial.</param>
		/// <param name="InitDat">Data inicial.</param>
      public ListaPeso(Func<V,V,V> OperSuma, V ObjetoNulo, Dictionary<T, V> InitDat)
         :this (OperSuma,ObjetoNulo)
      {
         foreach (var x in InitDat) Add(x.Key, x.Value);
      }

		protected ListaPeso()
		{
		}

		/// <summary>
		/// Establece el valor de Obj.Key como Obj.Valor.
		/// </summary>
		/// <param name="Obj"></param>
      public virtual void Set(Entrada Obj)
      {
         //Está en la lista?
         if (Data.ContainsKey(Obj.Key))
         {
            Data[Obj.Key] = Obj.Val;

         }
         else
         {
            Data.Add(Obj.Key, Obj.Val);
         }

         if (Data[Obj.Key].Equals(Nulo))
         {
            Data.Remove(Obj.Key);
         }
         if (CambioValor != null) CambioValor.Invoke(this, new EventArgs());
      }
		/// <summary>
		/// Establece el valor de una entrada: this(Key) = Val.
		/// </summary>
		/// <param name="Key"></param>
		/// <param name="Val"></param>
      public void Set(T Key, V Val)
      {
         Entrada E;
         E.Key = Key;
         E.Val = Val;
         this.Set(E);
      }
		/// <summary>
		/// Suma una entrada de la instancia.
		/// </summary>
		/// <param name="Key">Entrada que se le sumará.</param>
		/// <param name="Val">Comparador que se sumará a la entrada.</param>
      public void Add(T Key, V Val)
      {
         Set(Key, Suma(this[Key], Val));
      }
		/// <summary>
		/// Suma una entrada de la instancia.
		/// </summary>
		/// <param name="Obj">Info a sumar.</param>
      public void Add(Entrada Obj)
      {
         this.Add(Obj.Key, Obj.Val);
      }
		/// <summary>
		/// Hace la función instancia cero.
		/// </summary>
      public void Vaciar()
      {
         List<T> Keys = new List<T>();
         foreach (T x in Data.Keys) Keys.Add (x);
         foreach (var x in Keys) Set(x, Nulo);
      }

		/// <summary>
		/// Devuelve la lista inversa a esta instancia.
		/// </summary>
		/// <returns></returns>
      public ListaPeso<T, V> Inverso()
      {
         if (Inv==null) throw new NullReferenceException ("No está definito Inv");
         ListaPeso<T, V> ret = new ListaPeso<T, V>(Suma, Nulo);
         ret.Inv = Inv;
         foreach (var x in Data.Keys)
         {
            ret.Add (x, Inv(this[x]));
         }
         return ret;
      }

		/// <summary>
		/// Suma esta lista en otra.
		/// </summary>
		/// <param name="S">Lista sumando.</param>
		/// <returns></returns>
      public ListaPeso<T, V> SumarA (ListaPeso<T, V> S)
      {
         ListaPeso<T, V> ret = (ListaPeso<T, V>) this.MemberwiseClone();
         foreach (T x in S.Data.Keys )
         {
            ret.Add(x, S[x]);
         }
         return ret;
      }

		/// <summary>
		/// Suma dos ListaExtra.ListaPeso coordenada a coordenada.
		/// </summary>
		/// <param name="Left">Primer sumando.</param>
		/// <param name="Right">Segundo sumando.</param>
		/// <returns></returns>
      public static ListaPeso<T, V> Sumar(ListaPeso<T, V> Left, ListaPeso<T, V> Right)
      {
         return Left.SumarA(Right);
      }
      public static ListaPeso<T, V> operator +(ListaPeso<T, V> Left, ListaPeso<T, V> Right)
      {
         return Sumar(Left, Right);
      }
      public static ListaPeso<T, V> operator -(ListaPeso<T, V> x)
      {
         return x.Inverso();
      }
      public static ListaPeso<T, V> operator -(ListaPeso<T, V> Left, ListaPeso<T, V> Right)
      {
         return Left + -Right;
      }
       
      public void Guardar(string Archivo)
      {
         //Opens a file and serializes the object into it in binary format.
         Stream stream = File.Open(Archivo, FileMode.Create);
         //SoapFormatter formatter = new SoapFormatter();

         BinaryFormatter formatter = new BinaryFormatter();

         formatter.Serialize(stream, this);
         stream.Close();
      }
   }
    /// <summary>
    /// Representa una función de T a float de soporte finito. hereda la operación suma.
    /// </summary>
    /// <typeparam name="T"></typeparam>
   public class ListaPeso<T> : ListasExtra.ListaPeso<T, Single>
   {
       public ListaPeso()
           : base((x, y) => x + y, 0)
       {
       }
   }
   /// <summary>
   /// Es sólo una listaPeso de enteros largos.
   /// </summary>
   /// <typeparam name="T"></typeparam>
   public class ListaContador<T> : ListasExtra.ListaPeso<T, long>
   {
       public ListaContador()
           : base((x, y) => x + y, 0)
       {
       }

       public long CountIf(Func<T, bool> Selector)
       {
           long ret = 0;
           foreach (var x in Data.Keys)
           {
               if (Selector.Invoke(x))
               {
                   ret += this[x];
               }
           }
           return ret;
       }
       
   }
}
