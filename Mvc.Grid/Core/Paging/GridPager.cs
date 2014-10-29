using System;
using System.Linq;

namespace NonFactors.Mvc.Grid
{
    public class GridPager<TModel> : IGridPager where TModel : class
    {
        public const Int32 DefaultItemsPerPage = 20;

        public String PartialViewName
        {
            get;
            set;
        }

        public Int32 RowsPerPage
        {
            get;
            set;
        }
        public Int32 CurrentPage
        {
            get;
            set;
        }
        public Int32 TotalPages
        {
            get;
            private set;
        }

        public GridPager(IGrid<TModel> grid)
        {
            PartialViewName = "MvcGrid/_Pager";

            RowsPerPage = DefaultItemsPerPage;
            TotalPages = (Int32)(Math.Ceiling(grid.Source.Count() / (Double)DefaultItemsPerPage));
        }
    }
}
