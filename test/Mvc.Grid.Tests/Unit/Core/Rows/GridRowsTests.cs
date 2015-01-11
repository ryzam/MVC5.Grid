using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class GridRowsTests
    {
        #region Constructor: GridRows(IGrid<T> grid)

        [Test]
        public void GridRows_SetsGrid()
        {
            IGrid<GridModel> expected = new Grid<GridModel>(new GridModel[0]);
            IGrid<GridModel> actual = new GridRows<GridModel>(expected).Grid;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Method: GetEnumerator()

        [Test]
        public void GetEnumerator_OnNullCurrentRowsProcessesRows()
        {
            IQueryable<GridModel> models = new[] { new GridModel(), new GridModel() }.AsQueryable();
            IGridProcessor<GridModel> postProcessor = Substitute.For<IGridProcessor<GridModel>>();
            IGridProcessor<GridModel> preProcessor = Substitute.For<IGridProcessor<GridModel>>();
            IQueryable<GridModel> postProcessedModels = new[] { new GridModel() }.AsQueryable();
            IQueryable<GridModel> preProcessedModels = new[] { new GridModel() }.AsQueryable();
            postProcessor.ProcessorType = GridProcessorType.Post;
            preProcessor.ProcessorType = GridProcessorType.Pre;
            Grid<GridModel> grid = new Grid<GridModel>(models);

            postProcessor.Process(preProcessedModels).Returns(postProcessedModels);
            preProcessor.Process(models).Returns(preProcessedModels);
            grid.Processors.Add(postProcessor);
            grid.Processors.Add(preProcessor);

            GridRows<GridModel> rows = new GridRows<GridModel>(grid);
            IEnumerable<IGridRow> currentRows = rows.CurrentRows;

            IEnumerable<Object> actual = rows.ToList().Select(row => row.Model);
            IEnumerable<Object> expected = postProcessedModels;

            postProcessor.Received().Process(preProcessedModels);
            CollectionAssert.AreEqual(expected, actual);
            preProcessor.Received().Process(models);
            Assert.IsNull(currentRows);
        }

        [Test]
        public void GetEnumerator_SetsRowCssClasses()
        {
            IQueryable<GridModel> models = new[] { new GridModel(), new GridModel() }.AsQueryable();
            Grid<GridModel> grid = new Grid<GridModel>(models);

            GridRows<GridModel> rows = new GridRows<GridModel>(grid);
            rows.CssClasses = (model) => "grid-row";

            Assert.IsTrue(rows.All(row => row.CssClasses == "grid-row"));
        }

        [Test]
        public void GetEnumerator_ReturnsCurrentRows()
        {
            IQueryable<GridModel> models = new[] { new GridModel(), new GridModel() }.AsQueryable();
            IGridProcessor<GridModel> preProcessor = Substitute.For<IGridProcessor<GridModel>>();
            preProcessor.Process(models).Returns(new GridModel[0].AsQueryable());
            preProcessor.ProcessorType = GridProcessorType.Pre;
            Grid<GridModel> grid = new Grid<GridModel>(models);

            GridRows<GridModel> rows = new GridRows<GridModel>(grid);
            rows.ToList();

            grid.Processors.Add(preProcessor);

            IEnumerable<Object> actual = rows.ToList().Select(row => row.Model);
            IEnumerable<Object> expected = models;

            preProcessor.DidNotReceive().Process(Arg.Any<IQueryable<GridModel>>());
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetEnumerator_GetsSameEnumerable()
        {
            GridModel[] models = { new GridModel(), new GridModel() };
            Grid<GridModel> grid = new Grid<GridModel>(models);

            GridRows<GridModel> rows = new GridRows<GridModel>(grid);

            IEnumerator actual = (rows as IEnumerable).GetEnumerator();
            IEnumerator expected = rows.GetEnumerator();

            while (expected.MoveNext() | actual.MoveNext())
                Assert.AreSame((expected.Current as IGridRow).Model, (actual.Current as IGridRow).Model);
        }

        #endregion
    }
}
