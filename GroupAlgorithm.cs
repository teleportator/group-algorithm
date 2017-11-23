using System;
using System.Collections.Generic;

namespace GroupAlgorithm
{
	public class GroupAlgorithm
	{
		public Tuple<int, int>[] Group(int[] array, int x)
		{
			var linkedList = new LinkedList<int>(array);
			var pairs = new List<Tuple<int, int>>();
			while (linkedList.First != null)
			{
				var first = linkedList.First;
				var next = first.Next;
				while (next != null)
				{
					if (first.Value + next.Value == x)
					{
						linkedList.Remove(next);
						pairs.Add(new Tuple<int, int>(first.Value, next.Value));
						break;
					}

					next = next.Next;
				}

				linkedList.Remove(first);
			}

			return pairs.ToArray();
		}
	}
}
