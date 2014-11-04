using System;

namespace NonFactors.Mvc.Grid
{
    public interface IGridSortingQuery
    {
        GridSortOrder? SortOrder { get; set; }
        String ColumnName { get; set; }
    }
}
