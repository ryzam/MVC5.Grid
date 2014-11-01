using System;
using System.Collections.Generic;

namespace NonFactors.Mvc.Grid
{
    public class Grid<TModel> : IGrid<TModel> where TModel : class
    {
        public IEnumerable<TModel> Source { get; protected set; }

        public IGridColumns<TModel> Columns { get; protected set; }
        IGridColumns IGrid.Columns { get { return Columns; } }
        public IGridRows Rows { get; protected set; }

        public IGridPager Pager { get; set; }

        public String EmptyText { get; set; }
        public String Name { get; set; }

        public Grid(IEnumerable<TModel> source)
        {
            Source = source;

            Columns = new GridColumns<TModel>();
            Rows = new GridRows<TModel>(this, Source);
        }
    }
}
