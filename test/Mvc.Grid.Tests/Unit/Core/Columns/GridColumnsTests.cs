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
            GridModel gridModel = new GridModel { Name = "Kokoye" };
            columns.Add<String>(expression);

            GridColumn<GridModel, String> expected = new GridColumn<GridModel, String>(columns.Grid, expression);
            GridColumn<GridModel, String> actual = columns.Single() as GridColumn<GridModel, String>;

            Assert.AreEqual(expected.Expression, actual.Expression);
            Assert.AreEqual(expected.IsSortable, actual.IsSortable);
            Assert.AreEqual(expected.CssClasses, actual.CssClasses);
            Assert.AreEqual(expected.IsEncoded, actual.IsEncoded);
            Assert.AreEqual(expected.Format, actual.Format);
            Assert.AreEqual(expected.Title, actual.Title);
            Assert.AreEqual(expected.Name, actual.Name);
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
