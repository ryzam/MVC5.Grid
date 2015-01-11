using System;

namespace NonFactors.Mvc.Grid
{
    public interface ISortableColumn
    {
        GridSortOrder? FirstSortOrder { get; set; }
        GridSortOrder? SortOrder { get; set; }
        Boolean? IsSortable { get; set; }
    }

    public interface ISortableColumn<T> : IGridProcessor<T>
    {
    }
}
