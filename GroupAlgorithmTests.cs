using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GroupAlgorithm
{
	[TestClass]
	public class GroupAlgorithmTests
	{
		[TestMethod]
		public void GroupAlgorithm_SmokeTest()
		{
			var array = new [] { 1, 1, 2, 1, 1, 0, 1 };
			var expected = new[] {2, 2, 2};
			var initialValuesOccurance = Count(array);

			var sut = new GroupAlgorithm();
			var x = 2;
			var actual = sut.Group(array, x);

			actual.Select(p => p.Item1 + p.Item2).ShouldAllBeEquivalentTo(expected); // all pairs sum equals x
			var valueOccurance = Count(actual.SelectMany(t => new[] {t.Item1, t.Item2}));
			foreach (var item in valueOccurance)
			{
				item.Value.Should().BeLessOrEqualTo(initialValuesOccurance[item.Key]); // every element used in pair no more than once
			}
		}

		private static ConcurrentDictionary<int, int> Count(IEnumerable<int> array)
		{
			return array.Aggregate(
				new ConcurrentDictionary<int, int>(),
				(dic, i) =>
				{
					dic.AddOrUpdate(i, 1, (k, v) => v + 1);
					return dic;
				},
				dic => dic);
		}
	}
}