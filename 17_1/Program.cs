using System;
using System.Collections.Generic;
using System.IO;

namespace _17_1
{
	class Program
	{
		static void Main(string[] args)
		{
			List<string> input = ReadInput();
			char[,,] coordinates = new char[100, 100, 100];

			for (int z = 0; z < coordinates.GetLength(0); z++)
			{
				for (int y = 0; y < coordinates.GetLength(1); y++)
				{
					for (int x = 0; x < coordinates.GetLength(2); x++)
					{
						coordinates[x, y, z] = '.';
					}
				}
			}

			int startingPlaneZ = 50;
			int startingPlaneY = 50 - input.Count / 2;
			int startingPlaneX = 50 - input[0].Length / 2;
			for (int i = 0; i < input[0].Length; i++)
			{
				for (int j = 0; j < input.Count; j++)
				{
					coordinates[startingPlaneX + i, startingPlaneY + j, startingPlaneZ] = input[i][j];
				}
			}

			for (int cycles = 0; cycles < 6; cycles++)
			{
				char[,,] newState = new char[100, 100, 100];
				for (int z = 0; z < coordinates.GetLength(0); z++)
				{
					for (int y = 0; y < coordinates.GetLength(1); y++)
					{
						for (int x = 0; x < coordinates.GetLength(2); x++)
						{
							newState[x, y, z] = coordinates[x, y, z];
							int activeNeighbors = 0;
							for (int i = -1; i <= 1; i++)
							{
								for (int j = -1; j <= 1; j++)
								{
									for (int k = -1; k <= 1; k++)
									{
										if ((x + i) >= 100 || (y + j) >= 100 || (z + k) >= 100 || (x + i) < 0 || (y + j) < 0 || (z + k) < 0)
										{
											continue;
										}
										if (!(i == 0 && j == 0 && k == 0))
										{
											if (coordinates[x + i, y + j, z + k] == '#')
											{
												activeNeighbors++;
											}
										}
									}
								}
							}
							if (coordinates[x, y, z] == '#')
							{
								if (!(activeNeighbors == 2 || activeNeighbors == 3))
								{
									newState[x, y, z] = '.';
								}
							}
							else if (coordinates[x, y, z] == '.' && activeNeighbors == 3)
							{
								newState[x, y, z] = '#';
							}
						}
					}
				}
				coordinates = newState;
			}
			int activeCount = 0;
			for (int z = 0; z < coordinates.GetLength(0); z++)
			{
				for (int y = 0; y < coordinates.GetLength(1); y++)
				{
					for (int x = 0; x < coordinates.GetLength(2); x++)
					{
						if(coordinates[x, y, z] == '#')
						{
							activeCount++;
						}
					}
				}
			}
			Console.WriteLine(activeCount);
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
