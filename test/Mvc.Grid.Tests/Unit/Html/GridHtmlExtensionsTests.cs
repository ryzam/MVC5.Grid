using NonFactors.Mvc.Grid.Html;
using NonFactors.Mvc.Grid.Tests.Objects;
using NUnit.Framework;
using System;
using System.Linq;
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
            HtmlHelper expected = new HtmlHelper(new ViewContext(), new ViewPage());
            HtmlHelper actual = expected.Grid(new GridModel[0]).Html;

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void Grid_CreatesGridWithSource()
        {
            HtmlHelper html = new HtmlHelper(new ViewContext(), new ViewPage());
            IQueryable<GridModel> source = new GridModel[0].AsQueryable();

            IQueryable<GridModel> actual = html.Grid(source).Grid.Source;
            IQueryable<GridModel> expected = source;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Extension: Grid<TModel>(this HtmlHelper html, String partialViewName, IEnumerable<TModel> source)

        [Test]
        public void Grid_PartialViewName_CreatesHtmlGridWithHtml()
        {
            HtmlHelper expected = new HtmlHelper(new ViewContext(), new ViewPage());
            HtmlHelper actual = expected.Grid("_Partial", new GridModel[0]).Html;

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void Grid_PartialViewName_CreatesGridWithPartialViewName()
        {
            HtmlHelper html = new HtmlHelper(new ViewContext(), new ViewPage());

            String actual = html.Grid("_Partial", new GridModel[0]).PartialViewName;
            String expected = "_Partial";

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void Grid_PartialViewName_CreatesGridWithSource()
        {
            HtmlHelper html = new HtmlHelper(new ViewContext(), new ViewPage());
            IQueryable<GridModel> source = new GridModel[0].AsQueryable();

            IQueryable<GridModel> actual = html.Grid("_Partial", source).Grid.Source;
            IQueryable<GridModel> expected = source;

            Assert.AreSame(expected, actual);
        }

        #endregion
    }
}
