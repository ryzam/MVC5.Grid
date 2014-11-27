using NUnit.Framework;
using System;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class MvcGridTests
    {
        #region Static constructor: MvcGrid()

        [Test]
        public void MvcGrid_SetsFiltersToDefaultImplementation()
        {
            Type actual = MvcGrid.Filters.GetType();
            Type expected = typeof(GridFilters);

            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
