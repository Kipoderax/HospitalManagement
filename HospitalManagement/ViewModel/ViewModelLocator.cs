using HospitalManagement.Core;

namespace HospitalManagement
{
    /// <summary>
    /// Locates view models from the IoC for use in binding in xaml files
    /// </summary>
    public  class ViewModelLocator
    {
        #region Public Properties
        
        /// <summary>
        /// Siingleton instance of the locator
        /// </summary>
        public static ViewModelLocator Instance { get; private set; } = new ViewModelLocator();

        /// <summary>
        /// The application view model
        /// </summary>
        public static ApplicationViewModel ApplicationViewModel => IoC.Application;

        /// <summary>
        /// The settings view model
        /// </summary>
        public static SettingsViewModel SettingsViewModel => IoC.Settings;

        /// <summary>
        /// The register employee view model
        /// </summary>
        public static RegisterViewModel RegisterEmployeeViewModel => IoC.RegisterEmployee;

        #endregion
    }
}
