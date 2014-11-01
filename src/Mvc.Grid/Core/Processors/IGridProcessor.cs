using System.Collections.Generic;

namespace NonFactors.Mvc.Grid
{
    public interface IGridProcessor<T> where T : class
    {
        GridProcessorType Type { get; set; }

        IEnumerable<T> Process(IEnumerable<T> items);
    }
}
