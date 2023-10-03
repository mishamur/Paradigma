using Paradigma.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

//потоки данных -> task. stream
namespace Paradigma.Model
{
    public delegate bool CancelGenerate();
    public delegate void UpdateData();
    
    public class SinusGenerator : IGenerator
    {
        private event UpdateData _notify;
        private CancelGenerate IsCancelGenerate;

        private Queue<Point> points = new Queue<Point>();
        
        private double _step;
        private int _timeout;
        private int _dataSize;
        private double i;
        private bool IsGenerating;
        public double Amplitude { get; private set; }
        public double Frequency { get; private set; }
        public double CurrentX { get => i; }
        
        public void SubscribeToDataUpdate(Action action)
        {
            _notify += () => action?.Invoke();
        }
        public SinusGenerator(double amplitude, double frequency, double step = 0.1, int timeout = 200, int dataSize = 200)
        {
            this.Amplitude = amplitude;
            this.Frequency = frequency;
            this._step = step;
            this._timeout = timeout;
            this._dataSize = dataSize;
            IsCancelGenerate = () => false;
        }
        public async Task Generate()
        {
            if (IsGenerating) return;
            this.IsGenerating = true;
            
            while(!IsCancelGenerate())
            {   
                if (points.Count() > _dataSize)
                    points.Dequeue();

                points.Enqueue(new Point(i, this.Amplitude * Math.Sin(this.Frequency * i)));
                _notify?.Invoke();
                i = Math.Round(i + _step, 5);
                await Task.Delay(_timeout);
            }

            this.IsGenerating = false;
            return;
        }
        public void ChangeParams(double amplitude, double freq)
        {
            this.Amplitude = amplitude;
            this.Frequency = freq;
        }
        public void StopGenerate() => this.IsCancelGenerate = () => true;
        public void Refresh()
        {
            this.IsCancelGenerate = () => false;
        }
        public IEnumerable<Point> GetPoints() => this.points.ToList();
    }
}
