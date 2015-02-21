using System;

namespace NonFactors.Mvc.Grid
{
    public interface IFilterableColumn
    {
        Boolean? IsMultiFilterable { get; set; }
        Boolean? IsFilterable { get; set; }
        IGridColumnFilter Filter { get; }
        String FilterName { get; set; }
    }
    public interface IFilterableColumn<T> : IFilterableColumn, IGridProcessor<T>
    {
        new IGridColumnFilter<T> Filter { get; set; }
    }
}
