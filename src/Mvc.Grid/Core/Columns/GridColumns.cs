using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NonFactors.Mvc.Grid
{
    public class GridColumns<TModel> : IGridColumns<TModel> where TModel : class
    {
        protected IList<IGridColumn> Columns { get; private set; }
        public IGrid<TModel> Grid { get; private set; }

        public GridColumns(IGrid<TModel> grid)
        {
            Columns = new List<IGridColumn>();
            Grid = grid;
        }

        public IGridColumn<TModel, TKey> Add<TKey>(Expression<Func<TModel, TKey>> expression)
        {
            IGridColumn<TModel, TKey> column = new GridColumn<TModel, TKey>(expression);
            Columns.Add(column);

            return column;
        }

        public IEnumerator<IGridColumn> GetEnumerator()
        {
            return Columns.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
