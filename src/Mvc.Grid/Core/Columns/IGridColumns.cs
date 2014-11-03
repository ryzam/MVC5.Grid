using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NonFactors.Mvc.Grid
{
    public interface IGridColumns : IEnumerable<IGridColumn>
    {
    }

    public interface IGridColumns<TModel> : IGridColumns where TModel : class
    {
        IGrid<TModel> Grid { get; }

        IGridColumn<TModel, TKey> Add<TKey>(Expression<Func<TModel, TKey>> constraint);
    }
}
