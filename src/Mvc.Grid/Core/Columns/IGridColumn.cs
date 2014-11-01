using System;
using System.Web;

namespace NonFactors.Mvc.Grid
{
    public interface IGridColumn
    {
        Boolean? IsSortable { get; set; }
        Boolean IsEncoded { get; set; }
        String CssClasses { get; set; }
        String Format { get; set; }
        String Title { get; set; }

        IHtmlString ValueFor(IGridRow row);
    }

    public interface IGridColumn<TModel, TValue> : IGridColumn where TModel : class
    {
        Func<TModel, TValue> Expression { get; set; }

        IGridColumn<TModel, TValue> Sortable(Boolean enabled);
        IGridColumn<TModel, TValue> Formatted(String format);
        IGridColumn<TModel, TValue> Encoded(Boolean encode);
        IGridColumn<TModel, TValue> Css(String cssClasses);
        IGridColumn<TModel, TValue> Titled(String title);
    }
}
