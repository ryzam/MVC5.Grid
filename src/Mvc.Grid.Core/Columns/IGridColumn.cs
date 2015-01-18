using System;
using System.Linq.Expressions;
using System.Web;

namespace NonFactors.Mvc.Grid
{
    public interface IGridColumn : IFilterableColumn, ISortableColumn
    {
        Boolean IsEncoded { get; set; }
        String CssClasses { get; set; }
        String Format { get; set; }
        String Title { get; set; }
        String Name { get; set; }

        IHtmlString ValueFor(IGridRow row);
    }

    public interface IGridColumn<T> : IFilterableColumn<T>, ISortableColumn<T>, IGridColumn
    {
        LambdaExpression Expression { get; }
        IGrid<T> Grid { get; }

        IGridColumn<T> RenderedAs(Func<T, Object> value);

        IGridColumn<T> Filterable(Boolean isFilterable);
        IGridColumn<T> FilteredAs(String filterName);

        IGridColumn<T> InitialSort(GridSortOrder order);
        IGridColumn<T> FirstSort(GridSortOrder order);
        IGridColumn<T> Sortable(Boolean isSortable);

        IGridColumn<T> Encoded(Boolean isEncoded);
        IGridColumn<T> Formatted(String format);
        IGridColumn<T> Css(String cssClasses);
        IGridColumn<T> Titled(String title);
        IGridColumn<T> Named(String name);
    }
}
