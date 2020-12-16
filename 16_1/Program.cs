using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _16_1
{
	class Program
	{
		struct Rule
		{
			public long Min;
			public long Max;
		}
		static void Main(string[] args)
		{
			List<string> input = ReadInput();
			int startLineNearbyTickets = 26;
			List<List<long>> nearbyTickets = new List<List<long>>();
			for (int i = startLineNearbyTickets - 1; i < input.Count; i++)
			{
				string currentTicket = input[i];
				var split = currentTicket.Split(",");
				List<long> ticketValues = new List<long>();
				foreach (string s in split)
				{
					ticketValues.Add(Convert.ToInt32(s));
				}
				nearbyTickets.Add(ticketValues);
			}

			List<Rule> rules = new List<Rule>();
			int ruleEndIndex = 20;
			for (int i = 0; i < ruleEndIndex; i++)
			{
				string ruleString = input[i];
				Rule firstRule, secondRule;
				var split = ruleString.Split(" ");
				firstRule.Min = Convert.ToInt64(split[^3].Split("-")[0]);
				firstRule.Max = Convert.ToInt64(split[^3].Split("-")[1]);
				secondRule.Min = Convert.ToInt64(split[^1].Split("-")[0]);
				secondRule.Max = Convert.ToInt64(split[^1].Split("-")[1]);
				rules.Add(firstRule);
				rules.Add(secondRule);
			}

			long ticketError = 0;
			List<List<long>> validNearbyTickets = new List<List<long>>();
			List<List<long>> invalidNearbyTickets = new List<List<long>>();
			foreach (List<long> nearbyTicket in nearbyTickets)
			{
				foreach (long value in nearbyTicket)
				{
					ticketError += CheckRule(value, rules);
				}
			}
			Console.WriteLine(ticketError);
		}

		private static long CheckRule(long value, List<Rule> rules)
		{
			
			foreach(Rule rule in rules)
			{
				if (value >= rule.Min && value <= rule.Max)
				{
					return 0;
				}
			}
			return value;
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
