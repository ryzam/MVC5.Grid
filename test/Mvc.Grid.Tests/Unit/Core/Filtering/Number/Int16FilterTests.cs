using NUnit.Framework;
using System;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class Int16FilterTests
    {
        #region Method: GetNumericValue()

        [Test]
        public void GetNumericValue_ParsesValue()
        {
            Int16Filter filter = new Int16Filter();
            filter.Value = "-32768";

            Object actual = filter.GetNumericValue();
            Object expected = -32768;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetNumericValue_OnNotValidValueReturnsNull()
        {
            Int16Filter filter = new Int16Filter();
            filter.Value = "32768";

            Assert.IsNull(filter.GetNumericValue());
        }

        #endregion
    }
}
