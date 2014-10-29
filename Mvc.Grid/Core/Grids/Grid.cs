using System.Linq;

namespace NonFactors.Mvc.Grid
{
    public class Grid<T> : IGrid<T> where T : class
    {
        public IQueryable<T> Source
        {
            get;
            protected set;
        }

        public IGridColumns<T> Columns
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

        public Grid(IQueryable<T> source)
        {
            Columns = new GridColumns<T>();
            Rows = new GridRows<T>();
            Source = source;
        }
    }
}
