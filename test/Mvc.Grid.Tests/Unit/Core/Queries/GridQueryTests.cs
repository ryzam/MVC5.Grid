using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Specialized;
using System.Web;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class GridQueryTests
    {
        #region Constructor: GridQuery(IGrid grid, NameValueCollection query)

        [Test]
        public void GridQuery_CreateQueryFromCollection()
        {
            NameValueCollection colection = new NameValueCollection();
            colection["Keys"] = "Values";
            colection["Key"] = "Value";

            NameValueCollection actual = new GridQuery(null, colection);
            NameValueCollection expected = colection;

            foreach (String key in expected)
                Assert.AreEqual(expected[key], actual[key]);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GridQuery_SetsGrid()
        {
            IGrid grid = Substitute.For<IGrid>();

            IGrid actual = new GridQuery(grid, new NameValueCollection()).Grid;
            IGrid expected = grid;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Method: GetSortingQuery(String columnName)

        [Test]
        public void GetSortingQuery_GetsGridSortingQuery()
        {
            GridQuery gridQuery = new GridQuery(Substitute.For<IGrid>(), HttpUtility.ParseQueryString("Sort-Grid-Column=Asc"));

            GridSortingQuery actual = gridQuery.GetSortingQuery("Column") as GridSortingQuery;
            GridSortingQuery expected = new GridSortingQuery(gridQuery, "Column");

            Assert.AreEqual(expected.ColumnName, actual.ColumnName);
            Assert.AreEqual(expected.SortOrder, actual.SortOrder);
        }

        #endregion

        #region Method: ToString()

        [Test]
        public void ToString_FormsUrlEncodedQuery()
        {
            GridQuery query = new GridQuery(null, HttpUtility.ParseQueryString("a=b%26%3d&c=%20&a=c&d="));

            String expected = "?a=b%26%3d&a=c&c=+&d=";
            String actual = query.ToString();

            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
