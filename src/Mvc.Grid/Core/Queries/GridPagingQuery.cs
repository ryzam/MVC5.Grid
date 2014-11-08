using System;

namespace NonFactors.Mvc.Grid
{
    public class GridPagingQuery : IGridPagingQuery
    {
        public String GridName { get; set; }
        public Int32 CurrentPage { get; set; }

        public GridPagingQuery(IGridQuery query)
        {
            CurrentPage = GetPageValue(query);
            GridName = query.Grid.Name;
        }

        private String GetSortKey(IGrid grid)
        {
            if (String.IsNullOrEmpty(grid.Name))
                return "MG-Page";

            return String.Format("MG-Page-{0}", grid.Name);
        }
        private Int32 GetPageValue(IGridQuery gridQuery)
        {
            String key = GetSortKey(gridQuery.Grid);
            String value = gridQuery.Query[key];
            Int32 page = 0;

            Int32.TryParse(value, out page);

            return page;
        }
    }
}
