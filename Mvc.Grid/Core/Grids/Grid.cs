using System.Linq;

namespace NonFactors.Mvc.Grid
{
    public class Grid<TModel> : IGrid<TModel> where TModel : class
    {
        public IQueryable<TModel> Source
        {
            get;
            protected set;
        }

        public IGridColumns<TModel> Columns
        {
            get;
            protected set;
        }
        IGridColumns IGrid.Columns
        {
            get
            {
                return Columns;
            }
        }
        public IGridRows Rows
        {
            get;
            protected set;
        }

        public IGridPager Pager
        {
            get;
            protected set;
        }

        public Grid(IQueryable<TModel> source)
        {
            Source = source;
            Pager = new GridPager();

            Columns = new GridColumns<TModel>();
            Rows = new GridRows<TModel>(source, Pager);
        }
    }
}
