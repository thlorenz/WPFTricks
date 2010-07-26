using System.Windows;
using Callbacks.ViewModels;

namespace Callbacks.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView(MainViewModel viewModel)
            :this()
        {
            DataContext = viewModel;
        }

        public MainView()
        {
            InitializeComponent();
        }
    }
}
