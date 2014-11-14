using System;

namespace NonFactors.Mvc.Grid
{
    public interface IFilterableColumn
    {
        Boolean? IsFilterable { get; set; }
    }
    public interface IFilterableColumn<TModel> : IGridProcessor<TModel> where TModel : class
    {
        IGridFilter<TModel> Filter { get; set; }
    }
}
