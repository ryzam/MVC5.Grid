using System;

namespace NonFactors.Mvc.Grid
{
    public class GridSortingQuery : IGridSortingQuery
    {
        public GridSortOrder? SortOrder { get; set; }
        public String ColumnName { get; set; }

        public GridSortingQuery(IGridQuery query, String columnName)
        {
            SortOrder = GetSortValue(query, columnName);
            ColumnName = columnName;
        }

        private String GetSortKey(IGrid grid)
        {
            return grid.Name + "-Sort";
        }
        private String GetOrderKey(IGrid grid)
        {
            return grid.Name + "-Order";
        }
        private GridSortOrder? GetSortValue(IGridQuery gridQuery, String columnName)
        {
            String sortKey = GetSortKey(gridQuery.Grid);
            String sortValue = gridQuery.Query[sortKey];

            if (columnName == sortValue)
            {
                String orderKey = GetOrderKey(gridQuery.Grid);
                String orderValue = gridQuery.Query[orderKey];
                GridSortOrder order;

                if (Enum.TryParse<GridSortOrder>(orderValue, out order))
                    return order;
            }
            
            return null;
        }
    }
}
