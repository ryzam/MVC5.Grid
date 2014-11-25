using System;

namespace NonFactors.Mvc.Grid
{
    public class DoubleFilter<TModel> : NumberFilter<TModel> where TModel : class
    {
        public override Object GetNumericValue()
        {
            Double number;
            if (Double.TryParse(Value, out number))
                return number;

            return null;
        }
    }
}
