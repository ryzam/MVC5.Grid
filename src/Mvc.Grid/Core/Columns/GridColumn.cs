using System;
using System.Web;

namespace NonFactors.Mvc.Grid
{
    public class GridColumn<TModel, TValue> : IGridColumn<TModel> where TModel : class
    {
        protected Func<TModel, TValue> Expression { get; set; }
        public String Title { get; protected set; }

        public GridColumn() : this(null)
        {
        }
        public GridColumn(Func<TModel, TValue> expression)
        {
            Expression = expression;
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

            return new HtmlString(value.ToString());
        }
    }
}
