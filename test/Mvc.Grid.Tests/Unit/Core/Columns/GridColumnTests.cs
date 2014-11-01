using NonFactors.Mvc.Grid.Tests.Objects;
using NUnit.Framework;
using System;
using System.Net;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class GridColumnTests
    {
        private IGridColumn<GridModel, Object> column;

        [SetUp]
        public void SetUp()
        {
            column = new GridColumn<GridModel, Object>();
        }

        #region Constructor: GridColumn()

        [Test]
        public void GridColumn_SetsExpressionToNull()
        {
            column = new GridColumn<GridModel, Object>();

            Assert.IsNull(column.Expression);
        }

        [Test]
        public void GridColumn_SetsCssClassesToNull()
        {
            column = new GridColumn<GridModel, Object>();

            Assert.IsNull(column.CssClasses);
        }

        [Test]
        public void GridColumn_SetsIsEncodedToTrue()
        {
            column = new GridColumn<GridModel, Object>();

            Assert.IsTrue(column.IsEncoded);
        }

        [Test]
        public void GridColumn_SetsFormatToNull()
        {
            column = new GridColumn<GridModel, Object>();

            Assert.IsNull(column.Format);
        }

        [Test]
        public void GridColumn_SetsTitleToNull()
        {
            column = new GridColumn<GridModel, Object>();

            Assert.IsNull(column.Title);
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
        public void GridColumn_Expression_SetsCssClassesToNull()
        {
            column = new GridColumn<GridModel, Object>(model => model.Name);

            Assert.IsNull(column.CssClasses);
        }

        [Test]
        public void GridColumn_Expression_SetsIsEncodedToTrue()
        {
            column = new GridColumn<GridModel, Object>(model => model.Name);

            Assert.IsTrue(column.IsEncoded);
        }

        [Test]
        public void GridColumn_Expression_SetsFormatToNull()
        {
            column = new GridColumn<GridModel, Object>(model => model.Name);

            Assert.IsNull(column.Format);
        }

        [Test]
        public void GridColumn_Expression_SetsTitleToNull()
        {
            column = new GridColumn<GridModel, Object>(model => model.Name);

            Assert.IsNull(column.Title);
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

        #region Method: Encoded(Boolean encode)

        [Test]
        public void Encoded_SetsIsEncoded()
        {
            Boolean actual = column.Encoded(false).IsEncoded;
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

        #region Method: ValueFor(IGridRow row)

        [Test]
        public void ValueFor_OnNullFormatReturnsEncodedValue()
        {
            IGridRow row = new GridRow(new GridModel { Name = "<script />" });
            column = new GridColumn<GridModel, Object>(model => model.Name);
            column.Formatted(null);
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
            column.Formatted(null);
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
