using System;
using System.Collections.Generic;
using System.IO;

namespace _10_2
{
	class Program
	{
		static int count = 0;
		static int target;
		static void Main(string[] args)
		{
			List<int> input = ReadInput();
			target = input[input.Count - 1] + 3;
			input.Add(0);
			input.Sort();
			Console.WriteLine(GetPossibleWays(input));
		}

		private static void CountArrangements(List<int> input, int startIndex)
		{
			for (int i = startIndex; i < input.Count; i++)
			{
				int possibleAdapterCount = 0;
				List<int> newList = new List<int>(input);
				for (int j = 1; j <= Math.Min(3, input.Count - i - 1); j++)
				{
					if (input[i + j] - input[i] <= 3)
					{
						possibleAdapterCount++;
						if (possibleAdapterCount > 1)
						{
							newList.RemoveAt(i + j - 1);
							CountArrangements(newList, i);
						}
					}
				}
			}
			if (CheckValidity(input))
			{
				count++;
			}
		}

		private static long GetPossibleWays(List<int> input)
		{
			Dictionary<int, long> waysToConnect = new Dictionary<int, long>();
			//Last element has 1 way to connect
			waysToConnect[input.Count - 1] = 1;

			for (int i = input.Count - 2; i >= 0; i--)
			{
				long possibleAdapterCount = 0;
				for (int j = i + 1; j < Math.Min(i + 4, input.Count); j++)
				{
					if (input[j] - input[i] <= 3)
					{
						possibleAdapterCount += waysToConnect[j];
					}
				}
				waysToConnect[i] = possibleAdapterCount;
			}

			return waysToConnect[0];
		}

		private static bool CheckValidity(List<int> input)
		{
			List<int> copy = new List<int>(input);
			copy.Add(0);
			copy.Add(target);
			copy.Sort();
			for (int i = 0; i < copy.Count - 1; i++)
			{
				if (copy[i + 1] - copy[i] > 3)
				{
					return false;
				}
			}
			return true;
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
