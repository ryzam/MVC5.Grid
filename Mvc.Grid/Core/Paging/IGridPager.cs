using System;

namespace NonFactors.Mvc.Grid
{
    public interface IGridPager
    {
        Int32 ItemsPerPage { get; set; }
        Int32 CurrentPage { get; set; }
    }
}
