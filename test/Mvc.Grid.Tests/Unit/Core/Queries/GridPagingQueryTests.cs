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
        #region Constructor: GridPagingQuery(IGridQuery query)

        [Test]
        [TestCase("MG-Page=1", 0)]
        [TestCase("MG-Page-Grid =", 0)]
        [TestCase("MG-Page-Grid =1", 1)]
        public void GridSortingQuery_SetsCurrentPage(String query, Int32 expected)
        {
            IGridQuery gridQuery = Substitute.For<IGridQuery>();
            gridQuery.Query = HttpUtility.ParseQueryString(query);
            gridQuery.Grid.Name = "Grid ";

            Int32 actual = new GridPagingQuery(gridQuery).CurrentPage;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GridSortingQuery_SetsGridName()
        {
            IGridQuery gridQuery = Substitute.For<IGridQuery>();
            gridQuery.Query = new NameValueCollection();
            gridQuery.Grid.Name = "Grid";

            String actual = new GridPagingQuery(gridQuery).GridName;
            String expected = "Grid";

            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
