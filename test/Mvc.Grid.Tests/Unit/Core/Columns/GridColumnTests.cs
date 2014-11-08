using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections;
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
        public void GridColumn_SetsSortOrderFromGridQuery()
        {
            grid.Query.GetSortingQuery("Name").SortOrder = GridSortOrder.Desc;

            GridSortOrder? actual = new GridColumn<GridModel, String>(grid, model => model.Name).SortOrder;
            GridSortOrder? expected = GridSortOrder.Desc;

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Method: Process(IEnumerable<TModel> items)

        [Test]
        public void Process_IfNotSortableReturnsItems()
        {
            column.IsSortable = false;
            column.SortOrder = GridSortOrder.Desc;

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
        [TestCase(null, "For {0}", true, "")]
        [TestCase(null, "For {0}", false, "")]
        [TestCase("<name>", null, false, "<name>")]
        [TestCase("<name>", null, true, "&lt;name&gt;")]
        [TestCase("<name>", "For <{0}>", false, "For <<name>>")]
        [TestCase("<name>", "For <{0}>", true, "For &lt;&lt;name&gt;&gt;")]
        public void ValueFor_GetsValue(String name, String format, Boolean isEncoded, String expected)
        {
            IGridRow row = new GridRow(new GridModel { Name = name });
            column.Encoded(isEncoded);
            column.Formatted(format);

            String actual = column.ValueFor(row).ToString();

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Method: LinkForSort()

        [Test]
        [TestCase(null, "Grid", "", null, "#")]
        [TestCase(false, "Grid", "", null, "#")]
        [TestCase(true, null, "", null, "?MG-Sort-Name=Asc")]
        [TestCase(true, null, "", GridSortOrder.Asc, "?MG-Sort-Name=Desc")]
        [TestCase(true, null, "", GridSortOrder.Desc, "?MG-Sort-Name=Asc")]
        [TestCase(true, null, "Id=4&On=true", null, "?Id=4&On=true&MG-Sort-Name=Asc")]
        [TestCase(true, null, "Id=4&MG-Sort-Name=Asc", GridSortOrder.Asc, "?Id=4&MG-Sort-Name=Desc")]
        [TestCase(true, null, "Id=4&MG-Sort-Grid-Name=Asc", GridSortOrder.Asc, "?Id=4&MG-Sort-Grid-Name=Asc&MG-Sort-Name=Desc")]
        [TestCase(true, null, "Id=4&MG-Sort-Name=Desc", GridSortOrder.Desc, "?Id=4&MG-Sort-Name=Asc")]
        [TestCase(true, null, "Id=4&MG-Sort-Grid-Name=Desc", GridSortOrder.Desc, "?Id=4&MG-Sort-Grid-Name=Desc&MG-Sort-Name=Asc")]
        [TestCase(true, "", "", null, "?MG-Sort-Name=Asc")]
        [TestCase(true, "", "", GridSortOrder.Asc, "?MG-Sort-Name=Desc")]
        [TestCase(true, "", "", GridSortOrder.Desc, "?MG-Sort-Name=Asc")]
        [TestCase(true, "", "Id=4&On=true", null, "?Id=4&On=true&MG-Sort-Name=Asc")]
        [TestCase(true, "", "Id=4&MG-Sort-Name=Asc", GridSortOrder.Asc, "?Id=4&MG-Sort-Name=Desc")]
        [TestCase(true, "", "Id=4&MG-Sort-Grid-Name=Asc", GridSortOrder.Asc, "?Id=4&MG-Sort-Grid-Name=Asc&MG-Sort-Name=Desc")]
        [TestCase(true, "", "Id=4&MG-Sort-Name=Desc", GridSortOrder.Desc, "?Id=4&MG-Sort-Name=Asc")]
        [TestCase(true, "", "Id=4&MG-Sort-Grid-Name=Desc", GridSortOrder.Desc, "?Id=4&MG-Sort-Grid-Name=Desc&MG-Sort-Name=Asc")]
        [TestCase(true, "Grid ", "", null, "?MG-Sort-Grid%20-Name=Asc")]
        [TestCase(true, "Grid ", "", GridSortOrder.Asc, "?MG-Sort-Grid%20-Name=Desc")]
        [TestCase(true, "Grid ", "", GridSortOrder.Desc, "?MG-Sort-Grid%20-Name=Asc")]
        [TestCase(true, "Grid ", "Id=4&On=true", null, "?Id=4&On=true&MG-Sort-Grid%20-Name=Asc")]
        [TestCase(true, "Grid ", "Id=4&MG-Sort-Grid -Name=Asc", null, "?Id=4&MG-Sort-Grid%20-Name=Asc")]
        [TestCase(true, "Grid ", "Id=4&MG-Sort-Name=Asc", null, "?Id=4&MG-Sort-Name=Asc&MG-Sort-Grid%20-Name=Asc")]
        [TestCase(true, "Grid ", "Id=4&MG-Sort-Grid -Name=Desc", null, "?Id=4&MG-Sort-Grid%20-Name=Asc")]
        [TestCase(true, "Grid ", "Id=4&MG-Sort-Name=Desc", null, "?Id=4&MG-Sort-Name=Desc&MG-Sort-Grid%20-Name=Asc")]
        [TestCase(true, "Grid ", "Id=4&MG-Sort-Grid -Name=Asc", GridSortOrder.Asc, "?Id=4&MG-Sort-Grid%20-Name=Desc")]
        [TestCase(true, "Grid ", "Id=4&MG-Sort-Name=Asc", GridSortOrder.Asc, "?Id=4&MG-Sort-Name=Asc&MG-Sort-Grid%20-Name=Desc")]
        [TestCase(true, "Grid ", "Id=4&MG-Sort-Grid -Name=Desc", GridSortOrder.Desc, "?Id=4&MG-Sort-Grid%20-Name=Asc")]
        [TestCase(true, "Grid ", "Id=4&MG-Sort-Name=Desc", GridSortOrder.Desc, "?Id=4&MG-Sort-Name=Desc&MG-Sort-Grid%20-Name=Asc")]
        public void LinkForSort_GeneratesLinkForSort(Boolean? isSortable, String gridName, String query, GridSortOrder? order, String expected)
        {
            column.Grid.Query.Query = HttpUtility.ParseQueryString(query);
            column.IsSortable = isSortable;
            column.Grid.Name = gridName;
            column.SortOrder = order;

            String actual = column.LinkForSort();

            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
