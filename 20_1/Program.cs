using System;
using System.Collections.Generic;
using System.IO;

namespace _20_1
{
	class Program
	{

		static void Main(string[] args)
		{
			Dictionary<int, List<char[,]>> tiles = new Dictionary<int, List<char[,]>>();
			HashSet<int> Corners = new HashSet<int>();
			List<string> input = ReadInput();
			for (int i = 0; i < input.Count; i += 12)
			{
				int tileId = Convert.ToInt32(input[i].Split(" ")[1].Replace(":", ""));
				char[,] tile = new char[10, 10];
				for (int j = 1; j < 11; j++)
				{
					for (int k = 0; k < 10; k++)
					{
						tile[j - 1, k] = input[i + j][k];
					}
				}
				tiles[tileId] = new List<char[,]>();
				tiles[tileId].Add(tile);
				tiles[tileId].Add(FlipTileX(tile));
				tiles[tileId].Add(FlipTileY(tile));
				tiles[tileId].Add(FlipTileY(FlipTileX(tile)));
				for (int j = 1; j < 4; j++)
				{
					tiles[tileId].Add(RotateTile(tile, j));
					tiles[tileId].Add(FlipTileX(RotateTile(tile, j)));
					tiles[tileId].Add(FlipTileY(RotateTile(tile, j)));
					if (j != 2)
					{
						tiles[tileId].Add(FlipTileY(FlipTileX(RotateTile(tile, j))));
					}
				}
			}

			foreach (var kvp in tiles)
			{
				foreach (var tile in tiles[kvp.Key])
				{
					int alignmentCount = 0;
					foreach (var kvp2 in tiles)
					{
						if (alignmentCount > 2)
						{
							break;
						}
						if (kvp2.Key == kvp.Key)
						{
							continue;
						}
						bool increment = false;
						foreach (var tile2 in tiles[kvp2.Key])
						{
							if (CheckAlignment(tile, tile2, true, false, false, false) || CheckAlignment(tile, tile2, false, true, false, false) | CheckAlignment(tile, tile2, false, false, true, false) | CheckAlignment(tile, tile2, false, false, false, true))
							{
								increment = true;
								break;
							}
						}
						if (increment)
						{
							alignmentCount++;
						}
					}

					if (alignmentCount == 2)
					{
						Corners.Add(kvp.Key);
					}
				}
			}

			long result = 1;
			foreach (var index in Corners)
			{
				result *= index;
			}
			Console.WriteLine(result);
		}

		private static bool CheckAlignment(char[,] tile1, char[,] tile2, bool top, bool left, bool right, bool bottom)
		{
			bool aligns = true;
			if (left)
			{
				for (int y = 0; y < 10; y++)
				{
					aligns = tile1[9, y] == tile2[0, y];
					if (!aligns)
					{
						break;
					}
				}
			}

			if (top)
			{
				for (int x = 0; x < 10; x++)
				{
					aligns = tile1[x, 9] == tile2[x, 0];
					if (!aligns)
					{
						break;
					}
				}
			}

			if (right)
			{
				for (int y = 0; y < 10; y++)
				{
					aligns = tile1[0, y] == tile2[9, y];
					if (!aligns)
					{
						break;
					}
				}
			}
			if (bottom)
			{
				for (int x = 0; x < 10; x++)
				{
					aligns = tile1[x, 0] == tile2[x, 9];
					if (!aligns)
					{
						break;
					}
				}
			}
			return aligns;
		}

		private static void PrintTile(char[,] tile)
		{
			for (int j = 0; j < 10; j++)
			{
				Console.WriteLine();
				for (int k = 0; k < 10; k++)
				{
					Console.Write(tile[j, k]);
				}
			}
			Console.WriteLine();
		}

		private static char[,] RotateTile(char[,] tile, int amount)
		{
			char[,] result = new char[10, 10];
			if (amount == 3)
			{
				for (int y = 0; y < 10; y++)
				{
					for (int x = 0; x < 10; x++)
					{
						result[9 - y, x] = tile[x, y];
					}
				}
			}
			else if (amount == 2)
			{
				result = FlipTileY(FlipTileX(tile));
			}
			else if (amount == 1)
			{
				for (int y = 0; y < 10; y++)
				{
					for (int x = 0; x < 10; x++)
					{
						result[y, 9 - x] = tile[x, y];
					}
				}
			}
			return result;
		}

		private static char[,] FlipTileY(char[,] tile)
		{
			char[,] result = new char[10, 10];
			for (int y = 0; y < 10; y++)
			{
				for (int x = 0; x < 10; x++)
				{
					result[9 - y, x] = tile[y, x];
				}
			}
			return result;
		}

		private static char[,] FlipTileX(char[,] tile)
		{
			char[,] result = new char[10, 10];
			for (int y = 0; y < 10; y++)
			{
				for (int x = 0; x < 10; x++)
				{
					result[y, 9 - x] = tile[y, x];
				}
			}
			return result;
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
