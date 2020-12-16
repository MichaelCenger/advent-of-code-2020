using System;
using System.Collections.Generic;
using System.IO;

namespace _16_2
{
	class Program
	{
		static string[] Fields = new string[20];
		public struct Rule
		{
			public long Min1;
			public long Max1;
			public long Min2;
			public long Max2;
			public string Name;
		}

		public static bool valid;

		public static Dictionary<Rule, HashSet<int>> LookupTable = new Dictionary<Rule, HashSet<int>>();
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
				Rule rule;
				var split = ruleString.Split(" ");
				rule.Name = ruleString.Split(":")[0];
				rule.Min1 = Convert.ToInt64(split[^3].Split("-")[0]);
				rule.Max1 = Convert.ToInt64(split[^3].Split("-")[1]);
				rule.Min2 = Convert.ToInt64(split[^1].Split("-")[0]);
				rule.Max2 = Convert.ToInt64(split[^1].Split("-")[1]);
				rules.Add(rule);
			}

			List<List<long>> validNearbyTickets = new List<List<long>>();
			foreach (List<long> nearbyTicket in nearbyTickets)
			{
				bool invalid = false;
				foreach (long value in nearbyTicket)
				{
					if (CheckRules(value, rules) != -1)
					{
						invalid = true;
					}
				}
				if (!invalid)
				{
					validNearbyTickets.Add(nearbyTicket);
				}
			}

			for (int i = 0; i < validNearbyTickets[0].Count; i++)
			{
				foreach (Rule rule in rules)
				{
					bool valid = true;
					foreach (var ticket in validNearbyTickets)
					{
						long currentValue = ticket[i];
						if ((currentValue >= rule.Min1 && currentValue <= rule.Max1) || (currentValue >= rule.Min2 && currentValue <= rule.Max2))
						{

						}
						else
						{
							valid = false;
							break;
						}
					}
					if (valid)
					{
						if (!LookupTable.ContainsKey(rule))
						{
							LookupTable[rule] = new HashSet<int>();
						}
						LookupTable[rule].Add(i);
					}
				}
			}

			for (int i = 0; i < 20; i++)
			{
				int indexToRemove = 0;
				foreach (var entry in LookupTable)
				{
					if (entry.Value.Count == 1)
					{
						Fields[entry.Value.ToList()[0]] = entry.Key.Name;
						indexToRemove = entry.Value.ToList()[0];
					}
				}
				foreach (var entry in LookupTable)
				{
					entry.Value.Remove(indexToRemove);
				}
			}

			int myTicketLineIndex = 23;
			ulong result = 1;
			int count = 0;
			for (int i = 0; i < Fields.Length; i++)
			{
				if (Fields[i].Contains("departure"))
				{
					ulong value = Convert.ToUInt64(input[myTicketLineIndex - 1].Split(",")[i]);
					result *= value;
					count++;
				}
			}

			Console.WriteLine(result);
		}

		private static long CheckRules(long value, List<Rule> rules)
		{
			foreach (Rule rule in rules)
			{
				if ((value >= rule.Min1 && value <= rule.Max1) || (value >= rule.Min2 && value <= rule.Max2))
				{
					return -1;
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
