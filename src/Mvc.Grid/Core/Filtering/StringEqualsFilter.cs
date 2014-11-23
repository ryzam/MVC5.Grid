using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace NonFactors.Mvc.Grid
{
    public class StringEqualsFilter<TModel> : BaseGridFilter<TModel> where TModel : class
    {
        public override IQueryable<TModel> Process(IQueryable<TModel> items)
        {
            MethodInfo toUpperMethod = typeof(String).GetMethod("ToUpper", new Type[0]);
            ParameterExpression parameter = FilteredExpression.Parameters[0];
            Expression value = Expression.Constant(Value.ToUpper());

            Expression notNull = Expression.NotEqual(FilteredExpression.Body, Expression.Constant(null));
            Expression toUpper = Expression.Call(FilteredExpression.Body, toUpperMethod);
            Expression equals = Expression.Equal(toUpper, value);

            Expression notNullEquals = Expression.AndAlso(notNull, equals);
            Expression<Func<TModel, Boolean>> filter = Expression.Lambda<Func<TModel, Boolean>>(notNullEquals, parameter);

            return items.Where(filter);
        }
    }
}
