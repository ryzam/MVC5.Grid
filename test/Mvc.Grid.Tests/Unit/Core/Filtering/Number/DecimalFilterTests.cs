using NUnit.Framework;
using System;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class DecimalFilterTests
    {
        #region Method: GetNumericValue()

        [Test]
        public void GetNumericValue_ParsesValue()
        {
            DecimalFilter<GridModel> filter = new DecimalFilter<GridModel>();
            filter.Value = "79228162514264337593543950335";

            Object expected = 79228162514264337593543950335M;
            Object actual = filter.GetNumericValue();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetNumericValue_OnNotValidValueReturnsNull()
        {
            DecimalFilter<GridModel> filter = new DecimalFilter<GridModel>();
            filter.Value = "79228162514264337593543950336";

            Assert.IsNull(filter.GetNumericValue());
        }

        #endregion
    }
}
