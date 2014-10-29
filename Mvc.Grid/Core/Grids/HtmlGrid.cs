using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace NonFactors.Mvc.Grid
{
    public class HtmlGrid<T> : IGridOptions<T>, IHtmlString where T : class
    {
        private HtmlHelper html;
        private Grid<T> grid;

        public HtmlGrid(HtmlHelper html, Grid<T> grid)
        {
            this.html = html;
            this.grid = grid;
        }

        public IGridOptions<T> Build(Action<IGridColumns<T>> builder)
        {
            builder(grid.Columns);

            return this;
        }

        public String ToHtmlString()
        {
            return html.Partial("_MvcGrid", grid).ToHtmlString();
        }
    }
}
