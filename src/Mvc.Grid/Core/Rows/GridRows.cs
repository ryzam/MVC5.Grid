using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NonFactors.Mvc.Grid
{
    public class GridRows<TModel> : IGridRows where TModel : class
    {
        public IEnumerable<TModel> Source { get; protected set; }
        public IGrid Grid { get; protected set; }

        public GridRows(IGrid grid, IEnumerable<TModel> source)
        {
            Source = source;
            Grid = grid;
        }

        public IEnumerator<IGridRow> GetEnumerator()
        {
            if (Grid.Pager == null)
                return Source
                    .Select(model => new GridRow(model))
                    .ToList()
                    .GetEnumerator();

            return Source
                .Skip(Grid.Pager.CurrentPage * Grid.Pager.RowsPerPage)
                .Take(Grid.Pager.RowsPerPage)
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
