using System;
using System.Collections.Generic;
using System.Linq;

namespace NonFactors.Mvc.Grid
{
    public class Grid<TModel> : IGrid<TModel> where TModel : class
    {
        public IList<IGridProcessor<TModel>> Processors { get; set; }
        public IQueryable<TModel> Source { get; set; }
        public GridQuery Query { get; set; }

        public String CssClasses { get; set; }
        public String EmptyText { get; set; }
        public String Name { get; set; }

        public IGridColumns<TModel> Columns { get; protected set; }
        IGridColumns IGrid.Columns { get { return Columns; } }

        public IGridRows<TModel> Rows { get; protected set; }
        IGridRows IGrid.Rows { get { return Rows; } }

        IGridPager IGrid.Pager { get { return Pager; } }
        public IGridPager<TModel> Pager { get; set; }

        public Grid(IEnumerable<TModel> source)
        {
            Processors = new List<IGridProcessor<TModel>>();
            Source = source.AsQueryable();

            Name = "Grid";

            Columns = new GridColumns<TModel>(this);
            Rows = new GridRows<TModel>(this);
        }
    }
}
