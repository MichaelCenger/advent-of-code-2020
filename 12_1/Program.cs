using System;
using System.Collections.Generic;
using System.IO;

namespace _12_1
{
	class Program
	{
		enum Facing
		{
			East = 0,
			South = 90,
			West = 180,
			North = 270
		}
		static void Main(string[] args)
		{
			List<string> input = ReadInput();
			int x = 0;
			int y = 0;
			int rotation = 0;
			Facing CurrentFacing = Facing.East;
			for (int i = 0; i < input.Count; i++)
			{
				char command = input[i][0];
				int value = Convert.ToInt32(input[i].Substring(1, input[i].Length - 1));
				switch (command)
				{
					case 'N':
						y += value;
						break;
					case 'E':
						x += value;
						break;
					case 'S':
						y -= value;
						break;
					case 'W':
						x -= value;
						break;
					case 'R':
						rotation = (rotation + value) % 360;
						CurrentFacing = (Facing)rotation;
						break;
					case 'L':
						rotation = (360+(rotation - value))%360;
						CurrentFacing = (Facing)rotation;
						break;
					case 'F':
						switch (CurrentFacing)
						{
							case Facing.East:
								x += value;
								break;
							case Facing.South:
								y -= value;
								break;
							case Facing.West:
								x -= value;
								break;
							case Facing.North:
								y += value;
								break;
						}
						break;
				}
			}

			Console.WriteLine(Math.Abs(x) + Math.Abs(y));
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
