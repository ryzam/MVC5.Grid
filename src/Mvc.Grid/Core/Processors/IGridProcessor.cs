using System.Linq;

namespace NonFactors.Mvc.Grid
{
    public interface IGridProcessor<T> where T : class
    {
        GridProcessorType Type { get; set; }

        IQueryable<T> Process(IQueryable<T> items);
    }
}
