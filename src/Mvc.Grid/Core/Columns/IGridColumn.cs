using System;
using System.Web;

namespace NonFactors.Mvc.Grid
{
    public interface IGridColumn
    {
        String CssClasses { get; }
        String Format { get; }
        String Title { get; }
        Int32 Width { get; }

        IHtmlString ValueFor(IGridRow row);
    }

    public interface IGridColumn<TModel> : IGridColumn where TModel : class
    {
        IGridColumn<TModel> Formatted(String format);
        IGridColumn<TModel> Css(String cssClasses);
        IGridColumn<TModel> SetWidth(Int32 width);
        IGridColumn<TModel> Titled(String title);
    }
}
