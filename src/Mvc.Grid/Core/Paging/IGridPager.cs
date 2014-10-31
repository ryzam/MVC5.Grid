using System;

namespace NonFactors.Mvc.Grid
{
    public interface IGridPager
    {
        String PartialViewName { get; set; }

        Int32 PagesToDisplay { get; set; }
        Int32 RowsPerPage { get; set; }
        Int32 CurrentPage { get; set; }
        Int32 StartingPage { get; }
        Int32 TotalPages { get; }
    }
}
