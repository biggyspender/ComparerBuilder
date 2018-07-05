using System;
using System.Collections.Generic;
using System.Linq;

namespace ComparerBuilder
{
    public class ComparerBuilder<T> : IComparerBuilder<T>, IThenKeyComparerBuilder<T>
    {
        private readonly IList<IComparer<T>> comparers = new List<IComparer<T>>();

        public IThenKeyComparerBuilder<T> SortKey<TKey>(Func<T, TKey> selector) where TKey : IComparable<TKey>
        {
            var comparer = Comparer<T>.Create((a, b) => selector(a).CompareTo(selector(b)));
            comparers.Add(comparer);
            return this;
        }

        public IThenKeyComparerBuilder<T> SortKeyDescending<TKey>(Func<T, TKey> selector) where TKey : IComparable<TKey>
        {
            var comparer = Comparer<T>.Create((a, b) => selector(b).CompareTo(selector(a)));
            comparers.Add(comparer);
            return this;
        }

        public IComparer<T> Build()
        {
            var comparersCopy = comparers.ToList();
            return Comparer<T>.Create((a, b) => comparersCopy.Select(c => c.Compare(a, b)).FirstOrDefault(x => x != 0));
        }

        public IThenKeyComparerBuilder<T> ThenKey<TKey>(Func<T, TKey> selector) where TKey : IComparable<TKey>
        {
            return SortKey(selector);
        }

        public IThenKeyComparerBuilder<T> ThenKeyDescending<TKey>(Func<T, TKey> selector) where TKey : IComparable<TKey>
        {
            return SortKeyDescending(selector);
        }
    }
}