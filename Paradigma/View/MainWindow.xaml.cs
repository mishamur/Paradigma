using System;
using System.Windows;
using System.Windows.Controls;
using Paradigma.Model;
using Paradigma.ViewModel;

namespace Paradigma
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SinusGenerator sinusGenerator = new SinusGenerator(1, 1, 0.1, 200, 200);
            Plotter plotter = new Plotter(wpfPlot1, 2 * Math.PI);
            
            AppViewModel appViewModel = new AppViewModel(sinusGenerator, plotter);
            
            startButton.Click += (s, e) =>
            {
                appViewModel.Start();
            };

            stopButton.Click += (s, e) =>
            {
                appViewModel.Stop();
            };

            this.aplitude.TextChanged += (s, e) =>
            {
                appViewModel.AmplitudeChanged(((TextBox)s).Text);
            };

            this.freq.TextChanged += (s, e) =>
            {
                appViewModel.FreqChanged(((TextBox)s).Text);
            };
        }
    }
}
