using System;
using System.Web;

namespace NonFactors.Mvc.Grid
{
    public interface IGridColumn
    {
        Boolean IsEncoded { get; }
        String CssClasses { get; }
        String Format { get; }
        String Title { get; }

        IHtmlString ValueFor(IGridRow row);
    }

    public interface IGridColumn<TModel> : IGridColumn where TModel : class
    {
        IGridColumn<TModel> Formatted(String format);
        IGridColumn<TModel> Encoded(Boolean encode);
        IGridColumn<TModel> Css(String cssClasses);
        IGridColumn<TModel> Titled(String title);
    }
}
