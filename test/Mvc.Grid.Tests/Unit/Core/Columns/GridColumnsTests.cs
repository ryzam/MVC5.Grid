using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections;
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
        }

        #region Constructor: GridColumns()

        [Test]
        public void GridColumns_AreEmpty()
        {
            columns = new GridColumns<GridModel>(null);

            CollectionAssert.IsEmpty(columns);
        }

        [Test]
        public void GridColumns_SetsGrid()
        {
            IGrid<GridModel> grid = new Grid<GridModel>(new GridModel[0]);
            columns = new GridColumns<GridModel>(grid);

            IGrid<GridModel> actual = columns.Grid;
            IGrid<GridModel> expected = grid;

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Method: Add<TKey>(Expression<Func<TModel, TKey>> expression)

        [Test]
        public void Add_AddsColumn()
        {
            columns.Add<String>(model => model.Name);
            GridModel gridModel = new GridModel { Name = "Kokoye" };
            Expression<Func<GridModel, String>> expression = (model) => model.Name;

            GridColumn<GridModel, String> expected = new GridColumn<GridModel, String>(columns.Grid, model => model.Name);
            GridColumn<GridModel, String> actual = columns.Single() as GridColumn<GridModel, String>;

            Assert.AreEqual(expected.Expression(gridModel), actual.Expression(gridModel));
            Assert.AreEqual(expected.IsSortable, actual.IsSortable);
            Assert.AreEqual(expected.CssClasses, actual.CssClasses);
            Assert.AreEqual(expected.IsEncoded, actual.IsEncoded);
            Assert.AreEqual(expected.Format, actual.Format);
            Assert.AreEqual(expected.Title, actual.Title);
            Assert.AreEqual(expected.Name, actual.Name);
        }

        [Test]
        public void Add_ReturnsAddedColumn()
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
            IGridColumn[] addedColumns = { columns.Add(model => model.Name), columns.Add(model => model.Name) };

            IEnumerable actual = columns.ToList();
            IEnumerable expected = addedColumns;

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetEnumerator_ReturnsSameColumns()
        {
            IGridColumn[] addedColumns = { columns.Add(model => model.Name), columns.Add(model => model.Name) };

            IEnumerable expected = columns.ToList();
            IEnumerable actual = columns;

            CollectionAssert.AreEqual(expected, actual);
        }

        #endregion
    }
}
