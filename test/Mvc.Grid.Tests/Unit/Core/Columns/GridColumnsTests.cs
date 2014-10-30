using NonFactors.Mvc.Grid.Tests.Objects;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
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
        public void GridColumns_IsEmpty()
        {
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
            Assert.AreEqual(expected.Format, actual.Format);
            Assert.AreEqual(expected.Title, actual.Title);
            Assert.AreEqual(expected.Width, actual.Width);
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
            Assert.AreEqual(expected.Format, actual.Format);
            Assert.AreEqual(expected.Title, actual.Title);
            Assert.AreEqual(expected.Width, actual.Width);
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
            List<IGridColumn> addedColumns = new List<IGridColumn>();
            addedColumns.Add(columns.Add());
            addedColumns.Add(columns.Add());

            IEnumerator expected = addedColumns.GetEnumerator();
            IEnumerator actual = columns.GetEnumerator();

            while (expected.MoveNext() | actual.MoveNext())
                Assert.AreSame(expected.Current, actual.Current);
        }

        [Test]
        public void GetEnumerator_ReturnsSameColumns()
        {
            columns.Add();
            columns.Add();

            IEnumerator actual = (columns as IEnumerable).GetEnumerator();
            IEnumerator expected = columns.GetEnumerator();

            while (expected.MoveNext() | actual.MoveNext())
                Assert.AreSame(expected.Current, actual.Current);
        }

        #endregion
    }
}
