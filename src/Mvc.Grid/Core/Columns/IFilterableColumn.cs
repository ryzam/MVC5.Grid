using System;

namespace NonFactors.Mvc.Grid
{
    public interface IFilterableColumn
    {
        Boolean? IsFilterable { get; set; }
    }
}
