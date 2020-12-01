using System;
using System.Collections.Generic;
using System.IO;

namespace _01_2
{
	class Program
	{
		static void Main(string[] args)
		{
			List<int> input = ReadInput();
			for (int i = 0; i < input.Count; i++)
			{
				for (int j = i + 1; j < input.Count; j++)
				{
					int sum = input[i] + input[j];
					//Would be faster with a dictionary indexed on the remains
					for (int k = 0; k < input.Count; k++)
					{
						if (k == j || k == i)
						{
							continue;
						}
						if (sum + input[k] == 2020)
						{
							int result = input[i] * input[j] * input[k];
							Console.WriteLine($"The result is: {result}");
							return;
						}
					}
				}
			}


		}

		private static List<int> ReadInput()
		{
			List<int> input = new List<int>();
			string[] lines = File.ReadAllLines(@"input.txt");

			foreach (string line in lines)
			{
				input.Add(Convert.ToInt32(line));
			}
			return input;
		}
	}
}
