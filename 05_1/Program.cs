using System;
using System.Collections.Generic;
using System.IO;

namespace _05_1
{
	class Program
	{
		static void Main(string[] args)
		{
			List<string> input = ReadInput();
			int highestId = 0;
			foreach(string line in input)
			{
				string rowDescription = line.Substring(0, 7);
				string columnDescription = line.Substring(7, line.Length-7);
				int row = Partition(0,127, rowDescription);
				int column = Partition(0,7, columnDescription);
				int id = row * 8 + column;
				if(id > highestId)
				{
					highestId = id;
				}
			}
			Console.WriteLine(highestId);
		}

		private static int Partition(int min, int max, string partitioningString)
		{
			return PartitionRec(min, max, partitioningString);
		}

		private static int PartitionRec(int min, int max, string partitioningString)
		{
			char currentPartitioningCharacter = partitioningString[0];
			string newPartitioningString = partitioningString.Substring(1, partitioningString.Length - 1);
			int newMin = 0, newMax = 0;
			int half = (max-min) / 2;
			if(currentPartitioningCharacter == 'F' || currentPartitioningCharacter == 'L')
			{
				newMin = min;
				newMax = min+half;
			}
			else if (currentPartitioningCharacter == 'B' || currentPartitioningCharacter == 'R')
			{
				newMin = min+half+1;
				newMax = max;
			}
			else
			{
				return -1;
			}

			if(newPartitioningString.Length == 0)
			{
				//Doesnt matter which value is returned, as both newMin and new Ma contain the same (final) number 
				return newMin;
			}
			return PartitionRec(newMin, newMax, newPartitioningString);
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
