namespace HospitalManagement.Core
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
            JobPicture = @"pack://application:,,,/Images/EmployeeTypes/Doctor.jpg";
            ProfilePictureRGB = "3099c5";
        }

        #endregion
    }
}
