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

            Register(typeof(SByte), "Equals", typeof(SByteFilter<>));
            Register(typeof(SByte), "LessThan", typeof(SByteFilter<>));
            Register(typeof(SByte), "GreaterThan", typeof(SByteFilter<>));
            Register(typeof(SByte), "LessThanOrEqual", typeof(SByteFilter<>));
            Register(typeof(SByte), "GreaterThanOrEqual", typeof(SByteFilter<>));

            Register(typeof(Byte), "Equals", typeof(ByteFilter<>));
            Register(typeof(Byte), "LessThan", typeof(ByteFilter<>));
            Register(typeof(Byte), "GreaterThan", typeof(ByteFilter<>));
            Register(typeof(Byte), "LessThanOrEqual", typeof(ByteFilter<>));
            Register(typeof(Byte), "GreaterThanOrEqual", typeof(ByteFilter<>));

            Register(typeof(Int16), "Equals", typeof(Int16Filter<>));
            Register(typeof(Int16), "LessThan", typeof(Int16Filter<>));
            Register(typeof(Int16), "GreaterThan", typeof(Int16Filter<>));
            Register(typeof(Int16), "LessThanOrEqual", typeof(Int16Filter<>));
            Register(typeof(Int16), "GreaterThanOrEqual", typeof(Int16Filter<>));

            Register(typeof(UInt16), "Equals", typeof(UInt16Filter<>));
            Register(typeof(UInt16), "LessThan", typeof(UInt16Filter<>));
            Register(typeof(UInt16), "GreaterThan", typeof(UInt16Filter<>));
            Register(typeof(UInt16), "LessThanOrEqual", typeof(UInt16Filter<>));
            Register(typeof(UInt16), "GreaterThanOrEqual", typeof(UInt16Filter<>));

            Register(typeof(Int32), "Equals", typeof(Int32Filter<>));
            Register(typeof(Int32), "LessThan", typeof(Int32Filter<>));
            Register(typeof(Int32), "GreaterThan", typeof(Int32Filter<>));
            Register(typeof(Int32), "LessThanOrEqual", typeof(Int32Filter<>));
            Register(typeof(Int32), "GreaterThanOrEqual", typeof(Int32Filter<>));

            Register(typeof(UInt32), "Equals", typeof(UInt32Filter<>));
            Register(typeof(UInt32), "LessThan", typeof(UInt32Filter<>));
            Register(typeof(UInt32), "GreaterThan", typeof(UInt32Filter<>));
            Register(typeof(UInt32), "LessThanOrEqual", typeof(UInt32Filter<>));
            Register(typeof(UInt32), "GreaterThanOrEqual", typeof(UInt32Filter<>));

            Register(typeof(Int64), "Equals", typeof(Int64Filter<>));
            Register(typeof(Int64), "LessThan", typeof(Int64Filter<>));
            Register(typeof(Int64), "GreaterThan", typeof(Int64Filter<>));
            Register(typeof(Int64), "LessThanOrEqual", typeof(Int64Filter<>));
            Register(typeof(Int64), "GreaterThanOrEqual", typeof(Int64Filter<>));

            Register(typeof(UInt64), "Equals", typeof(UInt64Filter<>));
            Register(typeof(UInt64), "LessThan", typeof(UInt64Filter<>));
            Register(typeof(UInt64), "GreaterThan", typeof(UInt64Filter<>));
            Register(typeof(UInt64), "LessThanOrEqual", typeof(UInt64Filter<>));
            Register(typeof(UInt64), "GreaterThanOrEqual", typeof(UInt64Filter<>));

            Register(typeof(Single), "Equals", typeof(SingleFilter<>));
            Register(typeof(Single), "LessThan", typeof(SingleFilter<>));
            Register(typeof(Single), "GreaterThan", typeof(SingleFilter<>));
            Register(typeof(Single), "LessThanOrEqual", typeof(SingleFilter<>));
            Register(typeof(Single), "GreaterThanOrEqual", typeof(SingleFilter<>));

            Register(typeof(Double), "Equals", typeof(DoubleFilter<>));
            Register(typeof(Double), "LessThan", typeof(DoubleFilter<>));
            Register(typeof(Double), "GreaterThan", typeof(DoubleFilter<>));
            Register(typeof(Double), "LessThanOrEqual", typeof(DoubleFilter<>));
            Register(typeof(Double), "GreaterThanOrEqual", typeof(DoubleFilter<>));

            Register(typeof(Decimal), "Equals", typeof(DecimalFilter<>));
            Register(typeof(Decimal), "LessThan", typeof(DecimalFilter<>));
            Register(typeof(Decimal), "GreaterThan", typeof(DecimalFilter<>));
            Register(typeof(Decimal), "LessThanOrEqual", typeof(DecimalFilter<>));
            Register(typeof(Decimal), "GreaterThanOrEqual", typeof(DecimalFilter<>));

            Register(typeof(DateTime), "Equals", typeof(DateTimeFilter<>));
            Register(typeof(DateTime), "LessThan", typeof(DateTimeFilter<>));
            Register(typeof(DateTime), "GreaterThan", typeof(DateTimeFilter<>));
            Register(typeof(DateTime), "LessThanOrEqual", typeof(DateTimeFilter<>));
            Register(typeof(DateTime), "GreaterThanOrEqual", typeof(DateTimeFilter<>));

            Register(typeof(Boolean), "Equals", typeof(BooleanFilter<>));

            Register(typeof(String), "Equals", typeof(StringEqualsFilter<>));
            Register(typeof(String), "Contains", typeof(StringContainsFilter<>));
            Register(typeof(String), "EndsWith", typeof(StringEndsWithFilter<>));
            Register(typeof(String), "StartsWith", typeof(StringStartsWithFilter<>));
        }

        public IGridFilter<T> GetFilter<T>(IGridColumn<T> column, String type, String value)
        {
            Type valueType = Nullable.GetUnderlyingType(column.Expression.ReturnType) ?? column.Expression.ReturnType;
            if (!Table.ContainsKey(valueType))
                return null;

            IDictionary<String, Type> typedFilters = Table[valueType];
            if (!typedFilters.ContainsKey(type))
                return null;

            Type filterType = typedFilters[type].MakeGenericType(typeof(T));
            IGridFilter<T> filter = (IGridFilter<T>)Activator.CreateInstance(filterType);
            filter.FilteredExpression = column.Expression;
            filter.Value = value;
            filter.Type = type;

            return filter;
        }

        public void Register(Type forType, String filterType, Type filter)
        {
            IDictionary<String, Type> typedFilters = new Dictionary<String, Type>();
            Type underlyingType = Nullable.GetUnderlyingType(forType) ?? forType;

            if (Table.ContainsKey(underlyingType))
                typedFilters = Table[underlyingType];
            else
                Table.Add(underlyingType, typedFilters);

            typedFilters.Add(filterType, filter);
        }
        public void Unregister(Type forType, String filterType)
        {
            if (Table.ContainsKey(forType))
                Table[forType].Remove(filterType);
        }
    }
}
