using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace _14_2
{
	class Program
	{
		static void Main(string[] args)
		{
			List<string> input = ReadInput();
			Dictionary<long, long> memory = new Dictionary<long, long>();
			string currentMask = "";
			foreach (string line in input)
			{
				if (line.Contains("mask"))
				{
					currentMask = line.Substring(7, line.Length - 7);
				}
				else
				{
					string cutLine = line.Replace(" ", "");
					var parsedLine = cutLine.Split("=");
					long address = Convert.ToInt64(parsedLine[0].Substring(4, parsedLine[0].Length - 5));
					long value = Convert.ToInt64(parsedLine[1]);

					string binaryAddress = Convert.ToString(address, 2);
					StringBuilder maskedAddress = new StringBuilder(currentMask);

					int currentIndex = 0;
					for (int i = maskedAddress.Length - binaryAddress.Length; i < maskedAddress.Length; i++)
					{
						if (maskedAddress[i] == '0')
						{
							maskedAddress[i] = binaryAddress[currentIndex];
						}
						currentIndex++;
					}
					HashSet<string> permutations = new HashSet<string>();
					List<int> xIndices = new List<int>();
					for (int i = 0; i < maskedAddress.ToString().Length; i++)
					{
						if (maskedAddress[i] == 'X')
						{
							xIndices.Add(i);
						}
					}
					GeneratePermutations(permutations, maskedAddress.ToString(), xIndices, 0);
					foreach (string permutation in permutations)
					{
						long addressNumber = Convert.ToInt64(permutation, 2);
						memory[addressNumber] = value;
					}
				}
			}
			long sum = 0;
			foreach (var kvp in memory)
			{
				sum += kvp.Value;
			}

			Console.WriteLine(sum);
		}

		private static void GeneratePermutations(HashSet<string> result, string value, List<int> xIndices, int currentIndex)
		{
			if (currentIndex == xIndices.Count)
			{
				result.Add(value);
				return;
			}
			StringBuilder newValue1 = new StringBuilder(value);
			newValue1[xIndices[currentIndex]] = '1';

			StringBuilder newValue2 = new StringBuilder(value);
			newValue2[xIndices[currentIndex]] = '0';

			++currentIndex;

			GeneratePermutations(result, newValue1.ToString(), xIndices, currentIndex);
			GeneratePermutations(result, newValue2.ToString(), xIndices, currentIndex);

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
