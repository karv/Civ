using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using XML;
using System.IO;

namespace Store
{

	public static partial class Store<T>
	{
		public static void Serialize(string Filename, T Data)
		{
			System.Xml.Serialization.XmlSerializer writer =
				new System.Xml.Serialization.XmlSerializer(typeof(T));
			System.IO.StreamWriter file =
				new System.IO.StreamWriter(Filename);

			writer.Serialize(file, Data);
			file.Close();
		}

		public static T Deserialize (string Filename)
		{
			T ret;
			System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(T));
			System.IO.StreamReader file = new StreamReader(Filename);

			ret = (T)reader.Deserialize(file);
			file.Close();
			return ret;
		}
	}
}




