using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace _14_1
{
	class Program
	{
		static void Main(string[] args)
		{
			List<string> input = ReadInput();
			Dictionary<long, string> memory = new Dictionary<long, string>();

			string currentMask = "";
			foreach (string line in input)
			{
				if (line.Contains("mask"))
				{
					currentMask = line.Substring(7, line.Length - 7);
				}
				else
				{
					string cutLine = line.Replace(" ", "");
					var parsedLine = cutLine.Split("=");
					long address = Convert.ToInt32(parsedLine[0].Substring(4, parsedLine[0].Length - 5));
					long value = Convert.ToInt32(parsedLine[1]);

					string binary = Convert.ToString(value, 2);

					StringBuilder maskedValue = new StringBuilder(currentMask);

					for(int i = 0; i < binary.Length; i++)
					{
						if(currentMask[maskedValue.Length - binary.Length + i] == 'X')
						{
							maskedValue[maskedValue.Length-binary.Length+i] = binary[i];
						}
					}
					for(int i = 0; i < maskedValue.Length; i++)
					{
						if (maskedValue[i] == 'X')
						{
							maskedValue[i] = '0';
						}
					}
					memory[address] = maskedValue.ToString();
				}
			}
			long sum = 0;
			foreach(var kvp in memory)
			{
				sum += Convert.ToInt64(kvp.Value, 2);
			}

			Console.WriteLine(sum);
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
