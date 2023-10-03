using Paradigma.Model.Interfaces;
using System.Threading.Tasks;

namespace Paradigma.ViewModel
{
    public class AppViewModel
    {
        IGenerator generator;
        IPlotter plotter;

        public AppViewModel(IGenerator generator, IPlotter plotter)
        {
            this.generator = generator;
            this.plotter = plotter;
            this.generator.SubscribeToDataUpdate(this.ShowWhenUpdate);
        }
        public async void Start()
        {
            generator.Refresh();
            await Task.Run(() => generator.Generate());
        }
        public void Stop()
        {
            generator.StopGenerate();
        }
        public void AmplitudeChanged(string amplitude)
        {
            bool isDouble = double.TryParse(amplitude, out var amplitudeValue);
            if (isDouble)
            {
                generator.StopGenerate();
                generator.ChangeParams(amplitudeValue, generator.Frequency);
                this.Start();
            }
        }
        public void FreqChanged(string freq)
        {
            bool isDouble = double.TryParse(freq, out var freqValue);
            if (isDouble) 
            {
                generator.StopGenerate();
                generator.ChangeParams(generator.Amplitude, freqValue);
                this.Start();
            }
        }
        private void ShowWhenUpdate()
        {
            var points = generator.GetPoints();
            var task = Task.Run(() => this.plotter.PrintDataOnPlot(points));
            task.Wait();
        }
    }
}
