﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace NonFactors.Mvc.Grid
{
    public class Grid<T> : IGrid<T> where T : class
    {
        public String Name { get; set; }
        public String EmptyText { get; set; }
        public String CssClasses { get; set; }
        public String Id { get; set; }

        public IQueryable<T> Source { get; set; }
        public NameValueCollection Query { get; set; }
        public HttpContextBase HttpContext { get; set; }
        public IList<IGridProcessor<T>> Processors { get; set; }

        IGridColumns<IGridColumn> IGrid.Columns => Columns;
        public IGridColumnsOf<T> Columns { get; set; }

        IGridRows<Object> IGrid.Rows => Rows;
        public IGridRowsOf<T> Rows { get; set; }

        IGridPager IGrid.Pager => Pager;
        public IGridPager<T> Pager { get; set; }

        public Grid(IEnumerable<T> source)
        {
            Processors = new List<IGridProcessor<T>>();
            Source = source.AsQueryable();

            Name = "Grid";

            Columns = new GridColumns<T>(this);
            Rows = new GridRows<T>(this);
        }
    }
}
