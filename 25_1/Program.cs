using System;
using System.Collections.Generic;
using System.IO;

namespace _25_1
{
	class Program
	{
		static void Main(string[] args)
		{
			List<string> input = ReadInput();
			long cardPublicKey = Convert.ToInt64(input[0]);

			int subjectNumber = 7;

			long cardValue = 1;
			int cardLoopSize = 0;
			while (cardValue != cardPublicKey)
			{
				cardValue *= subjectNumber;
				cardValue = cardValue % 20201227;
				cardLoopSize++;
			}
			Console.WriteLine(cardLoopSize);

			long doorPublicKey = Convert.ToInt64(input[1]);
			long doorValue = 1;
			int doorLoopSize = 0;

			while (doorValue != doorPublicKey)
			{
				doorValue *= subjectNumber;
				doorValue = doorValue % 20201227;
				doorLoopSize++;
			}
			Console.WriteLine(doorLoopSize);

			cardValue = 1;
			for(int i = 0; i < cardLoopSize; i++)
			{
				cardValue *= doorPublicKey;
				cardValue = cardValue % 20201227;
			}

			Console.WriteLine(cardValue);

			doorValue = 1;
			for (int i = 0; i < doorLoopSize; i++)
			{
				doorValue *= cardPublicKey;
				doorValue = doorValue % 20201227;
			}

			Console.WriteLine(doorValue);
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
