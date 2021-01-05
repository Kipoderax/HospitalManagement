using System.Windows.Controls;
using HospitalManagement.Core;

namespace HospitalManagement
{
    /// <summary>
    /// Logika interakcji dla klasy EmployeeListControl.xaml
    /// </summary>
    public partial class EmployeeListControl : UserControl
    {
        public EmployeeListControl ()
        {
            InitializeComponent();
            DataContext = IoC.Employees;
        }
    }
}
