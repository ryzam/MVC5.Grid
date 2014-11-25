using System;

namespace NonFactors.Mvc.Grid
{
    public class SByteFilter<TModel> : NumberFilter<TModel> where TModel : class
    {
        public override Object GetNumericValue()
        {
            SByte number;
            if (SByte.TryParse(Value, out number))
                return number;

            return null;
        }
    }
}
