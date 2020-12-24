using System;
using System.Collections.Generic;
using System.IO;

namespace _24_1
{
	class Tile
	{
		public Tile East;
		public Tile SouthEast;
		public Tile SouthWest;
		public Tile West;
		public Tile NorthWest;
		public Tile NorthEast;
		public bool Black = false;
	}
	class Program
	{
		static void Main(string[] args)
		{
			List<string> input = ReadInput();
			int gridSize = 100;
			Tile[,] tiles = new Tile[gridSize, gridSize];
			for (int y = 0; y < gridSize; y++)
			{
				Tile lastTile = null;
				for (int x = 0; x < gridSize; x++)
				{
					Tile tile = new Tile();
					tiles[x, y] = tile;
					if (x != 0)
					{
						lastTile.East = tile;
						tile.West = lastTile;
					}
					if (y != 0)
					{
						if (y % 2 == 0)
						{
							tile.NorthEast = tiles[x, y - 1];
							tiles[x, y - 1].SouthWest = tile;
							if (x != 0)
							{
								tile.NorthWest = tiles[x - 1, y - 1];
								tiles[x - 1, y - 1].SouthEast = tile;
							}
						}
						else
						{
							tile.NorthWest = tiles[x, y - 1];
							tiles[x, y - 1].SouthEast = tile;
							if (x != gridSize - 1)
							{
								tile.NorthEast = tiles[x + 1, y - 1];
								tiles[x + 1, y - 1].SouthWest = tile;
							}
						}
					}
					lastTile = tile;
				}
			}

			HashSet<Tile> blackTiles = new HashSet<Tile>();
			Tile origin = tiles[gridSize / 2, gridSize / 2];
			for (int i = 0; i < input.Count; i++)
			{
				List<string> instructions = new List<string>();
				for (int j = 0; j < input[i].Length;)
				{
					string ins = input[i][j].ToString();
					if (ins != "e" && ins != "w")
					{
						j++;
						ins += input[i][j];
					}
					instructions.Add(ins);
					j++;
				}

				Tile currentTile = origin;
				foreach (var instruction in instructions)
				{
					switch (instruction)
					{
						case "e":
							currentTile = currentTile.East;
							break;
						case "se":
							currentTile = currentTile.SouthEast;
							break;
						case "sw":
							currentTile = currentTile.SouthWest;
							break;
						case "w":
							currentTile = currentTile.West;
							break;
						case "nw":
							currentTile = currentTile.NorthWest;
							break;
						case "ne":
							currentTile = currentTile.NorthEast;
							break;
					}
				}
				if (blackTiles.Contains(currentTile))
				{
					blackTiles.Remove(currentTile);
				}
				else
				{
					blackTiles.Add(currentTile);
				}
			}
			Console.WriteLine(blackTiles.Count);
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
