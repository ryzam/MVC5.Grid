using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
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
        private IGrid<GridModel> grid;

        [SetUp]
        public void SetUp()
        {
            HtmlHelper html = HtmlHelperFactory.CreateHtmlHelper();
            requestContext = html.ViewContext.RequestContext;
            grid = new Grid<GridModel>(new GridModel[8]);

            htmlGrid = new HtmlGrid<GridModel>(html, grid);
            grid.Columns.Add(model => model.Name);
            grid.Columns.Add(model => model.Sum);
        }

        #region Constructor: HtmlGrid(HtmlHelper html, IGrid<TModel> grid)

        [Test]
        public void HtmlGrid_DoesNotChangeExistingQuery()
        {
            grid.Query = Substitute.For<IGridQuery>();

            IGridQuery actual = new HtmlGrid<GridModel>(null, grid).Grid.Query;
            IGridQuery expected = grid.Query;

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void HtmlGrid_SetsGridQuery()
        {
            grid.Query = null;

            GridQuery actual = new HtmlGrid<GridModel>(htmlGrid.Html, grid).Grid.Query as GridQuery;
            GridQuery expected = new GridQuery(grid, htmlGrid.Html.ViewContext.HttpContext);

            Assert.AreSame(expected.Query, actual.Query);
            Assert.AreSame(expected.Grid, actual.Grid);
        }

        [Test]
        public void HtmlGrid_SetsDefaultPartialViewName()
        {
            String actual = new HtmlGrid<GridModel>(null, grid).PartialViewName;
            String expected = "MvcGrid/_Grid";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void HtmlGrid_SetsHtml()
        {
            HtmlHelper expected = HtmlHelperFactory.CreateHtmlHelper();
            HtmlHelper actual = new HtmlGrid<GridModel>(expected, grid).Html;

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void HtmlGrid_SetsGrid()
        {
            IGrid<GridModel> actual = new HtmlGrid<GridModel>(null, grid).Grid;
            IGrid<GridModel> expected = grid;

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

        #region Method: Sortable(Boolean isSortable)

        [Test]
        [TestCase(null, false, false)]
        [TestCase(null, true, true)]
        [TestCase(false, false, false)]
        [TestCase(false, true, false)]
        [TestCase(true, false, true)]
        [TestCase(true, true, true)]
        public void Sortable_SetsSortable(Boolean? isColumnSortable, Boolean isGridSortable, Boolean? expectedIsSortable)
        {
            foreach (IGridColumn column in htmlGrid.Grid.Columns)
                column.IsSortable = isColumnSortable;

            htmlGrid.Sortable(isGridSortable);

            foreach (IGridColumn actual in htmlGrid.Grid.Columns)
                Assert.AreEqual(expectedIsSortable, actual.IsSortable);
        }

        [Test]
        [TestCase(null, false)]
        [TestCase(null, true)]
        [TestCase(false, false)]
        [TestCase(false, true)]
        [TestCase(true, false)]
        [TestCase(true, true)]
        public void Sortable_ReturnsSameGrid(Boolean? isColumnSortable, Boolean isGridSortable)
        {
            foreach (IGridColumn column in htmlGrid.Grid.Columns)
                column.IsSortable = isColumnSortable;

            IHtmlGrid<GridModel> actual = htmlGrid.Sortable(isGridSortable);
            IHtmlGrid<GridModel> expected = htmlGrid;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Method: Css(String cssClasses)

        [Test]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("table")]
        public void Css_SetsCssClasses(String css)
        {
            String actual = htmlGrid.Css(css).Grid.CssClasses;
            String expected = css;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("table")]
        public void Css_ReturnsSameGrid(String css)
        {
            IHtmlGrid<GridModel> actual = htmlGrid.Css(css);
            IHtmlGrid<GridModel> expected = htmlGrid;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Method: Empty(String text)

        [Test]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("Text")]
        public void Empty_SetsEmptyText(String text)
        {
            String actual = htmlGrid.Empty(text).Grid.EmptyText;
            String expected = text;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("Text")]
        public void Empty_ReturnsSameGrid(String text)
        {
            IHtmlGrid<GridModel> actual = htmlGrid.Empty(text);
            IHtmlGrid<GridModel> expected = htmlGrid;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Method: Named(String name)

        [Test]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("Name")]
        public void Named_SetsName(String name)
        {
            String actual = htmlGrid.Named(name).Grid.Name;
            String expected = name;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("Name")]
        public void Named_ReturnsSameGrid(String name)
        {
            IHtmlGrid<GridModel> actual = htmlGrid.Named(name);
            IHtmlGrid<GridModel> expected = htmlGrid;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Method: WithPager(Action<IGridPager<TModel>> builder)

        [Test]
        public void WithPager_Builder_DoesNotChangeExistingPager()
        {
            IGridPager<GridModel> pager = new GridPager<GridModel>(htmlGrid.Grid, requestContext);
            htmlGrid.Grid.Pager = pager;

            htmlGrid.WithPager(gridPager => { });

            IGridPager actual = htmlGrid.Grid.Pager;
            IGridPager expected = pager;

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void WithPager_Builder_CreatesGridPager()
        {
            htmlGrid.Grid.Pager = null;

            htmlGrid.WithPager(pager => { });

            GridPager<GridModel> expected = new GridPager<GridModel>(htmlGrid.Grid, requestContext);
            GridPager<GridModel> actual = htmlGrid.Grid.Pager as GridPager<GridModel>;

            Assert.AreEqual(expected.PartialViewName, actual.PartialViewName);
            Assert.AreEqual(expected.PagesToDisplay, actual.PagesToDisplay);
            Assert.AreSame(expected.RequestContext, actual.RequestContext);
            Assert.AreEqual(expected.StartingPage, actual.StartingPage);
            Assert.AreEqual(expected.CurrentPage, actual.CurrentPage);
            Assert.AreEqual(expected.RowsPerPage, actual.RowsPerPage);
            Assert.AreEqual(expected.TotalPages, actual.TotalPages);
            Assert.AreEqual(expected.TotalRows, actual.TotalRows);
            Assert.AreEqual(expected.Type, actual.Type);
            Assert.AreSame(expected.Grid, actual.Grid);
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
        public void WithPager_Builder_AddsGridProcessor()
        {
            htmlGrid.Grid.Processors = new List<IGridProcessor<GridModel>>();

            htmlGrid.WithPager(pager => { });

            Object actual = htmlGrid.Grid.Processors.Single();
            Object expected = htmlGrid.Grid.Pager;

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void WithPager_Builder_DoesNotReaddGridProcessor()
        {
            htmlGrid.Grid.Processors = new List<IGridProcessor<GridModel>>();

            htmlGrid.WithPager(pager => { });
            htmlGrid.WithPager(pager => { });

            Object actual = htmlGrid.Grid.Processors.Single();
            Object expected = htmlGrid.Grid.Pager;

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void WithPager_Builder_ReturnsSameGrid()
        {
            IHtmlGrid<GridModel> actual = htmlGrid.WithPager(gridPager => { });
            IHtmlGrid<GridModel> expected = htmlGrid;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Method: WithPager()

        [Test]
        public void WithPager_DoesNotChangeExistingPager()
        {
            IGridPager<GridModel> pager = new GridPager<GridModel>(htmlGrid.Grid, null);
            htmlGrid.Grid.Pager = pager;

            htmlGrid.WithPager();

            IGridPager actual = htmlGrid.Grid.Pager;
            IGridPager expected = pager;

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void WithPager_CreatesGridPager()
        {
            htmlGrid.Grid.Pager = null;

            htmlGrid.WithPager();

            GridPager<GridModel> expected = new GridPager<GridModel>(htmlGrid.Grid, requestContext);
            GridPager<GridModel> actual = htmlGrid.Grid.Pager as GridPager<GridModel>;

            Assert.AreEqual(expected.PartialViewName, actual.PartialViewName);
            Assert.AreEqual(expected.PagesToDisplay, actual.PagesToDisplay);
            Assert.AreSame(expected.RequestContext, actual.RequestContext);
            Assert.AreEqual(expected.StartingPage, actual.StartingPage);
            Assert.AreEqual(expected.CurrentPage, actual.CurrentPage);
            Assert.AreEqual(expected.RowsPerPage, actual.RowsPerPage);
            Assert.AreEqual(expected.TotalPages, actual.TotalPages);
            Assert.AreEqual(expected.TotalRows, actual.TotalRows);
            Assert.AreEqual(expected.Type, actual.Type);
            Assert.AreSame(expected.Grid, actual.Grid);
        }

        [Test]
        public void WithPager_AddsGridPagerProcessor()
        {
            htmlGrid.Grid.Processors = new List<IGridProcessor<GridModel>>();

            htmlGrid.WithPager();

            Object actual = htmlGrid.Grid.Processors.Single();
            Object expected = htmlGrid.Grid.Pager;

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void WithPager_DoesNotReaddGridProcessor()
        {
            htmlGrid.Grid.Processors = new List<IGridProcessor<GridModel>>();

            htmlGrid.WithPager();
            htmlGrid.WithPager();

            Object actual = htmlGrid.Grid.Processors.Single();
            Object expected = htmlGrid.Grid.Pager;

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void WithPager_ReturnsSameGrid()
        {
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
