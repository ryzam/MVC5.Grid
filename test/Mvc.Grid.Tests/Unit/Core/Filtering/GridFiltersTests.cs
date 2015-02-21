using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq.Expressions;
using System.Web;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class GridFiltersTests
    {
        private IGridColumn<GridModel> column;
        private GridFilters filters;

        [SetUp]
        public void SetUp()
        {
            Expression<Func<GridModel, String>> expression = (model) => model.Name;
            column = Substitute.For<IGridColumn<GridModel>>();
            column.Grid.Query = new NameValueCollection();
            column.Expression.Returns(expression);
            column.IsMultiFilterable = true;
            column.Grid.Name = "Grid";
            column.Name = "Name";

            filters = new GridFilters();
        }

        #region Constructor: GridFilters()

        [Test]
        [TestCase(typeof(SByte), "Equals", typeof(SByteFilter))]
        [TestCase(typeof(SByte), "LessThan", typeof(SByteFilter))]
        [TestCase(typeof(SByte), "GreaterThan", typeof(SByteFilter))]
        [TestCase(typeof(SByte), "LessThanOrEqual", typeof(SByteFilter))]
        [TestCase(typeof(SByte), "GreaterThanOrEqual", typeof(SByteFilter))]

        [TestCase(typeof(Byte), "Equals", typeof(ByteFilter))]
        [TestCase(typeof(Byte), "LessThan", typeof(ByteFilter))]
        [TestCase(typeof(Byte), "GreaterThan", typeof(ByteFilter))]
        [TestCase(typeof(Byte), "LessThanOrEqual", typeof(ByteFilter))]
        [TestCase(typeof(Byte), "GreaterThanOrEqual", typeof(ByteFilter))]

        [TestCase(typeof(Int16), "Equals", typeof(Int16Filter))]
        [TestCase(typeof(Int16), "LessThan", typeof(Int16Filter))]
        [TestCase(typeof(Int16), "GreaterThan", typeof(Int16Filter))]
        [TestCase(typeof(Int16), "LessThanOrEqual", typeof(Int16Filter))]
        [TestCase(typeof(Int16), "GreaterThanOrEqual", typeof(Int16Filter))]

        [TestCase(typeof(UInt16), "Equals", typeof(UInt16Filter))]
        [TestCase(typeof(UInt16), "LessThan", typeof(UInt16Filter))]
        [TestCase(typeof(UInt16), "GreaterThan", typeof(UInt16Filter))]
        [TestCase(typeof(UInt16), "LessThanOrEqual", typeof(UInt16Filter))]
        [TestCase(typeof(UInt16), "GreaterThanOrEqual", typeof(UInt16Filter))]

        [TestCase(typeof(Int32), "Equals", typeof(Int32Filter))]
        [TestCase(typeof(Int32), "LessThan", typeof(Int32Filter))]
        [TestCase(typeof(Int32), "GreaterThan", typeof(Int32Filter))]
        [TestCase(typeof(Int32), "LessThanOrEqual", typeof(Int32Filter))]
        [TestCase(typeof(Int32), "GreaterThanOrEqual", typeof(Int32Filter))]

        [TestCase(typeof(UInt32), "Equals", typeof(UInt32Filter))]
        [TestCase(typeof(UInt32), "LessThan", typeof(UInt32Filter))]
        [TestCase(typeof(UInt32), "GreaterThan", typeof(UInt32Filter))]
        [TestCase(typeof(UInt32), "LessThanOrEqual", typeof(UInt32Filter))]
        [TestCase(typeof(UInt32), "GreaterThanOrEqual", typeof(UInt32Filter))]

        [TestCase(typeof(Int64), "Equals", typeof(Int64Filter))]
        [TestCase(typeof(Int64), "LessThan", typeof(Int64Filter))]
        [TestCase(typeof(Int64), "GreaterThan", typeof(Int64Filter))]
        [TestCase(typeof(Int64), "LessThanOrEqual", typeof(Int64Filter))]
        [TestCase(typeof(Int64), "GreaterThanOrEqual", typeof(Int64Filter))]

        [TestCase(typeof(UInt64), "Equals", typeof(UInt64Filter))]
        [TestCase(typeof(UInt64), "LessThan", typeof(UInt64Filter))]
        [TestCase(typeof(UInt64), "GreaterThan", typeof(UInt64Filter))]
        [TestCase(typeof(UInt64), "LessThanOrEqual", typeof(UInt64Filter))]
        [TestCase(typeof(UInt64), "GreaterThanOrEqual", typeof(UInt64Filter))]

        [TestCase(typeof(Single), "Equals", typeof(SingleFilter))]
        [TestCase(typeof(Single), "LessThan", typeof(SingleFilter))]
        [TestCase(typeof(Single), "GreaterThan", typeof(SingleFilter))]
        [TestCase(typeof(Single), "LessThanOrEqual", typeof(SingleFilter))]
        [TestCase(typeof(Single), "GreaterThanOrEqual", typeof(SingleFilter))]

        [TestCase(typeof(Double), "Equals", typeof(DoubleFilter))]
        [TestCase(typeof(Double), "LessThan", typeof(DoubleFilter))]
        [TestCase(typeof(Double), "GreaterThan", typeof(DoubleFilter))]
        [TestCase(typeof(Double), "LessThanOrEqual", typeof(DoubleFilter))]
        [TestCase(typeof(Double), "GreaterThanOrEqual", typeof(DoubleFilter))]

        [TestCase(typeof(Decimal), "Equals", typeof(DecimalFilter))]
        [TestCase(typeof(Decimal), "LessThan", typeof(DecimalFilter))]
        [TestCase(typeof(Decimal), "GreaterThan", typeof(DecimalFilter))]
        [TestCase(typeof(Decimal), "LessThanOrEqual", typeof(DecimalFilter))]
        [TestCase(typeof(Decimal), "GreaterThanOrEqual", typeof(DecimalFilter))]

        [TestCase(typeof(DateTime), "Equals", typeof(DateTimeFilter))]
        [TestCase(typeof(DateTime), "LessThan", typeof(DateTimeFilter))]
        [TestCase(typeof(DateTime), "GreaterThan", typeof(DateTimeFilter))]
        [TestCase(typeof(DateTime), "LessThanOrEqual", typeof(DateTimeFilter))]
        [TestCase(typeof(DateTime), "GreaterThanOrEqual", typeof(DateTimeFilter))]

        [TestCase(typeof(Boolean), "Equals", typeof(BooleanFilter))]

        [TestCase(typeof(String), "Equals", typeof(StringEqualsFilter))]
        [TestCase(typeof(String), "Contains", typeof(StringContainsFilter))]
        [TestCase(typeof(String), "EndsWith", typeof(StringEndsWithFilter))]
        [TestCase(typeof(String), "StartsWith", typeof(StringStartsWithFilter))]
        public void GridFilters_RegistersDefaultFilters(Type type, String name, Type expected)
        {
            GridFilters filters = new GridFilters();

            Type actual = filters.Table[type][name];

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Method: GetFilter<T>(IGridColumn<T> column)

        [Test]
        public void GetFilter_OnNotMultiFilterableColumnSetsSecondFilterToNull()
        {
            column.Grid.Query = HttpUtility.ParseQueryString("Grid-Name-Contains=a&Grid-Name-Equals=b&Grid-Name-Op=Or");
            column.IsMultiFilterable = false;

            Assert.IsNull(filters.GetFilter(column).Second);
        }

        [Test]
        [TestCase("Grid-Name-Equals")]
        [TestCase("Grid-Name=Equals")]
        [TestCase("Grid-Name-=Equals")]
        [TestCase("Grid-Name-Op=Equals")]
        public void GetFilter_OnNotFoundFilterSetsSecondFilterToNull(String query)
        {
            column.Grid.Query = HttpUtility.ParseQueryString(query);

            Assert.IsNull(filters.GetFilter(column).Second);
        }

        [Test]
        public void GetFilter_OnNotFoundValueTypeSetsSecondFilterToNull()
        {
            column.Grid.Query = HttpUtility.ParseQueryString("Grid-Name-Contains=a&Grid-Name-Equals=b&Grid-Name-Op=And");
            Expression<Func<GridModel, Object>> expression = (model) => model.Name;
            column.Expression.Returns(expression);

            Assert.IsNull(filters.GetFilter(column).Second);
        }

        [Test]
        [TestCase("Grid-Name-Eq=a&Grid-Name-Eq=b&Grid-Name-Op=And")]
        [TestCase("Grid-Name-Contains=a&Grid-Name-Eq=b&Grid-Name-Op=And")]
        public void GetFilter_OnNotFoundFilterTypeSetsSecondFilterToNull(String query)
        {
            column.Grid.Query = HttpUtility.ParseQueryString(query);

            Assert.IsNull(filters.GetFilter(column).Second);
        }

        [Test]
        [TestCase("Grid-Name-Eq=a&Grid-Name-Equals=b", "Equals", "b")]
        [TestCase("Grid-Name-Equals=a&Grid-Name-Equals=", "Equals", "")]
        [TestCase("Grid-Name-Equals=a&Grid-Name-Equals=b", "Equals", "b")]
        [TestCase("Grid-Name-Contains=a&Grid-Name-Equals=", "Equals", "")]
        [TestCase("Grid-Name-Contains=a&Grid-Name-Equals=b", "Equals", "b")]
        [TestCase("Grid-Name-Contains=a&Grid-Name-Equals=b&Grid-Name-Op=Or", "Equals", "b")]
        public void GetFilter_SetsSecondFilter(String query, String type, String value)
        {
            column.Grid.Query = HttpUtility.ParseQueryString(query);

            IGridFilter actual = filters.GetFilter(column).Second;

            Assert.AreEqual(typeof(StringEqualsFilter), actual.GetType());
            Assert.AreEqual(value, actual.Value);
            Assert.AreEqual(type, actual.Type);
        }

        [Test]
        [TestCase("Grid-Name-Equals")]
        [TestCase("Grid-Name=Equals")]
        [TestCase("Grid-Name-=Equals")]
        [TestCase("Grid-Name-Op=Equals")]
        public void GetFilter_OnNotFoundFilterSetsFirstFilterToNull(String query)
        {
            column.Grid.Query = HttpUtility.ParseQueryString(query);

            Assert.IsNull(filters.GetFilter(column).First);
        }

        [Test]
        public void GetFilter_OnNotFoundValueTypeSetsFirstFilterToNull()
        {
            column.Grid.Query = HttpUtility.ParseQueryString("Grid-Name-Contains=a&Grid-Name-Equals=b&Grid-Name-Op=And");
            Expression<Func<GridModel, Object>> expression = (model) => model.Name;
            column.Expression.Returns(expression);

            Assert.IsNull(filters.GetFilter(column).First);
        }

        [Test]
        [TestCase("Grid-Name-Eq=a&Grid-Name-Eq=b&Grid-Name-Op=And")]
        [TestCase("Grid-Name-Eq=a&Grid-Name-Contains=b&Grid-Name-Op=And")]
        public void GetFilter_OnNotFoundFilterTypeSetsFirstFilterToNull(String query)
        {
            column.Grid.Query = HttpUtility.ParseQueryString(query);

            Assert.IsNull(filters.GetFilter(column).First);
        }

        [Test]
        [TestCase("Grid-Name-Equals=a&Grid-Name-Eq=b", "Equals", "a")]
        [TestCase("Grid-Name-Equals=&Grid-Name-Equals=b", "Equals", "")]
        [TestCase("Grid-Name-Equals=a&Grid-Name-Equals=b", "Equals", "a")]
        [TestCase("Grid-Name-Equals=&Grid-Name-Contains=b", "Equals", "")]
        [TestCase("Grid-Name-Equals=a&Grid-Name-Contains=b", "Equals", "a")]
        [TestCase("Grid-Name-Equals=a&Grid-Name-Contains=b&Grid-Name-Op=Or", "Equals", "a")]
        public void GetFilter_SetsFirstFilter(String query, String type, String value)
        {
            column.Grid.Query = HttpUtility.ParseQueryString(query);

            IGridFilter actual = filters.GetFilter(column).First;

            Assert.AreEqual(typeof(StringEqualsFilter), actual.GetType());
            Assert.AreEqual(value, actual.Value);
            Assert.AreEqual(type, actual.Type);
        }

        [Test]
        public void GetFilter_OnNotMultiFilterableColumnSetsOperatorToNull()
        {
            column.Grid.Query = HttpUtility.ParseQueryString("Grid-Name-Contains=a&Grid-Name-Equals=b&Grid-Name-Op=Or");
            column.IsMultiFilterable = false;

            Assert.IsNull(filters.GetFilter(column).Operator);
        }

        [Test]
        [TestCase("Grid-Name-Op=", "")]
        [TestCase("Grid-Name-Op", null)]
        [TestCase("Grid-Name-Op=Or", "Or")]
        [TestCase("Grid-Name-Op=And", "And")]
        [TestCase("Grid-Name-Op-And=And", null)]
        [TestCase("Grid-Name-Op=And&Grid-Name-Op=Or", "And")]
        public void GetFilter_SetsOperatorFromQuery(String query, String expected)
        {
            column.Grid.Query = HttpUtility.ParseQueryString(query);

            String actual = filters.GetFilter(column).Operator;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetFilter_SetsColumn()
        {
            IGridColumn actual = filters.GetFilter(column).Column;
            IGridColumn expected = column;

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void GetFilter_ReturnsGridColumnFilter()
        {
            Type expected = typeof(GridColumnFilter<GridModel>);
            Type actual = filters.GetFilter(column).GetType();

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Method: Register(Type forType, String filterType, Type filter)

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
        public void Register_RegistersNullableFilterTypeForExistingType()
        {
            IDictionary<String, Type> filterPairs = new Dictionary<String, Type> { { "Test", typeof(Object) } };

            filters.Table.Remove(typeof(Int32));
            filters.Table.Add(typeof(Int32), filterPairs);

            filters.Register(typeof(Int32?), "TestFilter", typeof(String));

            Type actual = filterPairs["TestFilter"];
            Type expected = typeof(String);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Register_RegistersNullableTypeAsNotNullable()
        {
            filters.Register(typeof(Int32?), "Test", typeof(String));

            Type actual = filters.Table[typeof(Int32)]["Test"];
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

        #region Method: Unregister(Type forType, String filterType)

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
