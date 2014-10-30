using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NonFactors.Mvc.Grid
{
    public class GridColumns<TModel> : IGridColumns<TModel> where TModel : class
    {
        protected List<IGridColumn<TModel>> Columns { get; set; }

        public GridColumns()
        {
            Columns = new List<IGridColumn<TModel>>();
        }

        public IGridColumn<TModel> Add()
        {
            IGridColumn<TModel> column = new GridColumn<TModel, String>();
            Columns.Add(column);

            return column;
        }
        public IGridColumn<TModel> Add<TKey>(Expression<Func<TModel, TKey>> expression)
        {
            IGridColumn<TModel> column = new GridColumn<TModel, TKey>(expression.Compile());
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
