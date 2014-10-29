using System;
using System.Web;

namespace NonFactors.Mvc.Grid
{
    public class GridColumn<TModel, TProperty> : IGridColumn<TModel> where TModel : class
    {
        protected Func<TModel, TProperty> Expression
        {
            get;
            set;
        }
        public String Title
        {
            get;
            protected set;
        }

        public GridColumn()
        {
        }
        public GridColumn(Func<TModel, TProperty> expression)
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
            return new HtmlString(Expression(row.Model as TModel).ToString());
        }
    }
}
