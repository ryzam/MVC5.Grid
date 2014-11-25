using System;

namespace NonFactors.Mvc.Grid
{
    public class Int64Filter<TModel> : NumberFilter<TModel> where TModel : class
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
