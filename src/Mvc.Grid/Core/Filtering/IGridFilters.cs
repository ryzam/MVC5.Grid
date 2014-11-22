using System;

namespace NonFactors.Mvc.Grid
{
    public interface IGridFilters
    {
        IGridFilter<TModel> GetFilter<TModel, TValue>(IGridColumn<TModel, TValue> column, String name, String value) where TModel : class;

        void Register(Type forType, String name, Type filterType);
        void Unregister(Type forType, String name);
    }
}
