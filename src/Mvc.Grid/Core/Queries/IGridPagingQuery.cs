using System;

namespace NonFactors.Mvc.Grid
{
    public interface IGridPagingQuery
    {
        String GridName { get; set; }
        Int32 CurrentPage { get; set; }
    }
}
