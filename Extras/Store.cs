using System.IO;
using System.Xml.Serialization;

//--------------------------------------------------------------------------
// Saves an object to an XML file and Loads an object from an XML file.
//
// Examples -- saves to a file:
//      CMyClass myobject;
//      myobject = ....
//      Store<CMyClass>.Save("myobject.xml", myobject);
//
// Examples -- loads from file:
//      CMyClass myobject = Store<CMyClass>.Load("myobject.xml);
//--------------------------------------------------------------------------

public class Store<T>
{
    // For use with FileStream, MemoryStream, etc.
    public static void Save(Stream stream, T theObject)
    {
        new XmlSerializer(typeof(T)).Serialize(stream, theObject);
    }

    // Store the object to the specified file
    public static void Save(string filename, T theObject)
    {
        using (TextWriter stream = new StreamWriter(filename))
            new XmlSerializer(typeof(T)).Serialize(stream, theObject);
    }

    // For use with FileStream, MemoryStream, etc.
    public static T Load(Stream stream)
    {
        return (T)(new XmlSerializer(typeof(T)).Deserialize(stream));
    }

    // Store the object to the specified file
    public static T Load(string filename)
    {
        using (TextReader stream = new StreamReader(filename))
            return (T)(new XmlSerializer(typeof(T)).Deserialize(stream));
    }
     
}
