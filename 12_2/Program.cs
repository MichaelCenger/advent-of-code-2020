using System;
using System.Collections.Generic;
using System.IO;

namespace _12_2
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
			int waypointX = 10;
			int waypointY = 1;
			int x = 0;
			int y = 0;
			Facing CurrentFacing = Facing.East;
			for (int i = 0; i < input.Count; i++)
			{
				char command = input[i][0];
				int value = Convert.ToInt32(input[i].Substring(1, input[i].Length - 1));
				switch (command)
				{
					case 'N':
						waypointY += value;
						break;
					case 'E':
						waypointX += value;
						break;
					case 'S':
						waypointY -= value;
						break;
					case 'W':
						waypointX -= value;
						break;
					case 'R':
					case 'L':
						if (command == 'L')
						{
							value = 360 - value;
						}
						if (value == 90)
						{
							int startX = waypointX;
							waypointX = waypointY;
							waypointY = -startX;
						}

						if (value == 180)
						{
							waypointX = -waypointX;
							waypointY = -waypointY;
						}

						if (value == 270)
						{
							int startX = waypointX;
							waypointX = -waypointY;
							waypointY = startX;
						}
						break;
					case 'F':
						x += waypointX * value;
						y += waypointY * value;
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
