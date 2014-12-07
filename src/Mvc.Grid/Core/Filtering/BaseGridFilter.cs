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

        protected Expression<Func<TModel, Boolean>> ToLambda(Expression expression)
        {
            if (FilteredExpression.Body.Type.IsGenericType && FilteredExpression.Body.Type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                Expression notNull = Expression.NotEqual(FilteredExpression.Body, Expression.Constant(null));
                expression = Expression.AndAlso(notNull, expression);
            }

            return Expression.Lambda<Func<TModel, Boolean>>(expression, FilteredExpression.Parameters[0]);
        }
        protected Expression GetNullSafeExpression()
        {
            if (FilteredExpression.Body.Type.IsGenericType && FilteredExpression.Body.Type.GetGenericTypeDefinition() == typeof(Nullable<>))
                return Expression.Property(FilteredExpression.Body, FilteredExpression.Body.Type.GetProperty("Value"));

            return FilteredExpression.Body;
        }
    }
}
