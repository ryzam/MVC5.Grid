using System;

namespace NonFactors.Mvc.Grid
{
    public interface IGridFilters
    {
        IGridFilter<TModel> GetFilter<TModel, TValue>(IGridColumn<TModel, TValue> column, String type, String value) where TModel : class;

        void Register(Type forType, String filterType, Type filter);
        void Unregister(Type forType, String filterType);
    }
}
