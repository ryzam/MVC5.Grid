using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace NonFactors.Mvc.Grid
{
    public class HtmlGrid<TModel> : IHtmlGrid<TModel> where TModel : class
    {
        public String PartialViewName { get; set; }
        public Grid<TModel> Grid { get; set; }
        public HtmlHelper Html { get; set; }

        public HtmlGrid(HtmlHelper html, Grid<TModel> grid)
        {
            PartialViewName = "MvcGrid/_Grid";
            Html = html;
            Grid = grid;
        }

        public IHtmlGrid<TModel> Build(Action<IGridColumns<TModel>> builder)
        {
            builder(Grid.Columns);

            return this;
        }
        public IHtmlGrid<TModel> WithPager(Action<IGridPager> builder)
        {
            Grid.Pager = Grid.Pager ?? new GridPager<TModel>(Grid);
            builder(Grid.Pager);

            return this;
        }
        public IHtmlGrid<TModel> WithPager()
        {
            Grid.Pager = Grid.Pager ?? new GridPager<TModel>(Grid);

            return this;
        }

        public String ToHtmlString()
        {
            return Html.Partial(PartialViewName, Grid).ToHtmlString();
        }
    }
}
