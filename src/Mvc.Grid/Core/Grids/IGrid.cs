using System;
using System.Collections.Generic;
using System.Linq;

namespace NonFactors.Mvc.Grid
{
    public interface IGrid
    {
        IGridQuery Query { get; set; }

        String EmptyText { get; set; }
        String Name { get; set; }

        IGridColumns Columns { get; }
        IGridRows Rows { get; }

        IGridPager Pager { get; }
    }

    public interface IGrid<TModel> : IGrid where TModel : class
    {
        IList<IGridProcessor<TModel>> Processors { get; }
        IQueryable<TModel> Source { get; }

        new IGridColumns<TModel> Columns { get; }
        new IGridRows<TModel> Rows { get; }

        new IGridPager<TModel> Pager { get; set; }
    }
}
