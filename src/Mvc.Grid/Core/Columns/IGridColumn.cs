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

    public interface IGridColumn<TModel, TValue> : IFilterableColumn<TModel>, ISortableColumn<TModel>, IGridColumn where TModel : class
    {
        Expression<Func<TModel, TValue>> Expression { get; set; }
        Func<TModel, TValue> ExpressionValue { get; set; }
        Func<TModel, Object> RenderValue { get; set; }
        IGrid<TModel> Grid { get; set; }

        IGridColumn<TModel, TValue> RenderedAs(Func<TModel, Object> value);

        IGridColumn<TModel, TValue> Filterable(Boolean isFilterable);
        IGridColumn<TModel, TValue> FilteredAs(String filterName);

        IGridColumn<TModel, TValue> FirstSortIn(GridSortOrder order);
        IGridColumn<TModel, TValue> Sortable(Boolean isSortable);

        IGridColumn<TModel, TValue> Encoded(Boolean isEncoded);
        IGridColumn<TModel, TValue> Formatted(String format);
        IGridColumn<TModel, TValue> Css(String cssClasses);
        IGridColumn<TModel, TValue> Titled(String title);
        IGridColumn<TModel, TValue> Named(String name);
    }
}
