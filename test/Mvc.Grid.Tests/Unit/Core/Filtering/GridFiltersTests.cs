using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class GridFiltersTests
    {
        private GridFilters filters;

        [SetUp]
        public void SetUp()
        {
            filters = new GridFilters();
        }

        #region Constructor: GridFilters()

        [Test]
        [TestCase(typeof(String), "Equals", typeof(StringEqualsFilter<>))]
        [TestCase(typeof(String), "Contains", typeof(StringContainsFilter<>))]
        public void GridFilters_RegistersDefaultFilters(Type type, String name, Type expected)
        {
            GridFilters filters = new GridFilters();

            Type actual = filters.Table[type][name];

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Method: GetFilter<TModel, TValue>(IGridColumn<TModel, TValue> column, String name, String value)

        [Test]
        public void GetFilter_OnNotFoundValueTypeReturnsNull()
        {
            IGridFilter<GridModel> filter = filters.GetFilter<GridModel, Decimal>(null, "Equals", "1");

            Assert.IsNull(filter);
        }

        [Test]
        public void GetFilter_OnNotFoundFilterTypeReturnsNull()
        {
            IGridFilter<GridModel> filter = filters.GetFilter<GridModel, String>(null, "GreaterThan", "Test");

            Assert.IsNull(filter);
        }

        [Test]
        public void GetFilter_ReturnsFilter()
        {
            IGridColumn<GridModel, String> column = Substitute.For<IGridColumn<GridModel, String>>();
            column.Expression = (model) => model.Name;

            IGridFilter<GridModel> actual = filters.GetFilter<GridModel, String>(column, "Equals", "Test");
            IGridFilter<GridModel> expected = new StringEqualsFilter<GridModel>();
            expected.FilteredExpression = column.Expression;
            expected.Type = "Equals";
            expected.Value = "Test";

            Assert.AreEqual(expected.FilteredExpression, actual.FilteredExpression);
            Assert.AreEqual(expected.ProcessorType, actual.ProcessorType);
            Assert.AreEqual(expected.GetType(), actual.GetType());
            Assert.AreEqual(expected.Value, actual.Value);
            Assert.AreEqual(expected.Type, actual.Type);
        }

        #endregion

        #region Method: Register(Type forType, String name, Type filterType)

        [Test]
        public void Register_RegistersFilterForExistingType()
        {
            IDictionary<String, Type> filterPairs = new Dictionary<String, Type> { { "Test", typeof(Object) } };
            filters.Table.Add(typeof(Object), filterPairs);

            filters.Register(typeof(Object), "TestFilter", typeof(String));

            Type actual = filterPairs["TestFilter"];
            Type expected = typeof(String);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Register_RegistersFilterForNewType()
        {
            filters.Register(typeof(Object), "Test", typeof(String));

            Type actual = filters.Table[typeof(Object)]["Test"];
            Type expected = typeof(String);

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Method: Unregister(Type forType, String name, Type filterType)

        [Test]
        public void Unregister_UnregistersExistingFilter()
        {
            IDictionary<String, Type> filterPairs = new Dictionary<String, Type> { { "Test", null } };
            filters.Table.Add(typeof(Object), filterPairs);

            filters.Unregister(typeof(Object), "Test");

            CollectionAssert.IsEmpty(filterPairs);
        }

        [Test]
        public void Unregister_CanBeCalledOnNotExistingFilter()
        {
            filters.Unregister(typeof(Object), "Test");
        }

        #endregion
    }
}
