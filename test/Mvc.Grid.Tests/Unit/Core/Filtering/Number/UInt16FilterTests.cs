using NUnit.Framework;
using System;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class UInt16FilterTests
    {
        #region Method: GetNumericValue()

        [Test]
        public void GetNumericValue_ParsesValue()
        {
            UInt16Filter<GridModel> filter = new UInt16Filter<GridModel>();
            filter.Value = "65535";

            Object actual = filter.GetNumericValue();
            Object expected = 65535;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetNumericValue_OnNotValidValueReturnsNull()
        {
            UInt16Filter<GridModel> filter = new UInt16Filter<GridModel>();
            filter.Value = "-1";

            Assert.IsNull(filter.GetNumericValue());
        }

        #endregion
    }
}
