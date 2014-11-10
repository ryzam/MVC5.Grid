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

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            grid = Substitute.For<IGrid<GridModel>>();
            grid.Query = Substitute.For<GridQuery>(grid, new NameValueCollection());
            grid.Source.Returns(new GridModel[5]
            {
                new GridModel(),
                new GridModel(),
                new GridModel(),
                new GridModel(),
                new GridModel()
            }.AsQueryable());

            pager = new GridPager<GridModel>(grid);
        }

        #region Property: StartingPage

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
        public void StartingPage_GetsStartingPage(Int32 pagesToDisplay, Int32 currentPage, Int32 expected)
        {
            pager.PagesToDisplay = pagesToDisplay;
            pager.CurrentPage = currentPage;
            pager.RowsPerPage = 1;

            Int32 actual = pager.StartingPage;

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
        public void TotalPages_GetsTotalPages(Int32 itemsCount, Int32 rowsPerPage, Int32 expected)
        {
            grid.Source.Returns(new GridModel[itemsCount].AsQueryable());

            GridPager<GridModel> pager = new GridPager<GridModel>(grid);
            pager.RowsPerPage = rowsPerPage;

            Int32 actual = pager.TotalPages;

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Constructor: GridPager(IGrid<TModel> grid)

        [Test]
        public void GridPager_SetsDefaultPartialViewName()
        {
            String actual = new GridPager<GridModel>(grid).PartialViewName;
            String expected = "MvcGrid/_Pager";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GridPager_SetsCurrentPageFromQuery()
        {
            grid.Query.GetPagingQuery().CurrentPage.Returns(15);

            Int32 actual = new GridPager<GridModel>(grid).CurrentPage;
            Int32 expected = 15;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GridPager_SetsTotalRowsFromGridSource()
        {
            Int32 actual = new GridPager<GridModel>(grid).TotalRows;
            Int32 expected = grid.Source.Count();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GridPager_SetsTypeAsPostProcessor()
        {
            GridProcessorType actual = new GridPager<GridModel>(grid).Type;
            GridProcessorType expected = GridProcessorType.Post;

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
        public void GridPager_SetsRowsPerPage()
        {
            Int32 actual = new GridPager<GridModel>(grid).RowsPerPage;
            Int32 expected = 20;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GridPager_SetsGrid()
        {
            IGrid actual = new GridPager<GridModel>(grid).Grid;
            IGrid expected = grid;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Method: Process(IQueryable<TModel> items)

        [Test]
        public void Process_ReturnsPagedItems()
        {
            pager.CurrentPage = 2;
            pager.RowsPerPage = 1;

            IEnumerable expected = grid.Source.Skip(1).Take(1);
            IEnumerable actual = pager.Process(grid.Source);

            CollectionAssert.AreEqual(expected, actual);
        }

        #endregion

        #region Method: LinkForPage(Int32 page)

        [Test]
        [TestCase("?Id=4&On=true", 3, "?Id=4&On=true&Grid+-Page=3")]
        [TestCase("?Id=4&Grid -Page=10&On=true", 3, "?Id=4&Grid+-Page=3&On=true")]
        public void LinkForPage_GeneratesLinkForPage(String queryString, Int32 page, String expected)
        {
            grid.Name = "Grid ";
            grid.Query = new GridQuery(grid, HttpUtility.ParseQueryString(queryString));

            String actual = new GridPager<GridModel>(grid).LinkForPage(page);

            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
