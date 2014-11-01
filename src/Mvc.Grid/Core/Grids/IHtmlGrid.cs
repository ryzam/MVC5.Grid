using System;
using System.Web;

namespace NonFactors.Mvc.Grid
{
    public interface IHtmlGrid<TModel> : IHtmlString where TModel : class
    {
        String PartialViewName { get; set; }
        Grid<TModel> Grid { get; }

        IHtmlGrid<TModel> Build(Action<IGridColumns<TModel>> builder);
        IHtmlGrid<TModel> Empty(String text);

        IHtmlGrid<TModel> WithPager(Action<IGridPager> builder);
        IHtmlGrid<TModel> WithPager();
    }
}
