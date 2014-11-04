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

        private String GetSortKey(IGrid grid, String columnName)
        {
            if (!String.IsNullOrWhiteSpace(grid.Name))
                return String.Format("MG-Sort-{0}-{1}", grid.Name, columnName);

            return String.Format("MG-Sort-{0}", columnName);
        }
        private GridSortOrder? GetSortValue(IGridQuery gridQuery, String columnName)
        {
            String key = GetSortKey(gridQuery.Grid, columnName);
            String value = gridQuery.Query[key];
            GridSortOrder order;

            if (Enum.TryParse<GridSortOrder>(value, out order))
                return order;

            return null;
        }
    }
}
