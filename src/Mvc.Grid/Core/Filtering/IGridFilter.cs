using System;
using System.Linq.Expressions;

namespace NonFactors.Mvc.Grid
{
    public interface IGridFilter<TModel> : IGridProcessor<TModel> where TModel : class
    {
        LambdaExpression FilteredExpression { get; set; }
        String FilterValue { get; set; }
        String FilterType { get; set; }
    }
}
