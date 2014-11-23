using System;
using System.Collections;
using System.Collections.Generic;

namespace NonFactors.Mvc.Grid
{
    public class GridFilters : IGridFilters
    {
        public IDictionary<Type, IDictionary<String, Type>> Table
        {
            get;
            private set;
        }

        public GridFilters()
        {
            Table = new Dictionary<Type, IDictionary<String, Type>>();

            Register(typeof(String), "Equals", typeof(StringEqualsFilter<>));
            Register(typeof(String), "Contains", typeof(StringContainsFilter<>));
        }

        public IGridFilter<TModel> GetFilter<TModel, TValue>(IGridColumn<TModel, TValue> column, String name, String value) where TModel : class
        {
            Type valueType = typeof(TValue);
            if (!Table.ContainsKey(valueType))
                return null;

            IDictionary<String, Type> typedFilters = Table[valueType];
            if (!typedFilters.ContainsKey(name))
                return null;

            Type filterType = typedFilters[name].MakeGenericType(typeof(TModel));
            IGridFilter<TModel> filter = (IGridFilter<TModel>)Activator.CreateInstance(filterType);
            filter.FilteredExpression = column.Expression;
            filter.Value = value;
            filter.Type = name;

            return filter;
        }

        public void Register(Type forType, String name, Type filterType)
        {
            IDictionary<String, Type> typedFilters = new Dictionary<String, Type>();
            if (Table.ContainsKey(forType))
                typedFilters = Table[forType];
            else
                Table.Add(forType, typedFilters);

            typedFilters.Add(name, filterType);
        }
        public void Unregister(Type forType, String name)
        {
            if (Table.ContainsKey(forType))
                Table[forType].Remove(name);
        }
    }
}
