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
            ParameterExpression parameter = FilteredExpression.Parameters[0];
            Expression filterExpression;

            if (String.IsNullOrEmpty(Value))
            {
                Expression equalsNull = Expression.Equal(FilteredExpression.Body, Expression.Constant(null));
                Expression isEmpty = Expression.Equal(FilteredExpression.Body, Expression.Constant(""));

                filterExpression = Expression.OrElse(equalsNull, isEmpty);
            }
            else
            {
                MethodInfo toUpperMethod = typeof(String).GetMethod("ToUpper", new Type[0]);
                Expression value = Expression.Constant(Value.ToUpper());

                Expression notNull = Expression.NotEqual(FilteredExpression.Body, Expression.Constant(null));
                Expression toUpper = Expression.Call(FilteredExpression.Body, toUpperMethod);
                Expression equals = Expression.Equal(toUpper, value);

                filterExpression = Expression.AndAlso(notNull, equals);
            }

            Expression<Func<TModel, Boolean>> filter = Expression.Lambda<Func<TModel, Boolean>>(filterExpression, parameter);

            return items.Where(filter);
        }
    }
}
