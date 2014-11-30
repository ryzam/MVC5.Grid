using NUnit.Framework;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class StringEndsWithFilterTests
    {
        #region Method: Process(IQueryable<TModel> items)

        [Test]
        public void Process_FiltersItemsWithCaseInsensitiveComparison()
        {
            StringEndsWithFilter<GridModel> filter = new StringEndsWithFilter<GridModel>();
            Expression<Func<GridModel, String>> expression = (model) => model.Name;
            filter.FilteredExpression = expression;
            filter.Value = "Test";

            IQueryable<GridModel> models = new[]
            {
                new GridModel { Name = null },
                new GridModel { Name = "Tes" },
                new GridModel { Name = "test" },
                new GridModel { Name = "Test" },
                new GridModel { Name = "TEST2" }
            }.AsQueryable();

            IQueryable expected = models.Where(model => model.Name != null && model.Name.ToUpper().EndsWith("TEST"));
            IQueryable actual = filter.Process(models);

            CollectionAssert.AreEqual(expected, actual);
        }

        #endregion
    }
}
