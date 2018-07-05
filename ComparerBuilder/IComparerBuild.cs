using System.Collections.Generic;

namespace ComparerBuilder
{
    public interface IComparerBuild<T>
    {
        IComparer<T> Build();
    }
}