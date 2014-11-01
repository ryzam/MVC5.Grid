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
        public IHtmlGrid<TModel> Sortable(Boolean enabled)
        {
            foreach (IGridColumn column in Grid.Columns)
                if (column.IsSortable == null)
                    column.IsSortable = enabled;

            return this;
        }
        public IHtmlGrid<TModel> Empty(String text)
        {
            Grid.EmptyText = text;

            return this;
        }
        public IHtmlGrid<TModel> Named(String name)
        {
            Grid.Name = name;

            return this;
        }

        public IHtmlGrid<TModel> WithPager(Action<IGridPager> builder)
        {
            Grid.Pager = Grid.Pager ?? new GridPager<TModel>(Html.ViewContext.RequestContext, Grid.Source);
            builder(Grid.Pager);

            return this;
        }
        public IHtmlGrid<TModel> WithPager()
        {
            Grid.Pager = Grid.Pager ?? new GridPager<TModel>(Html.ViewContext.RequestContext, Grid.Source);

            return this;
        }

        public String ToHtmlString()
        {
            return Html.Partial(PartialViewName, Grid).ToHtmlString();
        }
    }
}
