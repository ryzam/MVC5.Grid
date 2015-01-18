using System;

namespace NonFactors.Mvc.Grid
{
    public class GridRow : IGridRow
    {
        public String CssClasses { get; set; }
        public Object Model { get; set; }

        public GridRow(Object model)
        {
            Model = model;
        }
    }
}
