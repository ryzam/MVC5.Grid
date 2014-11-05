using NSubstitute;
using NUnit.Framework;
using System;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class BaseGridColumnTests
    {
        private BaseGridColumn column;

        [SetUp]
        public void SetUp()
        {
            column = Substitute.For<BaseGridColumn>();
        }

        #region Method: Sortable(Boolean isSortable)

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void Sortable_SetsIsSortable(Boolean isSortable)
        {
            Boolean? actual = column.Sortable(isSortable).IsSortable;
            Boolean? expected = isSortable;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void Sortable_ReturnsSameGrid(Boolean isSortable)
        {
            IGridColumn actual = column.Sortable(isSortable);
            IGridColumn expected = column;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Method: Encoded(Boolean isEncoded)

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void Encoded_SetsIsEncoded(Boolean isEncoded)
        {
            Boolean actual = column.Encoded(isEncoded).IsEncoded;
            Boolean expected = isEncoded;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void Encoded_ReturnsSameGrid(Boolean isEncoded)
        {
            IGridColumn actual = column.Encoded(isEncoded);
            IGridColumn expected = column;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Method: Formatted(String format)

        [Test]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("Format")]
        public void Formatted_SetsFormat(String format)
        {
            String actual = column.Formatted(format).Format;
            String expected = format;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("Format")]
        public void Formatted_ReturnsSameGrid(String format)
        {
            IGridColumn actual = column.Formatted(format);
            IGridColumn expected = column;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Method: Css(String cssClasses)

        [Test]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("column-class")]
        [TestCase("column-class text-column")]
        public void Css_SetsCssClasses(String cssClasses)
        {
            String actual = column.Css(cssClasses).CssClasses;
            String expected = cssClasses;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("column-class")]
        [TestCase("column-class text-column")]
        public void Css_ReturnsSameGrid(String cssClasses)
        {
            IGridColumn actual = column.Css(cssClasses);
            IGridColumn expected = column;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Method: Titled(String title)

        [Test]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("Title")]
        public void Titled_SetsTitle(String title)
        {
            String actual = column.Titled(title).Title;
            String expected = title;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("Title")]
        public void Titled_ReturnsSameGrid(String title)
        {
            IGridColumn actual = column.Titled(title);
            IGridColumn expected = column;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Method: Named(String name)

        [Test]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("Name")]
        public void Named_SetsName(String name)
        {
            String actual = column.Named(name).Name;
            String expected = name;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("Name")]
        public void Named_ReturnsSameGrid(String name)
        {
            IGridColumn actual = column.Named(name);
            IGridColumn expected = column;

            Assert.AreSame(expected, actual);
        }

        #endregion
    }
}
