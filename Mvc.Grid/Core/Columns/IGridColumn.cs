using System;
using System.Web;

namespace NonFactors.Mvc.Grid
{
    public interface IGridColumn
    {
        String Title { get; }

        IHtmlString ValueFor(IGridRow row);
    }

    public interface IGridColumn<T> : IGridColumn where T : class
    {
        IGridColumn<T> Titled(String title);
    }
}
