using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NonFactors.Mvc.Grid
{
    public class GridColumns<T> : IGridColumns<T> where T : class
    {
        private List<IGridColumn<T>> columns;

        public GridColumns()
        {
            columns = new List<IGridColumn<T>>();
        }

        public IGridColumn<T> Add()
        {
            IGridColumn<T> column = new GridColumn<T>();
            columns.Add(column);

            return column;
        }
        public IGridColumn<T> Add<TKey>(Expression<Func<T, TKey>> constraint)
        {
            IGridColumn<T> column = new GridColumn<T>();
            columns.Add(column);

            return column;
        }

        public IEnumerator<IGridColumn> GetEnumerator()
        {
            return columns.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
