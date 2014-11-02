using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace NonFactors.Mvc.Grid
{
    public class HtmlGrid<TModel> : IHtmlGrid<TModel> where TModel : class
    {
        public String PartialViewName { get; set; }
        public IGrid<TModel> Grid { get; set; }
        public HtmlHelper Html { get; set; }

        public HtmlGrid(HtmlHelper html, IGrid<TModel> grid)
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

        public IHtmlGrid<TModel> WithPager(Action<IGridPager<TModel>> builder)
        {
            Grid.Pager = Grid.Pager ?? new GridPager<TModel>(Html.ViewContext.RequestContext, Grid.Source);
            builder(Grid.Pager);

            if (!Grid.Processors.Contains(Grid.Pager))
                Grid.Processors.Add(Grid.Pager);

            return this;
        }
        public IHtmlGrid<TModel> WithPager()
        {
            return WithPager(builder => { });
        }

        public String ToHtmlString()
        {
            return Html.Partial(PartialViewName, Grid).ToHtmlString();
        }
    }
}
