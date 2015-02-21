using NUnit.Framework;
using System;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class UInt64FilterTests
    {
        #region Method: GetNumericValue()

        [Test]
        public void GetNumericValue_ParsesValue()
        {
            UInt64Filter filter = new UInt64Filter();
            filter.Value = "18446744073709551615";

            Object actual = filter.GetNumericValue();
            Object expected = 18446744073709551615;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetNumericValue_OnNotValidValueReturnsNull()
        {
            UInt64Filter filter = new UInt64Filter();
            filter.Value = "-1";

            Assert.IsNull(filter.GetNumericValue());
        }

        #endregion
    }
}
