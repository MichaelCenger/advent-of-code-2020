using System;
using System.Collections.Generic;
using System.IO;

namespace _07_2
{
	class Program
	{
		static Dictionary<string, List<string>> bags = new Dictionary<string, List<string>>();
		static int count = -1;
		static void Main(string[] args)
		{
			List<string> input = ReadInput();
			for (int i = 0; i < input.Count; i++)
			{
				bags[input[i].Split("bags")[0].Trim()] = GetContents(input[i]);
			}
			CountChildren("shiny gold");
			Console.WriteLine(count);

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
				{
					int bagCount = Convert.ToInt32(trimmedBag[0].ToString());
					for (int i = 0; i < bagCount; i++)
					{
						result.Add(bagColor);
					}
				}
			}
			return result;
		}

		private static void CountChildren(string input)
		{
			count++;
			List<string> contents = bags[input];
			foreach (string content in contents)
			{
				CountChildren(content);
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
