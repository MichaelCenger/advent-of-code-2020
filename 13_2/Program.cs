using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _13_2
{
	class Program
	{
		static void Main(string[] args)
		{
			List<string> input = ReadInput();

			var lines = input[1].Split(',').ToList();

			List<int> numbers = new List<int>();
			List<int> remainders = new List<int>();

			for (int i = 0; i < lines.Count; i++)
			{
				if (lines[i] != "x")
				{
					numbers.Add(Convert.ToInt32(lines[i]));
					remainders.Add(i);
				}
			}

			int firstTimeStamp = Convert.ToInt32(numbers[0]);
			long currentTime = 0;
			long k = 0;
			//I <3 brute force
			while (true)
			{
				k++;
				currentTime = k * firstTimeStamp;
				bool valid = true;

				for (int j = 1; j < numbers.Count; j++)
				{
					if (((currentTime + remainders[j]) % numbers[j]) != 0)
					{
						valid = false;
						break;
					}
				}

				if (valid)
				{
					break;
				}
			}
			Console.WriteLine(currentTime);
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
