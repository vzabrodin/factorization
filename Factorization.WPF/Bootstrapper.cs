using System.Windows;
using Microsoft.Practices.Unity;
using Prism.Mvvm;
using Prism.Unity;

namespace Factorization.WPF
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override void InitializeShell() => Application.Current.MainWindow.Show();

        protected override DependencyObject CreateShell() => Container.Resolve<Shell>();

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            ViewModelLocationProvider.Register(typeof(Shell).FullName, () => Container.Resolve<ShellViewModel>());
        }
    }
}