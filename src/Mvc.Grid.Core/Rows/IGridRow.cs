using System;

namespace NonFactors.Mvc.Grid
{
    public interface IGridRow
    {
        String CssClasses { get; set; }
        Object Model { get; }
    }
}
