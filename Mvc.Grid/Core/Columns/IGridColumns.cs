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
        IGridColumn<TModel> Add();
        IGridColumn<TModel> Add<TKey>(Expression<Func<TModel, TKey>> constraint);
    }
}
