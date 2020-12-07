using System;
using System.Collections.Generic;
using System.IO;

namespace _13_1
{
	class Program
	{
		static void Main(string[] args)
		{
			List<string> input = ReadInput();
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
