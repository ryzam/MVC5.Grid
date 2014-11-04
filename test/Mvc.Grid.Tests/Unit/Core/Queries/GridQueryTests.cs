using NSubstitute;
using NUnit.Framework;
using System.Collections.Specialized;
using System.Web;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class GridQueryTests
    {
        #region Constructor: GridQuery(HttpContextBase httpContext)

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
    }
}
