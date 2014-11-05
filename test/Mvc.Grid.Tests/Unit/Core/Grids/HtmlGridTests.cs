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
            Grid<GridModel> grid = new Grid<GridModel>(new GridModel[8]);
            HtmlHelper html = HtmlHelperFactory.CreateHtmlHelper();
            requestContext = html.ViewContext.RequestContext;

            htmlGrid = new HtmlGrid<GridModel>(html, grid);
            grid.Columns.Add(model => model.Name);
            grid.Columns.Add(model => model.Sum);
        }

        #region Constructor: HtmlGrid(HtmlHelper html, Grid<TModel> grid)

        [Test]
        public void HtmlGrid_DoesNotChangeGridQuery()
        {
            IGrid<GridModel> grid = Substitute.For<IGrid<GridModel>>();
            grid.Query = Substitute.For<IGridQuery>();

            IGridQuery actual = new HtmlGrid<GridModel>(null, grid).Grid.Query;
            IGridQuery expected = grid.Query;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void HtmlGrid_OnNullGridQuerySetsGridQuery()
        {
            IGrid<GridModel> grid = Substitute.For<IGrid<GridModel>>();
            HtmlHelper html = HtmlHelperFactory.CreateHtmlHelper();
            grid.Query = null;

            GridQuery actual = new HtmlGrid<GridModel>(html, grid).Grid.Query as GridQuery;
            GridQuery expected = new GridQuery(grid, html.ViewContext.HttpContext);

            Assert.AreSame(expected.Query, actual.Query);
            Assert.AreSame(expected.Grid, actual.Grid);
        }

        [Test]
        public void HtmlGrid_SetsDefaultPartialViewName()
        {
            IGrid<GridModel> grid = Substitute.For<IGrid<GridModel>>();

            String actual = new HtmlGrid<GridModel>(null, grid).PartialViewName;
            String expected = "MvcGrid/_Grid";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void HtmlGrid_SetsHtmlHelper()
        {
            IGrid<GridModel> grid = Substitute.For<IGrid<GridModel>>();

            HtmlHelper expected = HtmlHelperFactory.CreateHtmlHelper();
            HtmlHelper actual = new HtmlGrid<GridModel>(expected, grid).Html;

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void HtmlGrid_SetsGrid()
        {
            IGrid<GridModel> expected = Substitute.For<IGrid<GridModel>>();
            IGrid<GridModel> actual = new HtmlGrid<GridModel>(null, expected).Grid;

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

        #region Method: Sortable(Boolean enabled)

        [Test]
        [TestCase(null, null, null)]
        [TestCase(null, false, false)]
        [TestCase(null, true, true)]
        [TestCase(false, null, false)]
        [TestCase(false, false, false)]
        [TestCase(false, true, false)]
        [TestCase(true, null, true)]
        [TestCase(true, false, true)]
        [TestCase(true, true, true)]
        public void Sortable_SetsSortable(Boolean? isColumnSortable, Boolean? isGridSortable, Boolean? expectedIsSortable)
        {
            foreach (IGridColumn column in htmlGrid.Grid.Columns)
                column.IsSortable = isColumnSortable;

            if (isGridSortable.HasValue)
                htmlGrid.Sortable(isGridSortable.Value);

            foreach (IGridColumn actual in htmlGrid.Grid.Columns)
                Assert.AreEqual(expectedIsSortable, actual.IsSortable);
        }

        [Test]
        public void Sortable_ReturnsSameGrid()
        {
            IHtmlGrid<GridModel> actual = htmlGrid.Sortable(true);
            IHtmlGrid<GridModel> expected = htmlGrid;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Method: Empty(String text)

        [Test]
        public void Empty_SetsEmptyText()
        {
            String actual = htmlGrid.Empty("Text").Grid.EmptyText;
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

        #region Method: Named(String name)

        [Test]
        public void Named_SetsName()
        {
            String actual = htmlGrid.Named("Name").Grid.Name;
            String expected = "Name";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Named_ReturnsSameGrid()
        {
            IHtmlGrid<GridModel> actual = htmlGrid.Named("Name");
            IHtmlGrid<GridModel> expected = htmlGrid;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Method: WithPager(Action<IGridPager> builder)

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
            IGridPager previousPager = htmlGrid.Grid.Pager;

            htmlGrid.WithPager(pager => { });

            GridPager<GridModel> expected = new GridPager<GridModel>(htmlGrid.Grid, requestContext);
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
        public void WithPager_Builder_AddsGridProcessor()
        {
            Assume.That(() => htmlGrid.Grid.Processors.Count, Is.EqualTo(0));

            htmlGrid.WithPager(pager => { });

            Object actual = htmlGrid.Grid.Processors.Single();
            Object expected = htmlGrid.Grid.Pager;

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void WithPager_Builder_DoesNotReaddGridProcessor()
        {
            Assume.That(() => htmlGrid.Grid.Processors.Count, Is.EqualTo(0));

            htmlGrid.WithPager(pager => { });
            htmlGrid.WithPager(pager => { });

            Object actual = htmlGrid.Grid.Processors.Single();
            Object expected = htmlGrid.Grid.Pager;

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void WithPager_Builder_ReturnsSameGrid()
        {
            IGridPager<GridModel> pager = new GridPager<GridModel>(htmlGrid.Grid, null);
            htmlGrid.Grid.Pager = pager;

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
            IGridPager previousPager = htmlGrid.Grid.Pager;

            htmlGrid.WithPager();

            GridPager<GridModel> expected = new GridPager<GridModel>(htmlGrid.Grid, requestContext);
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
        public void WithPager_AddsGridPagerProcessor()
        {
            Assume.That(() => htmlGrid.Grid.Processors.Count, Is.EqualTo(0));

            htmlGrid.WithPager();

            Object actual = htmlGrid.Grid.Processors.Single();
            Object expected = htmlGrid.Grid.Pager;

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void WithPager_DoesNotReaddGridProcessor()
        {
            Assume.That(() => htmlGrid.Grid.Processors.Count, Is.EqualTo(0));

            htmlGrid.WithPager();
            htmlGrid.WithPager();

            Object actual = htmlGrid.Grid.Processors.Single();
            Object expected = htmlGrid.Grid.Pager;

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void WithPager_ReturnsSameGrid()
        {
            IGridPager<GridModel> pager = new GridPager<GridModel>(htmlGrid.Grid, null);
            htmlGrid.Grid.Pager = pager;

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
