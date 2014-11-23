using NUnit.Framework;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class StringContainsFilterTests
    {
        #region Method: Process(IQueryable<TModel> items)

        [Test]
        public void Process_FiltersItems()
        {
            StringContainsFilter<GridModel> filter = new StringContainsFilter<GridModel>();
            Expression<Func<GridModel, String>> expression = (model) => model.Name;
            filter.FilteredExpression = expression;
            filter.Value = "Est";

            IQueryable<GridModel> models = new[]
            {
                new GridModel { Name = null },
                new GridModel { Name = "Tes" },
                new GridModel { Name = "test" },
                new GridModel { Name = "TEst" },
                new GridModel { Name = "TEst2" }
            }.AsQueryable();

            IQueryable expected = models.Where(model => model.Name != null && model.Name.Contains("Est"));
            var actual = filter.Process(models).ToList();

            CollectionAssert.AreEqual(expected, actual);
        }

        #endregion
    }
}
