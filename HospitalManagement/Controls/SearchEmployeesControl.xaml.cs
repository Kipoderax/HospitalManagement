using System.Windows.Controls;
using HospitalManagement.Core;

namespace HospitalManagement
{
    /// <summary>
    /// Logika interakcji dla klasy SearchEmployeesControl.xaml
    /// </summary>
    public partial class SearchEmployeesControl : UserControl
    {
        public SearchEmployeesControl ()
        {
            InitializeComponent();
            DataContext = IoC.Employees;
        }
    }
}
