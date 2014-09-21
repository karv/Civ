using System;

namespace Civ
{
	[Serializable()]
	/// <summary>
	/// Representa un adelanto científico.
	/// </summary>
	public class Ciencia : IRequerimiento
	{
		/// <summary>
		/// Nombre de la ciencia;
		/// </summary>
		public String Nombre;

		public override string ToString ()
		{
			return Nombre;
		}

			// Sobre los requerimientos.
		/// <summary>
		/// Recurso que se necesita para investigar.
		/// </summary>
		public Recurso RecursoReq;
		/// <summary>
		/// Cantidad de <see cref="RecursoReq"/> que se necesita para investigar.
		/// </summary>
		public float CantidadReq;

		/// <summary>
		/// Lista de requerimientos científicos.
		/// </summary>
		public System.Collections.Generic.List<Ciencia> ReqCiencia = new System.Collections.Generic.List<Ciencia>();


			// IRequerimiento
		bool Civ.IRequerimiento.LoSatisface (Ciudad C){
			return C.CivDueño.Avances.Contains(this);
		}
	}
}
