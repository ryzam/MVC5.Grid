using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NonFactors.Mvc.Grid
{
    public class GridRows<TModel> : IGridRows<TModel> where TModel : class
    {
        public Func<TModel, String> CssClasses { get; set; }
        public IGrid<TModel> Grid { get; private set; }

        public GridRows(IGrid<TModel> grid)
        {
            Grid = grid;
        }

        public IEnumerator<IGridRow> GetEnumerator()
        {
            IQueryable<TModel> items = Grid.Source;
            foreach (IGridProcessor<TModel> processor in Grid.Processors.Where(proc => proc.ProcessorType == GridProcessorType.Pre))
                items = processor.Process(items);

            foreach (IGridProcessor<TModel> processor in Grid.Processors.Where(proc => proc.ProcessorType == GridProcessorType.Post))
                items = processor.Process(items);

            return items
                .ToList()
                .Select(model => new GridRow(model)
                {
                    CssClasses = (CssClasses != null)
                        ? CssClasses(model)
                        : null
                })
                .GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
