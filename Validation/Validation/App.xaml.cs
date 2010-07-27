using System.Windows;
using Shared.Validating;
using Validation.ViewModels;
using Validation.Views;

namespace Validation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var vm = new ValidatingViewModel(new DataErrorInfoProvider());
            var view = new ValidatingView(vm);
            view.Show();
        }
    }
}
