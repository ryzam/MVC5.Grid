using System;

namespace NonFactors.Mvc.Grid
{
    public class Int64Filter<T> : NumberFilter<T>
    {
        public override Object GetNumericValue()
        {
            Int64 number;
            if (Int64.TryParse(Value, out number))
                return number;

            return null;
        }
    }
}
