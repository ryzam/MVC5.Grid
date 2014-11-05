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
        #region Constructor: GridRows(IGrid<TModel> grid)

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
        public void GetEnumerator_ProcessesRows()
        {
            IQueryable<GridModel> models = new[] { new GridModel(), new GridModel() }.AsQueryable();
            IGridProcessor<GridModel> postProcessor = Substitute.For<IGridProcessor<GridModel>>();
            IGridProcessor<GridModel> preProcessor = Substitute.For<IGridProcessor<GridModel>>();
            GridModel[] postProcessedModels = { new GridModel() };
            GridModel[] preProcessedModels = { new GridModel() };
            Grid<GridModel> grid = new Grid<GridModel>(models);
            postProcessor.Type = GridProcessorType.Post;
            preProcessor.Type = GridProcessorType.Pre;

            postProcessor.Process(preProcessedModels).Returns(postProcessedModels);
            preProcessor.Process(models).Returns(preProcessedModels);
            grid.Processors.Add(postProcessor);
            grid.Processors.Add(preProcessor);

            GridRows<GridModel> rows = new GridRows<GridModel>(grid);

            IEnumerable<Object> actual = rows.ToList().Select(row => row.Model);
            IEnumerable<Object> expected = postProcessedModels;

            postProcessor.Received().Process(preProcessedModels);
            CollectionAssert.AreEqual(expected, actual);
            preProcessor.Received().Process(models);
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
