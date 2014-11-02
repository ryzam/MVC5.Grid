using System;
using System.Collections.Generic;

namespace NonFactors.Mvc.Grid
{
    public class Grid<TModel> : IGrid<TModel> where TModel : class
    {
        public IList<IGridProcessor<TModel>> Processors { get; protected set; }
        public IEnumerable<TModel> Source { get; protected set; }

        public IGridColumns<TModel> Columns { get; protected set; }
        IGridColumns IGrid.Columns { get { return Columns; } }

        public IGridRows<TModel> Rows { get; protected set; }
        IGridRows IGrid.Rows { get { return Rows; } }

        IGridPager IGrid.Pager { get { return Pager; } }
        public IGridPager<TModel> Pager { get; set; }

        public String EmptyText { get; set; }
        public String Name { get; set; }

        public Grid(IEnumerable<TModel> source)
        {
            Processors = new List<IGridProcessor<TModel>>();
            Source = source;

            Columns = new GridColumns<TModel>(this);
            Rows = new GridRows<TModel>(this);
        }
    }
}
