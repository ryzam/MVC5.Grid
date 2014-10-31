using System;
using System.Collections.Generic;
using System.Linq;

namespace NonFactors.Mvc.Grid
{
    public class GridPager<TModel> : IGridPager where TModel : class
    {
        public String PartialViewName { get; set; }

        public Int32 PagesToDisplay { get; set; }
        public Int32 CurrentPage { get; set; }
        public Int32 RowsPerPage { get; set; }
        public Int32 TotalRows { get; set; }
        public Int32 StartingPage
        {
            get
            {
                Int32 middlePage = (PagesToDisplay / 2) + (PagesToDisplay % 2) - 1;
                if (CurrentPage - middlePage + PagesToDisplay > TotalPages)
                    return TotalPages - PagesToDisplay;

                if (CurrentPage < middlePage)
                    return 0;

                return CurrentPage - middlePage;
            }
        }
        public Int32 TotalPages
        {
            get
            {
                return (Int32)(Math.Ceiling(TotalRows / (Double)RowsPerPage));
            }
        }

        public GridPager(IEnumerable<TModel> source)
        {
            PartialViewName = "MvcGrid/_Pager";

            CurrentPage = 0;
            RowsPerPage = 20;
            PagesToDisplay = 5;
            TotalRows = source.Count();
        }
    }
}
