using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace NonFactors.Mvc.Grid
{
    public abstract class BaseGridColumn<TModel, TValue> : IGridColumn<TModel, TValue> where TModel : class
    {
        public Expression<Func<TModel, TValue>> Expression { get; set; }
        public Func<TModel, TValue> ExpressionValue { get; set; }
        public Func<TModel, Object> RenderValue { get; set; }
        public GridProcessorType ProcessorType { get; set; }
        public IGrid<TModel> Grid { get; set; }

        public virtual GridSortOrder? FirstSortOrder { get; set; }
        public virtual GridSortOrder? SortOrder { get; set; }
        public virtual Boolean? IsSortable { get; set; }

        public virtual IGridFilter<TModel> Filter { get; set; }
        public virtual Boolean? IsFilterable { get; set; }
        public virtual String FilterValue { get; set; }
        public virtual String FilterType { get; set; }
        public virtual String FilterName { get; set; }

        public Boolean IsEncoded { get; set; }
        public String CssClasses { get; set; }
        public String Format { get; set; }
        public String Title { get; set; }
        public String Name { get; set; }

        public virtual IGridColumn<TModel, TValue> RenderedAs(Func<TModel, Object> value)
        {
            RenderValue = value;

            return this;
        }

        public virtual IGridColumn<TModel, TValue> Filterable(Boolean isFilterable)
        {
            IsFilterable = isFilterable;

            return this;
        }
        public virtual IGridColumn<TModel, TValue> FilteredAs(String filterName)
        {
            FilterName = filterName;

            return this;
        }

        public virtual IGridColumn<TModel, TValue> FirstSortIn(GridSortOrder order)
        {
            FirstSortOrder = order;

            return this;
        }
        public virtual IGridColumn<TModel, TValue> Sortable(Boolean isSortable)
        {
            IsSortable = isSortable;

            return this;
        }

        public virtual IGridColumn<TModel, TValue> Encoded(Boolean isEncoded)
        {
            IsEncoded = isEncoded;

            return this;
        }
        public virtual IGridColumn<TModel, TValue> Formatted(String format)
        {
            Format = format;

            return this;
        }
        public virtual IGridColumn<TModel, TValue> Css(String cssClasses)
        {
            CssClasses = cssClasses;

            return this;
        }
        public virtual IGridColumn<TModel, TValue> Titled(String title)
        {
            Title = title;

            return this;
        }
        public virtual IGridColumn<TModel, TValue> Named(String name)
        {
            Name = name;

            return this;
        }

        public abstract IQueryable<TModel> Process(IQueryable<TModel> items);
        public abstract IHtmlString ValueFor(IGridRow row);
    }
}
