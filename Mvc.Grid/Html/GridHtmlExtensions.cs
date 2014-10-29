using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace NonFactors.Mvc.Grid.Html
{
    public static class GridHtmlExtensions
    {
        public static IGrid<T> Grid<T>(this HtmlHelper<T> html, IEnumerable<T> source) where T : class
        {
            return new Grid<T>(html, source.AsQueryable());
        }
    }
}
