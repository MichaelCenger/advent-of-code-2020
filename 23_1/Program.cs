using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _23_1
{
	class Node
	{
		public int Value;
		public Node Next;
		public Node Prev;
		public Node(int value, Node next, Node prev)
		{
			Value = value;
			Next = next;
			Prev = prev;
		}

		public Node(Node node)
		{
			Value = node.Value;
			Next = node.Next;
			Prev = node.Prev;
		}

		public Node()
		{

		}
	}
	class Program
	{
		static void Main(string[] args)
		{
			List<string> input = ReadInput();
			Node first = new Node(Convert.ToInt32(input[0][0].ToString()), null, null);
			Node currentNode = first;
			for (int i = 1; i < input[0].Length; i++)
			{
				currentNode.Next = new Node(Convert.ToInt32(input[0][i].ToString()), null, currentNode);
				currentNode = currentNode.Next;
			}
			currentNode.Next = first;
			first.Prev = currentNode;
			int currentCupIndex = 0;
			List<int> sortedCups = NodesToList(first);
			sortedCups.Sort();
			int lowestCup = sortedCups[0];
			int highestCup = sortedCups[sortedCups.Count - 1];
			string result = "";
			Node currentCup = first;
			for (int i = 0; i < 100; i++)
			{

				Node pickedUpCups = currentCup.Next;
				pickedUpCups.Prev = null;
				currentCup.Next = currentCup.Next.Next.Next.Next;
				currentCup.Next.Next.Next.Next.Prev = currentCup;
				pickedUpCups.Next.Next.Next = pickedUpCups;
				var pickedUpList = NodesToList(pickedUpCups);

				int destinationCup = currentCup.Value - 1;
				if (destinationCup < lowestCup)
				{
					destinationCup = highestCup;
				}
				while (Contains(pickedUpCups, destinationCup))
				{
					destinationCup--;
					if (destinationCup < lowestCup)
					{
						destinationCup = highestCup;
					}
				}

				Node curr = currentCup;
				HashSet<int> usedNodes = new HashSet<int>();
				while (!usedNodes.Contains(curr.Value))
				{
					if(curr.Value == destinationCup)
					{
						pickedUpCups.Next.Next.Next = curr.Next;
						curr.Next = pickedUpCups;
						break;
					}
					usedNodes.Add(curr.Value);
					curr = curr.Next;
				}
				currentCup = currentCup.Next;
			}
			PrintNodes(currentCup);

		}

		private static bool Contains(Node first, int value)
		{
			Node currentNode = first;
			HashSet<int> usedNodes = new HashSet<int>();
			while (!usedNodes.Contains(currentNode.Value))
			{
				usedNodes.Add(currentNode.Value);
				if (currentNode.Value == value)
				{
					return true;
				}
				currentNode = currentNode.Next;
			}
			return false;
		}

		private static List<int> NodesToList(Node first)
		{
			List<int> result = new List<int>();
			Node currentNode = first;
			HashSet<int> usedNodes = new HashSet<int>();
			while (!usedNodes.Contains(currentNode.Value))
			{
				result.Add(currentNode.Value);
				usedNodes.Add(currentNode.Value);
				currentNode = currentNode.Next;
			}
			return result;
		}

		private static void PrintNodes(Node first)
		{
			Node currentNode = first;
			HashSet<int> usedNodes = new HashSet<int>();
			while (!usedNodes.Contains(currentNode.Value))
			{
				usedNodes.Add(currentNode.Value);
				Console.Write(currentNode.Value);
				currentNode = currentNode.Next;
			}
			Console.WriteLine();
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
