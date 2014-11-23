using System;
using System.Linq;
using System.Linq.Expressions;

namespace NonFactors.Mvc.Grid
{
    public abstract class BaseGridFilter<TModel> : IGridFilter<TModel> where TModel : class
    {
        public LambdaExpression FilteredExpression { get; set; }
        public GridProcessorType ProcessorType { get; set; }
        public String Value { get; set; }
        public String Type { get; set; }

        public BaseGridFilter()
        {
            ProcessorType = GridProcessorType.Pre;
        }

        public abstract IQueryable<TModel> Process(IQueryable<TModel> items);
    }
}
