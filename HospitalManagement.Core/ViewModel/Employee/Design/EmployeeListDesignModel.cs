﻿using System.Collections.ObjectModel;

namespace HospitalManagement.Core
{
    /// <summary>
    /// The design-time data for a <see cref="EmployeeListViewModel"/>
    /// </summary>
    public class EmployeeListDesignModel : EmployeeListViewModel
    {

        #region Singleton
            
        /// <summary>
        /// A single instance of the design model
        /// </summary>
        public static EmployeeListDesignModel Instance => new EmployeeListDesignModel();

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmployeeListDesignModel ()
        {
            // Items = new ObservableCollection<EmployeeListItemViewModel>
            // {
            //     new EmployeeListItemViewModel
            //     {
            //         Name = "Jolanta",
            //         Who = "Lekarka",
            //         Job = "Kardiolog",
            //         JobPicture = @"pack://application:,,,/Images/EmployeeTypes/Doctor.jpg",
            //         ProfilePictureRGB = "3099c5",
            //         IsSelected = false
            //     },
            //     new EmployeeListItemViewModel
            //     {
            //         Name = "Jessica",
            //         Who = "Pielęgniarka",
            //         Job = "",
            //         JobPicture = @"pack://application:,,,/Images/EmployeeTypes/Doctor.jpg",
            //         ProfilePictureRGB = "ff0000"
            //     },
            //     new EmployeeListItemViewModel
            //     {
            //         Name = "Zbigniew",
            //         Who = "Administrator",
            //         Job = "Onkolog",
            //         JobPicture = @"pack://application:,,,/Images/EmployeeTypes/Doctor.jpg",
            //         ProfilePictureRGB = "3099c5",
            //         IsSelected = true
            //     },
            //     new EmployeeListItemViewModel
            //     {
            //         Name = "Jolanta",
            //         Who = "Lekarka",
            //         Job = "Kardiolog",
            //         JobPicture = @"pack://application:,,,/Images/EmployeeTypes/Doctor.jpg",
            //         ProfilePictureRGB = "3099c5"
            //     },
            //     new EmployeeListItemViewModel
            //     {
            //         Name = "Jessica",
            //         Who = "Pielęgniarka",
            //         Job = "",
            //         JobPicture = @"pack://application:,,,/Images/EmployeeTypes/Doctor.jpg",
            //         ProfilePictureRGB = "3099c5"
            //     },
            //     new EmployeeListItemViewModel
            //     {
            //         Name = "Zbigniew",
            //         Who = "Administrator",
            //         Job = "Onkolog",
            //         JobPicture = @"pack://application:,,,/Images/EmployeeTypes/Doctor.jpg",
            //         ProfilePictureRGB = "3099c5"
            //     },
            //     new EmployeeListItemViewModel
            //     {
            //         Name = "Jolanta",
            //         Who = "Lekarka",
            //         Job = "Kardiolog",
            //         JobPicture = @"pack://application:,,,/Images/EmployeeTypes/Doctor.jpg",
            //         ProfilePictureRGB = "3099c5"
            //     },
            //     new EmployeeListItemViewModel
            //     {
            //         Name = "Jessica",
            //         Who = "Pielęgniarka",
            //         Job = "",
            //         JobPicture = @"pack://application:,,,/Images/EmployeeTypes/Doctor.jpg",
            //         ProfilePictureRGB = "3099c5"
            //     },
            //     new EmployeeListItemViewModel
            //     {
            //         Name = "Zbigniew",
            //         Who = "Administrator",
            //         Job = "Onkolog",
            //         JobPicture = @"pack://application:,,,/Images/EmployeeTypes/Doctor.jpg",
            //         ProfilePictureRGB = "3099c5"
            //     },
            // };
        }

        #endregion


    }
}
