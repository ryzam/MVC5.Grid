using System;

namespace NonFactors.Mvc.Grid
{
    public interface IGridPager
    {
        String PartialViewName { get; set; }

        Int32 RowsPerPage { get; set; }
        Int32 CurrentPage { get; set; }
        Int32 TotalPages { get; }
    }
}
