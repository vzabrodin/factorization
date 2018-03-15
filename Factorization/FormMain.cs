using System;
using System.Diagnostics;
using System.Numerics;
using System.Windows.Forms;
using Factorization.Core;

namespace Factorization
{
    public partial class FormMain : Form
    {
        private readonly Stopwatch stopwatch = new Stopwatch();
        private readonly FactorizationController controller = new FactorizationController();

        public FormMain() => InitializeComponent();

        private void OnTimerTick(object sender, EventArgs eventArgs)
            => StopwatchToolStripStatusBarLabel.Text = $"{stopwatch.Elapsed}";

        private async void OnProcessButtonClick(object sender, EventArgs e)
        {
            Timer.Start();
            stopwatch.Restart();

            FactorizationResult result = await controller.ProcessAsync(BigInteger.Parse(textBox1.Text),
                Environment.ProcessorCount);

            stopwatch.Stop();
            Timer.Start();

            ResultTextBox.Text = $"P: {result.P}{Environment.NewLine}" +
                                 $"Q: {result.Q}{Environment.NewLine}";
        }
    }
}
