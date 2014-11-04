using System;
using System.Collections.Specialized;

namespace NonFactors.Mvc.Grid
{
    public interface IGridQuery
    {
        IGrid Grid { get; }
        NameValueCollection Query { get; set; }

        IGridSortingQuery GetSortingQuery(String columnName);
        IGridPagingQuery GetPagingQuery();
    }
}
