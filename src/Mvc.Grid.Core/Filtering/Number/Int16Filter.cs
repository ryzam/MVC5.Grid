using System;

namespace NonFactors.Mvc.Grid
{
    public class Int16Filter<T> : NumberFilter<T>
    {
        public override Object GetNumericValue()
        {
            Int16 number;
            if (Int16.TryParse(Value, out number))
                return number;

            return null;
        }
    }
}
