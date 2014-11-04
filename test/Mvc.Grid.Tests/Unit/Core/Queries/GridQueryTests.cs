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
        public void GridQuery_SetsQuery()
        {
            HttpContextBase httpContext = HttpContextFactory.CreateHttpContextBase();

            NameValueCollection expected = httpContext.Request.QueryString;
            NameValueCollection actual = new GridQuery(httpContext).Query;

            Assert.AreSame(expected, actual);
        }

        #endregion
    }
}
