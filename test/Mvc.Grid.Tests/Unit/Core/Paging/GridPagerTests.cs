using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class GridPagerTests
    {
        private GridPager<GridModel> pager;
        private IGrid<GridModel> grid;

        [SetUp]
        public void SetUp()
        {
            grid = Substitute.For<IGrid<GridModel>>();
            grid.Query = new NameValueCollection();
            grid.Name = "Grid";

            pager = new GridPager<GridModel>(grid);
        }

        #region Property: FirstDisplayPage

        [Test]
        [TestCase(1, 1, 1)]
        [TestCase(1, 2, 2)]
        [TestCase(1, 3, 3)]
        [TestCase(1, 4, 4)]
        [TestCase(1, 5, 5)]
        [TestCase(2, 1, 1)]
        [TestCase(2, 2, 2)]
        [TestCase(2, 3, 3)]
        [TestCase(2, 4, 4)]
        [TestCase(2, 5, 4)]
        [TestCase(3, 1, 1)]
        [TestCase(3, 2, 1)]
        [TestCase(3, 3, 2)]
        [TestCase(3, 4, 3)]
        [TestCase(3, 5, 3)]
        [TestCase(4, 1, 1)]
        [TestCase(4, 2, 1)]
        [TestCase(4, 3, 2)]
        [TestCase(4, 4, 2)]
        [TestCase(4, 5, 2)]
        [TestCase(5, 1, 1)]
        [TestCase(5, 2, 1)]
        [TestCase(5, 3, 1)]
        [TestCase(5, 4, 1)]
        [TestCase(5, 5, 1)]
        [TestCase(6, 1, 1)]
        [TestCase(6, 2, 1)]
        [TestCase(6, 3, 1)]
        [TestCase(6, 4, 1)]
        [TestCase(6, 5, 1)]
        public void FirstDisplayPage_GetsFirstDisplayPage(Int32 pagesToDisplay, Int32 currentPage, Int32 expected)
        {
            pager.Grid.Query = HttpUtility.ParseQueryString("Grid-Page=" + currentPage);
            pager.PagesToDisplay = pagesToDisplay;
            pager.RowsPerPage = 1;
            pager.TotalRows = 5;

            Int32 actual = pager.FirstDisplayPage;

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Property: CurrentPage

        [Test]
        [TestCase("Grid-Page=", 2, 2)]
        [TestCase("Grid-Page=3", 5, 3)]
        public void CurrentPage_DoesNotChangeCurrentPageAfterFirstGet(String query, Int32 initialPage, Int32 expected)
        {
            pager.Grid.Query = HttpUtility.ParseQueryString(query);
            pager.InitialPage = initialPage;
            Int32 value = pager.CurrentPage;

            pager.Grid.Query = HttpUtility.ParseQueryString("Grid-Page=5");

            Int32 actual = pager.CurrentPage;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("", 3)]
        [TestCase("Grid-Page=", 3)]
        [TestCase("Grid-Page=2a", 3)]
        public void CurrentPage_OnInvalidQueryPageUsesInitialPage(String query, Int32 initialPage)
        {
            pager.Grid.Query = HttpUtility.ParseQueryString(query);
            pager.InitialPage = initialPage;

            Int32 actual = pager.CurrentPage;
            Int32 expected = initialPage;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("Grid-Page=0")]
        [TestCase("Grid-Page=-1")]
        public void CurrentPage_OnLessOrEqualToZeroQueryPageReturnsOne(String query)
        {
            pager.Grid.Query = HttpUtility.ParseQueryString(query);
            pager.InitialPage = 5;

            Int32 actual = pager.CurrentPage;
            Int32 expected = 1;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void CurrentPage_OnLessOrEqualToZeroInitialPageReturnsOne(Int32 initialPage)
        {
            pager.Grid.Query = new NameValueCollection();
            pager.InitialPage = initialPage;

            Int32 actual = pager.CurrentPage;
            Int32 expected = 1;

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
        public void TotalPages_GetsTotalPages(Int32 totalRows, Int32 rowsPerPage, Int32 expected)
        {
            GridPager<GridModel> pager = new GridPager<GridModel>(grid);
            pager.RowsPerPage = rowsPerPage;
            pager.TotalRows = totalRows;

            Int32 actual = pager.TotalPages;

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Constructor: GridPager(IGrid<T> grid)

        [Test]
        public void GridPager_SetsGrid()
        {
            IGrid actual = new GridPager<GridModel>(grid).Grid;
            IGrid expected = grid;

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void GridPager_SetsInitialPage()
        {
            Int32 actual = new GridPager<GridModel>(grid).InitialPage;
            Int32 expected = 1;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GridPager_SetsRowsPerPage()
        {
            Int32 actual = new GridPager<GridModel>(grid).RowsPerPage;
            Int32 expected = 20;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GridPager_SetsPagesToDisplay()
        {
            Int32 actual = new GridPager<GridModel>(grid).PagesToDisplay;
            Int32 expected = 5;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GridPager_SetsDefaultPartialViewName()
        {
            String actual = new GridPager<GridModel>(grid).PartialViewName;
            String expected = "MvcGrid/_Pager";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GridPager_SetsProcessorTypeAsPostProcessor()
        {
            GridProcessorType actual = new GridPager<GridModel>(grid).ProcessorType;
            GridProcessorType expected = GridProcessorType.Post;

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Method: Process(IQueryable<T> items)

        [Test]
        public void Process_SetsTotalRows()
        {
            pager.Process(new GridModel[100].AsQueryable());

            Int32 actual = pager.TotalRows;
            Int32 expected = 100;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Process_ReturnsPagedItems()
        {
            IQueryable<GridModel> models = new[] { new GridModel(), new GridModel(), new GridModel() }.AsQueryable();
            pager.Grid.Query = HttpUtility.ParseQueryString("Grid-Page=2");
            pager.RowsPerPage = 1;

            IEnumerable expected = models.Skip(1).Take(1);
            IEnumerable actual = pager.Process(models);

            CollectionAssert.AreEqual(expected, actual);
        }

        #endregion
    }
}
