using HospitalManagement.Core;
using System.Windows.Controls;

namespace HospitalManagement
{
    /// <summary>
    /// Logika interakcji dla klasy DutyCalendarControl.xaml
    /// </summary>
    public partial class DutyCalendarControl : UserControl
    {
        public DutyCalendarControl ()
        {
            InitializeComponent();
            //DataContext = IoC.DutyCalendar;
            CalendarRange.BlackoutDates.Add( new CalendarDateRange( IoC.DutyCalendar.Start, IoC.DutyCalendar.End ) );
        }
    }
}
