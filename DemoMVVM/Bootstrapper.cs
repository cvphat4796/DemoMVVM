using Caliburn.Micro;
using DemoMVVM.ViewModels;
using System.Windows;

namespace DemoMVVM
{
    public class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<SinhVienViewModel>();
        }
    }
}
