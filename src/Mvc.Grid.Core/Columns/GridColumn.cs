using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NonFactors.Mvc.Grid
{
    public class GridColumn<T, TValue> : BaseGridColumn<T, TValue> where T : class
    {
        private Boolean SortOrderIsSet { get; set; }
        public override GridSortOrder? SortOrder
        {
            get
            {
                if (SortOrderIsSet)
                    return base.SortOrder;

                if (Grid.Query[Grid.Name + "-Sort"] == Name)
                {
                    String orderValue = Grid.Query[Grid.Name + "-Order"];
                    GridSortOrder order;

                    if (Enum.TryParse<GridSortOrder>(orderValue, out order))
                        SortOrder = order;
                }
                else if (Grid.Query[Grid.Name + "-Sort"] == null)
                {
                    SortOrder = InitialSortOrder;
                }

                SortOrderIsSet = true;

                return base.SortOrder;
            }
            set
            {
                base.SortOrder = value;
                SortOrderIsSet = true;
            }
        }

        private Boolean FilterIsSet { get; set; }
        public override IGridFilter<T> Filter
        {
            get
            {
                if (FilterIsSet)
                    return base.Filter;

                String filterKey = Grid.Query.AllKeys
                    .FirstOrDefault(key => (key ?? String.Empty)
                        .StartsWith(Grid.Name + "-" + Name + "-"));

                if (filterKey != null)
                {
                    String value = Grid.Query.GetValues(filterKey)[0];
                    String filterType = filterKey.Substring((Grid.Name + "-" + Name + "-").Length);

                    Filter = MvcGrid.Filters.GetFilter(this, filterType, value);
                }

                FilterIsSet = true;

                return base.Filter;
            }
            set
            {
                base.Filter = value;
                FilterIsSet = true;
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

        public GridColumn(IGrid<T> grid, Expression<Func<T, TValue>> expression)
        {
            Grid = grid;
            IsEncoded = true;
            Expression = expression;
            FilterName = GetFilterName();
            ExpressionValue = expression.Compile();
            ProcessorType = GridProcessorType.Pre;
            IsSortable = IsFilterable = IsMember(expression);
            Name = ExpressionHelper.GetExpressionText(expression);
        }

        public override IQueryable<T> Process(IQueryable<T> items)
        {
            if (Filter != null && IsFilterable == true)
                items = Filter.Process(items);

            if (SortOrder == null || IsSortable != true)
                return items;

            if (SortOrder == GridSortOrder.Asc)
                return items.OrderBy(Expression);

            return items.OrderByDescending(Expression);
        }
        public override IHtmlString ValueFor(IGridRow row)
        {
            String value = GetValueFor(row);
            if (IsEncoded) value = WebUtility.HtmlEncode(value);

            return new HtmlString(value);
        }

        private Boolean? IsMember(Expression<Func<T, TValue>> expression)
        {
            if (expression.Body is MemberExpression)
                return null;

            return false;
        }

        private String GetValueFor(IGridRow row)
        {
            Object value;
            try
            {
                if (RenderValue != null)
                    value = RenderValue(row.Model as T);
                else
                    value = ExpressionValue(row.Model as T);
            }
            catch(NullReferenceException)
            {
                return String.Empty;
            }

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
