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
        [TestCase(typeof(SByte), "Equals", typeof(SByteFilter<>))]
        [TestCase(typeof(SByte), "LessThan", typeof(SByteFilter<>))]
        [TestCase(typeof(SByte), "GreaterThan", typeof(SByteFilter<>))]
        [TestCase(typeof(SByte), "LessThanOrEqual", typeof(SByteFilter<>))]
        [TestCase(typeof(SByte), "GreaterThanOrEqual", typeof(SByteFilter<>))]

        [TestCase(typeof(Byte), "Equals", typeof(ByteFilter<>))]
        [TestCase(typeof(Byte), "LessThan", typeof(ByteFilter<>))]
        [TestCase(typeof(Byte), "GreaterThan", typeof(ByteFilter<>))]
        [TestCase(typeof(Byte), "LessThanOrEqual", typeof(ByteFilter<>))]
        [TestCase(typeof(Byte), "GreaterThanOrEqual", typeof(ByteFilter<>))]

        [TestCase(typeof(Int16), "Equals", typeof(Int16Filter<>))]
        [TestCase(typeof(Int16), "LessThan", typeof(Int16Filter<>))]
        [TestCase(typeof(Int16), "GreaterThan", typeof(Int16Filter<>))]
        [TestCase(typeof(Int16), "LessThanOrEqual", typeof(Int16Filter<>))]
        [TestCase(typeof(Int16), "GreaterThanOrEqual", typeof(Int16Filter<>))]

        [TestCase(typeof(UInt16), "Equals", typeof(UInt16Filter<>))]
        [TestCase(typeof(UInt16), "LessThan", typeof(UInt16Filter<>))]
        [TestCase(typeof(UInt16), "GreaterThan", typeof(UInt16Filter<>))]
        [TestCase(typeof(UInt16), "LessThanOrEqual", typeof(UInt16Filter<>))]
        [TestCase(typeof(UInt16), "GreaterThanOrEqual", typeof(UInt16Filter<>))]

        [TestCase(typeof(Int32), "Equals", typeof(Int32Filter<>))]
        [TestCase(typeof(Int32), "LessThan", typeof(Int32Filter<>))]
        [TestCase(typeof(Int32), "GreaterThan", typeof(Int32Filter<>))]
        [TestCase(typeof(Int32), "LessThanOrEqual", typeof(Int32Filter<>))]
        [TestCase(typeof(Int32), "GreaterThanOrEqual", typeof(Int32Filter<>))]

        [TestCase(typeof(UInt32), "Equals", typeof(UInt32Filter<>))]
        [TestCase(typeof(UInt32), "LessThan", typeof(UInt32Filter<>))]
        [TestCase(typeof(UInt32), "GreaterThan", typeof(UInt32Filter<>))]
        [TestCase(typeof(UInt32), "LessThanOrEqual", typeof(UInt32Filter<>))]
        [TestCase(typeof(UInt32), "GreaterThanOrEqual", typeof(UInt32Filter<>))]

        [TestCase(typeof(Int64), "Equals", typeof(Int64Filter<>))]
        [TestCase(typeof(Int64), "LessThan", typeof(Int64Filter<>))]
        [TestCase(typeof(Int64), "GreaterThan", typeof(Int64Filter<>))]
        [TestCase(typeof(Int64), "LessThanOrEqual", typeof(Int64Filter<>))]
        [TestCase(typeof(Int64), "GreaterThanOrEqual", typeof(Int64Filter<>))]

        [TestCase(typeof(UInt64), "Equals", typeof(UInt64Filter<>))]
        [TestCase(typeof(UInt64), "LessThan", typeof(UInt64Filter<>))]
        [TestCase(typeof(UInt64), "GreaterThan", typeof(UInt64Filter<>))]
        [TestCase(typeof(UInt64), "LessThanOrEqual", typeof(UInt64Filter<>))]
        [TestCase(typeof(UInt64), "GreaterThanOrEqual", typeof(UInt64Filter<>))]

        [TestCase(typeof(Single), "Equals", typeof(SingleFilter<>))]
        [TestCase(typeof(Single), "LessThan", typeof(SingleFilter<>))]
        [TestCase(typeof(Single), "GreaterThan", typeof(SingleFilter<>))]
        [TestCase(typeof(Single), "LessThanOrEqual", typeof(SingleFilter<>))]
        [TestCase(typeof(Single), "GreaterThanOrEqual", typeof(SingleFilter<>))]

        [TestCase(typeof(Double), "Equals", typeof(DoubleFilter<>))]
        [TestCase(typeof(Double), "LessThan", typeof(DoubleFilter<>))]
        [TestCase(typeof(Double), "GreaterThan", typeof(DoubleFilter<>))]
        [TestCase(typeof(Double), "LessThanOrEqual", typeof(DoubleFilter<>))]
        [TestCase(typeof(Double), "GreaterThanOrEqual", typeof(DoubleFilter<>))]

        [TestCase(typeof(Decimal), "Equals", typeof(DecimalFilter<>))]
        [TestCase(typeof(Decimal), "LessThan", typeof(DecimalFilter<>))]
        [TestCase(typeof(Decimal), "GreaterThan", typeof(DecimalFilter<>))]
        [TestCase(typeof(Decimal), "LessThanOrEqual", typeof(DecimalFilter<>))]
        [TestCase(typeof(Decimal), "GreaterThanOrEqual", typeof(DecimalFilter<>))]

        [TestCase(typeof(String), "Equals", typeof(StringEqualsFilter<>))]
        [TestCase(typeof(String), "Contains", typeof(StringContainsFilter<>))]
        [TestCase(typeof(String), "EndsWith", typeof(StringEndsWithFilter<>))]
        [TestCase(typeof(String), "StartsWith", typeof(StringStartsWithFilter<>))]
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
            IGridFilter<GridModel> filter = filters.GetFilter<GridModel, Object>(null, "Equals", "1");

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
