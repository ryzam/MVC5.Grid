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
        private Boolean GridSortIsSet { get; set; }
        public override GridSortOrder? SortOrder
        {
            get
            {
                if (GridSortIsSet)
                    return base.SortOrder;

                if (Grid.Query[Grid.Name + "-Sort"] == Name)
                {
                    String orderValue = Grid.Query[Grid.Name + "-Order"];
                    GridSortOrder order;

                    if (Enum.TryParse<GridSortOrder>(orderValue, out order))
                        SortOrder = order;
                }

                GridSortIsSet = true;

                return base.SortOrder;
            }
            set
            {
                base.SortOrder = value;
                GridSortIsSet = true;
            }
        }

        private Boolean GridFilterIsSet { get; set; }
        public override IGridFilter<TModel> Filter
        {
            get
            {
                if (GridFilterIsSet)
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

                GridFilterIsSet = true;

                return base.Filter;
            }
            set
            {
                base.Filter = value;
                GridFilterIsSet = true;
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
            RawValueFor = expression.Compile();
            ProcessorType = GridProcessorType.Pre;
            IsSortable = IsFilterable = IsMember(expression);
            Name = ExpressionHelper.GetExpressionText(expression);
        }

        public override IQueryable<TModel> Process(IQueryable<TModel> items)
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
            String value = GetRawValueFor(row);
            if (IsEncoded) value = WebUtility.HtmlEncode(value);

            return new HtmlString(value);
        }

        private Boolean? IsMember(Expression<Func<TModel, TValue>> expression)
        {
            if (expression.Body is MemberExpression)
                return null;

            return false;
        }

        private String GetRawValueFor(IGridRow row)
        {
            TValue value = RawValueFor(row.Model as TModel);
            if (value == null) return String.Empty;

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
