using System;

namespace NonFactors.Mvc.Grid
{
    public class UInt16Filter<TModel> : NumberFilter<TModel> where TModel : class
    {
        public override Object GetNumericValue()
        {
            UInt16 number;
            if (UInt16.TryParse(Value, out number))
                return number;

            return null;
        }
    }
}
