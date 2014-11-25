using NUnit.Framework;
using System;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class ByteFilterTests
    {
        #region Method: GetNumericValue()

        [Test]
        public void GetNumericValue_ParsesValue()
        {
            ByteFilter<GridModel> filter = new ByteFilter<GridModel>();
            filter.Value = "255";

            Object actual = filter.GetNumericValue();
            Object expected = 255;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetNumericValue_OnNotValidValueReturnsNull()
        {
            ByteFilter<GridModel> filter = new ByteFilter<GridModel>();
            filter.Value = "-1";

            Assert.IsNull(filter.GetNumericValue());
        }

        #endregion
    }
}
