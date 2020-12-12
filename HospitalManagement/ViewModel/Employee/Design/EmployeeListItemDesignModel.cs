using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace HospitalManagement
{
    /// <summary>
    /// The design-time data for a <see cref="EmployeeListItemViewModel"/>
    /// </summary>
    public class EmployeeListItemDesignModel : EmployeeListItemViewModel
    {
        #region Singleton
            
        /// <summary>
        /// A single instance of the design model
        /// </summary>
        public static EmployeeListItemDesignModel Instance => new EmployeeListItemDesignModel();

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmployeeListItemDesignModel ()
        {
            Name = "Jolanta";
            Who = "Lekarka";
            Job = "Kardiolog";
            //TODO: Create Images in styles and bind values as relative path to property and send to view in xaml
            JobPicture = @"C:\Users\Kipoderax\source\repos\HospitalManagement-master\HospitalManagement\Images\EmployeeTypes\Doctor.jpg";
            //JobPicture = @"pack://application:,,,/Images/EmployeeTypes/Doctor.jpg"; //to działa w xaml
            ProfilePictureRGB = "3099c5";
        }

        #endregion
    }
}
