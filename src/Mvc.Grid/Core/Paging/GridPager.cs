using System;
using System.Linq;

namespace NonFactors.Mvc.Grid
{
    public class GridPager<TModel> : IGridPager<TModel> where TModel : class
    {
        public String PartialViewName { get; set; }
        public IGrid<TModel> Grid { get; set; }
        public String CssClasses { get; set; }

        public GridProcessorType Type { get; set; }
        public Int32 PagesToDisplay { get; set; }
        public Int32 CurrentPage { get; set; }
        public Int32 RowsPerPage { get; set; }
        public Int32 TotalRows { get; set; }
        public Int32 StartingPage
        {
            get
            {
                Int32 middlePage = (PagesToDisplay / 2) + (PagesToDisplay % 2);
                if (CurrentPage < middlePage)
                    return 1;

                if (CurrentPage - middlePage + PagesToDisplay > TotalPages)
                    return Math.Max(TotalPages - PagesToDisplay + 1, 1);

                return CurrentPage - middlePage + 1;
            }
        }
        public Int32 TotalPages
        {
            get
            {
                return (Int32)(Math.Ceiling(TotalRows / (Double)RowsPerPage));
            }
        }

        public GridPager(IGrid<TModel> grid)
        {
            Grid = grid;
            RowsPerPage = 20;
            PagesToDisplay = 5;
            Type = GridProcessorType.Post;
            CurrentPage = GetCurrentPage();
            TotalRows = Grid.Source.Count();
            PartialViewName = "MvcGrid/_Pager";
        }

        public IQueryable<TModel> Process(IQueryable<TModel> items)
        {
            return items.Skip((CurrentPage - 1) * RowsPerPage).Take(RowsPerPage);
        }

        public String GetPagingQuery(Int32 page)
        {
            GridQuery query = new GridQuery(Grid.Query);
            query[Grid.Name + "-Page"] = page.ToString();

            return query.ToString();
        }

        private Int32 GetCurrentPage()
        {
            String key = Grid.Name + "-Page";
            String value = Grid.Query[key];
            Int32 page;

            if (Int32.TryParse(value, out page))
                return page;

            return 1;
        }
    }
}
