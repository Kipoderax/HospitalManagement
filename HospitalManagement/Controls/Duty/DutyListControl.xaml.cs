using System.Windows.Controls;
using HospitalManagement.Core;

namespace HospitalManagement
{
    /// <summary>
    /// Logika interakcji dla klasy DutyListControl.xaml
    /// </summary>
    public partial class DutyListControl : UserControl
    {
        public DutyListControl ()
        {
            InitializeComponent();
            DataContext = IoC.Duties;
        }
    }
}
