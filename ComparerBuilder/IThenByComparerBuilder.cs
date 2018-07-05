using System;

namespace ComparerBuilder
{
    public interface IThenByComparerBuilder<T> : IComparerBuild<T>
    {
        IThenByComparerBuilder<T> ThenKey<TKey>(Func<T, TKey> selector) where TKey : IComparable<TKey>;
        IThenByComparerBuilder<T> ThenKeyDescending<TKey>(Func<T, TKey> selector) where TKey : IComparable<TKey>;
    }
}