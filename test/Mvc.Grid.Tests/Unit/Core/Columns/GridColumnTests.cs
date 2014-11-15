using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class GridColumnTests
    {
        private GridColumn<GridModel, Object> column;
        private IGrid<GridModel> grid;

        [SetUp]
        public void SetUp()
        {
            grid = Substitute.For<IGrid<GridModel>>();
            grid.Query = new GridQuery(new NameValueCollection());
            column = new GridColumn<GridModel, Object>(grid, model => model.Name);
        }

        #region Constructor: GridColumn(IGrid<TModel> grid, Expression<Func<TModel, TValue>> expression)

        [Test]
        public void GridColumn_SetsGrid()
        {
            IGrid actual = new GridColumn<GridModel, Object>(grid, model => model.Name).Grid;
            IGrid expected = grid;

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void GridColumn_SetsIsEncodedToTrue()
        {
            column = new GridColumn<GridModel, Object>(grid, model => model.Name);

            Assert.IsTrue(column.IsEncoded);
        }

        [Test]
        public void GridColumn_SetsExpression()
        {
            Expression<Func<GridModel, String>> expected = (model) => model.Name;
            Expression<Func<GridModel, String>> actual = new GridColumn<GridModel, String>(grid, expected).Expression;

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void AddProperty_SetsFilterTypeAsNullForEnum()
        {
            AssertFilterTypeFor(model => model.EnumField, null);
        }

        [Test]
        public void AddProperty_SetsFilterTypeAsNumberForSByte()
        {
            AssertFilterTypeFor(model => model.SByteField, "Number");
        }

        [Test]
        public void AddProperty_SetsFilterTypeAsNumberForByte()
        {
            AssertFilterTypeFor(model => model.ByteField, "Number");
        }

        [Test]
        public void AddProperty_SetsFilterTypeAsNumberForInt16()
        {
            AssertFilterTypeFor(model => model.Int16Field, "Number");
        }

        [Test]
        public void AddProperty_SetsFilterTypeAsNumberForUInt16()
        {
            AssertFilterTypeFor(model => model.UInt16Field, "Number");
        }

        [Test]
        public void AddProperty_SetsFilterTypeAsNumberForInt32()
        {
            AssertFilterTypeFor(model => model.Int32Field, "Number");
        }

        [Test]
        public void AddProperty_SetsFilterTypeAsNumberForUInt32()
        {
            AssertFilterTypeFor(model => model.UInt32Field, "Number");
        }

        [Test]
        public void AddProperty_SetsFilterTypeAsNumberForInt64()
        {
            AssertFilterTypeFor(model => model.Int64Field, "Number");
        }

        [Test]
        public void AddProperty_SetsFilterTypeAsNumberForUInt64()
        {
            AssertFilterTypeFor(model => model.UInt64Field, "Number");
        }

        [Test]
        public void AddProperty_SetsFilterTypeAsNumberForSingle()
        {
            AssertFilterTypeFor(model => model.SingleField, "Number");
        }

        [Test]
        public void AddProperty_SetsFilterTypeAsNumberForDouble()
        {
            AssertFilterTypeFor(model => model.DoubleField, "Number");
        }

        [Test]
        public void AddProperty_SetsFilterTypeAsNumberForDecimal()
        {
            AssertFilterTypeFor(model => model.DecimalField, "Number");
        }

        [Test]
        public void AddProperty_SetsFilterTypeAsBooleanForBoolean()
        {
            AssertFilterTypeFor(model => model.BooleanField, "Boolean");
        }

        [Test]
        public void AddProperty_SetsFilterTypeAsDateCellForDateTime()
        {
            AssertFilterTypeFor(model => model.DateTimeField, "Date");
        }

        [Test]
        public void AddProperty_SetsFilterTypeAsNullForNullableEnum()
        {
            AssertFilterTypeFor(model => model.NullableEnumField, null);
        }

        [Test]
        public void AddProperty_SetsFilterTypeAsNumberForNullableSByte()
        {
            AssertFilterTypeFor(model => model.NullableSByteField, "Number");
        }

        [Test]
        public void AddProperty_SetsFilterTypeAsNumberForNullableByte()
        {
            AssertFilterTypeFor(model => model.NullableByteField, "Number");
        }

        [Test]
        public void AddProperty_SetsFilterTypeAsNumberForNullableInt16()
        {
            AssertFilterTypeFor(model => model.NullableInt16Field, "Number");
        }

        [Test]
        public void AddProperty_SetsFilterTypeAsNumberForNullableUInt16()
        {
            AssertFilterTypeFor(model => model.NullableUInt16Field, "Number");
        }

        [Test]
        public void AddProperty_SetsFilterTypeAsNumberForNullableInt32()
        {
            AssertFilterTypeFor(model => model.NullableInt32Field, "Number");
        }

        [Test]
        public void AddProperty_SetsFilterTypeAsNumberForNullableUInt32()
        {
            AssertFilterTypeFor(model => model.NullableUInt32Field, "Number");
        }

        [Test]
        public void AddProperty_SetsFilterTypeAsNumberForNullableInt64()
        {
            AssertFilterTypeFor(model => model.NullableInt64Field, "Number");
        }

        [Test]
        public void AddProperty_SetsFilterTypeAsNumberForNullableUInt64()
        {
            AssertFilterTypeFor(model => model.NullableUInt64Field, "Number");
        }

        [Test]
        public void AddProperty_SetsFilterTypeAsNumberForNullableSingle()
        {
            AssertFilterTypeFor(model => model.NullableSingleField, "Number");
        }

        [Test]
        public void AddProperty_SetsFilterTypeAsNumberForNullableDouble()
        {
            AssertFilterTypeFor(model => model.NullableDoubleField, "Number");
        }

        [Test]
        public void AddProperty_SetsFilterTypeAsNumberForNullableDecimal()
        {
            AssertFilterTypeFor(model => model.NullableDecimalField, "Number");
        }

        [Test]
        public void AddProperty_SetsFilterTypeAsBooleanForNullableBoolean()
        {
            AssertFilterTypeFor(model => model.NullableBooleanField, "Boolean");
        }

        [Test]
        public void AddProperty_SetsFilterTypeAsDateForNullableDateTime()
        {
            AssertFilterTypeFor(model => model.NullableDateTimeField, "Date");
        }

        [Test]
        public void AddProperty_SetsFilterTypeAsTextForOtherTypes()
        {
            AssertFilterTypeFor(model => model.StringField, "Text");
        }

        [Test]
        public void GridColumn_SetsValueFunctionAsCompiledExpression()
        {
            GridModel gridModel = new GridModel { Name = "TestName" };

            String actual = column.ValueFunction(gridModel) as String;
            String expected = "TestName";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GridColumn_SetsProcessorTypeAsPreProcessor()
        {
            GridProcessorType actual = new GridColumn<GridModel, Object>(grid, model => model.Name).ProcessorType;
            GridProcessorType expected = GridProcessorType.Pre;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GridColumn_OnNonMemberExpressionIsNotSortable()
        {
            Boolean? actual = new GridColumn<GridModel, String>(grid, model => model.ToString()).IsSortable;
            Boolean expected = false;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GridColumn_OnMemberExpressionIsSortableIsNull()
        {
            GridColumn<GridModel, Decimal> column = new GridColumn<GridModel, Decimal>(grid, model => model.Sum);

            Assert.IsNull(column.IsSortable);
        }

        [Test]
        public void GridColumn_OnNonMemberExpressionIsNotFilterable()
        {
            Boolean? actual = new GridColumn<GridModel, String>(grid, model => model.ToString()).IsFilterable;
            Boolean expected = false;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GridColumn_OnMemberExpressionIsFilterableIsNull()
        {
            GridColumn<GridModel, Decimal> column = new GridColumn<GridModel, Decimal>(grid, model => model.Sum);

            Assert.IsNull(column.IsFilterable);
        }

        [Test]
        public void GridColumn_SetsNameFromExpression()
        {
            Expression<Func<GridModel, String>> expression = (model) => model.Name;

            String actual = new GridColumn<GridModel, String>(grid, expression).Name;
            String expected = ExpressionHelper.GetExpressionText(expression);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("Sort=Name&Grid-Order=Desc", null)]
        [TestCase("Grid-Sort=Sum&Grid-Order=Desc", null)]
        [TestCase("Grid-Sort=Name&RGrid-Order=Asc", null)]
        [TestCase("Grid-Sort=Name&Grid-Order=Dasc", null)]
        [TestCase("Grid-Sort=Name&Grid-Order=Asc", GridSortOrder.Asc)]
        [TestCase("Grid-Sort=Name&Grid-Order=Desc", GridSortOrder.Desc)]
        public void GridColumn_SetsSortOrderFromGridQuery(String query, GridSortOrder? expected)
        {
            grid.Name = "Grid";
            grid.Query = new GridQuery(HttpUtility.ParseQueryString(query));

            GridSortOrder? actual = new GridColumn<GridModel, String>(grid, model => model.Name).SortOrder;

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Method: Process(IQueryable<TModel> items)

        [Test]
        public void Process_IfNotFilterableReturnsItems()
        {
            column.IsSortable = false;
            column.IsFilterable = false;
            column.SortOrder = GridSortOrder.Desc;
            column.Filter = Substitute.For<IGridFilter<GridModel>>();

            IQueryable<GridModel> expected = new GridModel[2].AsQueryable();
            IQueryable<GridModel> actual = column.Process(expected);

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void Process_IfFilterIsNullReturnsItems()
        {
            column.Filter = null;
            column.IsSortable = false;
            column.IsFilterable = true;
            column.SortOrder = GridSortOrder.Desc;

            IQueryable<GridModel> expected = new GridModel[2].AsQueryable();
            IQueryable<GridModel> actual = column.Process(expected);

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void Process_ReturnsFilteredItems()
        {
            column.IsSortable = false;
            column.IsFilterable = true;
            column.SortOrder = GridSortOrder.Desc;
            column.Filter = Substitute.For<IGridFilter<GridModel>>();
            IQueryable<GridModel> items = new GridModel[2].AsQueryable();
            IQueryable<GridModel> filteredItems = new GridModel[2].AsQueryable();

            column.Filter.Process(items).Returns(filteredItems);

            IQueryable<GridModel> actual = column.Process(items);
            IQueryable<GridModel> expected = filteredItems;

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void Process_IfNotSortableReturnsItems()
        {
            column.IsSortable = false;
            column.IsFilterable = false;
            column.SortOrder = GridSortOrder.Desc;

            IQueryable<GridModel> expected = new GridModel[2].AsQueryable();
            IQueryable<GridModel> actual = column.Process(expected);

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void Process_IfSortOrderIsNullReturnsItems()
        {
            column.IsFilterable = false;
            column.IsSortable = true;
            column.SortOrder = null;

            IQueryable<GridModel> expected = new GridModel[2].AsQueryable();
            IQueryable<GridModel> actual = column.Process(expected);

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void Process_ReturnsItemsSortedInAscendingOrder()
        {
            column.IsSortable = true;
            column.IsFilterable = false;
            column.SortOrder = GridSortOrder.Asc;
            GridModel[] models = { new GridModel { Name = "B" }, new GridModel { Name = "A" }};

            IEnumerable expected = models.OrderBy(model => model.Name);
            IEnumerable actual = column.Process(models.AsQueryable());

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Process_ReturnsItemsSortedInDescendingOrder()
        {
            column.IsSortable = true;
            column.IsFilterable = false;
            column.SortOrder = GridSortOrder.Desc;
            GridModel[] models = { new GridModel { Name = "A" }, new GridModel { Name = "B" } };

            IEnumerable expected = models.OrderByDescending(model => model.Name);
            IEnumerable actual = column.Process(models.AsQueryable());

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Process_ReturnsFilteredAndSortedItems()
        {
            column.IsSortable = true;
            column.IsFilterable = true;
            column.SortOrder = GridSortOrder.Desc;
            column.Filter = Substitute.For<IGridFilter<GridModel>>();
            IQueryable<GridModel> models = new [] { new GridModel { Name = "A" }, new GridModel { Name = "B" }, new GridModel { Name = "C" } }.AsQueryable();
            column.Filter.Process(models).Returns(models.Where(model => model.Name == "B").ToList().AsQueryable());

            IEnumerable expected = models.Where(model => model.Name == "B").OrderByDescending(model => model.Name);
            IEnumerable actual = column.Process(models);

            CollectionAssert.AreEqual(expected, actual);
        }

        #endregion

        #region Method: ValueFor(IGridRow row)

        [Test]
        public void ValueFor_UsesValueFunctionToGetValue()
        {
            column.ValueFunction = (model) => "TestValue";

            String actual = column.ValueFor(new GridRow(null)).ToString();
            String expected = "TestValue";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase(null, "For {0}", true, "")]
        [TestCase(null, "For {0}", false, "")]
        [TestCase("<name>", null, false, "<name>")]
        [TestCase("<name>", null, true, "&lt;name&gt;")]
        [TestCase("<name>", "For <{0}>", false, "For <<name>>")]
        [TestCase("<name>", "For <{0}>", true, "For &lt;&lt;name&gt;&gt;")]
        public void ValueFor_GetsValue(String value, String format, Boolean isEncoded, String expected)
        {
            IGridRow row = new GridRow(new GridModel { Name = value });
            column.Encoded(isEncoded);
            column.Formatted(format);

            String actual = column.ValueFor(row).ToString();

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Method: GetSortingQuery()

        [Test]
        [TestCase(null, "Id=1", null, null)]
        [TestCase(false, "Id=1", null, null)]
        [TestCase(true, "", null, "?G%3d1%26D%3d2+-Sort=Name&G%3d1%26D%3d2+-Order=Asc")]
        [TestCase(true, "Id=1", null, "?Id=1&G%3d1%26D%3d2+-Sort=Name&G%3d1%26D%3d2+-Order=Asc")]
        [TestCase(true, "Id=1", GridSortOrder.Asc, "?Id=1&G%3d1%26D%3d2+-Sort=Name&G%3d1%26D%3d2+-Order=Desc")]
        [TestCase(true, "Id=1", GridSortOrder.Desc, "?Id=1&G%3d1%26D%3d2+-Sort=Name&G%3d1%26D%3d2+-Order=Asc")]
        [TestCase(true, "G%3d1%26D%3d2+-Sort=Snatch&G%3d1%26D%3d2+-Order=Desc", null, "?G%3d1%26D%3d2+-Sort=Name&G%3d1%26D%3d2+-Order=Asc")]
        [TestCase(true, "G%3d1%26D%3d2+-Sort=Snatch&G%3d1%26D%3d2+-Order=Asc", GridSortOrder.Asc, "?G%3d1%26D%3d2+-Sort=Name&G%3d1%26D%3d2+-Order=Desc")]
        [TestCase(true, "G%3d1%26D%3d2+-Sort=Snatch&G%3d1%26D%3d2+-Order=Desc", GridSortOrder.Desc, "?G%3d1%26D%3d2+-Sort=Name&G%3d1%26D%3d2+-Order=Asc")]
        public void GetSortingQuery_GeneratesSortingQuery(Boolean? isSortable, String query, GridSortOrder? order, String expected)
        {
            column.Grid.Query = new GridQuery(HttpUtility.ParseQueryString(query));
            column.IsSortable = isSortable;
            column.Grid.Name = "G=1&D=2 ";
            column.SortOrder = order;

            String actual = column.GetSortingQuery();

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Test helpers

        private void AssertFilterTypeFor<TProperty>(Expression<Func<AllTypesModel, TProperty>> property, String expected)
        {
            IGrid<AllTypesModel> grid = Substitute.For<IGrid<AllTypesModel>>();
            grid.Query = new GridQuery();

            String actual = new GridColumn<AllTypesModel, TProperty>(grid, property).FilterType;

            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
