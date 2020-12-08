using System;
using System.Collections.Generic;
using System.IO;

namespace _08_2
{
	class Program
	{
		static void Main(string[] args)
		{
			List<string> input = ReadInput();
			Dictionary<int, List<string>> permutations = new Dictionary<int, List<string>>();
			for (int i = 0; i < input.Count; i++)
			{
				permutations[i] = new List<string>();
				for (int k = 0; k < input.Count; k++)
				{
					permutations[i].Add(input[k]);
				}
				string instruction = permutations[i][i];
				string[] splitString = instruction.Split(' ');
				if (instruction.Contains("jmp"))
				{
					string newInstruction = "nop " + splitString[1];
					permutations[i][i] = newInstruction;
				}
				else if (instruction.Contains("nop"))
				{
					string newInstruction = "jmp " + splitString[1];
					permutations[i][i] = newInstruction;
				}
			}

			for (int i = 0; i < permutations.Count; i++)
			{
				HashSet<int> visited = new HashSet<int>();
				int acc = 0;
				List<string> currentInput = permutations[i];

				int j = 0;
				while (true)
				{
					if (visited.Contains(j))
					{
						break;
					}
					visited.Add(j);
					if (j >= currentInput.Count)
					{
						Console.WriteLine(acc);
						break;
					}
					if (currentInput[j].Contains("nop"))
					{
						j++;
						continue;
					}
					string operation = currentInput[j].Substring(4, currentInput[j].Length - 4);
					char sign = operation[0];
					int value = Convert.ToInt32(operation.Substring(1, operation.Length - 1));
					if (currentInput[j].Contains("acc"))
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
					else if (currentInput[j].Contains("jmp"))
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
