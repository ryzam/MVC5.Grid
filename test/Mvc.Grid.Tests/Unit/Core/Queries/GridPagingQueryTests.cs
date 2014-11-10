using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Specialized;
using System.Web;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class GridPagingQueryTests
    {
        #region Constructor: GridPagingQuery(GridQuery query)

        [Test]
        [TestCase("Page=", 1)]
        [TestCase("Grid -Page=", 1)]
        [TestCase("Grid -Page=2", 2)]
        public void GridSortingQuery_SetsCurrentPage(String query, Int32 expected)
        {
            GridQuery gridQuery = new GridQuery(Substitute.For<IGrid>(), HttpUtility.ParseQueryString(query));
            gridQuery.Grid.Name = "Grid ";

            Int32 actual = new GridPagingQuery(gridQuery).CurrentPage;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GridSortingQuery_SetsGridName()
        {
            GridQuery gridQuery = new GridQuery(Substitute.For<IGrid>(), new NameValueCollection());
            gridQuery.Grid.Name = "Grid";

            String actual = new GridPagingQuery(gridQuery).GridName;
            String expected = "Grid";

            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
