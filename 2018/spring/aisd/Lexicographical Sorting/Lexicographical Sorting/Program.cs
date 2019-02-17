using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Lexicographical_Sorting {
	class Program {
		static void Main(string[] args) {
			foreach (var a in LexSort.Sort(Console.ReadLine().Split())) {
				Console.WriteLine(a);
			}
			Console.ReadLine();
		}
	}

	public static class LexSort {
		/// <summary>
		/// Возвращает отсортированную последовательность в порядке возрастания, алфавит a-z
		/// </summary>
		/// <param name="words"></param>
		/// <returns></returns>
		public static IEnumerable<string> Sort(string[] words) {
			if (words.Length == 0) {
				yield break;
			}

			int maxLen = words.Max(x => x.Length);

			Queue<int> prev = new Queue<int>(Enumerable.Range(0, words.Length));

			for (int i = maxLen; i >= 0; i--) {
				Queue<int> cur = new Queue<int>();
				Queue<int>[] queues = new Queue<int>[27];
				for (int j = 0; j < 27; j++) {
					queues[j] = new Queue<int>();
				}
				foreach (var ind in prev) {
					//беру или символ, если слово на нужном месте достаточно длинное, или 0
					queues[(i >= words[ind].Length) ? 0 : words[ind][i] - 'a' + 1].Enqueue(ind);
				}
				foreach (var queue in queues) {
					foreach (var ind in queue) {
						cur.Enqueue(ind);
					}
				}
				prev = cur;
			}

			foreach (var a in prev) {
				yield return words[a];
			}
		}

		[TestFixture]
		private class Tests {
			[TestCase(new string[] { }, new string[] { })]  //empty
			[TestCase(new string[] { "a" }, new string[] { "a" })]  //one word
			[TestCase(new string[] { "a", "b", "a" }, new string[] { "a", "a", "b" })] //few words
			public void LexSortTestCases(string[] words, string[] answer) {
				int i = 0;
				foreach (var word in Sort(words)) {
					Assert.Less(i, answer.Length, "returned sequence contains more words than necessary");
					Assert.AreEqual(answer[i], word);
					i++;
				}
				Assert.AreEqual(answer.Length, i, "returned sequence contains less words than necessary");
			}

			private int RandBetween(Random rand, int min, int max) {
				return rand.Next() % (max - min + 1) + min;
			}

			private string GetRandomWord(Random rand) {
				int min = 5;
				int max = 10;
				StringBuilder builder = new StringBuilder();
				for (int i = RandBetween(rand, min, max); i >= 0; i--) {
					builder.Append((char)(rand.Next() % 26 + 'a'));
				}
				return builder.ToString();
			}

			[Test]
			public void LexSortRandomTest() {
				int n = 1000;
				int minCount = 200;
				int maxCount = 500;

				Random rand = new Random();
				for (int i = 0; i < n; i++) {
					string[] arr = new string[RandBetween(rand, minCount, maxCount)];
					for (int j = 0; j < arr.Length; j++) {
						arr[j] = GetRandomWord(rand);
					}
					string[] lexSorted = Sort(arr).ToArray();
					Array.Sort(arr);
					LexSortTestCases(lexSorted, arr);
				}
			}
		}
	}
}
