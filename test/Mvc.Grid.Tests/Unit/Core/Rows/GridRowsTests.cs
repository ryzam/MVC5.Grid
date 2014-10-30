using NonFactors.Mvc.Grid.Tests.Objects;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class GridRowsTests
    {
        #region Constructor: GridRows(IGrid grid, IEnumerable<TModel> source)

        [Test]
        public void GridRows_SetsGrid()
        {
            IGrid expected = new Grid<GridModel>(null);
            IGrid actual = new GridRows<GridModel>(expected, null).Grid;

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void GridRows_SetsSource()
        {
            IEnumerable<GridModel> expected = new GridModel[1];
            IEnumerable<GridModel> actual = new GridRows<GridModel>(null, expected).Source;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Method: GetEnumerator()

        [Test]
        public void GetEnumerator_GetsNotPagedRows()
        {
            List<GridModel> models = new List<GridModel> { new GridModel(), new GridModel(), new GridModel() };
            Grid<GridModel> grid = new Grid<GridModel>(models.AsQueryable());

            GridRows<GridModel> gridRows = new GridRows<GridModel>(grid, grid.Source);

            IEnumerator<Object> actual = gridRows.Select(row => row.Model).GetEnumerator();
            IEnumerator<GridModel> expected = models.GetEnumerator();

            while (expected.MoveNext() | actual.MoveNext())
                Assert.AreSame(expected.Current, actual.Current);
        }

        [Test]
        public void GetEnumerator_GetsPagedRows()
        {
            GridModel[] models = { new GridModel(), new GridModel(), new GridModel() };
            Grid<GridModel> grid = new Grid<GridModel>(models.AsQueryable());
            grid.Pager = new GridPager<GridModel>(grid.Source);
            grid.Pager.CurrentPage = 1;
            grid.Pager.RowsPerPage = 1;

            GridRows<GridModel> gridRows = new GridRows<GridModel>(grid, grid.Source);

            IEnumerator actual = gridRows.Select(row => row.Model).GetEnumerator();
            IEnumerator expected = models.Skip(1).Take(1).GetEnumerator();

            while (expected.MoveNext() | actual.MoveNext())
                Assert.AreSame(expected.Current, actual.Current);
        }

        [Test]
        public void GetEnumerator_GetsSameEnumerable()
        {
            GridModel[] models = { new GridModel(), new GridModel(), new GridModel() };
            Grid<GridModel> grid = new Grid<GridModel>(models.AsQueryable());

            GridRows<GridModel> gridRows = new GridRows<GridModel>(grid, grid.Source);

            IEnumerator actual = (gridRows as IEnumerable).GetEnumerator();
            IEnumerator expected = gridRows.GetEnumerator();

            while (expected.MoveNext() | actual.MoveNext())
                Assert.AreSame((expected.Current as IGridRow).Model, (actual.Current as IGridRow).Model);
        }

        #endregion
    }
}
