using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _20_2
{
	class Program
	{
		static Dictionary<int, List<char[,]>> tiles = new Dictionary<int, List<char[,]>>();
		static int imageSize;
		static int maxDepth = 0;
		static int hashes = 0;
		static HashSet<(int id1, int index1, int id2, int index2, bool top)> Alignment = new HashSet<(int id1, int index1, int id2, int index2, bool top)>();

		static Dictionary<(int x, int y), (int tileId, int tileIndex, char[,] tile)> Board;
		static List<(int x, int y)> monsterIndices = new List<(int x, int y)>();
		static void Main(string[] args)
		{
			monsterIndices.Add((18, 0));

			monsterIndices.Add((0, 1));
			monsterIndices.Add((5, 1));
			monsterIndices.Add((6, 1));
			monsterIndices.Add((11, 1));
			monsterIndices.Add((12, 1));
			monsterIndices.Add((17, 1));
			monsterIndices.Add((18, 1));
			monsterIndices.Add((19, 1));

			monsterIndices.Add((1, 2));
			monsterIndices.Add((4, 2));
			monsterIndices.Add((7, 2));
			monsterIndices.Add((10, 2));
			monsterIndices.Add((13, 2));
			monsterIndices.Add((16, 2));

			HashSet<int> Corners = new HashSet<int>();
			HashSet<int> Edges = new HashSet<int>();
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
				tiles[tileId].Add(FlipTileX(tile, 10));
				tiles[tileId].Add(FlipTileY(tile, 10));
				tiles[tileId].Add(FlipTileY(FlipTileX(tile, 10), 10));
				for (int j = 1; j < 4; j++)
				{
					tiles[tileId].Add(RotateTile(tile, j, 10));
					tiles[tileId].Add(FlipTileX(RotateTile(tile, j, 10), 10));
					tiles[tileId].Add(FlipTileY(RotateTile(tile, j, 10), 10));
					if (j != 2)
					{
						tiles[tileId].Add(FlipTileY(FlipTileX(RotateTile(tile, j, 10), 10), 10));
					}
				}

				List<char[,]> tilesToRemove = new List<char[,]>();
				for (int o = 0; o < tiles[tileId].Count; o++)
				{
					for (int p = o + 1; p < tiles[tileId].Count; p++)
					{
						if (CheckDuplicate(tiles[tileId][o], tiles[tileId][p]))
							tilesToRemove.Add(tiles[tileId][o]);
					}
				}

				tiles[tileId] = tiles[tileId].Except(tilesToRemove).ToList();
			}
			imageSize = (int)Math.Sqrt(tiles.Count);


			Solve(0, 0, new HashSet<int>(), new Dictionary<(int x, int y), (int tileId, int tileIndex, char[,] tile)>());

			List<char[,]> images = new List<char[,]>();

			char[,] image = new char[8 * imageSize, 8 * imageSize];
			for (int x = 0; x < imageSize; x++)
			{
				for (int y = 0; y < imageSize; y++)
				{
					int newX = 0;
					for (int i = 1; i < 9; i++)
					{
						int newY = 0;
						for (int j = 1; j < 9; j++)
						{
							image[x * 8 + newX, y * 8 + newY] = Board[(x, y)].tile[i, j];
							newY++;
						}
						newX++;
					}
				}
			}
			images.Add(image);
			images.Add(FlipTileX(image, 8 * imageSize));
			images.Add(FlipTileY(image, 8 * imageSize));
			images.Add(FlipTileY(FlipTileX(image, 8 * imageSize), 8 * imageSize));
			for (int j = 1; j < 4; j++)
			{
				images.Add(RotateTile(image, j, 8 * imageSize));
				images.Add(FlipTileX(RotateTile(image, j, 8 * imageSize), 8 * imageSize));
				images.Add(FlipTileY(RotateTile(image, j, 8 * imageSize), 8 * imageSize));
				if (j != 2)
				{
					images.Add(FlipTileY(FlipTileX(RotateTile(image, j, 8 * imageSize), 8 * imageSize), 8 * imageSize));
				}
			}


			List<char[,]> imagesToRemove = new List<char[,]>();
			for (int i = 0; i < images.Count; i++)
			{
				for (int j = i + 1; j < images.Count; j++)
				{
					if (CheckDuplicate(images[i], images[j]))
						imagesToRemove.Add(images[i]);
				}
			}

			images = images.Except(imagesToRemove).ToList();

			int monsters = 0;

			for(int x = 0; x < images[0].GetLength(0);x++)
			{
				for (int y = 0; y < images[0].GetLength(0); y++)
				{
					if(images[0][x,y] == '#')
					{
						hashes++;
					}
				}
			}
			foreach (var img in images)
			{
				for (int x = 0; x < img.GetLength(0) - 18; x++)
				{
					for (int y = 0; y < img.GetLength(0) - 2; y++)
					{
						bool containsMonster = true;
						foreach (var index in monsterIndices)
						{
							if (img[y + index.y, x + index.x] != '#')
							{
								containsMonster = false;
								break;
							}
						}
						if (containsMonster)
						{
							monsters++;
						}
					}

					
				}
			}

			Console.WriteLine(hashes-15*monsters);
		}

		private static bool CheckDuplicate(char[,] tile1, char[,] tile2)
		{
			for (int x = 0; x < tile1.GetLength(0); x++)
			{
				for (int y = 0; y < tile1.GetLength(0); y++)
				{
					if (tile1[x, y] != tile2[x, y])
					{
						return false;
					}
				}
			}
			return true;
		}

		private static void Solve(int x, int y, HashSet<int> usedTiles, Dictionary<(int x, int y), (int tileId, int tileIndex, char[,] tile)> currentBoard)
		{
			if (usedTiles.Count > maxDepth)
			{
				maxDepth = usedTiles.Count;
			}
			if (y == imageSize)
			{
				Board = currentBoard;
				return;
			}
			int nextX = x;
			int nextY = y;

			if (x + 1 >= imageSize)
			{
				nextY++;
				nextX = 0;
			}
			else
			{
				nextX++;
			}
			foreach (var kvp in tiles)
			{
				if (usedTiles.Contains(kvp.Key))
				{
					continue;
				}
				int index = 0;
				foreach (var tile in kvp.Value)
				{
					bool aligns = true;
					//Check if aligns with right border of last tileX
					if (x != 0 && !Alignment.Contains((currentBoard[(x - 1, y)].tileId, currentBoard[(x - 1, y)].tileIndex, kvp.Key, index, false)))
					{
						aligns = CheckAlignment(currentBoard[(x - 1, y)].tile, tile, false, true, false, false, currentBoard[(x - 1, y)].tileId, currentBoard[(x - 1, y)].tileIndex, kvp.Key, index);
					}
					//Check if aligns with bottom border of last tileY
					if (y != 0 && !Alignment.Contains((currentBoard[(x, y - 1)].tileId, currentBoard[(x, y - 1)].tileIndex, kvp.Key, index, true)))
					{
						aligns = CheckAlignment(currentBoard[(x, y - 1)].tile, tile, true, false, false, false, currentBoard[(x, y - 1)].tileId, currentBoard[(x, y - 1)].tileIndex, kvp.Key, index);
					}

					if (aligns)
					{
						HashSet<int> newUsedTiles = new HashSet<int>(usedTiles);
						newUsedTiles.Add(kvp.Key);
						var newBoard = new Dictionary<(int x, int y), (int tileId, int tileIndex, char[,] tile)>(currentBoard);
						newBoard[(x, y)] = (kvp.Key, index, tile);
						Solve(nextX, nextY, newUsedTiles, newBoard);
					}
					index++;
				}
			}
		}

		private static bool CheckAlignment(char[,] tile1, char[,] tile2, bool top, bool left, bool right, bool bottom, int id1, int index1, int id2, int index2)
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

			if (aligns)
			{
				Alignment.Add((id1, index1, id2, index2, top));
			}

			return aligns;
		}

		private static void PrintTile(char[,] tile)
		{
			for (int j = 0; j < tile.GetLength(0); j++)
			{
				Console.WriteLine();
				for (int k = 0; k < tile.GetLength(1); k++)
				{
					Console.Write(tile[j, k]);
				}
			}
			Console.WriteLine();
		}

		private static char[,] RotateTile(char[,] tile, int amount, int size)
		{
			char[,] result = new char[size, size];
			if (amount == 3)
			{
				for (int y = 0; y < size; y++)
				{
					for (int x = 0; x < size; x++)
					{
						result[size - 1 - y, x] = tile[x, y];
					}
				}
			}
			else if (amount == 2)
			{
				result = FlipTileY(FlipTileX(tile, size), size);
			}
			else if (amount == 1)
			{
				for (int y = 0; y < size; y++)
				{
					for (int x = 0; x < size; x++)
					{
						result[y, size - 1 - x] = tile[x, y];
					}
				}
			}
			return result;
		}

		private static char[,] FlipTileY(char[,] tile, int size)
		{
			char[,] result = new char[size, size];
			for (int y = 0; y < size; y++)
			{
				for (int x = 0; x < size; x++)
				{
					result[size - 1 - y, x] = tile[y, x];
				}
			}
			return result;
		}

		private static char[,] FlipTileX(char[,] tile, int size)
		{
			char[,] result = new char[size, size];
			for (int y = 0; y < size; y++)
			{
				for (int x = 0; x < size; x++)
				{
					result[y, size - 1 - x] = tile[y, x];
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
