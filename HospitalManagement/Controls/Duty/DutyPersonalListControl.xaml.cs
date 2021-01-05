using System.Windows.Controls;
using HospitalManagement.Core;

namespace HospitalManagement
{
    /// <summary>
    /// Logika interakcji dla klasy DutyPersonalListControl.xaml
    /// </summary>
    public partial class DutyPersonalListControl : UserControl
    {
        public DutyPersonalListControl ()
        {
            InitializeComponent();
            DataContext = IoC.Duties;
        }
    }
}
