using ScottPlot;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Paradigma.Model.Interfaces;

namespace Paradigma.Model
{
    public class Plotter : IPlotter
    {
        private ScottPlot.WpfPlot _plot;
        public readonly double plotWidth;
        public Plotter(WpfPlot plot, double plotWidth)
        {
            _plot = plot;
            this.plotWidth = plotWidth;
            Configure();
        }
        public Task PrintDataOnPlot(IEnumerable<Point> points)
        {
            Application.Current.Dispatcher.InvokeAsync(() =>
            {
                lock (this._plot)
                {
                    _plot.Plot.AddScatter(points.Select(p => p.X).ToArray(), points.Select(p => p.Y).ToArray(), System.Drawing.Color.DarkSeaGreen);
                    _plot.Plot.SetAxisLimitsX(points.Last().X - this.plotWidth, points.Last().X);
                    _plot.Refresh();
                }
            }).Wait();
            return Task.CompletedTask;
        }
        public virtual void Configure()
        {
            _plot.Plot.SetAxisLimitsY(-1, 1);
        }
    }
}
