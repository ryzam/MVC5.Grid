using System.Collections.Specialized;
using System.Web;

namespace NonFactors.Mvc.Grid
{
    public class GridQuery : IGridQuery
    {
        public NameValueCollection Query { get; set; }

        public GridQuery(HttpContextBase httpContext)
        {
            Query = httpContext.Request.QueryString;
        }
    }
}
