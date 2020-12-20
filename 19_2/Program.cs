using System;
using System.Collections.Generic;
using System.IO;

namespace _19_2
{
	class Program
	{
		class Rule
		{
			public string ruleString;
			public HashSet<string> rules = new HashSet<string>();
		}
		static HashSet<int> alreadyParsedRules = new HashSet<int>();
		static Dictionary<int, Rule> rules = new Dictionary<int, Rule>();
		static int maxLength;
		static int stopEight = 0;
		static int stopEleven = 0;
		static List<string> input;

		static int inputSeperator = 133;
		static void Main(string[] args)
		{
			input = ReadInput();
			int aIndex = 26;
			int bIndex = 16;

			for (int i = 0; i < inputSeperator; i++)
			{
				int ruleIndex = Convert.ToInt32(input[i].Split(":")[0]);
				rules[ruleIndex] = new Rule();
				rules[ruleIndex].ruleString = input[i].Substring(input[i].IndexOf(" ") + 1, input[i].Length - input[i].IndexOf(" ") - 1).Replace("\"", "");
			}

			for (int i = inputSeperator; i < input.Count; i++)
			{
				int currentLength = input[i].Length;
				if (currentLength > maxLength)
				{
					maxLength = currentLength;
				}
			}

			rules[aIndex].rules.Add("a");
			rules[bIndex].rules.Add("b");
			alreadyParsedRules.Add(aIndex);
			alreadyParsedRules.Add(bIndex);

			while (alreadyParsedRules.Count != inputSeperator)
			{
				foreach (var rule in rules)
				{
					if (alreadyParsedRules.Contains(rule.Key))
					{
						continue;
					}
					bool skipRule = false;
					var split = rule.Value.ruleString.Split(" ");
					foreach (var ruleIndexString in split)
					{
						if (ruleIndexString.Contains("|"))
						{
							continue;
						}
						int ruleIndex = Convert.ToInt32(ruleIndexString);
						if (!alreadyParsedRules.Contains(ruleIndex))
						{
							if (ruleIndex != rule.Key)
							{
								skipRule = true;
								break;
							}
						}
					}
					if (skipRule)
					{
						continue;
					}
					List<string> ruleStrings = new List<string>();
					if (rule.Value.ruleString.Contains("|"))
					{
						ruleStrings.AddRange(rule.Value.ruleString.Split("|"));
					}
					else
					{
						ruleStrings.Add(rule.Value.ruleString);
					}
					if (rule.Key == 8)
					{
						stopEight++;
					}
					else if (rule.Key == 11)
					{
						stopEleven++;
					}
					foreach (var r in GetStringFromRule(ruleStrings, rule.Key))
					{
						rule.Value.rules.Add(r);
					}
					if (rule.Key != 8 && rule.Key != 11)
					{
						alreadyParsedRules.Add(rule.Key);
					}
					else
					{
						if (stopEight > 6 && rule.Key == 8 || stopEleven > 6 && rule.Key == 11)
						{
							alreadyParsedRules.Add(rule.Key);
						}
					}
				}
			}

			int matchingCount = 0;
			for (int i = inputSeperator; i < input.Count; i++)
			{
				foreach (var rule in rules)
				{
					foreach (var r in rule.Value.rules)
					{
						if (r == input[i])
						{
							matchingCount++;
							Console.WriteLine(r);
						}
					}
				}
			}
			Console.WriteLine(matchingCount);
		}

		private static List<string> GetStringFromRule(List<string> stringRules, int ruleIndex)
		{
			List<string> result = new List<string>();
			foreach (string ruleString in stringRules)
			{
				string trimmedRuleString = ruleString.Trim();
				List<string> permutations = new List<string>();
				int targetDepth = trimmedRuleString.Split(" ").Length - 1;
				List<int> indices = new List<int>();
				foreach (string s in trimmedRuleString.Split(" "))
				{
					indices.Add(Convert.ToInt32(s));
				}
				GetPermutations("", 0, targetDepth, indices, 0, permutations, ruleIndex);
				result.AddRange(permutations);
			}
			return result;
		}

		public static void GetPermutations(string currentString, int currentDepth, int targetDepth, List<int> indices, int currentIndex, List<string> result, int ruleIndex)
		{
			if (currentDepth > targetDepth)
			{
				result.Add(currentString);
				return;
			}

			if ((ruleIndex == 8 || ruleIndex == 11) && rules[indices[currentIndex]].rules.Count == 0)
			{
				string s = currentString;
				if (s.Length > maxLength)
				{
					return;
				}
				bool contains = false;
				for (int i = inputSeperator; i < input.Count; i++)
				{
					if (input[i].Contains(s))
					{
						contains = true;
						break;
					}
				}
				if (contains)
				{
					GetPermutations(s, currentDepth + 1, targetDepth, indices, currentIndex + 1, result, ruleIndex);
				}
			}
			foreach (string possibility in rules[indices[currentIndex]].rules)
			{
				string s = currentString;
				s += possibility;
				if (s.Length > maxLength)
				{
					return;
				}
				bool contains = false;
				for (int i = inputSeperator; i < input.Count; i++)
				{
					if (input[i].Contains(s))
					{
						contains = true;
						break;
					}
				}
				if (contains)
				{
					GetPermutations(s, currentDepth + 1, targetDepth, indices, currentIndex + 1, result, ruleIndex);
				}
			}
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
