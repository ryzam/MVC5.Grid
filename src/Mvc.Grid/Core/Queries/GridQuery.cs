using System;
using System.Collections.Specialized;

namespace NonFactors.Mvc.Grid
{
    public class GridQuery : NameValueCollection
    {
        public IGrid Grid { get; set; }

        public GridQuery(IGrid grid, NameValueCollection query) : base(query)
        {
            Grid = grid;
        }

        public virtual IGridSortingQuery GetSortingQuery(String columnName)
        {
            return new GridSortingQuery(this, columnName);
        }
        public virtual IGridPagingQuery GetPagingQuery()
        {
            return new GridPagingQuery(this);
        }
    }
}
