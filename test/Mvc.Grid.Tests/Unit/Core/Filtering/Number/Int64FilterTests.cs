using NUnit.Framework;
using System;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class Int64FilterTests
    {
        #region Method: GetNumericValue()

        [Test]
        public void GetNumericValue_ParsesValue()
        {
            Int64Filter<GridModel> filter = new Int64Filter<GridModel>();
            filter.Value = "-9223372036854775808";

            Object actual = filter.GetNumericValue();
            Object expected = -9223372036854775808;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetNumericValue_OnNotValidValueReturnsNull()
        {
            Int64Filter<GridModel> filter = new Int64Filter<GridModel>();
            filter.Value = "9223372036854775808";

            Assert.IsNull(filter.GetNumericValue());
        }

        #endregion
    }
}
