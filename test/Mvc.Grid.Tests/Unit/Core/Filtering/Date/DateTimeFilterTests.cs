using NUnit.Framework;
using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class DateTimeFilterTests : BaseGridFilterTests
    {
        private Expression<Func<GridModel, DateTime?>> nDateExpression;
        private Expression<Func<GridModel, DateTime>> dateExpression;
        private IQueryable<GridModel> items;
        private DateTimeFilter filter;

        [SetUp]
        public void SetUp()
        {
            items = new[]
            {
                new GridModel { Date = new DateTime(2013, 01, 01), NDate = null },
                new GridModel { Date = new DateTime(2014, 01, 01), NDate = new DateTime(2014, 01, 01) },
                new GridModel { Date = new DateTime(2015, 01, 01), NDate = new DateTime(2015, 01, 01) }
            }.AsQueryable();

            nDateExpression = (model) => model.NDate;
            dateExpression = (model) => model.Date;
            filter = new DateTimeFilter();
        }

        #region Method: Apply(Expression expression)

        [Test]
        public void Apply_OnInvalidDateTimeValueReturnsItems()
        {
            filter.Value = "Test";

            Assert.IsNull(filter.Apply(dateExpression.Body));
        }

        [Test]
        public void Apply_FiltersNullableUsingEquals()
        {
            filter.Value = new DateTime(2014, 01, 01).ToString();
            filter.Type = "Equals";

            IEnumerable actual = Filter(items, filter.Apply(nDateExpression.Body), nDateExpression);
            IEnumerable expected = items.Where(model => model.NDate == new DateTime(2014, 01, 01));

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Apply_FiltersUsingEquals()
        {
            filter.Value = new DateTime(2014, 01, 01).ToString();
            filter.Type = "Equals";

            IEnumerable actual = Filter(items, filter.Apply(dateExpression.Body), dateExpression);
            IEnumerable expected = items.Where(model => model.Date == new DateTime(2014, 01, 01));

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Apply_FiltersNullableUsingLessThan()
        {
            filter.Value = new DateTime(2014, 01, 01).ToString();
            filter.Type = "LessThan";

            IEnumerable actual = Filter(items, filter.Apply(nDateExpression.Body), nDateExpression);
            IEnumerable expected = items.Where(model => model.NDate < new DateTime(2014, 01, 01));

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Apply_FiltersUsingLessThan()
        {
            filter.Value = new DateTime(2014, 01, 01).ToShortDateString();
            filter.Type = "LessThan";

            IEnumerable actual = Filter(items, filter.Apply(dateExpression.Body), dateExpression);
            IEnumerable expected = items.Where(model => model.Date < new DateTime(2014, 01, 01));

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Apply_FiltersNullableUsingGreaterThan()
        {
            filter.Value = new DateTime(2014, 01, 01).ToString();
            filter.Type = "GreaterThan";

            IEnumerable actual = Filter(items, filter.Apply(nDateExpression.Body), nDateExpression);
            IEnumerable expected = items.Where(model => model.NDate > new DateTime(2014, 01, 01));

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Apply_FiltersUsingGreaterThan()
        {
            filter.Value = new DateTime(2014, 01, 01).ToLongDateString();
            filter.Type = "GreaterThan";

            IEnumerable actual = Filter(items, filter.Apply(dateExpression.Body), dateExpression);
            IEnumerable expected = items.Where(model => model.Date > new DateTime(2014, 01, 01));

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Apply_FiltersNullableUsingLessThanOrEqual()
        {
            filter.Value = new DateTime(2014, 01, 01).ToString();
            filter.Type = "LessThanOrEqual";

            IEnumerable actual = Filter(items, filter.Apply(nDateExpression.Body), nDateExpression);
            IEnumerable expected = items.Where(model => model.NDate <= new DateTime(2014, 01, 01));

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Apply_FiltersUsingLessThanOrEqual()
        {
            filter.Value = new DateTime(2014, 01, 01).ToShortDateString();
            filter.Type = "LessThanOrEqual";

            IEnumerable actual = Filter(items, filter.Apply(dateExpression.Body), dateExpression);
            IEnumerable expected = items.Where(model => model.Date <= new DateTime(2014, 01, 01));

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Apply_FiltersNullableUsingGreaterThanOrEqual()
        {
            filter.Value = new DateTime(2014, 01, 01).ToString();
            filter.Type = "GreaterThanOrEqual";

            IEnumerable actual = Filter(items, filter.Apply(nDateExpression.Body), nDateExpression);
            IEnumerable expected = items.Where(model => model.NDate >= new DateTime(2014, 01, 01));

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Apply_FiltersUsingGreaterThanOrEqual()
        {
            filter.Value = new DateTime(2014, 01, 01).ToShortDateString();
            filter.Type = "GreaterThanOrEqual";

            IEnumerable actual = Filter(items, filter.Apply(dateExpression.Body), dateExpression);
            IEnumerable expected = items.Where(model => model.Date >= new DateTime(2014, 01, 01));

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Apply_OnNotSupportedFilterTypeReturnsNull()
        {
            filter.Value = new DateTime(2014, 01, 01).ToShortDateString();
            filter.Type = "Test";

            Assert.IsNull(filter.Apply(dateExpression.Body));
        }

        #endregion
    }
}
