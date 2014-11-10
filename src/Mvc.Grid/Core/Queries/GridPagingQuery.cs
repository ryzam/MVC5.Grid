using System;

namespace NonFactors.Mvc.Grid
{
    public class GridPagingQuery : IGridPagingQuery
    {
        public String GridName { get; set; }
        public Int32 CurrentPage { get; set; }

        public GridPagingQuery(GridQuery query)
        {
            CurrentPage = GetPageValue(query);
            GridName = query.Grid.Name;
        }

        private String GetSortKey(IGrid grid)
        {
            return grid.Name + "-Page";
        }
        private Int32 GetPageValue(GridQuery gridQuery)
        {
            String key = GetSortKey(gridQuery.Grid);
            String value = gridQuery[key];
            Int32 page;

            if(Int32.TryParse(value, out page))
                return page;

            return 1;
        }
    }
}
