using NSubstitute;
using NUnit.Framework;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class BaseGridFilterTests
    {
        #region Constructor: BaseGridFilter()

        [Test]
        public void BaseGridFilter_SetsProcessorTypeAsPreProcessor()
        {
            GridProcessorType actual = Substitute.For<BaseGridFilter<GridModel>>().ProcessorType;
            GridProcessorType expected = GridProcessorType.Pre;

            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
