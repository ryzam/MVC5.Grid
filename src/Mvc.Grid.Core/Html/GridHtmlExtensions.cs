using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace NonFactors.Mvc.Grid
{
    public static class GridHtmlExtensions
    {
        public static HtmlGrid<T> Grid<T>(this HtmlHelper html, IEnumerable<T> source) where T : class
        {
            return new HtmlGrid<T>(html, new Grid<T>(source));
        }
        public static HtmlGrid<T> Grid<T>(this HtmlHelper html, String partialViewName, IEnumerable<T> source) where T : class
        {
            HtmlGrid<T> grid = new HtmlGrid<T>(html, new Grid<T>(source));
            grid.PartialViewName = partialViewName;

            return grid;
        }
    }
}
