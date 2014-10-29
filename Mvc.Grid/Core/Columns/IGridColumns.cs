using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NonFactors.Mvc.Grid
{
    public interface IGridColumns : IEnumerable<IGridColumn>
    {
    }

    public interface IGridColumns<T> : IGridColumns where T : class
    {
        IGridColumn<T> Add();
        IGridColumn<T> Add<TKey>(Expression<Func<T, TKey>> constraint);
    }
}
