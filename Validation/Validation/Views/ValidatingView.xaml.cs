using System.Windows;
using Validation.ViewModels;

namespace Validation.Views
{
    /// <summary>
    /// Interaction logic for ValidatingView.xaml
    /// </summary>
    public partial class ValidatingView : Window
    {
        public ValidatingView(ValidatingViewModel viewModel)
            :this()
        {
            DataContext = viewModel;
        }

        public ValidatingView()
        {
            InitializeComponent();
        }
    }
}
