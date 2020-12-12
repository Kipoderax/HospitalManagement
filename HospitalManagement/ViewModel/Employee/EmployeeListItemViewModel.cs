using System.Windows.Media;

namespace HospitalManagement
{
    /// <summary>
    /// A view model for each employee list item in the overview employee list
    /// </summary>
    public class EmployeeListItemViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// The display name of this employee list
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Type of employee like doctor, admin, ...
        /// </summary>
        public string Who { get; set; }

        /// <summary>
        /// The employee job
        /// </summary>
        public string Job { get; set; }

        /// <summary>
        /// Picture present the employee job in the profile background
        /// </summary>
        public string JobPicture { get; set; }

        /// <summary>
        /// The RGB values (in hex) for the background color of the profile picture
        /// For example FF00FF for red and blue mixed
        /// </summary>
        public string ProfilePictureRGB { get; set; }

        /// <summary>
        /// True if user application clicked on the specific employee in search content
        /// </summary>
        public bool IsSelected { get; set; }

        #endregion
    }
}
