using System;

namespace NonFactors.Mvc.Grid
{
    public class GridPager : IGridPager
    {
        public const Int32 DefaultItemsPerPage = 20;

        public Int32 ItemsPerPage
        {
            get;
            set;
        }
        public Int32 CurrentPage
        {
            get;
            set;
        }

        public GridPager()
        {
            ItemsPerPage = DefaultItemsPerPage;
            CurrentPage = 0;
        }
    }
}
