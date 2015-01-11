using System;

namespace NonFactors.Mvc.Grid
{
    public interface IGridFilters
    {
        IGridFilter<T> GetFilter<T>(IGridColumn<T> column, String type, String value);

        void Register(Type forType, String filterType, Type filter);
        void Unregister(Type forType, String filterType);
    }
}
