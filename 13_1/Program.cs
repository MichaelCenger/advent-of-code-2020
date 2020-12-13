using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace _13_1
{
	class Program
	{
		static void Main(string[] args)
		{
			List<string> input = ReadInput();
			float timeToWait = int.MaxValue;
			int busId = 0;

			int time = Convert.ToInt32(input[0]);

			var lines = input[1].Split(',').ToList();

			lines.RemoveAll(line => { return line == "x"; });
			Dictionary<int, List<int>> times = new Dictionary<int, List<int>>();

			for (int i = 0; i < lines.Count; i++)
			{
				int lineNumber = Convert.ToInt32(lines[i]);
				int j = 0;
				times[lineNumber] = new List<int>();
				while (j* lineNumber < time)
				{
					j++;
					times[lineNumber].Add(lineNumber * j);
				}
			}

			foreach(var kvp in times)
			{
				if ( kvp.Value[kvp.Value.Count - 1] -time < timeToWait)
				{
					timeToWait = kvp.Value[kvp.Value.Count - 1]-time ;
					busId = kvp.Key;
				}
			}

			Console.WriteLine((int)timeToWait * busId);
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
