﻿using HospitalManagement.Core;
using System.Threading.Tasks;
using System.Windows;

namespace HospitalManagement
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Custom startup so we load our IoC immediately before anything else
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup ( StartupEventArgs e )
        {
            // Let the base application do what it needs
            base.OnStartup( e );

            // Setup IoC
            IoC.Setup();

            // Show the main window
            Current.MainWindow = new MainWindow();
            Current.MainWindow.Show();
        }
    }
}
