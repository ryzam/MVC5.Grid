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

    public interface IGridColumn<TModel, TValue> : ISortableColumn<TModel>, IGridColumn where TModel : class
    {
        Expression<Func<TModel, TValue>> Expression { get; set; }
        Func<TModel, TValue> ValueFunction { get; set; }
        IGrid<TModel> Grid { get; set; }

        IGridColumn<TModel, TValue> As(Func<TModel, TValue> value);
        IGridColumn<TModel, TValue> Filterable(Boolean isFilterable);
        IGridColumn<TModel, TValue> Sortable(Boolean isSortable);
        IGridColumn<TModel, TValue> Encoded(Boolean isEncoded);
        IGridColumn<TModel, TValue> Formatted(String format);
        IGridColumn<TModel, TValue> Css(String cssClasses);
        IGridColumn<TModel, TValue> Titled(String title);
        IGridColumn<TModel, TValue> Named(String name);
    }
}
