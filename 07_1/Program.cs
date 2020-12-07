using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _07_1
{
	class Program
	{
		static Dictionary<string, List<string>> bags = new Dictionary<string, List<string>>();
		static void Main(string[] args)
		{
			List<string> input = ReadInput();
			for (int i = 0; i < input.Count; i++)
			{
				bags[input[i].Split("bags")[0].Trim()] = GetContents(input[i]);
			}

			int sum = 0;
			foreach (var kvp in bags)
			{
				if (SearchBag(kvp.Key))
				{
					sum++;
				}
			}
			Console.WriteLine(sum);

		}

		private static bool SearchBag(string bag)
		{
			bool correct = false;
			List<string> contents = bags[bag];
			if (contents.Contains("shiny gold"))
			{
				return true;
			}
			foreach (string content in contents)
			{
				if (SearchBag(content))
				{
					correct = true;
				}
			}
			return correct;
		}

		private static List<string> GetContents(string input)
		{
			List<string> result = new List<string>();
			string contents = input.Split("contain")[1];
			string[] singleBags = contents.Split(',');
			foreach (string bag in singleBags)
			{
				string trimmedBag = bag.Trim();
				int end = trimmedBag.IndexOf("bag") - 1;
				int start = trimmedBag.IndexOf(' ') + 1;
				string bagColor = trimmedBag.Substring(start, end - start);
				if (bagColor != "other")
					result.Add(bagColor);
			}
			return result;
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
