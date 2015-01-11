using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace NonFactors.Mvc.Grid
{
    public class StringStartsWithFilter<T> : BaseGridFilter<T>
    {
        public override IQueryable<T> Process(IQueryable<T> items)
        {
            MethodInfo startsWithMethod = typeof(String).GetMethod("StartsWith", new[] { typeof(String) });
            MethodInfo toUpperMethod = typeof(String).GetMethod("ToUpper", new Type[0]);
            ParameterExpression parameter = FilteredExpression.Parameters[0];
            Expression value = Expression.Constant(Value.ToUpper());

            Expression notNull = Expression.NotEqual(FilteredExpression.Body, Expression.Constant(null));
            Expression toUpper = Expression.Call(FilteredExpression.Body, toUpperMethod);
            Expression startsWith = Expression.Call(toUpper, startsWithMethod, value);

            Expression notNullStartsWith = Expression.AndAlso(notNull, startsWith);
            Expression<Func<T, Boolean>> filter = Expression.Lambda<Func<T, Boolean>>(notNullStartsWith, parameter);

            return items.Where(filter);
        }
    }
}
