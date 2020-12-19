using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _19_1
{
	class Program
	{
		static HashSet<int> alreadyParsedRules = new HashSet<int>();
		static Dictionary<int, List<string>> rules = new Dictionary<int, List<string>>();
		static void Main(string[] args)
		{
			List<string> input = ReadInput();
			int aIndex = 26;
			int bIndex = 16;
			int inputSeperator = 133;

			for (int i = 0; i < inputSeperator; i++)
			{
				int ruleIndex = Convert.ToInt32(input[i].Split(":")[0]);
				rules[ruleIndex] = new List<string>();
				rules[ruleIndex].Add(input[i].Substring(input[i].IndexOf(" ") + 1, input[i].Length - input[i].IndexOf(" ") - 1).Replace("\"", ""));
			}

			rules[aIndex].Insert(0, "");
			rules[bIndex].Insert(0, "");
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
					var split = rule.Value[0].Split(" ");
					foreach (var ruleIndexString in split)
					{
						if (ruleIndexString.Contains("|"))
						{
							continue;
						}
						int ruleIndex = Convert.ToInt32(ruleIndexString);
						if (!alreadyParsedRules.Contains(ruleIndex))
						{
							skipRule = true;
							break;
						}
					}
					if (skipRule)
					{
						continue;
					}
					List<string> ruleStrings = new List<string>();
					if (rule.Value[0].Contains("|"))
					{
						ruleStrings.AddRange(rule.Value[0].Split("|"));
					}
					else
					{
						ruleStrings.Add(rule.Value[0]);
					}
					rule.Value.AddRange(GetStringFromRule(ruleStrings));
					alreadyParsedRules.Add(rule.Key);
				}
			}

			int matchingCount = 0;
			for (int i = inputSeperator; i < input.Count; i++)
			{
				foreach(var rule in rules)
				{
					for(int j = 1; j < rule.Value.Count; j++)
					{
						if (rule.Value[j] == input[i])
						{
							matchingCount++;
						}
					}
				}
			}
			Console.WriteLine(matchingCount);
		}

		private static List<string> GetStringFromRule(List<string> stringRules)
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
				GetPermutations("", 0, targetDepth, indices, 0, permutations);
				result.AddRange(permutations);
			}
			return result;
		}

		public static void GetPermutations(string currentString, int currentDepth, int targetDepth, List<int> indices, int currentIndex, List<string> result)
		{
			if (currentDepth > targetDepth)
			{
				result.Add(currentString);
				return;
			}
			bool first = true;
			foreach (string possibility in rules[indices[currentIndex]])
			{
				if (first)
				{
					first = false;
					continue;
				}
				string s = currentString;
				s += possibility;
				GetPermutations(s, currentDepth + 1, targetDepth, indices, currentIndex + 1, result);
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
