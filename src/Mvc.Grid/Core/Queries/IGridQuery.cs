using System.Collections.Specialized;

namespace NonFactors.Mvc.Grid
{
    public interface IGridQuery
    {
        NameValueCollection Query { get; set; }
    }
}
