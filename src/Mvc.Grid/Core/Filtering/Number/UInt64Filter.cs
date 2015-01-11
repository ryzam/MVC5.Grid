using System;

namespace NonFactors.Mvc.Grid
{
    public class UInt64Filter<T> : NumberFilter<T>
    {
        public override Object GetNumericValue()
        {
            UInt64 number;
            if (UInt64.TryParse(Value, out number))
                return number;

            return null;
        }
    }
}
