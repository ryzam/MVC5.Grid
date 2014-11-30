using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace NonFactors.Mvc.Grid
{
    public class StringContainsFilter<TModel> : BaseGridFilter<TModel> where TModel : class
    {
        public override IQueryable<TModel> Process(IQueryable<TModel> items)
        {
            MethodInfo toUpperMethod = typeof(String).GetMethod("ToUpper", new Type[0]);
            MethodInfo containsMethod = typeof(String).GetMethod("Contains");
            ParameterExpression parameter = FilteredExpression.Parameters[0];
            Expression value = Expression.Constant(Value.ToUpper());

            Expression notNull = Expression.NotEqual(FilteredExpression.Body, Expression.Constant(null));
            Expression toUpper = Expression.Call(FilteredExpression.Body, toUpperMethod);
            Expression contains = Expression.Call(toUpper, containsMethod, value);

            Expression notNullContains = Expression.AndAlso(notNull, contains);
            Expression<Func<TModel, Boolean>> filter = Expression.Lambda<Func<TModel, Boolean>>(notNullContains, parameter);

            return items.Where(filter);
        }
    }
}
