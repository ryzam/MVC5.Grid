using NUnit.Framework;
using System;
using System.Linq;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class GridTests
    {
        #region Property: IGrid.Columns

        [Test]
        public void IGridColumns_ReturnsColumns()
        {
            Grid<GridModel> grid = new Grid<GridModel>(new GridModel[0]);

            IGridColumns actual = (grid as IGrid).Columns;
            IGridColumns expected = grid.Columns;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Property: IGrid.Rows

        [Test]
        public void IGridRows_ReturnsRows()
        {
            Grid<GridModel> grid = new Grid<GridModel>(new GridModel[0]);

            IGridRows actual = (grid as IGrid).Rows;
            IGridRows expected = grid.Rows;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Property: IGrid.Pager

        [Test]
        public void IGridPager_ReturnsPager()
        {
            Grid<GridModel> grid = new Grid<GridModel>(new GridModel[0]);

            IGridPager actual = (grid as IGrid).Pager;
            IGridPager expected = grid.Pager;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Constructor: Grid(IEnumerable<TModel> source)

        [Test]
        public void Grid_CreatesEmptyProcessorsList()
        {
            Grid<GridModel> grid = new Grid<GridModel>(new GridModel[0]);

            CollectionAssert.IsEmpty(grid.Processors);
        }

        [Test]
        public void Grid_SetsSource()
        {
            IQueryable<GridModel> expected = new GridModel[2].AsQueryable();
            IQueryable<GridModel> actual = new Grid<GridModel>(expected).Source;

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void Grid_SetsName()
        {
            String actual = new Grid<GridModel>(new GridModel[0]).Name;
            String expected = "Grid";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Grid_CreatesColumns()
        {
            Grid<GridModel> grid = new Grid<GridModel>(new GridModel[0]);

            GridColumns<GridModel> actual = grid.Columns as GridColumns<GridModel>;
            GridColumns<GridModel> expected = new GridColumns<GridModel>(grid);

            CollectionAssert.AreEqual(expected, actual);
            Assert.AreSame(actual.Grid, actual.Grid);
        }

        [Test]
        public void Grid_CreatesRows()
        {
            Grid<GridModel> grid = new Grid<GridModel>(new GridModel[0]);

            GridRows<GridModel> actual = grid.Rows as GridRows<GridModel>;
            GridRows<GridModel> expected = new GridRows<GridModel>(grid);

            CollectionAssert.AreEqual(expected, actual);
            Assert.AreSame(actual.Grid, actual.Grid);
        }

        #endregion
    }
}
