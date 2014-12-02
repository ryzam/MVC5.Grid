using System;
using System.Linq;
using System.Linq.Expressions;

namespace NonFactors.Mvc.Grid
{
    public class DateTimeFilter<TModel> : BaseGridFilter<TModel> where TModel : class
    {
        public override IQueryable<TModel> Process(IQueryable<TModel> items)
        {
            Expression<Func<TModel, Boolean>> filter = GetFilterExpression();
            if (filter == null) return items;

            return items.Where(filter);
        }

        private Expression<Func<TModel, Boolean>> GetFilterExpression()
        {
            Object value = GetDateValue();
            if (value == null) return null;

            switch (Type)
            {
                case "Equals":
                    return ToLambda(Expression.Equal(FilteredExpression.Body, Expression.Constant(value)));
                case "LessThan":
                    return ToLambda(Expression.LessThan(FilteredExpression.Body, Expression.Constant(value)));
                case "GreaterThan":
                    return ToLambda(Expression.GreaterThan(FilteredExpression.Body, Expression.Constant(value)));
                case "LessThanOrEqual":
                    return ToLambda(Expression.LessThanOrEqual(FilteredExpression.Body, Expression.Constant(value)));
                case "GreaterThanOrEqual":
                    return ToLambda(Expression.GreaterThanOrEqual(FilteredExpression.Body, Expression.Constant(value)));
            }

            return null;
        }
        private Expression<Func<TModel, Boolean>> ToLambda(Expression expression)
        {
            return Expression.Lambda<Func<TModel, Boolean>>(expression, FilteredExpression.Parameters[0]);
        }
        private Object GetDateValue()
        {
            DateTime date;
            if (DateTime.TryParse(Value, out date))
                return date;

            return null;
        }
    }
}
