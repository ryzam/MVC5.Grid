using NUnit.Framework;
using System;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class GridRowTests
    {
        #region Constructor: GridRow(Object model)

        [Test]
        public void GridRow_SetsModel()
        {
            Object expected = new Object();
            Object actual = new GridRow(expected).Model;

            Assert.AreSame(expected, actual);
        }

        #endregion
    }
}
