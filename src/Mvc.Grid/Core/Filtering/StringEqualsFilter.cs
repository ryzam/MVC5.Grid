using System;
using System.Linq;
using System.Linq.Expressions;

namespace NonFactors.Mvc.Grid
{
    public class StringEqualsFilter<TModel> : BaseGridFilter<TModel> where TModel : class
    {
        public override IQueryable<TModel> Process(IQueryable<TModel> items)
        {
            Expression constraint = Expression.Constant(FilterValue);
            ParameterExpression parameter = FilteredExpression.Parameters[0];
            Expression equal = Expression.Equal(FilteredExpression.Body, constraint);
            Expression<Func<TModel, Boolean>> filter = Expression.Lambda<Func<TModel, Boolean>>(equal, parameter);

            return items.Where(filter);
        }
    }
}
