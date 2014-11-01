using NonFactors.Mvc.Grid.Tests.Helpers;
using NonFactors.Mvc.Grid.Tests.Objects;
using NSubstitute;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class HtmlGridTests
    {
        private RequestContext requestContext;
        private HtmlGrid<GridModel> htmlGrid;

        [SetUp]
        public void SetUp()
        {
            Grid<GridModel> grid = new Grid<GridModel>(new GridModel[10].AsQueryable());
            HtmlHelper html = HtmlHelperFactory.CreateHtmlHelper();
            requestContext = html.ViewContext.RequestContext;

            htmlGrid = new HtmlGrid<GridModel>(html, grid);
        }

        #region Constructor: HtmlGrid(HtmlHelper html, Grid<TModel> grid)

        [Test]
        public void HtmlGrid_SetsDefaultPartialViewName()
        {
            String actual = new HtmlGrid<GridModel>(null, null).PartialViewName;
            String expected = "MvcGrid/_Grid";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void HtmlGrid_SetsHtmlHelper()
        {
            HtmlHelper expected = new HtmlHelper(new ViewContext(), new ViewPage());
            HtmlHelper actual = new HtmlGrid<GridModel>(expected, null).Html;

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void HtmlGrid_SetsGrid()
        {
            Grid<GridModel> expected = new Grid<GridModel>(null);
            Grid<GridModel> actual = new HtmlGrid<GridModel>(null, expected).Grid;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Method: Build(Action<IGridColumns<TModel>> builder)

        [Test]
        public void Build_BuildsColumns()
        {
            Boolean builderCalled = false;

            htmlGrid.Build(columns =>
            {
                Assert.AreSame(htmlGrid.Grid.Columns, columns);
                builderCalled = true;
            });

            Assert.IsTrue(builderCalled);
        }

        [Test]
        public void Build_ReturnsSameGrid()
        {
            IHtmlGrid<GridModel> actual = htmlGrid.Build(columns => { });
            IHtmlGrid<GridModel> expected = htmlGrid;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Method: Empty(String text)

        [Test]
        public void Empty_SetsEmptyText()
        {
            htmlGrid.Empty("Text");

            String actual = htmlGrid.Grid.EmptyText;
            String expected = "Text";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Empty_ReturnsSameGrid()
        {
            IHtmlGrid<GridModel> actual = htmlGrid.Empty("Text");
            IHtmlGrid<GridModel> expected = htmlGrid;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Method: WithPager(Action<IGridPager> builder)

        [Test]
        public void WithPager_Builder_DoesNotChangeExistingPager()
        {
            IGridPager gridPager = new GridPager<GridModel>(requestContext, new GridModel[0]);
            htmlGrid.Grid.Pager = gridPager;

            htmlGrid.WithPager(pager => { });

            IGridPager actual = htmlGrid.Grid.Pager;
            IGridPager expected = gridPager;

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void WithPager_Builder_CreatesGridPager()
        {
            IGridPager previousPager = htmlGrid.Grid.Pager;

            htmlGrid.WithPager(pager => { });

            GridPager<GridModel> expected = new GridPager<GridModel>(requestContext, htmlGrid.Grid.Source);
            GridPager<GridModel> actual = htmlGrid.Grid.Pager as GridPager<GridModel>;

            Assert.AreEqual(expected.PartialViewName, actual.PartialViewName);
            Assert.AreEqual(expected.RequestContext, actual.RequestContext);
            Assert.AreEqual(expected.PagesToDisplay, actual.PagesToDisplay);
            Assert.AreEqual(expected.CurrentPage, actual.CurrentPage);
            Assert.AreEqual(expected.RowsPerPage, actual.RowsPerPage);
            Assert.AreEqual(expected.TotalRows, actual.TotalRows);
            Assert.IsNull(previousPager);
        }

        [Test]
        public void WithPager_Builder_BuildsPager()
        {
            Boolean builderCalled = false;

            htmlGrid.WithPager(pager =>
            {
                Assert.AreSame(htmlGrid.Grid.Pager, pager);
                builderCalled = true;
            });

            Assert.IsTrue(builderCalled);
        }

        [Test]
        public void WithPager_Builder_ReturnsSameGrid()
        {
            IGridPager gridPager = new GridPager<GridModel>(requestContext, new GridModel[0]);
            htmlGrid.Grid.Pager = gridPager;

            IHtmlGrid<GridModel> actual = htmlGrid.WithPager(pager => { });
            IHtmlGrid<GridModel> expected = htmlGrid;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Method: WithPager()

        [Test]
        public void WithPager_DoesNotChangeExistingPager()
        {
            IGridPager gridPager = new GridPager<GridModel>(requestContext, new GridModel[0]);
            htmlGrid.Grid.Pager = gridPager;

            htmlGrid.WithPager();

            IGridPager actual = htmlGrid.Grid.Pager;
            IGridPager expected = gridPager;

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void WithPager_CreatesGridPager()
        {
            IGridPager previousPager = htmlGrid.Grid.Pager;

            htmlGrid.WithPager();

            GridPager<GridModel> expected = new GridPager<GridModel>(requestContext, htmlGrid.Grid.Source);
            GridPager<GridModel> actual = htmlGrid.Grid.Pager as GridPager<GridModel>;

            Assert.AreEqual(expected.PartialViewName, actual.PartialViewName);
            Assert.AreEqual(expected.RequestContext, actual.RequestContext);
            Assert.AreEqual(expected.PagesToDisplay, actual.PagesToDisplay);
            Assert.AreEqual(expected.CurrentPage, actual.CurrentPage);
            Assert.AreEqual(expected.RowsPerPage, actual.RowsPerPage);
            Assert.AreEqual(expected.TotalRows, actual.TotalRows);
            Assert.IsNull(previousPager);
        }

        [Test]
        public void WithPager_ReturnsSameGrid()
        {
            IGridPager gridPager = new GridPager<GridModel>(requestContext, new GridModel[0]);
            htmlGrid.Grid.Pager = gridPager;

            IHtmlGrid<GridModel> actual = htmlGrid.WithPager();
            IHtmlGrid<GridModel> expected = htmlGrid;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Method: ToHtmlString()

        [Test]
        public void ToHtmlString_RendersPartialView()
        {
            IView view = Substitute.For<IView>();
            IViewEngine engine = Substitute.For<IViewEngine>();
            ViewEngineResult result = Substitute.For<ViewEngineResult>(view, engine);
            engine.FindPartialView(Arg.Any<ControllerContext>(), htmlGrid.PartialViewName, Arg.Any<Boolean>()).Returns(result);
            view.When(sub => sub.Render(Arg.Any<ViewContext>(), Arg.Any<TextWriter>())).Do(sub =>
            {
                Assert.AreEqual(htmlGrid.Grid, sub.Arg<ViewContext>().ViewData.Model);
                sub.Arg<TextWriter>().Write("Rendered");
            });

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(engine);

            String actual = htmlGrid.ToHtmlString();
            String expected = "Rendered";

            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
