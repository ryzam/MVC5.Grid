using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
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
            columns.Grid.Query = new GridQuery();
        }

        #region Constructor: GridColumns(IGrid<TModel> grid)

        [Test]
        public void GridColumns_SetsGrid()
        {
            IGrid expected = columns.Grid;
            IGrid actual = new GridColumns<GridModel>(columns.Grid).Grid;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Method: Add<TKey>(Expression<Func<TModel, TKey>> expression)

        [Test]
        public void Add_AddsGridColumn()
        {
            Expression<Func<GridModel, String>> expression = (model) => model.Name;

            IGridColumn<GridModel, String> expected = new GridColumn<GridModel, String>(columns.Grid, expression);
            IGridColumn<GridModel, String> actual = columns.Add<String>(expression);

            Assert.AreEqual(expected.ProcessorType, actual.ProcessorType);
            Assert.AreEqual(expected.IsFilterable, actual.IsFilterable);
            Assert.AreEqual(expected.FilterValue, actual.FilterValue);
            Assert.AreEqual(expected.FilterType, actual.FilterType);
            Assert.AreEqual(expected.FilterName, actual.FilterName);
            Assert.AreEqual(expected.Expression, actual.Expression);
            Assert.AreEqual(expected.CssClasses, actual.CssClasses);
            Assert.AreEqual(expected.IsSortable, actual.IsSortable);
            Assert.AreEqual(expected.IsSortable, actual.SortOrder);
            Assert.AreEqual(expected.IsEncoded, actual.IsEncoded);
            Assert.AreEqual(expected.Format, actual.Format);
            Assert.AreEqual(expected.Title, actual.Title);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Grid, actual.Grid);
        }

        [Test]
        public void Add_AddsGridColumnProcessor()
        {
            columns.Add<String>(model => model.Name);

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
    }
}
