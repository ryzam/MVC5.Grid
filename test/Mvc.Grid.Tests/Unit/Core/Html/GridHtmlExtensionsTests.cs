using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class GridHtmlExtensionsTests
    {
        private HtmlHelper html;

        [TestFixtureSetUp]
        public void SetUp()
        {
            html = HtmlHelperFactory.CreateHtmlHelper();
        }

        #region Extension: Grid<T>(this HtmlHelper html, IEnumerable<T> source)

        [Test]
        public void Grid_CreatesHtmlGridWithHtml()
        {
            HtmlHelper actual = html.Grid(new GridModel[0]).Html;
            HtmlHelper expected = html;

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void Grid_CreatesGridWithSource()
        {
            IEnumerable<GridModel> expected = new GridModel[0].AsQueryable();
            IEnumerable<GridModel> actual = html.Grid(expected).Grid.Source;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Extension: Grid<T>(this HtmlHelper html, String partialViewName, IEnumerable<T> source)

        [Test]
        public void Grid_PartialViewName_CreatesHtmlGridWithHtml()
        {
            HtmlHelper actual = html.Grid("_Partial", new GridModel[0]).Html;
            HtmlHelper expected = html;

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void Grid_PartialViewName_CreatesGridWithSource()
        {
            IEnumerable<GridModel> expected = new GridModel[0].AsQueryable();
            IEnumerable<GridModel> actual = html.Grid("_Partial", expected).Grid.Source;

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void Grid_PartialViewName_CreatesGridWithPartialViewName()
        {
            String actual = html.Grid("_Partial", new GridModel[0]).PartialViewName;
            String expected = "_Partial";

            Assert.AreSame(expected, actual);
        }

        #endregion
    }
}
