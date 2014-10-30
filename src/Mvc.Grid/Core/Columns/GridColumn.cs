using System;
using System.Web;

namespace NonFactors.Mvc.Grid
{
    public class GridColumn<TModel, TValue> : IGridColumn<TModel> where TModel : class
    {
        public Func<TModel, TValue> Expression { get; private set; }
        public String CssClasses { get; private set; }
        public String Format { get; private set; }
        public String Title { get; private set; }
        public Int32 Width { get; private set; }

        public GridColumn() : this(null)
        {
        }
        public GridColumn(Func<TModel, TValue> expression)
        {
            Expression = expression;
        }

        public IGridColumn<TModel> Formatted(String format)
        {
            Format = format;

            return this;
        }
        public IGridColumn<TModel> Css(String cssClasses)
        {
            CssClasses = cssClasses;

            return this;
        }
        public IGridColumn<TModel> SetWidth(Int32 width)
        {
            Width = width;

            return this;
        }
        public IGridColumn<TModel> Titled(String title)
        {
            Title = title;

            return this;
        }

        public IHtmlString ValueFor(IGridRow row)
        {
            if (Expression == null)
                return new HtmlString(String.Empty);

            TValue value = Expression(row.Model as TModel);
            if (value == null)
                return new HtmlString(String.Empty);

            if (Format == null)
                return new HtmlString(value.ToString());

            return new HtmlString(String.Format(Format, value));
        }
    }
}
