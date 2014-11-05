using NUnit.Framework;
using System.Linq;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class GridTests
    {
        #region Property: IGrid.Columns

        [Test]
        public void IGridColumns_ReturnsSameColumns()
        {
            Grid<GridModel> grid = new Grid<GridModel>(new GridModel[0]);

            IGridColumns actual = (grid as IGrid).Columns;
            IGridColumns expected = grid.Columns;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Property: IGrid.Rows

        [Test]
        public void IGridRows_ReturnsSameRows()
        {
            Grid<GridModel> grid = new Grid<GridModel>(new GridModel[0]);

            IGridRows actual = (grid as IGrid).Rows;
            IGridRows expected = grid.Rows;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Property: IGrid.Pager

        [Test]
        public void IGridPager_ReturnsSamePager()
        {
            Grid<GridModel> grid = new Grid<GridModel>(new GridModel[0]);

            IGridPager actual = (grid as IGrid).Pager;
            IGridPager expected = grid.Pager;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Constructor: Grid(IEnumerable<TModel> source)

        [Test]
        public void Grid_CreatesEmptyProcessors()
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
        public void Grid_CreatesEmptyColumns()
        {
            Grid<GridModel> grid = new Grid<GridModel>(new GridModel[0]);

            CollectionAssert.IsEmpty(grid.Columns);
        }

        [Test]
        public void Grid_CreatesColumnsWithGrid()
        {
            Grid<GridModel> grid = new Grid<GridModel>(new GridModel[0]);

            GridColumns<GridModel> actual = grid.Columns as GridColumns<GridModel>;
            GridColumns<GridModel> expected = new GridColumns<GridModel>(grid);

            Assert.AreSame(actual.Grid, actual.Grid);
        }

        [Test]
        public void Grid_CreatesRowsWithGrid()
        {
            Grid<GridModel> grid = new Grid<GridModel>(new GridModel[0]);

            GridRows<GridModel> actual = grid.Rows as GridRows<GridModel>;
            GridRows<GridModel> expected = new GridRows<GridModel>(grid);

            Assert.AreSame(actual.Grid, actual.Grid);
        }

        #endregion
    }
}
