using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NonFactors.Mvc.Grid
{
    public class GridColumns<TModel> : List<IGridColumn>, IGridColumns<TModel> where TModel : class
    {
        public IGrid<TModel> Grid { get; set; }

        public GridColumns(IGrid<TModel> grid)
        {
            Grid = grid;
        }

        public virtual IGridColumn<TModel, TKey> Add<TKey>(Expression<Func<TModel, TKey>> expression)
        {
            IGridColumn<TModel, TKey> column = new GridColumn<TModel, TKey>(Grid, expression);
            Grid.Processors.Add(column);
            Add(column);

            return column;
        }
    }
}
