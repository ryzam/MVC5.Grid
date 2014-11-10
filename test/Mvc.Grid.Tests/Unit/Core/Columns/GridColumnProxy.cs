using NUnit.Framework;
using System;
using System.Linq.Expressions;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class GridColumnProxy<TModel, TValue> : GridColumn<TModel, TValue> where TModel : class
    {
        public Func<TModel, TValue> BaseCompiledExpression
        {
            get
            {
                return base.CompiledExpression;
            }
            set
            {
                base.CompiledExpression = value;
            }
        }

        public GridColumnProxy(IGrid<TModel> grid, Expression<Func<TModel, TValue>> expression)
            : base(grid, expression)
        {
        }
    }
}
