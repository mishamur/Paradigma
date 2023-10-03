using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace Paradigma.Model.Interfaces
{
    public interface IGenerator
    {
        double Amplitude { get; }
        double Frequency { get; }
        Task Generate();
        void Refresh();
        void StopGenerate();
        void ChangeParams(double amplitude, double freq);
        IEnumerable<Point> GetPoints();
        void SubscribeToDataUpdate(Action action);
    }
}
