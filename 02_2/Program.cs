using System;
using System.Collections.Generic;
using System.IO;

namespace _02_2
{
	class Program
	{
		static void Main(string[] args)
		{
			List<string> input = ReadInput();
			int validPasswordCount = 0;
			foreach (string inputLine in input)
			{
				int firstIndex = 0, secondIndex = 0;
				int minEndIndex = inputLine.IndexOf('-');
				firstIndex = Convert.ToInt32(inputLine.Substring(0, minEndIndex));
				int maxStartIndex = minEndIndex + 1;
				int maxEndIndex = inputLine.IndexOf(' ');
				secondIndex = Convert.ToInt32(inputLine.Substring(maxStartIndex, maxEndIndex - maxStartIndex));
				int characterToCheckIndex = maxEndIndex + 1;
				char characterToCheck = inputLine[characterToCheckIndex];
				int passwordStartIndex = characterToCheckIndex + 3;
				string password = inputLine.Substring(passwordStartIndex, inputLine.Length - passwordStartIndex);
				int characterCount = 0;
				if (password[firstIndex - 1] == characterToCheck)
				{
					characterCount++;
				}
				if (password[secondIndex - 1] == characterToCheck)
				{
					characterCount++;
				}
				if (characterCount == 1)
				{
					validPasswordCount++;
				}
			}
			Console.WriteLine(validPasswordCount);
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
