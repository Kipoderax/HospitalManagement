using System.Collections.Generic;

namespace HospitalManagement.Core
{
    /// <summary>
    /// Register employee forms as view model
    /// </summary>
    public class RegisterEmployeeViewModel : BaseViewModel
    {
        #region Singleton

        /// <summary>
        /// A single instance of the view model
        /// </summary>
        public static RegisterEmployeeViewModel Instance => new RegisterEmployeeViewModel();

        #endregion

        #region Employee Hints Information To Register

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Pesel { get; set; }
        public List<string> Types { get; set; }
        public List<string> Specializes { get; set; }
        public string PwzNumber { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public RegisterEmployeeViewModel ()
        {
            // Set tags to register view form
            FirstName = "Imię pracownika";
            LastName = "Nazwisko pracownika";
            Pesel = "Pesel pracownika";
            Types = new List<string> { "Administrator", "Lekarz", "Pielęgniarka" };
            Specializes = new List<string> { "Urolog", "Neurolog", "Kardiolog", "Laryngolog" };
            PwzNumber = "Numer prawa wykonywanego zawodu";
        }

        #endregion
    }
}
