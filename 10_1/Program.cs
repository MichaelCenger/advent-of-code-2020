using System;
using System.Collections.Generic;
using System.IO;

namespace _10_1
{
	class Program
	{
		static void Main(string[] args)
		{
			List<int> input = ReadInput();
			int oneJoltDiff = 0;
			int threeJoltDiff = 0;
			input.Add(0);
			input.Sort();
			if (input[0] == 1)
			{
				oneJoltDiff++;
			}
			else if (input[0] == 3)
			{
				threeJoltDiff++;
			}
			for (int i = 0; i < input.Count - 1; i++)
			{
				if (input[i + 1] - input[i] == 1)
				{
					oneJoltDiff++;
				}
				else if (input[i + 1] - input[i] == 3)
				{
					threeJoltDiff++;
				}
			}
			threeJoltDiff++;
			Console.WriteLine(oneJoltDiff * threeJoltDiff);
		}

		private static List<int> ReadInput()
		{
			List<int> input = new List<int>();
			string[] lines = File.ReadAllLines(@"input.txt");

			foreach (string line in lines)
			{
				if (line != "")
					input.Add(Convert.ToInt32(line));
			}
			return input;
		}
	}
}
