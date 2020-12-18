using System;
using System.Collections.Generic;
using System.IO;

namespace _20_2
{
	class Program
	{
		static void Main(string[] args)
		{
			List<string> input = ReadInput();
			for (int i = 0; i < input.Count; i++)
			{

			}
		}

		private static List<string> ReadInput()
		{
			List<string> input = new List<string>();
			string[] lines = File.ReadAllLines(@"input.txt");

			foreach (string line in lines)
			{
				input.Add(line);
			}
			return input;
		}
	}
}
