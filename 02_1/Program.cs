using System;
using System.Collections.Generic;
using System.IO;

namespace _02_1
{
	class Program
	{
		static void Main(string[] args)
		{
			List<string> input = ReadInput();
			int validPasswordCount = 0;
			foreach (string inputLine in input)
			{
				int min = 0, max = 0;
				int minEndIndex = inputLine.IndexOf('-');
				min = Convert.ToInt32(inputLine.Substring(0, minEndIndex));
				int maxStartIndex = minEndIndex + 1;
				int maxEndIndex = inputLine.IndexOf(' ');
				max = Convert.ToInt32(inputLine.Substring(maxStartIndex, maxEndIndex - maxStartIndex));
				int characterToCheckIndex = maxEndIndex + 1;
				char characterToCheck = inputLine[characterToCheckIndex];
				int passwordStartIndex = characterToCheckIndex + 3;
				string password = inputLine.Substring(passwordStartIndex, inputLine.Length - passwordStartIndex);
				int characterCount = 0;
				foreach(char passwordCharacter in password)
				{
					if(passwordCharacter == characterToCheck)
					{
						characterCount++;
					}
				}
				if(characterCount <= max && characterCount >= min)
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
