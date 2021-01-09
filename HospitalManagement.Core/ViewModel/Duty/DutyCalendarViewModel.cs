using System;

namespace HospitalManagement.Core
{
    /// <summary>
    /// A view model for the get duty date
    /// </summary>
    public class DutyCalendarViewModel
    {
        #region Public Properties

        /// <summary>
        /// The range from begin employees can't set self duty
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        /// The range to end employees can't set self duty
        /// </summary>
        public DateTime End { get; set; }

        /// <summary>
        /// Part time of hour for duty set
        /// </summary>
        public string HourTime { get; set; }

        /// <summary>
        /// Part time of minut for duty set
        /// </summary>
        public string MinutTime { get; set; }

        #endregion

        #region Public Commands


        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public DutyCalendarViewModel ()
        {
            Start = DateTime.Now.AddDays( 30 );
            End = Start.AddMonths( 1 );
            HourTime = "7";
            MinutTime = "15";
        }

        #endregion

        #region Command Methods


        #endregion
    }
}
