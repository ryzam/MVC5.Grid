using System;
using System.Web;

namespace NonFactors.Mvc.Grid
{
    public interface IHtmlGrid<T> : IHtmlString
    {
        String PartialViewName { get; set; }
        IGrid<T> Grid { get; }

        IHtmlGrid<T> Build(Action<IGridColumns<T>> builder);
        IHtmlGrid<T> ProcessWith(IGridProcessor<T> processor);

        IHtmlGrid<T> DataSourceAction(String action);
        IHtmlGrid<T> DataSource(String url);

        IHtmlGrid<T> Filterable(Boolean isFilterable);
        IHtmlGrid<T> Filterable();

        IHtmlGrid<T> Sortable(Boolean isSortable);
        IHtmlGrid<T> Sortable();

        IHtmlGrid<T> RowCss(Func<T, String> cssClasses);
        IHtmlGrid<T> Css(String cssClasses);
        IHtmlGrid<T> Empty(String text);
        IHtmlGrid<T> Named(String name);

        IHtmlGrid<T> Pageable(Action<IGridPager<T>> builder);
        IHtmlGrid<T> Pageable();
    }
}
