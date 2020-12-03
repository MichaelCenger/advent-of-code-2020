using System;
using System.Collections.Generic;
using System.IO;

namespace _03_2
{
	class Program
	{
		static void Main(string[] args)
		{
			List<string> input = ReadInput();
			long result = GetTreeCount(input, 1, 1) * GetTreeCount(input, 3, 1) *
			GetTreeCount(input, 5, 1) *
			GetTreeCount(input, 7, 1) *
			GetTreeCount(input, 1, 2);
			Console.WriteLine(result);
		}

		private static long GetTreeCount(List<string> input, int slopeX, int slopeY)
		{
			long treeCount = 0;

			int x = 0, y = 0;
			while (true)
			{
				x = (x + slopeX) % input[0].Length;
				y += slopeY;
				if (y >= input.Count)
				{
					break;
				}
				if (input[y][x] == '#')
				{
					treeCount++;
				}
			}
			return treeCount;
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
