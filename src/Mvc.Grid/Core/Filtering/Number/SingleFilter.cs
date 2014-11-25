using System;

namespace NonFactors.Mvc.Grid
{
    public class SingleFilter<TModel> : NumberFilter<TModel> where TModel : class
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
