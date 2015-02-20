using System;
using System.Linq;
using System.Linq.Expressions;

namespace NonFactors.Mvc.Grid
{
    public abstract class BaseGridFilter<T> : IGridFilter<T>
    {
        public LambdaExpression FilteredExpression { get; set; }
        public GridProcessorType ProcessorType { get; set; }
        public String Value { get; set; }
        public String Type { get; set; }

        protected BaseGridFilter()
        {
            ProcessorType = GridProcessorType.Pre;
        }

        public abstract IQueryable<T> Process(IQueryable<T> items);

        internal Expression<Func<T, Boolean>> ToLambda(Expression expression)
        {
            if (FilteredExpression.Body.Type.IsGenericType && FilteredExpression.Body.Type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                Expression notNull = Expression.NotEqual(FilteredExpression.Body, Expression.Constant(null));
                expression = Expression.AndAlso(notNull, expression);
            }

            return Expression.Lambda<Func<T, Boolean>>(expression, FilteredExpression.Parameters[0]);
        }
        internal Expression GetNullSafeExpression()
        {
            if (FilteredExpression.Body.Type.IsGenericType && FilteredExpression.Body.Type.GetGenericTypeDefinition() == typeof(Nullable<>))
                return Expression.Property(FilteredExpression.Body, FilteredExpression.Body.Type.GetProperty("Value"));

            return FilteredExpression.Body;
        }
    }
}
