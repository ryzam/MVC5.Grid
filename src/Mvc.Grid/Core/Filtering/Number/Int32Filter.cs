using System;

namespace NonFactors.Mvc.Grid
{
    public class Int32Filter<TModel> : NumberFilter<TModel> where TModel : class
    {
        public override Object GetNumericValue()
        {
            Int32 number;
            if (Int32.TryParse(Value, out number))
                return number;

            return null;
        }
    }
}
