using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Callbacks.Views;
using Callbacks.ViewModels;
using Callbacks.DomainModel;

namespace Callbacks
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var mainVM = new MainViewModel(new User { FirstName = "Bob", LastName = "Nerdhead", Email = "Bob@uncool.com" });
            var mainWin = new MainView(mainVM);
            mainWin.Show();
        }
    }
}
