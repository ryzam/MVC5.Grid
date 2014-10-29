using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NonFactors.Mvc.Grid
{
    public class GridRows<TModel> : IGridRows<TModel> where TModel : class
    {
        private IEnumerable<TModel> source;
        private IGrid grid;

        public GridRows(IEnumerable<TModel> source, IGrid grid)
        {
            this.source = source;
            this.grid = grid;
        }

        public IEnumerator<IGridRow> GetEnumerator()
        {
            if (grid.Pager == null)
                return source
                    .Select(model => new GridRow(model))
                    .ToList()
                    .GetEnumerator();

            return source
                .Skip(grid.Pager.CurrentPage * grid.Pager.RowsPerPage)
                .Take(grid.Pager.RowsPerPage)
                .Select(model => new GridRow(model))
                .ToList()
                .GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
