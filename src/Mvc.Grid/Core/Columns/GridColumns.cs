using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NonFactors.Mvc.Grid
{
    public class GridColumns<TModel> : IGridColumns<TModel> where TModel : class
    {
        protected List<IGridColumn> Columns { get; set; }

        public GridColumns()
        {
            Columns = new List<IGridColumn>();
        }

        public IGridColumn<TModel, String> Add()
        {
            IGridColumn<TModel, String> column = new GridColumn<TModel, String>();
            Columns.Add(column);

            return column;
        }
        public IGridColumn<TModel, TKey> Add<TKey>(Expression<Func<TModel, TKey>> expression)
        {
            IGridColumn<TModel, TKey> column = new GridColumn<TModel, TKey>(expression.Compile());
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
