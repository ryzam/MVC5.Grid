using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NonFactors.Mvc.Grid
{
    public class GridRows<TModel> : IGridRows<TModel> where TModel : class
    {
        private IEnumerable<IGridRow> rows;

        public GridRows(IEnumerable<TModel> source)
        {
            rows = source.Select(model => new GridRow(model)).ToList();
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
