using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace NonFactors.Mvc.Grid
{
    public class HtmlGrid<TModel> : IGridOptions<TModel>, IHtmlString where TModel : class
    {
        private HtmlHelper html;
        private Grid<TModel> grid;

        public HtmlGrid(HtmlHelper html, Grid<TModel> grid)
        {
            this.html = html;
            this.grid = grid;
        }

        public IGridOptions<TModel> Build(Action<IGridColumns<TModel>> builder)
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
