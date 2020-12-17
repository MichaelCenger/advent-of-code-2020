using System;
using System.Collections.Generic;
using System.IO;

namespace _17_2
{
	class Program
	{
		static void Main(string[] args)
		{
			List<string> input = ReadInput();
			const int SIZE = 50;
			char[,,,] coordinates = new char[SIZE, SIZE, SIZE, SIZE];

			for (int z = 0; z < coordinates.GetLength(0); z++)
			{
				for (int y = 0; y < coordinates.GetLength(1); y++)
				{
					for (int x = 0; x < coordinates.GetLength(2); x++)
					{
						for (int w = 0; w < coordinates.GetLength(3); w++)
						{
							coordinates[x, y, z, w] = '.';
						}
					}
				}
			}

			int startingPlaneZ = SIZE / 2;
			int startingPlaneW = SIZE / 2;
			int startingPlaneY = SIZE / 2 - input.Count / 2;
			int startingPlaneX = SIZE / 2 - input[0].Length / 2;
			for (int i = 0; i < input[0].Length; i++)
			{
				for (int j = 0; j < input.Count; j++)
				{
					coordinates[startingPlaneX + i, startingPlaneY + j, startingPlaneZ, startingPlaneW] = input[i][j];
				}
			}

			for (int cycles = 0; cycles < 6; cycles++)
			{
				char[,,,] newState = new char[SIZE, SIZE, SIZE, SIZE];
				for (int w = 0; w < coordinates.GetLength(3); w++)
				{
					for (int z = 0; z < coordinates.GetLength(0); z++)
					{
						for (int y = 0; y < coordinates.GetLength(1); y++)
						{
							for (int x = 0; x < coordinates.GetLength(2); x++)
							{
								newState[x, y, z, w] = coordinates[x, y, z, w];
								int activeNeighbors = 0;
								for (int i = -1; i <= 1; i++)
								{
									for (int j = -1; j <= 1; j++)
									{
										for (int k = -1; k <= 1; k++)
										{
											for (int l = -1; l <= 1; l++)
											{
												if ((x + i) >= SIZE || (y + j) >= SIZE || (z + k) >= SIZE || (w + l) >= SIZE || (x + i) < 0 || (y + j) < 0 || (z + k) < 0 || (w + l) < 0)
												{
													continue;
												}
												if (!(i == 0 && j == 0 && k == 0 && l == 0))
												{
													if (coordinates[x + i, y + j, z + k, w + l] == '#')
													{
														activeNeighbors++;
													}
												}
											}
										}
									}
								}
								if (coordinates[x, y, z, w] == '#')
								{
									if (!(activeNeighbors == 2 || activeNeighbors == 3))
									{
										newState[x, y, z, w] = '.';
									}
								}
								else if (coordinates[x, y, z, w] == '.' && activeNeighbors == 3)
								{
									newState[x, y, z, w] = '#';
								}
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
						for (int w = 0; w < coordinates.GetLength(3); w++)
						{
							if (coordinates[x, y, z, w] == '#')
							{
								activeCount++;
							}
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
