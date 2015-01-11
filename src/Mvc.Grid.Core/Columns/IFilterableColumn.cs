using System;

namespace NonFactors.Mvc.Grid
{
    public interface IFilterableColumn
    {
        Boolean? IsFilterable { get; set; }
        String FilterValue { get; set; }
        String FilterType { get; set; }
        String FilterName { get; set; }
    }
    public interface IFilterableColumn<T> : IGridProcessor<T>
    {
        IGridFilter<T> Filter { get; set; }
    }
}
