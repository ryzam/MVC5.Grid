using NonFactors.Mvc.Grid.Tests.Helpers;
using NonFactors.Mvc.Grid.Tests.Objects;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;

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
            List<GridModel> models = new List<GridModel> { new GridModel(), new GridModel() };
            Grid<GridModel> grid = new Grid<GridModel>(models);

            GridRows<GridModel> rows = new GridRows<GridModel>(grid, grid.Source);

            IEnumerable<Object> actual = rows.ToList().Select(row => row.Model);
            IEnumerable<Object> expected = models;

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetEnumerator_GetsPagedRows()
        {
            RequestContext context = HtmlHelperFactory.CreateHtmlHelper().ViewContext.RequestContext;
            GridModel[] models = { new GridModel(), new GridModel(), new GridModel() };
            Grid<GridModel> grid = new Grid<GridModel>(models);

            grid.Pager = new GridPager<GridModel>(context, grid.Source);
            grid.Pager.CurrentPage = 1;
            grid.Pager.RowsPerPage = 1;

            GridRows<GridModel> gridRows = new GridRows<GridModel>(grid, grid.Source);

            IEnumerable<Object> actual = gridRows.ToList().Select(row => row.Model);
            IEnumerable<Object> expected = models.Skip(1).Take(1);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetEnumerator_GetsSameEnumerable()
        {
            GridModel[] models = { new GridModel(), new GridModel() };
            Grid<GridModel> grid = new Grid<GridModel>(models);

            GridRows<GridModel> rows = new GridRows<GridModel>(grid, grid.Source);

            IEnumerator actual = (rows as IEnumerable).GetEnumerator();
            IEnumerator expected = rows.GetEnumerator();

            while (expected.MoveNext() | actual.MoveNext())
                Assert.AreSame((expected.Current as IGridRow).Model, (actual.Current as IGridRow).Model);
        }

        #endregion
    }
}
