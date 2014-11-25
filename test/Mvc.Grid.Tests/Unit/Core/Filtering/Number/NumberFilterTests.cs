using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class NumberFilterTests
    {
        private NumberFilter<GridModel> filter;
        private IQueryable<GridModel> models;

        [SetUp]
        public void SetUp()
        {
            models = new[] { new GridModel(), new GridModel { Sum = 1 }, new GridModel() { Sum = 2 } }.AsQueryable();
            Expression<Func<GridModel, Int32>> expression = (model) => model.Sum;
            filter = Substitute.ForPartsOf<NumberFilter<GridModel>>();
            filter.FilteredExpression = expression;
        }

        #region Method: Process(IQueryable<TModel> items)

        [Test]
        public void Process_OnNullNumericValueReturnsSameItems()
        {
            filter.GetNumericValue().Returns(null);

            IEnumerable actual = filter.Process(models);
            IEnumerable expected = models;

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void Process_FiltersUsingEquals()
        {
            filter.GetNumericValue().Returns(1);
            filter.Type = "Equals";

            IEnumerable expected = models.Where(model => model.Sum == 1);
            IEnumerable actual = filter.Process(models);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Process_FiltersUsingLessThan()
        {
            filter.GetNumericValue().Returns(1);
            filter.Type = "LessThan";

            IEnumerable expected = models.Where(model => model.Sum < 1);
            IEnumerable actual = filter.Process(models);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Process_FiltersUsingGreaterThan()
        {
            filter.GetNumericValue().Returns(1);
            filter.Type = "GreaterThan";

            IEnumerable expected = models.Where(model => model.Sum > 1);
            IEnumerable actual = filter.Process(models);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Process_FiltersUsingLessThanOrEqual()
        {
            filter.GetNumericValue().Returns(1);
            filter.Type = "LessThanOrEqual";

            IEnumerable expected = models.Where(model => model.Sum <= 1);
            IEnumerable actual = filter.Process(models);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Process_FiltersUsingGreaterThanOrEqual()
        {
            filter.GetNumericValue().Returns(1);
            filter.Type = "GreaterThanOrEqual";

            IEnumerable expected = models.Where(model => model.Sum >= 1);
            IEnumerable actual = filter.Process(models);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Process_OnNotSupportedFilterTypeReturnsSameItems()
        {
            filter.GetNumericValue().Returns(1);
            filter.Type = "Test";

            IEnumerable actual = filter.Process(models);
            IEnumerable expected = models;

            Assert.AreSame(expected, actual);
        }

        #endregion
    }
}
