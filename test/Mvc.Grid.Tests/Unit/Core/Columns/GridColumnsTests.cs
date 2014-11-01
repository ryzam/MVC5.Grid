using NonFactors.Mvc.Grid.Tests.Objects;
using NUnit.Framework;
using System;
using System.Collections;
using System.Linq;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class GridColumnsTests
    {
        private GridColumns<GridModel> columns;

        [SetUp]
        public void SetUp()
        {
            columns = new GridColumns<GridModel>();
        }

        #region Constructor: GridColumns()

        [Test]
        public void GridColumns_AreEmpty()
        {
            columns = new GridColumns<GridModel>();

            CollectionAssert.IsEmpty(columns);
        }

        #endregion

        #region Method: Add()

        [Test]
        public void Add_AddsEmptyColumn()
        {
            columns.Add();

            GridColumn<GridModel, String> actual = columns.Single() as GridColumn<GridModel, String>;
            GridColumn<GridModel, String> expected = new GridColumn<GridModel, String>();

            Assert.AreEqual(expected.Expression, actual.Expression);
            Assert.AreEqual(expected.CssClasses, actual.CssClasses);
            Assert.AreEqual(expected.IsEncoded, actual.IsEncoded);
            Assert.AreEqual(expected.Format, actual.Format);
            Assert.AreEqual(expected.Title, actual.Title);
        }

        [Test]
        public void Add_ReturnsAddedColumn()
        {
            IGridColumn actual = columns.Add();
            IGridColumn expected = columns.Single();

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Method: Add<TKey>(Expression<Func<TModel, TKey>> expression)

        [Test]
        public void Add_AddsColumnWithExpression()
        {
            columns.Add<String>(model => model.Name);

            GridColumn<GridModel, String> actual = columns.Single() as GridColumn<GridModel, String>;
            GridColumn<GridModel, String> expected = new GridColumn<GridModel, String>(actual.Expression);

            Assert.AreEqual(expected.Expression, actual.Expression);
            Assert.AreEqual(expected.CssClasses, actual.CssClasses);
            Assert.AreEqual(expected.IsEncoded, actual.IsEncoded);
            Assert.AreEqual(expected.Format, actual.Format);
            Assert.AreEqual(expected.Title, actual.Title);
        }

        [Test]
        public void Add_ReturnsAddedColumnWithExpression()
        {
            IGridColumn actual = columns.Add(model => model.Name);
            IGridColumn expected = columns.Single();

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Method: GetEnumerator()

        [Test]
        public void GetEnumarator_ReturnsColumns()
        {
            IGridColumn[] addedColumns = { columns.Add(), columns.Add() };

            IEnumerable actual = columns.ToList();
            IEnumerable expected = addedColumns;

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetEnumerator_ReturnsSameColumns()
        {
            IGridColumn[] addedColumns = { columns.Add(), columns.Add() };

            IEnumerable expected = columns.ToList();
            IEnumerable actual = columns;

            CollectionAssert.AreEqual(expected, actual);
        }

        #endregion
    }
}
