using System;

namespace NonFactors.Mvc.Grid
{
    public interface IGridOptions<TModel> where TModel : class
    {
        IGridOptions<TModel> Build(Action<IGridColumns<TModel>> builder);
    }
}
