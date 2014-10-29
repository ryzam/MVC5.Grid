using System;
using System.Collections;
using System.Collections.Generic;

namespace NonFactors.Mvc.Grid
{
    public class GridRows<T> : IGridRows<T> where T : class
    {
        private List<IGridRow> rows;

        public GridRows()
        {
            rows = new List<IGridRow>() { new GridRow(new Object()) };
        }

        public IEnumerator<IGridRow> GetEnumerator()
        {
            return rows.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
