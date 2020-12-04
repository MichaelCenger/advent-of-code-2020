using System;
using System.Collections.Generic;
using System.IO;

namespace _04_1
{
	class Program
	{
		static void Main(string[] args)
		{
			List<string> input = ReadInput();

			List<string> currentPassport = new List<string>();
			int validPassportCount = 0;
			for (int i = 0; i < input.Count; i++)
			{
				if (String.IsNullOrEmpty(input[i]))
				{
					if (CheckPassport(currentPassport))
					{
						validPassportCount++;
					}
					currentPassport = new List<string>();
					continue;
				}
				currentPassport.Add(input[i]);
			}
			if (CheckPassport(currentPassport))
			{
				validPassportCount++;
			}

			Console.WriteLine(validPassportCount);
		}

		private static bool CheckPassport(List<string> passportData)
		{
			Dictionary<string, bool> passportFields = new Dictionary<string, bool>();
			passportFields["byr"] = false;
			passportFields["iyr"] = false;
			passportFields["eyr"] = false;
			passportFields["hgt"] = false;
			passportFields["hcl"] = false;
			passportFields["ecl"] = false;
			passportFields["pid"] = false;

			bool isValid = true;
			foreach (string line in passportData)
			{
				string[] splitData = line.Split(' ');
				foreach(string data in splitData)
				{
					string key = data.Substring(0, 3);
					if (passportFields.ContainsKey(key))
					{
						passportFields[key] = true;
					}
				}
			}
			foreach (var field in passportFields)
			{
				if (field.Value == false)
				{
					isValid = false;
				}
			}
			return isValid;
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
