using System;
using System.Collections.Generic;
using System.IO;

namespace _18_2
{
	class Program
	{
		static void Main(string[] args)
		{
			List<string> input = ReadInput();
			long totalResult = 0;
			foreach (string line in input)
			{
				Stack<char> operators = new Stack<char>();
				string cleanLine = line.Replace(" ", "");
				string infix = "";
				long lineResult = 0;
				for (int i = 0; i < cleanLine.Length; i++)
				{
					char currentSymbol = cleanLine[i];
					if (char.IsDigit(currentSymbol))
					{
						infix += cleanLine[i];
					}
					else if (IsOperator(currentSymbol))
					{
						while (operators.Count != 0 && operators.Peek() != '(' && GetPrecedence(operators.Peek()) >= GetPrecedence(currentSymbol))
						{
							infix += operators.Pop();
						}
						operators.Push(currentSymbol);
					}
					else if (currentSymbol == '(')
					{
						operators.Push(currentSymbol);
					}
					else if (currentSymbol == ')')
					{
						while (operators.Peek() != '(')
						{
							infix += operators.Pop();
						}
						operators.Pop();
					}
				}

				while (operators.Count != 0)
				{
					infix += operators.Pop();
				}

				Stack<long> stack = new Stack<long>();
				foreach (char currentSymbol in infix)
				{
					if (char.IsDigit(currentSymbol))
					{
						stack.Push(Convert.ToInt64(currentSymbol.ToString()));
					}
					else
					{
						long operand1 = stack.Pop();
						long operand2 = stack.Pop();
						long result = 0;
						switch (currentSymbol)
						{
							case '+':
								result = operand1 + operand2;
								break;
							case '*':
								result = operand1 * operand2;
								break;
						}
						stack.Push(result);
					}
				}
				lineResult = stack.Pop();
				totalResult += lineResult;
			}
			Console.WriteLine(totalResult);
		}

		private static bool IsOperator(char character)
		{
			return character == '+' || character == '*';
		}

		public static int GetPrecedence(char op)
		{
			switch (op)
			{
				case '*':
					return 0;
				case '+':
					return 1;
			}
			return 0;
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
