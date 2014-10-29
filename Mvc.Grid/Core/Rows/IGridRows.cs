using System.Collections.Generic;

namespace NonFactors.Mvc.Grid
{
    public interface IGridRows : IEnumerable<IGridRow>
    {
    }

    public interface IGridRows<TModel> : IGridRows where TModel : class
    {
    }
}
