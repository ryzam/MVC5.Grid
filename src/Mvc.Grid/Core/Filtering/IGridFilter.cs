using System;
using System.Linq.Expressions;

namespace NonFactors.Mvc.Grid
{
    public interface IGridFilter<TModel> : IGridProcessor<TModel> where TModel : class
    {
        LambdaExpression FilteredExpression { get; set; }
        String Value { get; set; }
        String Type { get; set; }
    }
}
