using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Specialized;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class GridPagingQueryTests
    {
        #region Constructor: GridPagingQuery(IGridQuery query)

        [Test]
        [TestCase(null, "MG-Page", "", 0)]
        [TestCase(null, "MG-Page", "1", 1)]

        [TestCase("  ", "MG-Page", "", 0)]
        [TestCase("  ", "MG-Page", "5", 5)]

        [TestCase("Grid", "MG-Page-Grid", "", 0)]
        [TestCase("Grid", "MG-Page-Grid", "10", 10)]
        public void GridSortingQuery_SetsCurrentPage(String gridName, String queryKey, String queryVal, Int32 currentPage)
        {
            IGridQuery gridQuery = Substitute.For<IGridQuery>();
            gridQuery.Query = new NameValueCollection();
            gridQuery.Query[queryKey] = queryVal;
            gridQuery.Grid.Name = gridName;

            Int32 actual = new GridPagingQuery(gridQuery).CurrentPage;
            Int32 expected = currentPage;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GridSortingQuery_SetsGridName()
        {
            IGridQuery gridQuery = Substitute.For<IGridQuery>();
            gridQuery.Query = new NameValueCollection();
            gridQuery.Grid.Name = "GridName";

            String actual = new GridPagingQuery(gridQuery).GridName;
            String expected = "GridName";

            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
