using System.IO;
using System.Xml.Serialization;
using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using System.Collections.Generic;
//using Gtk;

namespace Store
{

	[Serializable()]	
	public partial class Store<T>
	{
		// For use with FileStream, MemoryStream, etc.
		/// <summary>
		/// Guarda algún objeto por un protocolo Stream.
		/// </summary>
		/// <param name="stream">El stream. FileStream, MemoryStream, etc.</param>
		/// <param name="theObject">Objeto a guardar</param>
		[Obsolete()]
		public static void Save(Stream stream, T theObject)
		{
			new XmlSerializer(typeof(T)).Serialize(stream, theObject);
		}

		// Store the object to the specified file
		[Obsolete()]
		public static void Save(string filename, T theObject)
		{
			using (TextWriter stream = new StreamWriter(filename))
				new XmlSerializer(typeof(T)).Serialize(stream, theObject);
		}

		// For use with FileStream, MemoryStream, etc.
		[Obsolete()]
		public static T Load(Stream stream)
		{
			return (T)(new XmlSerializer(typeof(T)).Deserialize(stream));
		}

		// Store the object to the specified file
		[Obsolete()]
		public static T Load(string filename)
		{
			using (TextReader stream = new StreamReader(filename))
				return (T)(new XmlSerializer(typeof(T)).Deserialize(stream));
		}
		[Obsolete()]
		public static void SerializeOld(string FileName, T mp)
		{
			Stream stream = File.Open(FileName, FileMode.Create);
			BinaryFormatter bformatter = new BinaryFormatter();

			Console.WriteLine("Writing Employee Information");
			bformatter.Serialize(stream, mp);
			stream.Close();
		}
		[Obsolete()]
		public static T DeSerializeOld(string FileName)
		{
			T mp;
			Stream stream = File.Open (FileName, FileMode.Open);
			BinaryFormatter bformatter = new BinaryFormatter ();

			mp = (T)bformatter.Deserialize (stream);
			stream.Close ();

			return mp;
		}
	}
}
