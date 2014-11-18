using System;
using System.Collections;

namespace NonFactors.Mvc.Grid
{
    public static class GridFilterFactory
    {
        private static Hashtable filters;

        static GridFilterFactory()
        {
            filters = new Hashtable();
            Register(typeof(String), "Equals", typeof(StringEqualsFilter<>));
            Register(typeof(String), "Contains", typeof(StringContainsFilter<>));
        }

        public static IGridFilter<TModel> GetFilter<TModel, TValue>(IGridColumn<TModel, TValue> column, String name, String value) where TModel : class
        {
            Type valueType = typeof(TValue);
            if (!filters.ContainsKey(valueType))
                return null;

            Hashtable typedFilters = (Hashtable)filters[valueType];
            if (!typedFilters.ContainsKey(name))
                return null;

            Type filterType = (Type)typedFilters[name];
            filterType = filterType.MakeGenericType(typeof(TModel));
            IGridFilter<TModel> filter = (IGridFilter<TModel>)Activator.CreateInstance(filterType);
            filter.FilteredExpression = column.Expression;
            filter.FilterValue = value;
            filter.FilterType = name;

            return filter;
        }

        public static void Register(Type forType, String name, Type filterType)
        {
            Hashtable typedFilters = new Hashtable();
            if (filters.ContainsKey(forType))
                typedFilters = (Hashtable)filters[forType];
            else
                filters.Add(forType, typedFilters);

            if (typedFilters.ContainsKey(name))
                throw new Exception();

            typedFilters.Add(name, filterType);
        }
    }
}
