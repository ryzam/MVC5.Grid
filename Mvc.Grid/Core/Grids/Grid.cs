using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace NonFactors.Mvc.Grid
{
    public class Grid<T> : IGrid<T> where T : class
    {
        private HtmlHelper html;

        public IQueryable<T> Source
        {
            get;
            protected set;
        }

        public IGridColumns<T> Columns
        {
            get;
            protected set;
        }
        IGridColumns IGrid.Columns
        {
            get
            {
                return Columns;
            }
        }
        public IGridRows Rows
        {
            get;
            protected set;
        }

        public Grid(HtmlHelper html, IQueryable<T> source)
        {
            this.html = html;

            Columns = new GridColumns<T>();
            Rows = new GridRows<T>();
            Source = source;
        }

        public IGridOptions<T> Build(Action<IGridColumns<T>> builder)
        {
            builder(Columns);

            return this;
        }

        public String ToHtmlString()
        {
            return html.Partial("_MvcGrid", this).ToHtmlString();
        }
    }
}
