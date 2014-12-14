using System;
using System.Web;

namespace NonFactors.Mvc.Grid
{
    public interface IHtmlGrid<TModel> : IHtmlString where TModel : class
    {
        String PartialViewName { get; set; }
        IGrid<TModel> Grid { get; }

        IHtmlGrid<TModel> Build(Action<IGridColumns<TModel>> builder);
        IHtmlGrid<TModel> ProcessWith(IGridProcessor<TModel> processor);

        IHtmlGrid<TModel> Filterable(Boolean isFilterable);
        IHtmlGrid<TModel> Filterable();

        IHtmlGrid<TModel> Sortable(Boolean isSortable);
        IHtmlGrid<TModel> Sortable();

        IHtmlGrid<TModel> RowCss(Func<TModel, String> cssClasses);
        IHtmlGrid<TModel> Css(String cssClasses);
        IHtmlGrid<TModel> Empty(String text);
        IHtmlGrid<TModel> Named(String name);

        IHtmlGrid<TModel> Pageable(Action<IGridPager<TModel>> builder);
        IHtmlGrid<TModel> Pageable();
    }
}
