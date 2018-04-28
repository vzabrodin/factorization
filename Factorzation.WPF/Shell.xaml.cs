using System.Windows;

namespace Factorization.WPF
{
    public partial class Shell
    {
        public Shell()
        {
            InitializeComponent();
            Loaded += OnShellLoaded;
        }

        private void OnShellLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            Loaded -= OnShellLoaded;
            (DataContext as ShellViewModel)?.OnShellLoaded();
        }
    }
}
