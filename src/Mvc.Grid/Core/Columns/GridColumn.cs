using System;
using System.Net;
using System.Web;

namespace NonFactors.Mvc.Grid
{
    public class GridColumn<TModel, TValue> : IGridColumn<TModel, TValue> where TModel : class
    {
        public Func<TModel, TValue> Expression { get; private set; }

        public Boolean? IsSortable { get; private set; }
        public Boolean IsEncoded { get; private set; }
        public String CssClasses { get; private set; }
        public String Format { get; private set; }
        public String Title { get; private set; }

        public GridColumn() : this(null)
        {
        }
        public GridColumn(Func<TModel, TValue> expression)
        {
            Expression = expression;
            IsEncoded = true;
        }

        public IGridColumn<TModel, TValue> Sortable(Boolean enabled)
        {
            IsSortable = enabled;

            return this;
        }
        public IGridColumn<TModel, TValue> Formatted(String format)
        {
            Format = format;

            return this;
        }
        public IGridColumn<TModel, TValue> Encoded(Boolean encode)
        {
            IsEncoded = encode;

            return this;
        }
        public IGridColumn<TModel, TValue> Css(String cssClasses)
        {
            CssClasses = cssClasses;

            return this;
        }
        public IGridColumn<TModel, TValue> Titled(String title)
        {
            Title = title;

            return this;
        }

        public IHtmlString ValueFor(IGridRow row)
        {
            String value = GetRawValueFor(row);
            if (IsEncoded) value = WebUtility.HtmlEncode(value);

            return new HtmlString(value);
        }

        private String GetRawValueFor(IGridRow row)
        {
            if (Expression == null)
                return String.Empty;

            TValue value = Expression(row.Model as TModel);
            if (value == null)
                return String.Empty;

            if (Format == null)
                return value.ToString();

            return String.Format(Format, value);
        }
    }
}
