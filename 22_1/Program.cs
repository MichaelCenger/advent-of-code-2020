using System;
using System.Collections.Generic;
using System.IO;

namespace _22_1
{
	class Program
	{
		static void Main(string[] args)
		{
			int player1Index = 1;
			int player2Index = 28;
			List<int> player1Cards = new List<int>();
			List<int> player2Cards = new List<int>();
			List<string> input = ReadInput();



			for (int i = player1Index; i < input.Count; i++)
			{
				if (input[i] == "")
				{
					break;
				}
				player1Cards.Add(Convert.ToInt32(input[i]));
			}

			for (int i = player2Index; i < input.Count; i++)
			{
				player2Cards.Add(Convert.ToInt32(input[i]));
			}

			while (!(player1Cards.Count == 0 || player2Cards.Count == 0))
			{
				int card1 = player1Cards[0];
				int card2 = player2Cards[0];
				player1Cards.RemoveAt(0);
				player2Cards.RemoveAt(0);
				if (card1 > card2)
				{
					player1Cards.Add(card1);
					player1Cards.Add(card2);
				}
				else
				{
					player2Cards.Add(card2);
					player2Cards.Add(card1);
				}
			}
			int result = 0;
			for (int i = 0; i < player1Cards.Count; i++)
			{
				result += player1Cards[i] * (player1Cards.Count - i);
			}
			for (int i = 0; i < player2Cards.Count; i++)
			{
				result += player2Cards[i] * (player2Cards.Count - i);
			}
			Console.WriteLine(result);
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
