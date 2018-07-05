﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace ComparerBuilder
{
    public class ComparerBuilder<T> : IComparerBuilder<T>, IThenKeyComparerBuilder<T>
    {
        private readonly IList<IComparer<T>> comparers;

        public ComparerBuilder()
        {
            comparers = new List<IComparer<T>>();
        }

        private ComparerBuilder(IList<IComparer<T>> comparers)
        {
            this.comparers = comparers;
        }

        public IThenKeyComparerBuilder<T> SortKey<TKey>(Func<T, TKey> selector) where TKey : IComparable<TKey>
        {
            var comparer = Comparer<T>.Create((a, b) => selector(a).CompareTo(selector(b)));
            var newComparers = comparers.ToList();
            newComparers.Add(comparer);
            return new ComparerBuilder<T>(newComparers);
        }

        public IThenKeyComparerBuilder<T> SortKeyDescending<TKey>(Func<T, TKey> selector) where TKey : IComparable<TKey>
        {
            var comparer = Comparer<T>.Create((a, b) => selector(b).CompareTo(selector(a)));
            var newComparers = comparers.ToList();
            newComparers.Add(comparer);
            return new ComparerBuilder<T>(newComparers);
        }

        public IComparer<T> Build()
        {
            return Comparer<T>.Create((a, b) => comparers.Select(c => c.Compare(a, b)).FirstOrDefault(x => x != 0));
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