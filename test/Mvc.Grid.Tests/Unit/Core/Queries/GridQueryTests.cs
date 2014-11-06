using NSubstitute;
using NUnit.Framework;
using System.Collections.Specialized;
using System.Web;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class GridQueryTests
    {
        #region Constructor: GridQuery(IGrid grid, HttpContextBase httpContext)

        [Test]
        public void GridQuery_SetsGrid()
        {
            HttpContextBase httpContext = HttpContextFactory.CreateHttpContextBase();
            IGrid grid = Substitute.For<IGrid>();

            IGrid actual = new GridQuery(grid, httpContext).Grid;
            IGrid expected = grid;

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void GridQuery_SetsQuery()
        {
            HttpContextBase httpContext = HttpContextFactory.CreateHttpContextBase();

            NameValueCollection actual = new GridQuery(null, httpContext).Query;
            NameValueCollection expected = httpContext.Request.QueryString;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Method: GetSortingQuery(String columnName)

        [Test]
        public void GetSortingQuery_GetsGridSortingQuery()
        {
            HttpContextBase httpContext = HttpContextFactory.CreateHttpContextBase("MG-Sort-Column=Asc");
            GridQuery gridQuery = new GridQuery(Substitute.For<IGrid>(), httpContext);

            GridSortingQuery actual = gridQuery.GetSortingQuery("Column") as GridSortingQuery;
            GridSortingQuery expected = new GridSortingQuery(gridQuery, "Column");

            Assert.AreEqual(expected.ColumnName, actual.ColumnName);
            Assert.AreEqual(expected.SortOrder, actual.SortOrder);
        }

        #endregion

        #region Method: GetPagingQuery()

        [Test]
        public void GetPagingQuery_GetsGridPagingQuery()
        {
            HttpContextBase httpContext = HttpContextFactory.CreateHttpContextBase("MG-Page=11");
            GridQuery gridQuery = new GridQuery(Substitute.For<IGrid>(), httpContext);

            GridPagingQuery actual = gridQuery.GetPagingQuery() as GridPagingQuery;
            GridPagingQuery expected = new GridPagingQuery(gridQuery);

            Assert.AreEqual(expected.CurrentPage, actual.CurrentPage);
            Assert.AreEqual(expected.GridName, actual.GridName);
        }

        #endregion
    }
}
