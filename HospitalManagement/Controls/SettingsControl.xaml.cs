using HospitalManagement.Core;
using System.Windows.Controls;

namespace HospitalManagement
{
    /// <summary>
    /// Logika interakcji dla klasy SettingsControl.xaml
    /// </summary>
    public partial class SettingsControl : UserControl
    {
        public SettingsControl ()
        {
            InitializeComponent();

            // Set data context to settings view model
            DataContext = IoC.Settings;
        }
    }
}
