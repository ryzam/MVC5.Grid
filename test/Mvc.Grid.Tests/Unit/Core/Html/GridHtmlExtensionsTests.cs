using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
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

        #region Extension: AjaxGrid(this HtmlHelper, String dataSource)

        [Test]
        public void AjaxGrid_RendersAjaxGridPartial()
        {
            IView view = Substitute.For<IView>();
            IViewEngine engine = Substitute.For<IViewEngine>();
            ViewEngineResult result = Substitute.For<ViewEngineResult>(view, engine);
            engine.FindPartialView(Arg.Any<ControllerContext>(), "MvcGrid/_AjaxGrid", Arg.Any<Boolean>()).Returns(result);
            view.When(sub => sub.Render(Arg.Any<ViewContext>(), Arg.Any<TextWriter>())).Do(sub =>
            {
                Assert.AreEqual("DataSource", sub.Arg<ViewContext>().ViewData.Model);
                sub.Arg<TextWriter>().Write("Rendered");
            });

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(engine);

            String actual = html.AjaxGrid("DataSource").ToHtmlString();
            String expected = "Rendered";

            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
