using NUnit.Framework;
using System;
using System.Collections.Specialized;
using System.Web;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class GridQueryTests
    {
        #region Constructor: GridQuery()

        [Test]
        public void GridQuery_CreateEmptyQuery()
        {
            CollectionAssert.IsEmpty(new GridQuery());
        }

        #endregion

        #region Constructor: GridQuery(NameValueCollection query)

        [Test]
        public void GridQuery_CreateQueryFromCollection()
        {
            NameValueCollection expected = HttpUtility.ParseQueryString("K=N&B=S");
            NameValueCollection actual = new GridQuery(expected);

            foreach (String key in expected)
                Assert.AreEqual(expected[key], actual[key]);

            CollectionAssert.AreEqual(expected, actual);
        }

        #endregion

        #region Method: ToString()

        [Test]
        public void ToString_FormsUrlEncodedQuery()
        {
            GridQuery query = new GridQuery(HttpUtility.ParseQueryString("a=b%26%3d&c=%20&a=c&d="));

            String expected = "?a=b%26%3d&a=c&c=+&d=";
            String actual = query.ToString();

            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
