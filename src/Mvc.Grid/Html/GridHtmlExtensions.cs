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
    }
}
