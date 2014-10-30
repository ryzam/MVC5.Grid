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

            IEnumerator actual = (gridRows.Select(row => row.Model) as IEnumerable).GetEnumerator();
            IEnumerator expected = models.GetEnumerator();

            while (expected.MoveNext() | actual.MoveNext())
                Assert.AreSame(expected.Current, actual.Current);
        }

        #endregion
    }
}
