using NonFactors.Mvc.Grid.Html;
using NonFactors.Mvc.Grid.Tests.Helpers;
using NonFactors.Mvc.Grid.Tests.Objects;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace NonFactors.Mvc.Grid.Tests.Unit.Html
{
    [TestFixture]
    public class GridHtmlExtensionsTests
    {
        #region Extension: Grid<TModel>(this HtmlHelper html, IEnumerable<TModel> source)

        [Test]
        public void Grid_CreatesHtmlGridWithHtml()
        {
            HtmlHelper expected = HtmlHelperFactory.CreateHtmlHelper();
            HtmlHelper actual = expected.Grid(new GridModel[0]).Html;

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void Grid_CreatesGridWithSource()
        {
            HtmlHelper html = HtmlHelperFactory.CreateHtmlHelper();
            IEnumerable<GridModel> source = new GridModel[0];

            IEnumerable<GridModel> actual = html.Grid(source).Grid.Source;
            IEnumerable<GridModel> expected = source;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Extension: Grid<TModel>(this HtmlHelper html, String partialViewName, IEnumerable<TModel> source)

        [Test]
        public void Grid_PartialViewName_CreatesHtmlGridWithHtml()
        {
            HtmlHelper expected = HtmlHelperFactory.CreateHtmlHelper();
            HtmlHelper actual = expected.Grid("_Partial", new GridModel[0]).Html;

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void Grid_PartialViewName_CreatesGridWithPartialViewName()
        {
            HtmlHelper html = HtmlHelperFactory.CreateHtmlHelper();

            String actual = html.Grid("_Partial", new GridModel[0]).PartialViewName;
            String expected = "_Partial";

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void Grid_PartialViewName_CreatesGridWithSource()
        {
            HtmlHelper html = HtmlHelperFactory.CreateHtmlHelper();
            IEnumerable<GridModel> source = new GridModel[0];

            IEnumerable<GridModel> actual = html.Grid("_Partial", source).Grid.Source;
            IEnumerable<GridModel> expected = source;

            Assert.AreSame(expected, actual);
        }

        #endregion
    }
}
