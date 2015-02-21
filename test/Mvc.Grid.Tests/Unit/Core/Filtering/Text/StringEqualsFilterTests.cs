using NUnit.Framework;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class StringEqualsFilterTests : BaseGridFilterTests
    {
        #region Method: Apply(Expression expression)

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void Apply_FiltersEmptyOrNullValues(String value)
        {
            Expression<Func<GridModel, String>> expression = (model) => model.Name;
            StringEqualsFilter filter = new StringEqualsFilter();
            filter.Value = value;

            IQueryable<GridModel> items = new[]
            {
                new GridModel { Name = null },
                new GridModel { Name = "" },
                new GridModel { Name = "test" },
                new GridModel { Name = "Test" },
                new GridModel { Name = "Test2" }
            }.AsQueryable();

            IQueryable expected = items.Where(model => model.Name == null || model.Name == "");
            IQueryable actual = Filter(items, filter.Apply(expression.Body), expression);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Apply_FiltersItemsWithCaseInsensitiveComparison()
        {
            Expression<Func<GridModel, String>> expression = (model) => model.Name;
            StringEqualsFilter filter = new StringEqualsFilter();
            filter.Value = "Test";

            IQueryable<GridModel> items = new[]
            {
                new GridModel { Name = null },
                new GridModel { Name = "Tes" },
                new GridModel { Name = "test" },
                new GridModel { Name = "Test" },
                new GridModel { Name = "Test2" }
            }.AsQueryable();

            IQueryable expected = items.Where(model => model.Name != null && model.Name.ToUpper() == "TEST");
            IQueryable actual = Filter(items, filter.Apply(expression.Body), expression);

            CollectionAssert.AreEqual(expected, actual);
        }

        #endregion
    }
}
