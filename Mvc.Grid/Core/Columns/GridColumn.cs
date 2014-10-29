using System;
using System.Web;

namespace NonFactors.Mvc.Grid
{
    public class GridColumn<T> : IGridColumn<T> where T : class
    {
        public String Title
        {
            get;
            protected set;
        }

        public IGridColumn<T> Titled(String title)
        {
            Title = title;

            return this;
        }

        public IHtmlString ValueFor(IGridRow row)
        {
            return new HtmlString(row.Model.ToString());
        }
    }
}
