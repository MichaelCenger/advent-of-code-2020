using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace _11_2
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
						string row = input[i];

						string right = row.Substring(j + 1, input[0].Length - j - 1);
						right = right.Replace(".", "");
						if (right.Length > 0)
						{
							adjacentOccupied = right[0] == '#' ? adjacentOccupied + 1 : adjacentOccupied;
						}
						string left = row.Substring(0, j);
						left = left.Replace(".", "");
						if (left.Length > 0)
						{
							adjacentOccupied = left[left.Length - 1] == '#' ? adjacentOccupied + 1 : adjacentOccupied;
						}

						string down = "";
						for (int currentHeight = i + 1; currentHeight < input.Count; currentHeight++)
						{
							down += input[currentHeight][j];
						}
						down = down.Replace(".", "");
						if (down.Length > 0)
						{
							adjacentOccupied = down[0] == '#' ? adjacentOccupied + 1 : adjacentOccupied;
						}

						string up = "";
						for (int currentHeight = i - 1; currentHeight >= 0; currentHeight--)
						{
							up += input[currentHeight][j];
						}
						up = up.Replace(".", "");
						if (up.Length > 0)
						{
							adjacentOccupied = up[0] == '#' ? adjacentOccupied + 1 : adjacentOccupied;
						}

						string topRight = "";
						string topLeft = "";
						int count = 0;
						for (int currentHeight = i - 1; currentHeight >= 0; currentHeight--)
						{
							count++;
							if (j + count < input[0].Length)
							{
								topRight += input[currentHeight][j + count];
							}
							if (j - count >= 0)
							{
								topLeft += input[currentHeight][j - count];
							}
						}
						if (i == 0)
						{
							topRight = "";
							topLeft = "";
						}
						if (j == 0)
						{
							topLeft = "";
						}
						if (j == input[0].Length - 1)
						{
							topRight = "";
						}
						topRight = topRight.Replace(".", "");
						if (topRight.Length > 0)
						{
							adjacentOccupied = topRight[0] == '#' ? adjacentOccupied + 1 : adjacentOccupied;
						}
						topLeft = topLeft.Replace(".", "");
						if (topLeft.Length > 0)
						{
							adjacentOccupied = topLeft[0] == '#' ? adjacentOccupied + 1 : adjacentOccupied;
						}

						string bottomRight = "";
						string bottomLeft = "";
						count = 0;
						for (int currentHeight = i + 1; currentHeight < input.Count; currentHeight++)
						{
							count++;
							if (j + count < input[0].Length)
							{
								bottomRight += input[currentHeight][j + count];
							}
							if (j - count >= 0)
							{
								bottomLeft += input[currentHeight][j - count];
							}
						}
						if (i == input.Count - 1)
						{
							bottomRight = "";
							bottomLeft = "";
						}
						if (j == 0)
						{
							bottomLeft = "";
						}
						if (j == input[0].Length - 1)
						{
							bottomRight = "";
						}
						bottomRight = bottomRight.Replace(".", "");
						if (bottomRight.Length > 0)
						{
							adjacentOccupied = bottomRight[0] == '#' ? adjacentOccupied + 1 : adjacentOccupied;
						}
						bottomLeft = bottomLeft.Replace(".", "");
						if (bottomLeft.Length > 0)
						{
							adjacentOccupied = bottomLeft[0] == '#' ? adjacentOccupied + 1 : adjacentOccupied;
						}

						if (input[i][j] == 'L' && adjacentOccupied == 0)
						{
							StringBuilder newString = new StringBuilder(newLayout[i]);
							newString[j] = '#';
							newLayout[i] = newString.ToString();
							changed++;
						}
						if (input[i][j] == '#' && adjacentOccupied >= 5)
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
