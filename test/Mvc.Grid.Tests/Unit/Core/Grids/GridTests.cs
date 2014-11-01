using NUnit.Framework;
using System.Collections.Generic;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class GridTests
    {
        #region Property: IGrid.Columns

        [Test]
        public void IGridColumns_ReturnsSameColumns()
        {
            Grid<GridModel> grid = new Grid<GridModel>(null);

            IGridColumns actual = (grid as IGrid).Columns;
            IGridColumns expected = grid.Columns;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Constructor: Grid(IEnumerable<TModel> source)

        [Test]
        public void Grid_CreatesEmptyProcessors()
        {
            Grid<GridModel> grid = new Grid<GridModel>(null);

            CollectionAssert.IsEmpty(grid.Processors);
        }

        [Test]
        public void Grid_SetsSource()
        {
            IEnumerable<GridModel> expected = new GridModel[2];
            IEnumerable<GridModel> actual = new Grid<GridModel>(expected).Source;

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void Grid_CreatesEmptyColumns()
        {
            Grid<GridModel> grid = new Grid<GridModel>(null);

            CollectionAssert.IsEmpty(grid.Columns);
        }

        [Test]
        public void Grid_CreatesRows()
        {
            Grid<GridModel> grid = new Grid<GridModel>(null);

            GridRows<GridModel> actual = grid.Rows as GridRows<GridModel>;
            GridRows<GridModel> expected = new GridRows<GridModel>(grid);

            Assert.AreSame(actual.Grid, actual.Grid);
        }

        [Test]
        public void Grid_SetsPagerToNull()
        {
            Grid<GridModel> grid = new Grid<GridModel>(null);

            Assert.IsNull(grid.Pager);
        }

        [Test]
        public void Grid_SetsEmptyTextToNull()
        {
            Grid<GridModel> grid = new Grid<GridModel>(null);

            Assert.IsNull(grid.EmptyText);
        }

        [Test]
        public void Grid_SetsNameToNull()
        {
            Grid<GridModel> grid = new Grid<GridModel>(null);

            Assert.IsNull(grid.Name);
        }

        #endregion
    }
}
