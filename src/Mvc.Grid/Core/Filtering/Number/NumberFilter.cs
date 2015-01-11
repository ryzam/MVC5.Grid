using System;
using System.Linq;
using System.Linq.Expressions;

namespace NonFactors.Mvc.Grid
{
    public abstract class NumberFilter<T> : BaseGridFilter<T>
    {
        public override IQueryable<T> Process(IQueryable<T> items)
        {
            Object value = GetNumericValue();
            if (value == null) return items;

            Expression<Func<T, Boolean>> filter = GetFilterExpression(value);
            if (filter == null) return items;

            return items.Where(filter);
        }

        private Expression<Func<T, Boolean>> GetFilterExpression(Object value)
        {
            switch (Type)
            {
                case "Equals":
                    return ToLambda(Expression.Equal(GetNullSafeExpression(), Expression.Constant(value)));
                case "LessThan":
                    return ToLambda(Expression.LessThan(GetNullSafeExpression(), Expression.Constant(value)));
                case "GreaterThan":
                    return ToLambda(Expression.GreaterThan(GetNullSafeExpression(), Expression.Constant(value)));
                case "LessThanOrEqual":
                    return ToLambda(Expression.LessThanOrEqual(GetNullSafeExpression(), Expression.Constant(value)));
                case "GreaterThanOrEqual":
                    return ToLambda(Expression.GreaterThanOrEqual(GetNullSafeExpression(), Expression.Constant(value)));
            }

            return null;
        }

        public abstract Object GetNumericValue();
    }
}
