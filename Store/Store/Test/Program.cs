using System;
using System.Collections.Generic;

namespace Test
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			string file = "algo.txt";
			MyClass S = new MyClass ();
			S.aList.Add ("Hue");
			S.aList.Add ("Fua");
			S.aList.Add ("Hui");
			S.aList.Add ("Hse");

			Store.Store<MyClass>.Serialize (file, S);

			Console.WriteLine ("Hello World!");
		}
	}

	public class MyClass
	{
		public List<string> aList = new List<string>();
	}
}
