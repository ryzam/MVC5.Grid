using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NonFactors.Mvc.Grid
{
    public class GridColumn<TModel, TValue> : BaseGridColumn<TModel, TValue> where TModel : class
    {
        private Boolean IsGridSortSet { get; set; }
        public override GridSortOrder? SortOrder
        {
            get
            {
                if (IsGridSortSet)
                    return base.SortOrder;

                if (Grid.Query[Grid.Name + "-Sort"] == Name)
                {
                    String orderValue = Grid.Query[Grid.Name + "-Order"];
                    GridSortOrder order;

                    if (Enum.TryParse<GridSortOrder>(orderValue, out order))
                        SortOrder = order;
                }

                IsGridSortSet = true;

                return base.SortOrder;
            }
            set
            {
                base.SortOrder = value;
                IsGridSortSet = true;
            }
        }

        private Boolean IsGridFilterSet { get; set; }
        public override IGridFilter<TModel> Filter
        {
            get
            {
                if (IsGridFilterSet)
                    return base.Filter;

                String filterKey = Grid.Query.AllKeys
                    .FirstOrDefault(key => (key ?? String.Empty)
                        .Contains(Grid.Name + "-" + Name + "-"));

                if (filterKey != null)
                {
                    String value = Grid.Query.GetValues(filterKey)[0];
                    String filterType = filterKey.Substring((Grid.Name + "-" + Name + "-").Length);

                    Filter = MvcGrid.Filters.GetFilter(this, filterType, value);
                }

                IsGridFilterSet = true;

                return base.Filter;
            }
            set
            {
                base.Filter = value;
                IsGridFilterSet = true;
            }
        }
        public override String FilterValue
        {
            get
            {
                if (Filter != null)
                    return Filter.Value;

                return null;
            }
            set
            {
                if (Filter != null)
                    Filter.Value = value;
            }
        }
        public override String FilterType
        {
            get
            {
                if (Filter != null)
                    return Filter.Type;

                return null;
            }
            set
            {
                if (Filter != null)
                    Filter.Type = value;
            }
        }

        public GridColumn(IGrid<TModel> grid, Expression<Func<TModel, TValue>> expression)
        {
            Grid = grid;
            IsEncoded = true;
            Expression = expression;
            FilterName = GetFilterName();
            IsSortable = IsMember(expression);
            IsFilterable = IsMember(expression);
            ValueFunction = expression.Compile();
            ProcessorType = GridProcessorType.Pre;
            Name = ExpressionHelper.GetExpressionText(expression);
        }

        public override IQueryable<TModel> Process(IQueryable<TModel> items)
        {
            if (IsFilterable == true && Filter != null)
                items = Filter.Process(items);

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
                return null;

            GridQuery query = new GridQuery(Grid.Query);
            query[Grid.Name + "-Sort"] = Name;

            if (SortOrder == GridSortOrder.Asc)
                query[Grid.Name + "-Order"] = GridSortOrder.Desc.ToString();
            else
                query[Grid.Name + "-Order"] = GridSortOrder.Asc.ToString();

            return query.ToString();
        }

        private Boolean? IsMember(Expression<Func<TModel, TValue>> expression)
        {
            if (expression.Body is MemberExpression)
                return null;

            return false;
        }
        private String GetRawValueFor(IGridRow row)
        {
            TValue value = ValueFunction(row.Model as TModel);
            if (value == null)
                return String.Empty;

            if (Format == null)
                return value.ToString();

            return String.Format(Format, value);
        }

        private String GetFilterName()
        {
            Type type = Nullable.GetUnderlyingType(typeof(TValue)) ?? typeof(TValue);
            if (type.IsEnum) return null;

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.SByte:
                case TypeCode.Byte:
                case TypeCode.Int16:
                case TypeCode.UInt16:
                case TypeCode.Int32:
                case TypeCode.UInt32:
                case TypeCode.Int64:
                case TypeCode.UInt64:
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Decimal:
                    return "Number";
                case TypeCode.DateTime:
                    return "Date";
                case TypeCode.Boolean:
                    return "Boolean";
                default:
                    return "Text";
            }
        }
    }
}
