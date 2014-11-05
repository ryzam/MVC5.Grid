using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
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

        #region Constructor: GridColumn(Expression<Func<TModel, TKey>> expression)

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
        public void GridColumn_SetsTypeAsPreProcessor()
        {
            GridProcessorType actual = new GridColumn<GridModel, Object>(grid, model => model.Name).Type;
            GridProcessorType expected = GridProcessorType.Pre;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GridColumn_SetsExpression()
        {
            Expression<Func<GridModel, String>> expression = (model) => model.Name;
            GridModel gridModel = new GridModel { Name = "Saiwai" };

            Expression actual = new GridColumn<GridModel, String>(grid, expression).Expression;
            Expression expected = expression;

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void GridColumn_SetsName()
        {
            Expression<Func<GridModel, String>> expression = (model) => model.Name;

            String actual = new GridColumn<GridModel, String>(grid, expression).Name;
            String expected = ExpressionHelper.GetExpressionText(expression);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GridColumn_SetsSortOrderFromGridQuery()
        {
            grid.Query.GetSortingQuery("Name").SortOrder.Returns(GridSortOrder.Desc);
            Expression<Func<GridModel, String>> expression = (model) => model.Name;

            GridSortOrder? actual = new GridColumn<GridModel, String>(grid, expression).SortOrder;
            GridSortOrder? expected = GridSortOrder.Desc;

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Method: Process(IEnumerable<TModel> items)

        [Test]
        public void Process_OnIsSortableNullReturnsSameItems()
        {
            column.IsSortable = null;
            column.SortOrder = GridSortOrder.Desc;

            IQueryable<GridModel> expected = new GridModel[2].AsQueryable();
            IQueryable<GridModel> actual = column.Process(expected);

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void Process_OnIsSortableFalseReturnsSameItems()
        {
            column.IsSortable = false;
            column.SortOrder = GridSortOrder.Desc;

            IQueryable<GridModel> expected = new GridModel[2].AsQueryable();
            IQueryable<GridModel> actual = column.Process(expected);

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void Process_OnSortOrderNullReturnsSameItems()
        {
            column.SortOrder = null;
            column.IsSortable = true;

            IQueryable<GridModel> expected = new GridModel[2].AsQueryable();
            IQueryable<GridModel> actual = column.Process(expected);

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void Process_ReturnsItemsSortedByAscendingOrder()
        {
            column.IsSortable = true;
            column.SortOrder = GridSortOrder.Asc;
            GridModel[] models = { new GridModel { Name = "B" }, new GridModel { Name = "A" }};

            IEnumerable<GridModel> expected = models.OrderBy(model => model.Name);
            IEnumerable<GridModel> actual = column.Process(models.AsQueryable());

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Process_ReturnsItemsSortedByDescendingOrder()
        {
            column.IsSortable = true;
            column.SortOrder = GridSortOrder.Desc;
            GridModel[] models = { new GridModel { Name = "A" }, new GridModel { Name = "B" } };

            IEnumerable<GridModel> expected = models.OrderByDescending(model => model.Name);
            IEnumerable<GridModel> actual = column.Process(models.AsQueryable());

            CollectionAssert.AreEqual(expected, actual);
        }

        #endregion

        #region Method: ValueFor(IGridRow row)

        [Test]
        public void ValueFor_OnNullFormatReturnsEncodedValue()
        {
            IGridRow row = new GridRow(new GridModel { Name = "<script />" });
            column.Formatted(null);
            column.Encoded(true);

            String expected = WebUtility.HtmlEncode("<script />");
            String actual = column.ValueFor(row).ToString();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ValueFor_ReturnsEncodedAndFormattedValue()
        {
            IGridRow row = new GridRow(new GridModel { Sum = 100 });
            column.Formatted("<script value='{0:C2}' />");
            column.Expression = (model) => model.Sum;
            column.Encoded(true);

            String expected = WebUtility.HtmlEncode(String.Format("<script value='{0:C2}' />", 100));
            String actual = column.ValueFor(row).ToString();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ValueFor_OnNullValueReturnsEmpty()
        {
            IGridRow row = new GridRow(new GridModel { Name = null });

            String actual = column.ValueFor(row).ToString();
            String expected = String.Empty;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ValueFor_OnNullFormatReturnsNotEncodedValue()
        {
            IGridRow row = new GridRow(new GridModel { Name = "<script />" });
            column.Formatted(null);
            column.Encoded(false);

            String actual = column.ValueFor(row).ToString();
            String expected = "<script />";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ValueFor_ReturnsNotEncodedButFormattedValue()
        {
            IGridRow row = new GridRow(new GridModel { Sum = 100 });
            column.Formatted("<script value='{0:C2}' />");
            column.Expression = (model) => model.Sum;
            column.Encoded(false);

            String expected = String.Format("<script value='{0:C2}' />", 100);
            String actual = column.ValueFor(row).ToString();

            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
