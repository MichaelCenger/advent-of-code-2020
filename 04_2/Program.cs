using System;
using System.Collections.Generic;
using System.IO;

namespace _04_2
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
				foreach (string data in splitData)
				{
					string key = data.Substring(0, 3);
					string dataValue = data.Substring(4, data.Length - 4);
					if (passportFields.ContainsKey(key))
					{
						if (key == "byr")
						{
							int year = Convert.ToInt32(dataValue);
							if (year <= 2002 && year >= 1920)
							{
								passportFields[key] = true;
							}
						}
						if (key == "iyr")
						{
							int year = Convert.ToInt32(dataValue);
							if (year <= 2020 && year >= 2010)
							{
								passportFields[key] = true;
							}
						}
						if (key == "eyr")
						{
							int year = Convert.ToInt32(dataValue);
							if (year <= 2030 && year >= 2020)
							{
								passportFields[key] = true;
							}
						}
						if (key == "hgt")
						{
							string unit = dataValue.Substring(dataValue.Length - 2, 2);
							if (unit == "in" || unit == "cm")
							{
								int height = Convert.ToInt32(dataValue.Substring(0, dataValue.Length - 2));
								if (unit == "in" && height >= 59 && height <= 76)
								{
									passportFields[key] = true;
								}
								if (unit == "cm" && height >= 150 && height <= 193)
								{
									passportFields[key] = true;
								}
							}
						}
						if (key == "hcl")
						{
							if (dataValue[0] == '#' && dataValue.Length == 7)
							{
								bool valid = true;
								foreach (char character in dataValue)
								{
									if (!(char.IsDigit(character) || character == 'a' || character == 'b' || character == 'c' || character == 'd' || character == 'e' || character == 'f' || character == '#'))
									{
										valid = false;
										continue;
									}
								}
								passportFields[key] = valid;
							}
						}
						if (key == "ecl")
						{
							bool valid = false;
							switch (dataValue)
							{
								case "amb":
								case "blu":
								case "brn":
								case "gry":
								case "grn":
								case "hzl":
								case "oth":
									valid = true;
									break;
							}
							passportFields[key] = valid;
						}
						if (key == "pid")
						{
							if (dataValue.Length == 9)
							{
								passportFields[key] = true;
							}
						}
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
