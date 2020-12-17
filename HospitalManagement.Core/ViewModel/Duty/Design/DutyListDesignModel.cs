using System;
using System.Collections.Generic;

namespace HospitalManagement.Core
{
    /// <summary>
    /// The design-time data for a <see cref="DutyListViewModel"/>
    /// </summary>
    public class DutyListDesignModel : DutyListViewModel
    {
        #region Singleton

        /// <summary>
        /// A single instance of the design model
        /// </summary>
        public static DutyListDesignModel Instance => new DutyListDesignModel();

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public DutyListDesignModel ()
        {
            Items = new List<DutyListItemViewModel>
            {
                new DutyListItemViewModel
                {
                    FirstName = "Braian",
                    FirstLetterOfLastName = "S.",
                    StartShift = DateTimeOffset.Parse( "2020-12-18 7:15" ),
                    EndShift = DateTimeOffset.Parse("2020-12-18 19:15"),
                    JobName = "Kardiolog"
                },
                new DutyListItemViewModel
                {
                    FirstName = "Zbigniew",
                    FirstLetterOfLastName = "S.",
                    StartShift = DateTimeOffset.Parse( "2020-12-18 22:30" ),
                    EndShift = DateTimeOffset.Parse("2020-12-19 10:30"),
                    JobName = "Kardiolog"
                },
                new DutyListItemViewModel
                {
                    FirstName = "Jolanta",
                    FirstLetterOfLastName = "S.",
                    StartShift = DateTimeOffset.Parse( "2020-12-19 14:00" ),
                    EndShift = DateTimeOffset.Parse("2020-12-20 2:00"),
                    JobName = "Kardiolog"
                },
                new DutyListItemViewModel
                {
                    FirstName = "Zbigniew",
                    FirstLetterOfLastName = "S.",
                    StartShift = DateTimeOffset.Parse( "2020-12-18 22:30" ),
                    EndShift = DateTimeOffset.Parse("2020-12-19 10:30"),
                    JobName = "Onkologia"
                },
                new DutyListItemViewModel
                {
                    FirstName = "Jolanta",
                    FirstLetterOfLastName = "G.",
                    StartShift = DateTimeOffset.Parse( "2020-12-18 22:30" ),
                    EndShift = DateTimeOffset.Parse("2020-12-19 10:30"),
                    JobName = "Kardiologia"
                },
            };
        }

        #endregion
    }
}
