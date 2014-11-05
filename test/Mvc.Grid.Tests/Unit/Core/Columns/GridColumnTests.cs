using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
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
        public void GridColumn_SetsTypeAsPreProcessor()
        {
            GridProcessorType actual = new GridColumn<GridModel, Object>(grid, model => model.Name).Type;
            GridProcessorType expected = GridProcessorType.Pre;

            Assert.AreEqual(expected, actual);
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
        [TestCase(null)]
        [TestCase(GridSortOrder.Asc)]
        [TestCase(GridSortOrder.Desc)]
        public void GridColumn_SetsSortOrderFromGridQuery(GridSortOrder? order)
        {
            grid.Query.GetSortingQuery("Name").SortOrder.Returns(order);

            GridSortOrder? actual = new GridColumn<GridModel, String>(grid, model => model.Name).SortOrder;
            GridSortOrder? expected = order;

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Method: Process(IEnumerable<TModel> items)

        [Test]
        [TestCase(null, null)]
        [TestCase(null, GridSortOrder.Asc)]
        [TestCase(null, GridSortOrder.Desc)]
        [TestCase(false, null)]
        [TestCase(false, GridSortOrder.Asc)]
        [TestCase(false, GridSortOrder.Desc)]
        public void Process_IfNotSortableReturnsItems(Boolean? isSortable, GridSortOrder? order)
        {
            column.IsSortable = isSortable;
            column.SortOrder = order;

            IQueryable<GridModel> expected = new GridModel[2].AsQueryable();
            IQueryable<GridModel> actual = column.Process(expected);

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void Process_IfSortOrderIsNullReturnsItems()
        {
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
            column.SortOrder = GridSortOrder.Desc;
            GridModel[] models = { new GridModel { Name = "A" }, new GridModel { Name = "B" } };

            IEnumerable expected = models.OrderByDescending(model => model.Name);
            IEnumerable actual = column.Process(models.AsQueryable());

            CollectionAssert.AreEqual(expected, actual);
        }

        #endregion

        #region Method: ValueFor(IGridRow row)

        [Test]
        [TestCase(null, null, false, "")]
        [TestCase(null, null, true, "")]
        [TestCase(null, "", false, "")]
        [TestCase(null, "", true, "")]
        [TestCase(null, "Format {0}", false, "")]
        [TestCase(null, "Format {0}", true, "")]
        [TestCase("", null, false, "")]
        [TestCase("", null, true, "")]
        [TestCase("", "", false, "")]
        [TestCase("", "", true, "")]
        [TestCase("", "Format {0}", false, "Format ")]
        [TestCase("", "Format {0}", true, "Format ")]
        [TestCase("name", null, false, "name")]
        [TestCase("name", null, true, "name")]
        [TestCase("name", "", false, "")]
        [TestCase("name", "", true, "")]
        [TestCase("name", "Format {0}", false, "Format name")]
        [TestCase("name", "Format {0}", true, "Format name")]
        [TestCase("<name>", null, false, "<name>")]
        [TestCase("<name>", null, true, "&lt;name&gt;")]
        [TestCase("<name>", "", false, "")]
        [TestCase("<name>", "", true, "")]
        [TestCase("<name>", "Format {0}", false, "Format <name>")]
        [TestCase("<name>", "Format {0}", true, "Format &lt;name&gt;")]
        public void ValueFor_GetsValue(String name, String format, Boolean isEncoded, String value)
        {
            IGridRow row = new GridRow(new GridModel { Name = name });
            column.Encoded(isEncoded);
            column.Formatted(format);

            String actual = column.ValueFor(row).ToString();
            String expected = value;

            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
