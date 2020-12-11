using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace _11_1
{
	class Program
	{
		static void Main(string[] args)
		{
			List<string> input = ReadInput();
			List<string> newLayout = new List<string>(input);
			int changed = -1;

			while (changed != 0)
			{
				changed = 0;
				for (int i = 0; i < input.Count; i++)
				{
					for (int j = 0; j < input[0].Length; j++)
					{
						int adjacentOccupied = 0;
						for (int k = -1; k <= 1; k++)
						{
							for (int l = -1; l <= 1; l++)
							{
								if (i + k < 0 || i + k >= input.Count || j + l < 0 || j + l >= input[0].Length || (k == 0 && l==0))
								{
									continue;
								}
								if (input[i + k][j + l] == '#')
								{
									adjacentOccupied++;
								}
							}
						}
						if (input[i][j] == 'L' && adjacentOccupied == 0)
						{
							StringBuilder newString = new StringBuilder(newLayout[i]);
							newString[j] = '#';
							newLayout[i] = newString.ToString();
							changed++;
						}
						if (input[i][j] == '#' && adjacentOccupied >= 4)
						{
							StringBuilder newString = new StringBuilder(newLayout[i]);
							newString[j] = 'L';
							newLayout[i] = newString.ToString();
							changed++;
						}
					}
				}
				input = new List<string>(newLayout);
			}
			int occupied = 0;
			for (int i = 0; i < input.Count; i++)
			{
				for (int j = 0; j < input[0].Length; j++)
				{
					if (input[i][j] == '#')
					{
						occupied++;
					}
				}
			}
			Console.WriteLine(occupied);
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
