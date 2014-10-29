using System.Linq;

namespace NonFactors.Mvc.Grid
{
    public interface IGrid
    {
        IGridColumns Columns { get; }
        IGridRows Rows { get; }
    }

    public interface IGrid<T> : IGrid, IGridOptions<T> where T : class
    {
        IQueryable<T> Source { get; }
    }
}
