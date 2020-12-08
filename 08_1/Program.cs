using System;
using System.Collections.Generic;
using System.IO;

namespace _08_1
{
	class Program
	{
		static void Main(string[] args)
		{
			List<string> input = ReadInput();
			Dictionary<int, bool> executed = new Dictionary<int, bool>();
			for (int i = 0; i < input.Count; i++)
			{
				executed[i] = false;
			}
			int acc = 0;
			int j = 0;
			while(true)
			{
				if (executed[j] == true)
				{
					break;
				}
				if (input[j].Contains("nop"))
				{
					j++;
					continue;
				}

				executed[j] = true;
				string operation = input[j].Substring(4, input[j].Length - 4);
				char sign = operation[0];
				int value = Convert.ToInt32(operation.Substring(1, operation.Length - 1));
				if (input[j].Contains("acc"))
				{
					switch (sign)
					{
						case '+':
							acc += value;
							
							break;
						case '-':
							acc -= value;
							break;
					}
					j++;
				}
				else if (input[j].Contains("jmp"))
				{
					switch (sign)
					{
						case '+':
							j += value;
							break;
						case '-':
							j -= value;
							break;
					}
				}
			}
			Console.WriteLine(acc);
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
