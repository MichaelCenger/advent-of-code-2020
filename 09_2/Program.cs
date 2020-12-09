using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _09_2
{
	class Program
	{
		static void Main(string[] args)
		{
			List<long> input = ReadInput();
			int preambleLength = 25;
			List<long> numbersToCheck = input.Skip(preambleLength).ToList();
			List<long> preamble = input.Take(preambleLength).ToList();
			long invalidNumber = 0;
			for (int i = 0; i < numbersToCheck.Count; i++)
			{
				bool correct = false;
				for (int j = 0; j < preamble.Count; j++)
				{
					for (int k = j + 1; k < preamble.Count; k++)
					{
						if (preamble[j] + preamble[k] == numbersToCheck[i])
						{
							correct = true;
						}
					}
				}
				if (!correct)
				{
					invalidNumber = numbersToCheck[i];
					break;
				}
				preamble.RemoveAt(0);
				preamble.Add(numbersToCheck[i]);
			}
			bool found = false;
			for (int i = 0; i < input.Count; i++)
			{
				long sum = input[i];

				List<long> numbers = new List<long>();
				numbers.Add(input[i]);
				for (int j = i + 1; j < input.Count; j++)
				{
					if (sum > invalidNumber)
					{
						break;
					}
					else if (sum == invalidNumber)
					{
						found = true;
						break;
					}
					sum += input[j];
					numbers.Add(input[j]);
				}
				if (found)
				{
					numbers.Sort();
					Console.WriteLine(numbers[0] + numbers[numbers.Count - 1]);
					break;
				}
			}
		}

		private static List<long> ReadInput()
		{
			List<long> input = new List<long>();
			string[] lines = File.ReadAllLines(@"input.txt");

			foreach (string line in lines)
			{
				input.Add(Convert.ToInt64(line));
			}
			return input;
		}
	}
}
