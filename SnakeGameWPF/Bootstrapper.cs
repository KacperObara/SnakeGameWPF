using Caliburn.Micro;
using System.Windows;
using SnakeGameWPF.ViewModels;

namespace SnakeGameWPF
{
    public class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }
    }
}
