using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _09_1
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
			for(int i = 0; i < numbersToCheck.Count; i++)
			{
				bool correct = false;
				for(int j = 0; j < preamble.Count; j++)
				{
					for (int k = j+1; k < preamble.Count; k++)
					{
						if(preamble[j] + preamble[k] == numbersToCheck[i])
						{
							correct = true;
						}
					}
				}
				if (!correct)
				{
					invalidNumber = numbersToCheck[i];
					Console.WriteLine(invalidNumber);
					break;
				}
				preamble.RemoveAt(0);
				preamble.Add(numbersToCheck[i]);
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
