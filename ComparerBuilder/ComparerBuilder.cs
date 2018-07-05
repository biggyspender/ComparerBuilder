using System;
using System.Collections.Generic;
using System.Linq;

namespace ComparerBuilder
{
    public class ComparerBuilder<T> : IComparerBuilder<T>, IThenByComparerBuilder<T>
    {
        IList<IComparer<T>> comparers = new List<IComparer<T>>();
        public IThenByComparerBuilder<T> SortKey<TKey>(Func<T, TKey> selector) where TKey : IComparable<TKey>
        {
            var comparer = Comparer<T>.Create((a, b) => selector(a).CompareTo(selector(b)));
            comparers.Add(comparer);
            return this;
        }
        public IThenByComparerBuilder<T> SortKeyDescending<TKey>(Func<T, TKey> selector) where TKey : IComparable<TKey>
        {
            var comparer = Comparer<T>.Create((a, b) => selector(b).CompareTo(selector(a)));
            comparers.Add(comparer);
            return this;
        }
        public IThenByComparerBuilder<T> ThenKey<TKey>(Func<T, TKey> selector) where TKey : IComparable<TKey>
        {
            return SortKey(selector);
        }
        public IThenByComparerBuilder<T> ThenKeyDescending<TKey>(Func<T, TKey> selector) where TKey : IComparable<TKey>
        {
            return SortKeyDescending(selector);
        }

        public IComparer<T> Build()
        {
            var comparersCopy = comparers.ToList();
            return Comparer<T>.Create((a, b) => comparersCopy.Select(c => c.Compare(a, b)).FirstOrDefault(x => x != 0));
        }
    }
}