using System;
using System.Collections.Generic;

namespace NonFactors.Mvc.Grid
{
    public interface IGridRows : IEnumerable<IGridRow>
    {
    }

    public interface IGridRows<T> : IGridRows
    {
        Func<T, String> CssClasses { get; set; }
        IGrid<T> Grid { get; }
    }
}
