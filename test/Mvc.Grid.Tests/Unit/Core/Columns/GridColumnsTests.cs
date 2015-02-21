using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class GridColumnsTests
    {
        private GridColumns<GridModel> columns;

        [SetUp]
        public void SetUp()
        {
            columns = new GridColumns<GridModel>(Substitute.For<IGrid<GridModel>>());
            columns.Grid.Processors = new List<IGridProcessor<GridModel>>();
            columns.Grid.Query = new NameValueCollection();
        }

        #region Constructor: GridColumns(IGrid<T> grid)

        [Test]
        public void GridColumns_SetsGrid()
        {
            IGrid expected = columns.Grid;
            IGrid actual = new GridColumns<GridModel>(columns.Grid).Grid;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Method: Add<TValue>(Expression<Func<T, TValue>> expression)

        [Test]
        public void Add_AddsGridColumn()
        {
            Expression<Func<GridModel, String>> expression = (model) => model.Name;
            columns.Add(expression);

            GridColumn<GridModel, String> expected = new GridColumn<GridModel, String>(columns.Grid, expression);
            GridColumn<GridModel, String> actual = columns.Single() as GridColumn<GridModel, String>;

            Assert.AreEqual(expected.ProcessorType, actual.ProcessorType);
            Assert.AreEqual(expected.IsFilterable, actual.IsFilterable);
            Assert.AreEqual(expected.FilterName, actual.FilterName);
            Assert.AreEqual(expected.Expression, actual.Expression);
            Assert.AreEqual(expected.CssClasses, actual.CssClasses);
            Assert.AreEqual(expected.IsSortable, actual.IsSortable);
            Assert.AreEqual(expected.SortOrder, actual.SortOrder);
            Assert.AreEqual(expected.IsEncoded, actual.IsEncoded);
            Assert.AreEqual(expected.Format, actual.Format);
            Assert.AreEqual(expected.Title, actual.Title);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Grid, actual.Grid);
        }

        [Test]
        public void Add_AddsGridColumnProcessor()
        {
            columns.Add(model => model.Name);

            Object actual = columns.Grid.Processors.Single();
            Object expected = columns.Single();

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void Add_ReturnsAddedColumn()
        {
            IGridColumn actual = columns.Add(model => model.Name);
            IGridColumn expected = columns.Single();

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Method: Insert<TValue>(Int32 index, Expression<Func<T, TValue>> expression)

        [Test]
        public void Insert_InsertsGridColumn()
        {
            Expression<Func<GridModel, Int32>> expression = (model) => model.Sum;
            columns.Add(model => model.Name);
            columns.Insert(0, expression);

            GridColumn<GridModel, Int32> expected = new GridColumn<GridModel, Int32>(columns.Grid, expression);
            GridColumn<GridModel, Int32> actual = columns.First() as GridColumn<GridModel, Int32>;

            Assert.AreEqual(expected.ProcessorType, actual.ProcessorType);
            Assert.AreEqual(expected.IsFilterable, actual.IsFilterable);
            Assert.AreEqual(expected.FilterName, actual.FilterName);
            Assert.AreEqual(expected.Expression, actual.Expression);
            Assert.AreEqual(expected.CssClasses, actual.CssClasses);
            Assert.AreEqual(expected.IsSortable, actual.IsSortable);
            Assert.AreEqual(expected.SortOrder, actual.SortOrder);
            Assert.AreEqual(expected.IsEncoded, actual.IsEncoded);
            Assert.AreEqual(expected.Format, actual.Format);
            Assert.AreEqual(expected.Title, actual.Title);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Grid, actual.Grid);
        }

        [Test]
        public void Insert_AddsGridColumnProcessor()
        {
            columns.Insert(0, model => model.Name);

            Object actual = columns.Grid.Processors.Single();
            Object expected = columns.Single();

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void Insert_ReturnsAddedColumn()
        {
            IGridColumn actual = columns.Insert(0, model => model.Name);
            IGridColumn expected = columns.Single();

            Assert.AreSame(expected, actual);
        }

        #endregion
    }
}
