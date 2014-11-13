using NSubstitute;
using NUnit.Framework;
using System;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class BaseGridColumnTests
    {
        private BaseGridColumn<GridModel, String> column;

        [SetUp]
        public void SetUp()
        {
            column = Substitute.For<BaseGridColumn<GridModel, String>>();
        }

        #region Method: As(Func<TModel, TValue> value)

        [Test]
        public void As_SetsValueFunction()
        {
            Func<GridModel, String> expected = (model) => model.Name;
            Func<GridModel, String> actual = column.As(expected).ValueFunction;

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void As_ReturnsSameColumn()
        {
            IGridColumn actual = column.As(model => model.Name);
            IGridColumn expected = column;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Method: Filterable(Boolean isFilterable)

        [Test]
        public void Filterable_SetsIsFilterable()
        {
            Boolean? actual = column.Filterable(true).IsFilterable;
            Boolean? expected = true;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Filterable_ReturnsSameGrid()
        {
            IGridColumn actual = column.Filterable(true);
            IGridColumn expected = column;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Method: Sortable(Boolean isSortable)

        [Test]
        public void Sortable_SetsIsSortable()
        {
            Boolean? actual = column.Sortable(true).IsSortable;
            Boolean? expected = true;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Sortable_ReturnsSameGrid()
        {
            IGridColumn actual = column.Sortable(true);
            IGridColumn expected = column;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Method: Encoded(Boolean isEncoded)

        [Test]
        public void Encoded_SetsIsEncoded()
        {
            Boolean actual = column.Encoded(true).IsEncoded;
            Boolean expected = true;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Encoded_ReturnsSameGrid()
        {
            IGridColumn actual = column.Encoded(true);
            IGridColumn expected = column;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Method: Formatted(String format)

        [Test]
        public void Formatted_SetsFormat()
        {
            String actual = column.Formatted("Format").Format;
            String expected = "Format";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Formatted_ReturnsSameGrid()
        {
            IGridColumn actual = column.Formatted("Format");
            IGridColumn expected = column;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Method: Css(String cssClasses)

        [Test]
        public void Css_SetsCssClasses()
        {
            String actual = column.Css("column-class").CssClasses;
            String expected = "column-class";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Css_ReturnsSameGrid()
        {
            IGridColumn actual = column.Css("column-class");
            IGridColumn expected = column;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Method: Titled(String title)

        [Test]
        public void Titled_SetsTitle()
        {
            String actual = column.Titled("Title").Title;
            String expected = "Title";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Titled_ReturnsSameGrid()
        {
            IGridColumn actual = column.Titled("Title");
            IGridColumn expected = column;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Method: Named(String name)

        [Test]
        public void Named_SetsName()
        {
            String actual = column.Named("Name").Name;
            String expected = "Name";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Named_ReturnsSameGrid()
        {
            IGridColumn actual = column.Named("Name");
            IGridColumn expected = column;

            Assert.AreSame(expected, actual);
        }

        #endregion
    }
}
