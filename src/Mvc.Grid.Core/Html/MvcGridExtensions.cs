using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace NonFactors.Mvc.Grid
{
    public static class MvcGridExtensions
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

        public static MvcHtmlString AjaxGrid(this HtmlHelper html, String dataSource)
        {
            return html.Partial("MvcGrid/_AjaxGrid", dataSource);
        }
    }
}
