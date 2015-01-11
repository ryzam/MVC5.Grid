using System;
using System.Linq.Expressions;

namespace NonFactors.Mvc.Grid
{
    public interface IGridFilter<T> : IGridProcessor<T>
    {
        LambdaExpression FilteredExpression { get; set; }
        String Value { get; set; }
        String Type { get; set; }
    }
}
