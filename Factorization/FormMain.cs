using System;
using System.Diagnostics;
using System.Numerics;
using System.Windows.Forms;
using Factorization.Core;

namespace Factorization
{
    public partial class FormMain : Form
    {
        private readonly FactorizationController factorizationController = new FactorizationController();

        public FormMain() => InitializeComponent();

        private async void button1_Click(object sender, EventArgs e)
        {
            Stopwatch t = Stopwatch.StartNew();

            FactorizationResult result = await factorizationController.ProcessMulticoreAsync(BigInteger.Parse(textBox1.Text));

            t.Stop();
            textBox2.Text = $"P = {result.P}\n" +
                            $"Q = {result.Q}\n" +
                            $"Время:{t.Elapsed}";
        }
    }
}
