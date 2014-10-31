using NonFactors.Mvc.Grid.Tests.Objects;
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
            Grid<GridModel> grid = new Grid<GridModel>(null);

            IGridColumns actual = (grid as IGrid).Columns;
            IGridColumns expected = grid.Columns;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Constructor: Grid(IQueryable<TModel> source)

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
            IQueryable<GridModel> source = new GridModel[2].AsQueryable();
            GridColumns<GridModel> actual = new Grid<GridModel>(source).Columns as GridColumns<GridModel>;

            CollectionAssert.IsEmpty(actual);
        }

        [Test]
        public void Grid_CreatesRows()
        {
            IQueryable<GridModel> source = new GridModel[2].AsQueryable();
            Grid<GridModel> grid = new Grid<GridModel>(source);

            GridRows<GridModel> actual = grid.Rows as GridRows<GridModel>;

            Assert.AreSame(source, actual.Source);
            Assert.AreSame(grid, actual.Grid);
        }

        [Test]
        public void Grid_SetsPagerToNull()
        {
            Grid<GridModel> actual = new Grid<GridModel>(null);

            Assert.IsNull(actual.Pager);
        }

        #endregion
    }
}
