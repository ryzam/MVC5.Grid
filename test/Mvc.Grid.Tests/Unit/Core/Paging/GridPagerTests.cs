using NonFactors.Mvc.Grid.Tests.Objects;
using NUnit.Framework;
using System;
using System.Linq;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class GridPagerTests
    {
        #region Property: StartingPage

        [Test]
        [TestCase(1, 0, 0)]
        [TestCase(1, 1, 1)]
        [TestCase(1, 2, 2)]
        [TestCase(1, 3, 3)]
        [TestCase(1, 4, 4)]
        [TestCase(1, 5, 5)]
        [TestCase(3, 0, 0)]
        [TestCase(3, 1, 0)]
        [TestCase(3, 2, 1)]
        [TestCase(3, 3, 2)]
        [TestCase(3, 4, 3)]
        [TestCase(3, 5, 3)]
        [TestCase(4, 0, 0)]
        [TestCase(4, 1, 0)]
        [TestCase(4, 2, 1)]
        [TestCase(4, 3, 2)]
        [TestCase(4, 4, 2)]
        [TestCase(4, 5, 2)]
        public void StartingPage_GetsStartingPage(Int32 pagesToDisplay, Int32 currentPage, Int32 startingPage)
        {
            GridPager<GridModel> pager = new GridPager<GridModel>(new GridModel[6]);
            pager.PagesToDisplay = pagesToDisplay;
            pager.CurrentPage = currentPage;
            pager.RowsPerPage = 1;

            Int32 actual = pager.StartingPage;
            Int32 expected = startingPage;

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Property: TotalPages

        [Test]
        [TestCase(0, 20, 0)]
        [TestCase(1, 20, 1)]
        [TestCase(19, 20, 1)]
        [TestCase(20, 20, 1)]
        [TestCase(21, 20, 2)]
        [TestCase(39, 20, 2)]
        [TestCase(40, 20, 2)]
        [TestCase(41, 20, 3)]
        public void TotalPages_GetsTotalPages(Int32 itemsCount, Int32 rowsPerPage, Int32 totalPages)
        {
            GridPager<GridModel> pager = new GridPager<GridModel>(new GridModel[itemsCount]);
            pager.RowsPerPage = rowsPerPage;

            Int32 actual = pager.TotalPages;
            Int32 expected = totalPages;

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Constructor: GridPager(IEnumerable<TModel> source)

        [Test]
        public void GridPager_SetsDefaultPartialViewName()
        {
            String actual = new GridPager<GridModel>(new GridModel[0]).PartialViewName;
            String expected = "MvcGrid/_Pager";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GridPager_SetsCurrentPageToZero()
        {
            Int32 actual = new GridPager<GridModel>(new GridModel[0]).CurrentPage;
            Int32 expected = 0;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GridPager_SetsRowsPerPageTo20()
        {
            Int32 actual = new GridPager<GridModel>(new GridModel[0]).RowsPerPage;
            Int32 expected = 20;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GridPager_SetsPagesToDisplayTo5()
        {
            Int32 actual = new GridPager<GridModel>(new GridModel[0]).PagesToDisplay;
            Int32 expected = 5;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GridPager_SetsTotalRowsFromSource()
        {
            GridModel[] source = new GridModel[2];

            Int32 actual = new GridPager<GridModel>(source).TotalRows;
            Int32 expected = source.Count();

            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
