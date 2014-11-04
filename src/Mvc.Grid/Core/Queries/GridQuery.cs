using System.Collections.Specialized;
using System.Web;

namespace NonFactors.Mvc.Grid
{
    public class GridQuery : IGridQuery
    {
        public IGrid Grid { get; set; }
        public NameValueCollection Query { get; set; }

        public GridQuery(IGrid grid, HttpContextBase httpContext)
        {
            Grid = grid;
            Query = httpContext.Request.QueryString;
        }
    }
}
