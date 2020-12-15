using System;
using System.Collections.Generic;
using System.IO;

namespace _15_2
{
	class Program
	{
		static void Main(string[] args)
		{
			List<int> input = ReadInput();
			long lastSpokenNumber = -1;
			Dictionary<long, long> lastSpoken = new Dictionary<long, long>();
			Dictionary<long, long> lastSpokenBefore = new Dictionary<long, long>();
			long currentTurn = 1;
			for (int i = 0; i < input.Count; i++)
			{
				lastSpoken[input[i]] = currentTurn;
				currentTurn++;
				lastSpokenNumber = input[i];
			}

			while (currentTurn != 30000001)
			{
				long numberToSpeak = -1;
				if (lastSpokenBefore.ContainsKey(lastSpokenNumber))
				{
					numberToSpeak = lastSpoken[lastSpokenNumber] - lastSpokenBefore[lastSpokenNumber];
				}
				else
				{
					numberToSpeak = 0;
				}
				if (lastSpoken.ContainsKey(numberToSpeak))
				{
					lastSpokenBefore[numberToSpeak] = lastSpoken[numberToSpeak];
				}
				lastSpoken[numberToSpeak] = currentTurn;
				lastSpokenNumber = numberToSpeak;
				currentTurn++;

			}
			Console.WriteLine(lastSpokenNumber);
		}

		private static List<int> ReadInput()
		{
			List<int> input = new List<int>();
			string[] lines = File.ReadAllLines(@"input.txt");
			var split = lines[0].Split(",");
			foreach (string number in split)
			{
				input.Add(Convert.ToInt32(number));
			}
			return input;
		}
	}
}
