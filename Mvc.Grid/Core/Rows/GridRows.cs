using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NonFactors.Mvc.Grid
{
    public class GridRows<TModel> : IGridRows<TModel> where TModel : class
    {
        private IEnumerable<TModel> source;
        private IGridPager pager;

        public GridRows(IEnumerable<TModel> source, IGridPager pager)
        {
            this.source = source;
            this.pager = pager;
        }

        public IEnumerator<IGridRow> GetEnumerator()
        {
            return source
                .Skip(pager.CurrentPage * pager.ItemsPerPage)
                .Take(pager.ItemsPerPage)
                .Select(model => new GridRow(model))
                .ToList()
                .GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
