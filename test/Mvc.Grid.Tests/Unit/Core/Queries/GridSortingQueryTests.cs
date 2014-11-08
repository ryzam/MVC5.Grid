using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Specialized;
using System.Web;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class GridSortingQueryTests
    {
        #region Constructor: GridSortingQuery(IGridQuery query, String columnName)

        [Test]
        [TestCase("", "Column", "Column=Asc", null)]
        [TestCase("", "Column", "MG-Sort-Column=", null)]
        [TestCase("", "Column", "MG-Sort-Column=Asc", GridSortOrder.Asc)]
        [TestCase("", "Column", "MG-Sort-Column=Desc", GridSortOrder.Desc)]

        [TestCase(null, "Column", "Column=Asc", null)]
        [TestCase(null, "Column", "MG-Sort-Column=", null)]
        [TestCase(null, "Column", "MG-Sort-Column=Asc", GridSortOrder.Asc)]
        [TestCase(null, "Column", "MG-Sort-Column=Desc", GridSortOrder.Desc)]

        [TestCase("Grid", "Column", "MG-Sort-Column=Asc", null)]
        [TestCase("Grid", "Column", "MG-Sort-Grid-Column=", null)]
        [TestCase("Grid", "Column", "MG-Sort-Grid-Column=Asc", GridSortOrder.Asc)]
        [TestCase("Grid", "Column", "MG-Sort-Grid-Column=Desc", GridSortOrder.Desc)]
        public void GridSortingQuery_SetsSortOrder(String gridName, String columnName, String query, GridSortOrder? expected)
        {
            IGridQuery gridQuery = Substitute.For<IGridQuery>();
            gridQuery.Query = HttpUtility.ParseQueryString(query);
            gridQuery.Grid.Name = gridName;

            GridSortOrder? actual = new GridSortingQuery(gridQuery, columnName).SortOrder;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GridSortingQuery_SetsColumnName()
        {
            IGridQuery gridQuery = Substitute.For<IGridQuery>();
            gridQuery.Query = new NameValueCollection();

            String actual = new GridSortingQuery(gridQuery, "ColumnName").ColumnName;
            String expected = "ColumnName";

            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
