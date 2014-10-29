using System;

namespace NonFactors.Mvc.Grid
{
    public class GridRow : IGridRow
    {
        public Object Model
        {
            get;
            protected set;
        }

        public GridRow(Object model)
        {
            Model = model;
        }
    }
}
