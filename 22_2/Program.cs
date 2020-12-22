using System;
using System.Collections.Generic;
using System.IO;

namespace _22_2
{
	class Program
	{
		static int gameCount = 1;
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


			int currentGameCount = gameCount;
			int roundCount = 0;
			List<List<int>> previousDecksPlayer1;
			List<List<int>> previousDecksPlayer2;
			previousDecksPlayer1 = new List<List<int>>();
			previousDecksPlayer2 = new List<List<int>>();
			while (!(player1Cards.Count == 0 || player2Cards.Count == 0))
			{
				if (previousDecksPlayer1.Count > 0 && CheckPrematureEnd(previousDecksPlayer1, previousDecksPlayer2, player1Cards, player2Cards))
				{
					break;
				}
				previousDecksPlayer1.Add(new List<int>(player1Cards));
				previousDecksPlayer2.Add(new List<int>(player2Cards));
				//Console.WriteLine($"Round {roundCount} (Game {currentGameCount})");
				//Console.WriteLine($"Player1 deck: {DeckToString(player1Cards)} ");
				//Console.WriteLine($"Player2 deck: {DeckToString(player2Cards)} ");
				int card1 = player1Cards[0];
				int card2 = player2Cards[0];

				if (player1Cards.Count - 1 >= card1 && player2Cards.Count - 1 >= card2)
				{
					List<int> recursiveDeckPlayer1 = new List<int>();
					for (int i = 0; i < card1; i++)
					{
						recursiveDeckPlayer1.Add(player1Cards[i + 1]);
					}
					List<int> recursiveDeckPlayer2 = new List<int>();
					for (int i = 0; i < card2; i++)
					{
						recursiveDeckPlayer2.Add(player2Cards[i + 1]);
					}
					int recursiveWinner = PlayGame(recursiveDeckPlayer1, recursiveDeckPlayer2);

					player1Cards.RemoveAt(0);
					player2Cards.RemoveAt(0);
					if (recursiveWinner == 1)
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
				else
				{
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
				roundCount++;
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

		private static int PlayGame(List<int> deckPlayer1, List<int> deckPlayer2)
		{
			gameCount++;


			int roundCount = 0;
			int currentGameCount = gameCount;
			List<List<int>> previousDecksPlayer1;
			List<List<int>> previousDecksPlayer2;
			previousDecksPlayer1 = new List<List<int>>();
			previousDecksPlayer2 = new List<List<int>>();
			while (!(deckPlayer1.Count == 0 || deckPlayer2.Count == 0))
			{
				if (previousDecksPlayer1.Count > 0 && CheckPrematureEnd(previousDecksPlayer1, previousDecksPlayer2, deckPlayer1, deckPlayer2))
				{
					return 1;
				}
				previousDecksPlayer1.Add(new List<int>(deckPlayer1));
				previousDecksPlayer2.Add(new List<int>(deckPlayer2));
				//Console.WriteLine($"Round {roundCount} (Game {currentGameCount})");
				//Console.WriteLine($"Player1 deck: {DeckToString(deckPlayer1)} ");
				//Console.WriteLine($"Player2 deck: {DeckToString(deckPlayer2)} ");
				int card1 = deckPlayer1[0];
				int card2 = deckPlayer2[0];

				if (deckPlayer1.Count - 1 >= card1 && deckPlayer2.Count - 1 >= card2)
				{
					List<int> recursiveDeckPlayer1 = new List<int>();
					for (int i = 0; i < card1; i++)
					{
						recursiveDeckPlayer1.Add(deckPlayer1[i + 1]);
					}
					List<int> recursiveDeckPlayer2 = new List<int>();
					for (int i = 0; i < card2; i++)
					{
						recursiveDeckPlayer2.Add(deckPlayer2[i + 1]);
					}
					int recursiveWinner = PlayGame(recursiveDeckPlayer1, recursiveDeckPlayer2);

					deckPlayer1.RemoveAt(0);
					deckPlayer2.RemoveAt(0);
					if (recursiveWinner == 1)
					{
						deckPlayer1.Add(card1);
						deckPlayer1.Add(card2);
					}
					else
					{
						deckPlayer2.Add(card2);
						deckPlayer2.Add(card1);
					}
				}
				else
				{
					deckPlayer1.RemoveAt(0);
					deckPlayer2.RemoveAt(0);
					if (card1 > card2)
					{
						deckPlayer1.Add(card1);
						deckPlayer1.Add(card2);
					}
					else
					{
						deckPlayer2.Add(card2);
						deckPlayer2.Add(card1);
					}
				}

				roundCount++;
			}
			if (deckPlayer1.Count > 0)
			{
				return 1;
			}
			else
			{
				return 2;
			}

		}

		private static string DeckToString(List<int> deck)
		{
			string result = "";
			foreach (var card in deck)
			{
				result += card.ToString() + ", ";
			}
			return result;
		}

		private static bool CheckPrematureEnd(List<List<int>> previousDecksPlayer1, List<List<int>> previousDecksPlayer2, List<int> deckPlayer1, List<int> deckPlayer2)
		{
			for (int i = 0; i < previousDecksPlayer1.Count; i++)
			{
				if (deckPlayer1.Count == previousDecksPlayer1[i].Count && deckPlayer2.Count == previousDecksPlayer2[i].Count)
				{
					bool same1 = true;
					for (int j = 0; j < previousDecksPlayer1[i].Count; j++)
					{
						if (previousDecksPlayer1[i][j] != deckPlayer1[j])
						{
							same1 = false;
						}
					}
					bool same2 = true;
					for (int k = 0; k < previousDecksPlayer2[i].Count; k++)
					{
						if (previousDecksPlayer2[i][k] != deckPlayer2[k])
						{
							same2 = false;
						}
					}
					if (same1 && same2)
					{
						return true;
					}
				}
			}
			return false;
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
