using System;
using System.Web;

namespace NonFactors.Mvc.Grid
{
    public interface IHtmlGrid<TModel> : IHtmlString where TModel : class
    {
        String PartialViewName { get; set; }

        IHtmlGrid<TModel> Build(Action<IGridColumns<TModel>> builder);
        IHtmlGrid<TModel> WithPager(Action<IGridPager> builder);
        IHtmlGrid<TModel> WithPager();
    }
}
