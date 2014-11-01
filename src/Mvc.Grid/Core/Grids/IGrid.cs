using System;
using System.Collections.Generic;

namespace NonFactors.Mvc.Grid
{
    public interface IGrid
    {
        String EmptyText { get; set; }
        String Name { get; set; }

        IGridColumns Columns { get; }
        IGridRows Rows { get; }

        IGridPager Pager { get; }
    }

    public interface IGrid<TModel> : IGrid where TModel : class
    {
        IList<IGridProcessor<TModel>> Processors { get; }
        IEnumerable<TModel> Source { get; }
    }
}
