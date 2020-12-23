namespace HospitalManagement.Core
{
    /// <summary>
    /// The design-time data for a <see cref="SettingsViewModel"/>
    /// </summary>
    public class SettingsDesignModel : SettingsViewModel
    {
        #region Singleton

        /// <summary>
        /// A single instance of the design model
        /// </summary>
        public static SettingsDesignModel Instance => new SettingsDesignModel();

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public SettingsDesignModel ()
        {
            FirstName = new TextEntryViewModel { Label = "Imię", OriginalText = "Jessica" };
            LastName = new TextEntryViewModel { Label = "Nazwisko", OriginalText = "Stalon" };
            Identify = new TextEntryViewModel { Label = "Identyfikator", OriginalText = "JS12321" };
            Type = new TextEntryViewModel { Label = "Posada", OriginalText = "Lekarz" };
            Specialize = new TextEntryViewModel { Label = "Specjalizacja", OriginalText = "Urolog" };
            PwdNumber = new TextEntryViewModel { Label = "Numer PWD", OriginalText = "pwd135" };
            Password = new PasswordEntryViewModel { Label = "Hasło", FakePassword = "********" };
        }

        #endregion
    }
}
