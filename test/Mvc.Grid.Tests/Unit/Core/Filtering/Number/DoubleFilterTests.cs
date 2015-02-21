using NUnit.Framework;
using System;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class DoubleFilterTests
    {
        #region Method: GetNumericValue()

        [Test]
        public void GetNumericValue_ParsesValue()
        {
            DoubleFilter filter = new DoubleFilter();
            filter.Value = "-3.40281540545454";

            Object actual = filter.GetNumericValue();
            Object expected = -3.4028154054545401d;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetNumericValue_OnNotValidValueReturnsNull()
        {
            DoubleFilter filter = new DoubleFilter();
            filter.Value = "3.2f";

            Assert.IsNull(filter.GetNumericValue());
        }

        #endregion
    }
}
