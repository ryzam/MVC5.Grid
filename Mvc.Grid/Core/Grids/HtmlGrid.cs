using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace NonFactors.Mvc.Grid
{
    public class HtmlGrid<TModel> : IHtmlGrid<TModel> where TModel : class
    {
        public Grid<TModel> Grid
        {
            get;
            set;
        }
        public HtmlHelper Html
        {
            get;
            set;
        }

        public HtmlGrid(HtmlHelper html, Grid<TModel> grid)
        {
            Html = html;
            Grid = grid;
        }

        public IHtmlGrid<TModel> Build(Action<IGridColumns<TModel>> builder)
        {
            builder(Grid.Columns);

            return this;
        }
        public IHtmlGrid<TModel> Pageable(Action<IGridPager> builder)
        {
            builder(Grid.Pager);

            return this;
        }

        public String ToHtmlString()
        {
            return Html.Partial("_MvcGrid", Grid).ToHtmlString();
        }
    }
}
