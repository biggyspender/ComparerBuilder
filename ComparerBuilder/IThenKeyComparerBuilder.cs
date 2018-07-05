using System;

namespace ComparerBuilder
{
    public interface IThenKeyComparerBuilder<T> : IComparerBuild<T>
    {
        IThenKeyComparerBuilder<T> ThenKey<TKey>(Func<T, TKey> selector) where TKey : IComparable<TKey>;
        IThenKeyComparerBuilder<T> ThenKeyDescending<TKey>(Func<T, TKey> selector) where TKey : IComparable<TKey>;
    }
}