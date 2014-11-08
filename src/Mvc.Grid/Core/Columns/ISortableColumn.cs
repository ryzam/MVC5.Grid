using System;

namespace NonFactors.Mvc.Grid
{
    public interface ISortableColumn
    {
        GridSortOrder? SortOrder { get; set; }
        Boolean? IsSortable { get; set; }

        IGridColumn Sortable(Boolean isSortable);

        String LinkForSort();
    }

    public interface ISortableColumn<TModel> : IGridProcessor<TModel> where TModel : class
    {
    }
}
