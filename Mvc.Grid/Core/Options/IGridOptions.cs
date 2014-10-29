using System;
using System.Web;

namespace NonFactors.Mvc.Grid
{
    public interface IGridOptions<T> where T : class
    {
        IGridOptions<T> Build(Action<IGridColumns<T>> builder);
    }
}
