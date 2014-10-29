using System;
using System.Web;

namespace NonFactors.Mvc.Grid
{
    public interface IGridColumn
    {
        String Title { get; }

        IHtmlString ValueFor(IGridRow row);
    }

    public interface IGridColumn<TModel> : IGridColumn where TModel : class
    {
        IGridColumn<TModel> Titled(String title);
    }
}
