using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class HtmlGridTests
    {
        private HtmlGrid<GridModel> htmlGrid;
        private IGrid<GridModel> grid;

        [SetUp]
        public void SetUp()
        {
            HtmlHelper html = HtmlHelperFactory.CreateHtmlHelper();
            grid = new Grid<GridModel>(new GridModel[8]);

            htmlGrid = new HtmlGrid<GridModel>(html, grid);
            grid.Columns.Add(model => model.Name);
            grid.Columns.Add(model => model.Sum);
        }

        #region Constructor: HtmlGrid(HtmlHelper html, IGrid<TModel> grid)

        [Test]
        public void HtmlGrid_DoesNotChangeExistingQuery()
        {
            grid.Query = new GridQuery();

            GridQuery actual = new HtmlGrid<GridModel>(null, grid).Grid.Query;
            GridQuery expected = grid.Query;

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void HtmlGrid_SetsGridQuery()
        {
            HtmlHelper html = HtmlHelperFactory.CreateHtmlHelper("id=3&name=jim");
            grid.Query = null;

            GridQuery expected = new GridQuery(HttpUtility.ParseQueryString("id=3&name=jim"));
            GridQuery actual = new HtmlGrid<GridModel>(html, grid).Grid.Query;

            foreach (String key in expected)
                Assert.AreEqual(expected[key], actual[key]);

            CollectionAssert.AreEqual(expected, actual);
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
            IGridColumns expected = htmlGrid.Grid.Columns;
            Boolean builderCalled = false;

            htmlGrid.Build(actual =>
            {
                Assert.AreSame(expected, actual);
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

        #region Method: ProcessWith(IGridProcessor<TModel> processor)

        [Test]
        public void ProcessWith_AddsProcessorToGrid()
        {
            IGridProcessor<GridModel> processor = Substitute.For<IGridProcessor<GridModel>>();
            htmlGrid.Grid.Processors.Clear();
            htmlGrid.ProcessWith(processor);

            IGridProcessor<GridModel> actual = htmlGrid.Grid.Processors.Single();
            IGridProcessor<GridModel> expected = processor;

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void ProcessWith_ReturnsSameGrid()
        {
            IHtmlGrid<GridModel> actual = htmlGrid.ProcessWith(null);
            IHtmlGrid<GridModel> expected = htmlGrid;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Method: Filterable(Boolean isFilterable)

        [Test]
        [TestCase(null, false, false)]
        [TestCase(null, true, true)]
        [TestCase(false, false, false)]
        [TestCase(false, true, false)]
        [TestCase(true, false, true)]
        [TestCase(true, true, true)]
        public void Filterable_Set_SetsIsFilterable(Boolean? isColumnFilterable, Boolean isGridFilterable, Boolean? expectedIsFilterable)
        {
            foreach (IGridColumn column in htmlGrid.Grid.Columns)
                column.IsFilterable = isColumnFilterable;

            htmlGrid.Filterable(isGridFilterable);

            foreach (IGridColumn actual in htmlGrid.Grid.Columns)
                Assert.AreEqual(expectedIsFilterable, actual.IsFilterable);
        }

        [Test]
        public void Filterable_Set_ReturnsSameGrid()
        {
            IHtmlGrid<GridModel> actual = htmlGrid.Filterable(true);
            IHtmlGrid<GridModel> expected = htmlGrid;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Method: Filterable()

        [Test]
        [TestCase(null, true)]
        [TestCase(false, false)]
        [TestCase(true, true)]
        public void Filterable_SetsIsFilterableToTrue(Boolean? isColumnFilterable, Boolean? expectedIsFilterable)
        {
            foreach (IGridColumn column in htmlGrid.Grid.Columns)
                column.IsFilterable = isColumnFilterable;

            htmlGrid.Filterable();

            foreach (IGridColumn actual in htmlGrid.Grid.Columns)
                Assert.AreEqual(expectedIsFilterable, actual.IsFilterable);
        }

        [Test]
        public void Filterable_ReturnsSameGrid()
        {
            IHtmlGrid<GridModel> actual = htmlGrid.Filterable();
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
        public void Sortable_Set_SetsIsSortable(Boolean? isColumnSortable, Boolean isGridSortable, Boolean? expectedIsSortable)
        {
            foreach (IGridColumn column in htmlGrid.Grid.Columns)
                column.IsSortable = isColumnSortable;

            htmlGrid.Sortable(isGridSortable);

            foreach (IGridColumn actual in htmlGrid.Grid.Columns)
                Assert.AreEqual(expectedIsSortable, actual.IsSortable);
        }

        [Test]
        public void Sortable_Set_ReturnsSameGrid()
        {
            IHtmlGrid<GridModel> actual = htmlGrid.Sortable(true);
            IHtmlGrid<GridModel> expected = htmlGrid;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Method: Sortable()

        [Test]
        [TestCase(null, true)]
        [TestCase(false, false)]
        [TestCase(true, true)]
        public void Sortable_SetsIsSortableToTrue(Boolean? isColumnSortable, Boolean? expectedIsSortable)
        {
            foreach (IGridColumn column in htmlGrid.Grid.Columns)
                column.IsSortable = isColumnSortable;

            htmlGrid.Sortable();

            foreach (IGridColumn actual in htmlGrid.Grid.Columns)
                Assert.AreEqual(expectedIsSortable, actual.IsSortable);
        }

        [Test]
        public void Sortable_ReturnsSameGrid()
        {
            IHtmlGrid<GridModel> actual = htmlGrid.Sortable();
            IHtmlGrid<GridModel> expected = htmlGrid;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Method: RowCss(Func<TModel, String> cssClasses)

        [Test]
        public void RowCss_SetsRowsCssClasses()
        {
            Func<GridModel, String> expected = (model) => "";
            Func<GridModel, String> actual = htmlGrid.RowCss(expected).Grid.Rows.CssClasses;

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void RowCss_ReturnsSameGrid()
        {
            IHtmlGrid<GridModel> actual = htmlGrid.RowCss(null);
            IHtmlGrid<GridModel> expected = htmlGrid;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Method: Css(String cssClasses)

        [Test]
        public void Css_SetsCssClasses()
        {
            String actual = htmlGrid.Css("table").Grid.CssClasses;
            String expected = "table";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Css_ReturnsSameGrid()
        {
            IHtmlGrid<GridModel> actual = htmlGrid.Css("table");
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

        #region Method: Pageable(Action<IGridPager<TModel>> builder)

        [Test]
        public void Pageable_Builder_DoesNotChangeExistingPager()
        {
            IGridPager<GridModel> pager = new GridPager<GridModel>(htmlGrid.Grid);
            htmlGrid.Grid.Pager = pager;

            htmlGrid.Pageable(gridPager => { });

            IGridPager actual = htmlGrid.Grid.Pager;
            IGridPager expected = pager;

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void Pageable_Builder_CreatesGridPager()
        {
            htmlGrid.Grid.Pager = null;

            htmlGrid.Pageable(pager => { });

            IGridPager<GridModel> expected = new GridPager<GridModel>(htmlGrid.Grid);
            IGridPager<GridModel> actual = htmlGrid.Grid.Pager;

            Assert.AreEqual(expected.PartialViewName, actual.PartialViewName);
            Assert.AreEqual(expected.PagesToDisplay, actual.PagesToDisplay);
            Assert.AreEqual(expected.ProcessorType, actual.ProcessorType);
            Assert.AreEqual(expected.StartingPage, actual.StartingPage);
            Assert.AreEqual(expected.CurrentPage, actual.CurrentPage);
            Assert.AreEqual(expected.RowsPerPage, actual.RowsPerPage);
            Assert.AreEqual(expected.TotalPages, actual.TotalPages);
            Assert.AreEqual(expected.TotalRows, actual.TotalRows);
            Assert.AreSame(expected.Grid, actual.Grid);
        }

        [Test]
        public void Pageable_Builder_BuildsPager()
        {
            htmlGrid.Grid.Pager = Substitute.For<IGridPager<GridModel>>();
            IGridPager expected = htmlGrid.Grid.Pager;
            Boolean builderCalled = false;

            htmlGrid.Pageable(actual =>
            {
                Assert.AreSame(expected, actual);
                builderCalled = true;
            });

            Assert.IsTrue(builderCalled);
        }

        [Test]
        public void Pageable_Builder_AddsGridProcessor()
        {
            htmlGrid.Grid.Processors = new List<IGridProcessor<GridModel>>();

            htmlGrid.Pageable(pager => { });

            Object actual = htmlGrid.Grid.Processors.Single();
            Object expected = htmlGrid.Grid.Pager;

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void Pageable_Builder_DoesNotReaddGridProcessor()
        {
            htmlGrid.Grid.Processors = new List<IGridProcessor<GridModel>>();

            htmlGrid.Pageable(pager => { });
            htmlGrid.Pageable(pager => { });

            Object actual = htmlGrid.Grid.Processors.Single();
            Object expected = htmlGrid.Grid.Pager;

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void Pageable_Builder_ReturnsSameGrid()
        {
            IHtmlGrid<GridModel> actual = htmlGrid.Pageable(gridPager => { });
            IHtmlGrid<GridModel> expected = htmlGrid;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Method: Pageable()

        [Test]
        public void Pageable_DoesNotChangeExistingPager()
        {
            IGridPager<GridModel> pager = new GridPager<GridModel>(htmlGrid.Grid);
            htmlGrid.Grid.Pager = pager;

            htmlGrid.Pageable();

            IGridPager actual = htmlGrid.Grid.Pager;
            IGridPager expected = pager;

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void Pageable_CreatesGridPager()
        {
            htmlGrid.Grid.Pager = null;

            htmlGrid.Pageable();

            IGridPager<GridModel> expected = new GridPager<GridModel>(htmlGrid.Grid);
            IGridPager<GridModel> actual = htmlGrid.Grid.Pager;

            Assert.AreEqual(expected.PartialViewName, actual.PartialViewName);
            Assert.AreEqual(expected.PagesToDisplay, actual.PagesToDisplay);
            Assert.AreEqual(expected.ProcessorType, actual.ProcessorType);
            Assert.AreEqual(expected.StartingPage, actual.StartingPage);
            Assert.AreEqual(expected.CurrentPage, actual.CurrentPage);
            Assert.AreEqual(expected.RowsPerPage, actual.RowsPerPage);
            Assert.AreEqual(expected.TotalPages, actual.TotalPages);
            Assert.AreEqual(expected.TotalRows, actual.TotalRows);
            Assert.AreSame(expected.Grid, actual.Grid);
        }

        [Test]
        public void Pageable_AddsGridPagerProcessor()
        {
            htmlGrid.Grid.Processors = new List<IGridProcessor<GridModel>>();

            htmlGrid.Pageable();

            Object actual = htmlGrid.Grid.Processors.Single();
            Object expected = htmlGrid.Grid.Pager;

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void Pageable_DoesNotReaddGridProcessor()
        {
            htmlGrid.Grid.Processors = new List<IGridProcessor<GridModel>>();

            htmlGrid.Pageable();
            htmlGrid.Pageable();

            Object actual = htmlGrid.Grid.Processors.Single();
            Object expected = htmlGrid.Grid.Pager;

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void Pageable_ReturnsSameGrid()
        {
            IHtmlGrid<GridModel> actual = htmlGrid.Pageable();
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
