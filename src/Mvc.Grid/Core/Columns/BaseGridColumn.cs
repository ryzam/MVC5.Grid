using System;
using System.Web;

namespace NonFactors.Mvc.Grid
{
    public abstract class BaseGridColumn : IGridColumn
    {
        public GridSortOrder? SortOrder { get; set; }
        public Boolean? IsSortable { get; set; }
        
        public Boolean IsEncoded { get; set; }
        public String CssClasses { get; set; }
        public String Format { get; set; }
        public String Title { get; set; }
        public String Name { get; set; }

        public IGridColumn Sortable(Boolean enabled)
        {
            IsSortable = enabled;

            return this;
        }
        
        public IGridColumn Formatted(String format)
        {
            Format = format;

            return this;
        }
        public IGridColumn Encoded(Boolean encode)
        {
            IsEncoded = encode;

            return this;
        }
        public IGridColumn Css(String cssClasses)
        {
            CssClasses = cssClasses;

            return this;
        }
        public IGridColumn Titled(String title)
        {
            Title = title;

            return this;
        }
        public IGridColumn Named(String name)
        {
            Name = name.ToLower();

            return this;
        }

        public abstract IHtmlString ValueFor(IGridRow row);
    }
}
