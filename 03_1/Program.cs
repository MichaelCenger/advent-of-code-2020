using System;
using System.Collections.Generic;
using System.IO;

namespace _03_1
{
	class Program
	{
		static void Main(string[] args)
		{
			List<string> input = ReadInput();
			int x = 0, y = 0;
			int treeCount = 0;
			while (true)
			{

				x = (x + 3) % input[0].Length;
				y++;
				if (y >= input.Count)
				{
					break;
				}
				if (input[y][x] == '#')
				{
					treeCount++;
				}
			}
			Console.WriteLine(treeCount);
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
