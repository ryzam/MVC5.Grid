using System;

namespace NonFactors.Mvc.Grid
{
    public interface IGridColumnFilter
    {
        IGridFilter Second { get; set; }
        IGridFilter First { get; set; }
        String Operator { get; set; }
    }
    public interface IGridColumnFilter<T> : IGridColumnFilter, IGridProcessor<T>
    {
        IGridColumn<T> Column { get; set; }
    }
}
