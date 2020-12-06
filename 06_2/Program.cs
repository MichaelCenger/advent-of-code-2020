using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _06_2
{
	class Program
	{
		static void Main(string[] args)
		{
			List<string> input = ReadInput();
			Dictionary<char, int> possibleValues = new Dictionary<char, int>();
			ResetDictionary(possibleValues);
			int sum = 0;
			List<string> currentGroup = new List<string>();
			for (int i = 0; i < input.Count; i++)
			{
				if (!string.IsNullOrEmpty(input[i]))
				{
					currentGroup.Add(input[i]);
				}
				if (string.IsNullOrEmpty(input[i]) || i == input.Count - 1)
				{
					foreach (string groupEntry in currentGroup)
					{
						foreach (char character in groupEntry)
						{
							possibleValues[character]++;
						}
					}
					sum += possibleValues.Where(val => val.Value == currentGroup.Count).ToList().Count;
					ResetDictionary(possibleValues);
					currentGroup.Clear();
				}
			}

			Console.WriteLine(sum);
		}

		private static void ResetDictionary(Dictionary<char, int> dictionary)
		{
			for (int i = 97; i <= 122; i++)
			{
				dictionary[((char)i)] = 0;
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
