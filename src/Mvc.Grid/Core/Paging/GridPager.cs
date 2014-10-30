using System;
using System.Linq;

namespace NonFactors.Mvc.Grid
{
    public class GridPager<TModel> : IGridPager where TModel : class
    {
        public String PartialViewName { get; set; }

        public Int32 TotalPages { get; private set; }
        public Int32 RowsPerPage { get; set; }
        public Int32 CurrentPage { get; set; }

        public GridPager(IGrid<TModel> grid)
        {
            PartialViewName = "MvcGrid/_Pager";

            TotalPages = (Int32)(Math.Ceiling(grid.Source.Count() / 20.0));
            RowsPerPage = 20;
            CurrentPage = 0;
        }
    }
}
