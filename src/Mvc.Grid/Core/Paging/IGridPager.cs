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

        String LinkForPage(Int32 page);
    }

    public interface IGridPager<TModel> : IGridProcessor<TModel>, IGridPager where TModel : class
    {
        IGrid<TModel> Grid { get; set; }
    }
}
