using System;
using System.Web;

namespace NonFactors.Mvc.Grid
{
    public interface IHtmlGrid<TModel> : IHtmlString where TModel : class
    {
        IHtmlGrid<TModel> Build(Action<IGridColumns<TModel>> builder);
        IHtmlGrid<TModel> Pageable(Action<IGridPager> builder);
    }
}
