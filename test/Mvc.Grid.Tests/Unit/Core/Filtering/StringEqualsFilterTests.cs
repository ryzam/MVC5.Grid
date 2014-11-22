using NUnit.Framework;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class StringEqualsFilterTests
    {
        #region Method: Process(IQueryable<TModel> items)

        [Test]
        public void Process_FiltersItems()
        {
            StringEqualsFilter<GridModel> filter = new StringEqualsFilter<GridModel>();
            Expression<Func<GridModel, String>> expression = (model) => model.Name;
            filter.FilteredExpression = expression;
            filter.FilterValue = "Test";

            IQueryable<GridModel> models = new[]
            {
                new GridModel { Name = null },
                new GridModel { Name = "Tes" },
                new GridModel { Name = "test" },
                new GridModel { Name = "Test" },
                new GridModel { Name = "Test2" }
            }.AsQueryable();

            IQueryable expected = models.Where(model => model.Name == "Test");
            IQueryable actual = filter.Process(models);

            CollectionAssert.AreEqual(expected, actual);
        }

        #endregion
    }
}
