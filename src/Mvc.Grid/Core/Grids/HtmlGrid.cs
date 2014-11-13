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
            grid.Query = grid.Query ?? new GridQuery(html.ViewContext.HttpContext.Request.QueryString);
            PartialViewName = "MvcGrid/_Grid";
            Html = html;
            Grid = grid;
        }

        public IHtmlGrid<TModel> Build(Action<IGridColumns<TModel>> builder)
        {
            builder(Grid.Columns);

            return this;
        }
        public IHtmlGrid<TModel> Filterable(Boolean isFilterable)
        {
            foreach (IGridColumn column in Grid.Columns)
                if (column.IsFilterable == null)
                    column.IsFilterable = isFilterable;

            return this;
        }
        public IHtmlGrid<TModel> Sortable(Boolean isSortable)
        {
            foreach (IGridColumn column in Grid.Columns)
                if (column.IsSortable == null)
                    column.IsSortable = isSortable;

            return this;
        }
        public IHtmlGrid<TModel> Css(String cssClasses)
        {
            Grid.CssClasses = cssClasses;

            return this;
        }
        public IHtmlGrid<TModel> Empty(String text)
        {
            Grid.EmptyText = text;

            return this;
        }
        public IHtmlGrid<TModel> Named(String name)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentException("Grid name can not be null or empty.");

            Grid.Name = name;

            return this;
        }

        public IHtmlGrid<TModel> WithPager(Action<IGridPager<TModel>> builder)
        {
            Grid.Pager = Grid.Pager ?? new GridPager<TModel>(Grid);
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
