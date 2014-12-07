using NUnit.Framework;
using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class DateTimeFilterTests
    {
        private DateTimeFilter<GridModel> filter;
        private IQueryable<GridModel> models;

        [SetUp]
        public void SetUp()
        {
            models = new[]
            {
                new GridModel { Date = new DateTime(2013, 01, 01) },
                new GridModel { Date = new DateTime(2014, 01, 01) },
                new GridModel { Date = new DateTime(2015, 01, 01) }
            }.AsQueryable();

            Expression<Func<GridModel, DateTime>> expression = (model) => model.Date;
            filter = new DateTimeFilter<GridModel>();
            filter.FilteredExpression = expression;
        }

        #region Method: Process(IQueryable<TModel> items)

        [Test]
        public void Process_OnInvalidDateTimeValueReturnsItems()
        {
            filter.Value = "Test";

            IEnumerable actual = filter.Process(models);
            IEnumerable expected = models;

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void Process_FiltersUsingEquals()
        {
            filter.Value = new DateTime(2014, 01, 01).ToString();
            filter.Type = "Equals";

            IEnumerable expected = models.Where(model => model.Date == new DateTime(2014, 01, 01));
            IEnumerable actual = filter.Process(models);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Process_FiltersUsingLessThan()
        {
            filter.Value = new DateTime(2014, 01, 01).ToShortDateString();
            filter.Type = "LessThan";

            IEnumerable expected = models.Where(model => model.Date < new DateTime(2014, 01, 01));
            IEnumerable actual = filter.Process(models);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Process_FiltersUsingGreaterThan()
        {
            filter.Value = new DateTime(2014, 01, 01).ToLongDateString();
            filter.Type = "GreaterThan";

            IEnumerable expected = models.Where(model => model.Date > new DateTime(2014, 01, 01));
            IEnumerable actual = filter.Process(models);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Process_FiltersUsingLessThanOrEqual()
        {
            filter.Value = new DateTime(2014, 01, 01).ToShortDateString();
            filter.Type = "LessThanOrEqual";

            IEnumerable expected = models.Where(model => model.Date <= new DateTime(2014, 01, 01));
            IEnumerable actual = filter.Process(models);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Process_FiltersUsingGreaterThanOrEqual()
        {
            filter.Value = new DateTime(2014, 01, 01).ToShortDateString();
            filter.Type = "GreaterThanOrEqual";

            IEnumerable expected = models.Where(model => model.Date >= new DateTime(2014, 01, 01));
            IEnumerable actual = filter.Process(models);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Process_OnNotSupportedFilterTypeReturnsSameItems()
        {
            filter.Value = new DateTime(2014, 01, 01).ToShortDateString();
            filter.Type = "Test";

            IEnumerable actual = filter.Process(models);
            IEnumerable expected = models;

            Assert.AreSame(expected, actual);
        }

        #endregion
    }
}
