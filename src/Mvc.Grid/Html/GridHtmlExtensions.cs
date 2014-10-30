using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace NonFactors.Mvc.Grid.Html
{
    public static class GridHtmlExtensions
    {
        public static HtmlGrid<TModel> Grid<TModel>(this HtmlHelper html, IEnumerable<TModel> source) where TModel : class
        {
            return new HtmlGrid<TModel>(html, new Grid<TModel>(source.AsQueryable()));
        }
        public static HtmlGrid<TModel> Grid<TModel>(this HtmlHelper html, String partialViewName, IEnumerable<TModel> source) where TModel : class
        {
            HtmlGrid<TModel> grid = new HtmlGrid<TModel>(html, new Grid<TModel>(source.AsQueryable()));
            grid.PartialViewName = partialViewName;

            return grid;
        }
    }
}
