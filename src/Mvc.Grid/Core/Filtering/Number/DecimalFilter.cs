using System;

namespace NonFactors.Mvc.Grid
{
    public class DecimalFilter<TModel> : NumberFilter<TModel> where TModel : class
    {
        public override Object GetNumericValue()
        {
            Decimal number;
            if (Decimal.TryParse(Value, out number))
                return number;

            return null;
        }
    }
}
