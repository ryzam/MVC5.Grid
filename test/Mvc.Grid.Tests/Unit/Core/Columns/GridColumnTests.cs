using NonFactors.Mvc.Grid.Tests.Objects;
using NUnit.Framework;
using System;

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

        #region Method: SetWidth(Int32 width)

        [Test]
        public void SetWidth_SetsWidth()
        {
            column.SetWidth(11);

            Int32 actual = column.Width;
            Int32 expected = 11;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SetWidth_ReturnsSameGrid()
        {
            IGridColumn actual = column.SetWidth(11);
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
        public void ValueFor_OnNullFormatReturnsNotFormattedValue()
        {
            column = new GridColumn<GridModel, Object>(model => model.Sum);
            IGridRow row = new GridRow(new GridModel { Sum = 100 });

            String actual = column.ValueFor(row).ToString();
            String expected = "100";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ValueFor_ReturnsFormattedValue()
        {
            column = new GridColumn<GridModel, Object>(model => model.Sum);
            IGridRow row = new GridRow(new GridModel { Sum = 100 });
            column.Formatted("{0:C2}");

            String actual = column.ValueFor(row).ToString();
            String expected = String.Format("{0:C2}", 100);

            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
