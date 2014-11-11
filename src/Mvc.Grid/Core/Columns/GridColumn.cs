using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NonFactors.Mvc.Grid
{
    public class GridColumn<TModel, TValue> : BaseGridColumn, IGridColumn<TModel, TValue> where TModel : class
    {
        public Expression<Func<TModel, TValue>> Expression { get; set; }
        protected Func<TModel, TValue> CompiledExpression { get; set; }

        public GridProcessorType Type { get; set; }
        public IGrid<TModel> Grid { get; set; }

        public GridColumn(IGrid<TModel> grid, Expression<Func<TModel, TValue>> expression)
        {
            Grid = grid;
            IsEncoded = true;
            Expression = expression;
            Type = GridProcessorType.Pre;
            CompiledExpression = expression.Compile();
            Name = ExpressionHelper.GetExpressionText(expression);

            SortOrder = GetSortOrder();
        }

        public IQueryable<TModel> Process(IQueryable<TModel> items)
        {
            if (IsSortable != true)
                return items;

            if (SortOrder == null)
                return items;

            if (SortOrder == GridSortOrder.Asc)
                return items.OrderBy(Expression);

            return items.OrderByDescending(Expression);
        }

        public override IHtmlString ValueFor(IGridRow row)
        {
            String value = GetRawValueFor(row);
            if (IsEncoded) value = WebUtility.HtmlEncode(value);

            return new HtmlString(value);
        }
        public override String GetSortingQuery()
        {
            if (!(IsSortable == true))
                return "#";

            GridQuery query = new GridQuery(Grid.Query);
            query[Grid.Name + "-Sort"] = Name;

            if (SortOrder == GridSortOrder.Asc)
                query[Grid.Name + "-Order"] = GridSortOrder.Desc.ToString();
            else
                query[Grid.Name + "-Order"] = GridSortOrder.Asc.ToString();

            return query.ToString();
        }

        private String GetRawValueFor(IGridRow row)
        {
            TValue value = CompiledExpression(row.Model as TModel);
            if (value == null)
                return String.Empty;

            if (Format == null)
                return value.ToString();

            return String.Format(Format, value);
        }
        private GridSortOrder? GetSortOrder()
        {
            if (Grid.Query[Grid.Name + "-Sort"] == Name)
            {
                String orderValue = Grid.Query[Grid.Name + "-Order"];
                GridSortOrder order;

                if (Enum.TryParse<GridSortOrder>(orderValue, out order))
                    return order;
            }

            return null;
        }
    }
}
