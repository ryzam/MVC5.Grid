using NonFactors.Mvc.Grid.Tests.Objects;
using NUnit.Framework;
using System;
using System.Net;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class GridColumnTests
    {
        private GridColumn<GridModel, Object> column;

        [SetUp]
        public void SetUp()
        {
            column = new GridColumn<GridModel, Object>();
        }

        #region Constructor: GridColumn()

        [Test]
        public void GridColumn_SetsExpressionToNull()
        {
            Func<GridModel, Object> actual = new GridColumn<GridModel, Object>().Expression;

            Assert.IsNull(actual);
        }

        [Test]
        public void GridColumn_SetsIsEncodedToTrue()
        {
            Boolean actual = new GridColumn<GridModel, Object>().IsEncoded;

            Assert.IsTrue(actual);
        }

        [Test]
        public void GridColumn_SetsCssClassesToNull()
        {
            String actual = new GridColumn<GridModel, Object>().CssClasses;

            Assert.IsNull(actual);
        }

        [Test]
        public void GridColumn_SetsFormatToNull()
        {
            String actual = new GridColumn<GridModel, Object>().CssClasses;

            Assert.IsNull(actual);
        }

        [Test]
        public void GridColumn_SetsTitleToNull()
        {
            String actual = new GridColumn<GridModel, Object>().Title;

            Assert.IsNull(actual);
        }

        [Test]
        public void GridColumn_SetsWidthToNull()
        {
            String actual = new GridColumn<GridModel, Object>().CssClasses;

            Assert.IsNull(actual);
        }

        #endregion

        #region Constructor: GridColumn(Func<TModel, TValue> expression)

        [Test]
        public void GridColumn_Expression_SetsExpression()
        {
            Func<GridModel, String> expression = (model) => model.Name;

            Func<GridModel, String> actual = new GridColumn<GridModel, String>(expression).Expression;
            Func<GridModel, String> expected = expression;

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void GridColumn_Expression_SetsIsEncodedToTrue()
        {
            Boolean actual = new GridColumn<GridModel, Object>().IsEncoded;

            Assert.IsTrue(actual);
        }

        [Test]
        public void GridColumn_Expression_SetsCssClassesToNull()
        {
            String actual = new GridColumn<GridModel, String>(model => model.Name).CssClasses;

            Assert.IsNull(actual);
        }

        [Test]
        public void GridColumn_Expression_SetsFormatToNull()
        {
            String actual = new GridColumn<GridModel, String>(model => model.Name).CssClasses;

            Assert.IsNull(actual);
        }

        [Test]
        public void GridColumn_Expression_SetsTitleToNull()
        {
            String actual = new GridColumn<GridModel, String>(model => model.Name).Title;

            Assert.IsNull(actual);
        }

        [Test]
        public void GridColumn_Expression_SetsWidthToNull()
        {
            String actual = new GridColumn<GridModel, String>(model => model.Name).CssClasses;

            Assert.IsNull(actual);
        }

        #endregion

        #region Method: Formatted(String format)

        [Test]
        public void Formatted_SetsFormat()
        {
            column.Formatted("Format");

            String actual = column.Format;
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

        #region Method: Encoded(Boolean encode)

        [Test]
        public void Encoded_SetsIsEncoded()
        {
            column.Encoded(false);

            Boolean actual = column.IsEncoded;
            Boolean expected = false;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Encoded_ReturnsSameGrid()
        {
            IGridColumn actual = column.Encoded(false);
            IGridColumn expected = column;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Method: Css(String cssClasses)

        [Test]
        public void Css_SetsCssClasses()
        {
            column.Css("column-class");

            String actual = column.CssClasses;
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
            column.Titled("Title");

            String actual = column.Title;
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

        #region Method: ValueFor(IGridRow row)

        [Test]
        public void ValueFor_OnNullFormatReturnsEncodedValue()
        {
            IGridRow row = new GridRow(new GridModel { Name = "<script />" });
            column = new GridColumn<GridModel, Object>(model => model.Name);
            column.Encoded(true);

            String expected = WebUtility.HtmlEncode("<script />");
            String actual = column.ValueFor(row).ToString();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ValueFor_ReturnsEncodedAndFormattedValue()
        {
            column = new GridColumn<GridModel, Object>(model => model.Sum);
            IGridRow row = new GridRow(new GridModel { Sum = 100 });
            column.Formatted("<script value='{0:C2}' />");
            column.Encoded(true);

            String expected = WebUtility.HtmlEncode(String.Format("<script value='{0:C2}' />", 100));
            String actual = column.ValueFor(row).ToString();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ValueFor_OnNullExpressionReturnsEmpty()
        {
            column = new GridColumn<GridModel, Object>(null);

            String actual = column.ValueFor(null).ToString();
            String expected = String.Empty;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ValueFor_OnNullValueReturnsEmpty()
        {
            column = new GridColumn<GridModel, Object>(model => model.Name);
            IGridRow row = new GridRow(new GridModel { Name = null });

            String actual = column.ValueFor(row).ToString();
            String expected = String.Empty;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ValueFor_OnNullFormatReturnsNotEncodedValue()
        {
            IGridRow row = new GridRow(new GridModel { Name = "<script />" });
            column = new GridColumn<GridModel, Object>(model => model.Name);
            column.Encoded(false);

            String actual = column.ValueFor(row).ToString();
            String expected = "<script />";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ValueFor_ReturnsNotEncodedButFormattedValue()
        {
            column = new GridColumn<GridModel, Object>(model => model.Sum);
            IGridRow row = new GridRow(new GridModel { Sum = 100 });
            column.Formatted("<script value='{0:C2}' />");
            column.Encoded(false);

            String expected = String.Format("<script value='{0:C2}' />", 100);
            String actual = column.ValueFor(row).ToString();

            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
