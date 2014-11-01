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
            grid.Columns.Add(model => model.Name);
            grid.Columns.Add(model => model.Sum);

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
            Grid<GridModel> actual = new HtmlGrid<GridModel>(null, htmlGrid.Grid).Grid;
            Grid<GridModel> expected = htmlGrid.Grid;

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
            IGridPager pager = new GridPager<GridModel>(requestContext, new GridModel[8]);
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
        public void WithPager_Builder_DoesNotAddNonGridProcessor()
        {
            Assume.That(() => htmlGrid.Grid.Processors.Count, Is.EqualTo(0));
            htmlGrid.Grid.Pager = Substitute.For<IGridPager>();

            htmlGrid.WithPager(pager => { });

            CollectionAssert.IsEmpty(htmlGrid.Grid.Processors);
        }

        [Test]
        public void WithPager_Builder_ReturnsSameGrid()
        {
            IGridPager pager = new GridPager<GridModel>(requestContext, new GridModel[8]);
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
            IGridPager pager = new GridPager<GridModel>(requestContext, new GridModel[8]);
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
        public void WithPager_AddsGridProcessor()
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
        public void WithPager_DoesNotAddNonGridProcessor()
        {
            Assume.That(() => htmlGrid.Grid.Processors.Count, Is.EqualTo(0));
            htmlGrid.Grid.Pager = Substitute.For<IGridPager>();

            htmlGrid.WithPager();

            CollectionAssert.IsEmpty(htmlGrid.Grid.Processors);
        }

        [Test]
        public void WithPager_ReturnsSameGrid()
        {
            IGridPager pager = new GridPager<GridModel>(requestContext, new GridModel[8]);
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
