using System;

namespace NonFactors.Mvc.Grid
{
    public class ByteFilter<TModel> : NumberFilter<TModel> where TModel : class
    {
        public override Object GetNumericValue()
        {
            Byte number;
            if (Byte.TryParse(Value, out number))
                return number;

            return null;
        }
    }
}
