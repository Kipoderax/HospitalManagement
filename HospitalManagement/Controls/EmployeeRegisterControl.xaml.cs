using HospitalManagement.Core;
using System.Windows.Controls;

namespace HospitalManagement
{
    /// <summary>
    /// Logika interakcji dla klasy EmployeeRegisterPage.xaml
    /// </summary>
    public partial class EmployeeRegisterControl : UserControl
    {
        public EmployeeRegisterControl ()
        {
            InitializeComponent();

            // Set data context to register employee view model
            DataContext = IoC.RegisterEmployee;
        }
    }
}
