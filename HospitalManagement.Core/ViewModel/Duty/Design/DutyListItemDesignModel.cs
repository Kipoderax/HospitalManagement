using System;

namespace HospitalManagement.Core
{
    /// <summary>
    /// The design-time data for a <see cref="DutyListItemViewModel"/>
    /// </summary>
    public class DutyListItemDesignModel : DutyListItemViewModel
    {
        #region Singleton

        /// <summary>
        /// A single instance of the design model
        /// </summary>
        public static DutyListItemDesignModel Instance => new DutyListItemDesignModel();

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public DutyListItemDesignModel ()
        {
            FirstName = "Braian";
            FirstLetterOfLastName = "S.";
            StartShift = DateTimeOffset.Now;
            EndShift = DateTimeOffset.Now.AddHours(12);
            JobName = "Kardiolog";
        }

        #endregion
    }
}
