using System;
using ComparerBuilder;
using MoreLinq;
using Xunit;

namespace ComparerBuilderTest
{
    public class Item
    {
        public Item(int a, int b, int c)
        {
            A = a;
            B = b;
            C = c;
        }

        public int A { get; }
        public int B { get; }
        public int C { get; }
    }

    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var items = new[]
            {
                new Item(0, 0, 1),
                new Item(0, 2, 1),
                new Item(0, 1, 2),
                new Item(1, 0, 4),
                new Item(1, 2, 1),
                new Item(1, 0, 3)
            };

            var builder = new ComparerBuilder<Item>();
            var comparer = builder.SortKey(x => x.A).ThenKeyDescending(x => x.B).ThenKey(x => x.C).Build();
            var selectedItem = items.MinBy(x => x, comparer).First();
            Assert.Equal(selectedItem.A, 0);
            Assert.Equal(selectedItem.B, 2);
            Assert.Equal(selectedItem.C, 1);
        }
    }
}