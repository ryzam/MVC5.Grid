using System;

namespace NonFactors.Mvc.Grid
{
    public class SingleFilter<T> : NumberFilter<T>
    {
        public override Object GetNumericValue()
        {
            Single number;
            if (Single.TryParse(Value, out number))
                return number;

            return null;
        }
    }
}
