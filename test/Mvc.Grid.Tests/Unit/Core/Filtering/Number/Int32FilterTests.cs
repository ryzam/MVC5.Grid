using NUnit.Framework;
using System;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class Int32FilterTests
    {
        #region Method: GetNumericValue()

        [Test]
        public void GetNumericValue_ParsesValue()
        {
            Int32Filter<GridModel> filter = new Int32Filter<GridModel>();
            filter.Value = "-2147483648";

            Object actual = filter.GetNumericValue();
            Object expected = -2147483648;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetNumericValue_OnNotValidValueReturnsNull()
        {
            Int32Filter<GridModel> filter = new Int32Filter<GridModel>();
            filter.Value = "1a";

            Assert.IsNull(filter.GetNumericValue());
        }

        #endregion
    }
}
