using NSubstitute;
using NUnit.Framework;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class BaseGridFilterTests
    {
        #region Constructor: BaseGridFilter()

        [Test]
        public void BaseGridFilter_SetsTypeAsPreProcessor()
        {
            GridProcessorType actual = Substitute.For<BaseGridFilter<GridModel>>().Type;
            GridProcessorType expected = GridProcessorType.Pre;

            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
