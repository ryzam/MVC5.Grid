using NUnit.Framework;
using System;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class UInt32FilterTests
    {
        #region Method: GetNumericValue()

        [Test]
        public void GetNumericValue_ParsesValue()
        {
            UInt32Filter<GridModel> filter = new UInt32Filter<GridModel>();
            filter.Value = "4294967200";

            Object actual = filter.GetNumericValue();
            Object expected = 4294967200;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetNumericValue_OnNotValidValueReturnsNull()
        {
            UInt32Filter<GridModel> filter = new UInt32Filter<GridModel>();
            filter.Value = "-1";

            Assert.IsNull(filter.GetNumericValue());
        }

        #endregion
    }
}
