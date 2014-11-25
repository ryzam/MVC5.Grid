using NUnit.Framework;
using System;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class SingleFilterTests
    {
        #region Method: GetNumericValue()

        [Test]
        public void GetNumericValue_ParsesValue()
        {
            SingleFilter<GridModel> filter = new SingleFilter<GridModel>();
            filter.Value = "-3.40281540545454";

            Object actual = filter.GetNumericValue();
            Object expected = -3.40281534f;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetNumericValue_OnNotValidValueReturnsNull()
        {
            SingleFilter<GridModel> filter = new SingleFilter<GridModel>();
            filter.Value = "3.2f";

            Assert.IsNull(filter.GetNumericValue());
        }

        #endregion
    }
}
