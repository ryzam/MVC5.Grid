using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace NonFactors.Mvc.Grid
{
    public abstract class BaseGridColumn<TModel, TValue> : IGridColumn<TModel, TValue> where TModel : class
    {
        public Expression<Func<TModel, TValue>> Expression { get; set; }
        public Func<TModel, TValue> ValueFunction { get; set; }

        public GridProcessorType Type { get; set; }
        public IGrid<TModel> Grid { get; set; }

        public GridSortOrder? SortOrder { get; set; }
        public Boolean? IsFilterable { get; set; }
        public Boolean? IsSortable { get; set; }
        public Boolean IsEncoded { get; set; }
        public String CssClasses { get; set; }
        public String Format { get; set; }
        public String Title { get; set; }
        public String Name { get; set; }

        public IGridColumn<TModel, TValue> RenderAs(Func<TModel, TValue> value)
        {
            ValueFunction = value;

            return this;
        }
        public IGridColumn<TModel, TValue> Filterable(Boolean isFilterable)
        {
            IsFilterable = isFilterable;

            return this;
        }
        public IGridColumn<TModel, TValue> Sortable(Boolean isSortable)
        {
            IsSortable = isSortable;

            return this;
        }
        public IGridColumn<TModel, TValue> Encoded(Boolean isEncoded)
        {
            IsEncoded = isEncoded;

            return this;
        }
        public IGridColumn<TModel, TValue> Formatted(String format)
        {
            Format = format;

            return this;
        }
        public IGridColumn<TModel, TValue> Css(String cssClasses)
        {
            CssClasses = cssClasses;

            return this;
        }
        public IGridColumn<TModel, TValue> Titled(String title)
        {
            Title = title;

            return this;
        }
        public IGridColumn<TModel, TValue> Named(String name)
        {
            Name = name;

            return this;
        }

        public abstract IQueryable<TModel> Process(IQueryable<TModel> items);
        public abstract IHtmlString ValueFor(IGridRow row);
        public abstract String GetSortingQuery();
    }
}
