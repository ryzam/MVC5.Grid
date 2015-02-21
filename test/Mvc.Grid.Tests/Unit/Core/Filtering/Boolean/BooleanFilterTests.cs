using NUnit.Framework;
using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class BooleanFilterTests : BaseGridFilterTests
    {
        private IQueryable<GridModel> items;
        private BooleanFilter filter;

        [SetUp]
        public void SetUp()
        {
            items = new[]
            {
                new GridModel(),
                new GridModel { IsChecked = true, NIsChecked = true },
                new GridModel{ IsChecked = false, NIsChecked = false }
            }.AsQueryable();

            filter = new BooleanFilter();
        }

        #region Method: Apply(Expression expression)

        [Test]
        public void Apply_OnInvalidBooleanValueReturnsNull()
        {
            Expression<Func<GridModel, Boolean>> expression = (model) => model.IsChecked;
            filter.Value = "Test";

            Assert.IsNull(filter.Apply(expression.Body));
        }

        [Test]
        [TestCase("true", true)]
        [TestCase("True", true)]
        [TestCase("TRUE", true)]
        [TestCase("false", false)]
        [TestCase("False", false)]
        [TestCase("FALSE", false)]
        public void Apply_FiltersNullableBooleanProperty(String value, Boolean isChecked)
        {
            Expression<Func<GridModel, Boolean?>> expression = (model) => model.NIsChecked;
            filter.Value = value;

            IEnumerable actual = Filter(items, filter.Apply(expression.Body), expression);
            IEnumerable expected = items.Where(model => model.NIsChecked == isChecked);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("true", true)]
        [TestCase("True", true)]
        [TestCase("TRUE", true)]
        [TestCase("false", false)]
        [TestCase("False", false)]
        [TestCase("FALSE", false)]
        public void Apply_FiltersBooleanProperty(String value, Boolean isChecked)
        {
            Expression<Func<GridModel, Boolean?>> expression = (model) => model.IsChecked;
            filter.Value = value;

            IEnumerable actual = Filter(items, filter.Apply(expression.Body), expression);
            IEnumerable expected = items.Where(model => model.IsChecked == isChecked);

            CollectionAssert.AreEqual(expected, actual);
        }

        #endregion
    }
}
