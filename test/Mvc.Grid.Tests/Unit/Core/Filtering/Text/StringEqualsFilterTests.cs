using NUnit.Framework;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class StringEqualsFilterTests
    {
        #region Method: Process(IQueryable<T> items)

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void Process_FiltersEmptyOrNullValues(String value)
        {
            StringEqualsFilter<GridModel> filter = new StringEqualsFilter<GridModel>();
            Expression<Func<GridModel, String>> expression = (model) => model.Name;
            filter.FilteredExpression = expression;
            filter.Value = value;

            IQueryable<GridModel> models = new[]
            {
                new GridModel { Name = null },
                new GridModel { Name = "" },
                new GridModel { Name = "test" },
                new GridModel { Name = "Test" },
                new GridModel { Name = "Test2" }
            }.AsQueryable();

            IQueryable expected = models.Where(model => model.Name == null || model.Name == "");
            IQueryable actual = filter.Process(models);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Process_FiltersItemsWithCaseInsensitiveComparison()
        {
            StringEqualsFilter<GridModel> filter = new StringEqualsFilter<GridModel>();
            Expression<Func<GridModel, String>> expression = (model) => model.Name;
            filter.FilteredExpression = expression;
            filter.Value = "Test";

            IQueryable<GridModel> models = new[]
            {
                new GridModel { Name = null },
                new GridModel { Name = "Tes" },
                new GridModel { Name = "test" },
                new GridModel { Name = "Test" },
                new GridModel { Name = "Test2" }
            }.AsQueryable();

            IQueryable expected = models.Where(model => model.Name != null && model.Name.ToUpper() == "TEST");
            IQueryable actual = filter.Process(models);

            CollectionAssert.AreEqual(expected, actual);
        }

        #endregion
    }
}
