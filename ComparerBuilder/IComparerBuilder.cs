using System;

namespace ComparerBuilder
{
    public interface IComparerBuilder<T> : IComparerBuild<T>
    {
        IThenByComparerBuilder<T> SortKey<TKey>(Func<T, TKey> selector) where TKey : IComparable<TKey>;
        IThenByComparerBuilder<T> SortKeyDescending<TKey>(Func<T, TKey> selector) where TKey : IComparable<TKey>;
    }
}