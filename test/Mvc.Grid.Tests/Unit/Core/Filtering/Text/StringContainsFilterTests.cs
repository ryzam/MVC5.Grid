using NUnit.Framework;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class StringContainsFilterTests : BaseGridFilterTests
    {
        #region Method: Apply(Expression expression)

        [Test]
        public void Apply_FiltersItemsWithCaseInsensitiveComparison()
        {
            Expression<Func<GridModel, String>> expression = (model) => model.Name;
            StringContainsFilter filter = new StringContainsFilter();
            filter.Value = "Est";

            IQueryable<GridModel> items = new[]
            {
                new GridModel { Name = null },
                new GridModel { Name = "Tes" },
                new GridModel { Name = "test" },
                new GridModel { Name = "TEst" },
                new GridModel { Name = "Tst22" },
                new GridModel { Name = "TTEst2" }
            }.AsQueryable();

            IQueryable expected = items.Where(model => model.Name != null && model.Name.ToUpper().Contains("EST"));
            IQueryable actual = Filter(items, filter.Apply(expression.Body), expression);

            CollectionAssert.AreEqual(expected, actual);
        }

        #endregion
    }
}
