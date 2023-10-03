using System.Collections.Generic;
using System.Windows;
using System.Threading.Tasks;

namespace Paradigma.Model.Interfaces
{
    public interface IPlotter
    {
        public Task PrintDataOnPlot(IEnumerable<Point> points);
    }
}
