using System;
using Dna;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HospitalManagement.Core
{
    /// <summary>
    /// The settings state as a view model
    /// </summary>
    public class SettingsViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// Employee authentication token to allow use application after login
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// True to show the attachment content, false to hide it
        /// </summary>
        public bool AttachmentMenuVisible { get; set; }

        /// <summary>
        /// If we are on register employee content return true, false otherwise
        /// </summary>
        public bool IsRegisterPageForm { get; set; }
        
        /// <summary>
        /// True to show register employee button, false to hide it
        /// </summary>
        public bool IsEmployeeAdm { get; set; }
        
        /// <summary>
        /// True to hide buttons of other employee profiles, false to show
        /// </summary>
        public bool IsOtherProfile { get; set; }

        public bool IsEditMode { get; set; }
        
        public string SelectedDate { get; set; } 

        #region Transactional Properties

        /// <summary>
        /// Indicates if the first name is current being saved
        /// </summary>
        public bool DetailIsSaving { get; set; }

        #endregion

        #region Employee Details From View Application

        /// <summary>
        /// The current users first name
        /// </summary>
        public TextEntryViewModel FirstName { get; set; }

        /// <summary>
        /// The current users last name
        /// </summary>
        public TextEntryViewModel LastName { get; set; }

        /// <summary>
        /// The current users identify as login
        /// </summary>
        public TextEntryViewModel Identify { get; set; }

        /// <summary>
        /// The current users type
        /// </summary>
        public TextEntryViewModel Type { get; set; }

        /// <summary>
        /// The current users specialize
        /// </summary>
        public TextEntryViewModel Specialize { get; set; }

        /// <summary>
        /// The current users pwd number
        /// </summary>
        public TextEntryViewModel PwdNumber { get; set; }

        /// <summary>
        /// The current users mask password
        /// </summary>
        public PasswordEntryViewModel Password { get; set; }

        /// <summary>
        /// The current users Pesel
        /// </summary>
        public string Pesel { get; set; }

        #endregion

        #region Button Texts

        /// <summary>
        /// The text for the logout button
        /// </summary>
        public string LogoutButtonText { get; set; }

        /// <summary>
        /// The text for the new employee button
        /// NOTE: It's visible for users login as administrator
        /// </summary>
        public string NewEmployeeButtonText { get; set; }

        /// <summary>
        /// The text for the duty button
        /// </summary>
        public string AddDutyButtonText { get; set; } 

        #endregion

        #endregion

        #region Public Commands

        /// <summary>
        /// The command to open the settings menu
        /// </summary>
        public ICommand OpenCommand { get; set; }

        /// <summary>
        /// The command to close the settings menu
        /// </summary>
        public ICommand CloseCommand { get; set; }

        /// <summary>
        /// The command to logout of the application
        /// </summary>
        public ICommand LogoutCommand { get; set; }

        /// <summary>
        /// The command to clear the users data from the view model
        /// </summary>
        public ICommand ClearUserDataCommand { get; set; }

        /// <summary>
        /// The command to open employee forms for register to application
        /// </summary>
        public ICommand NewEmployeeCommand { get; set; }

        /// <summary>
        /// The command to update at least one detail of the employee
        /// </summary>
        public ICommand UpdateEmployeeCommand { get; set; }

        /// <summary>
        /// The command for when the attachment button is clicked
        /// </summary>
        public ICommand AttachmentButtonCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public SettingsViewModel ()
        {
            // Create commands
            OpenCommand = new RelayCommand( Open );
            CloseCommand = new RelayCommand ( async () => await CloseAsync() );
            LogoutCommand = new RelayCommand( Logout );
            ClearUserDataCommand = new RelayCommand( ClearUserData );
            NewEmployeeCommand = new RelayCommand( NewEmployee );
            AttachmentButtonCommand = new RelayCommand( AttachmentButton );

            // Saving user details commands
            UpdateEmployeeCommand = new RelayCommand( async () => await UpdateEmployeeDetailAsync() );

            // TODO: Get from localization
            LogoutButtonText = "Wyloguj";
            AddDutyButtonText = "Dodaj dyżur";
            NewEmployeeButtonText = "Nowy pracownik";
        }

        #endregion

        /// <summary>
        /// Closes the settings menu
        /// </summary>
        private async Task CloseAsync ()
        {
            // Close register form employee if is open
            if (IoC.Application.NewEmployeeFormVisible)
            {
                IoC.Application.NewEmployeeFormVisible = false;
                IsRegisterPageForm = false;
            }

            // Otherwise close settings menu
            else
            {
                IoC.Application.SettingsMenuVisible = false;

                // Set to out from employee profile
                IoC.Settings.IsOtherProfile = false;

                // Retrieve back login data employee
                await IoC.Employees.LoadEmployeeByPesel ( IoC.Settings.Pesel );
                await IoC.Duties.LoadEmployeeDutiesAsync ( IoC.Settings.Identify.OriginalText );
                
                // Set back administrator flag if employee as administrator out from other profile
                if( IoC.Settings.Type.OriginalText == "Administrator" )
                    IoC.Settings.IsEmployeeAdm = true;
            }
        }

        /// <summary>
        /// Open the settings menu
        /// </summary>
        private void Open ()
        {
            IoC.Application.SettingsMenuVisible = true;
        }

        /// <summary>
        /// Logs the user out
        /// </summary>
        private void Logout ()
        {
            // Clean all application level view models that contain
            // any information about the current user
            ClearUserData();

            // Go to login page
            IoC.Application.GoToPage( ApplicationPage.Login );
        }

        /// <summary>
        /// Clears any data specific to the current user
        /// </summary>
        private void ClearUserData()
        {
            // Clear all view models containing the users info
            FirstName = null;
            LastName = null;
            Identify = null;
            Type = null;
            Specialize = null;
            PwdNumber = null;
            Password = null;
            IsEmployeeAdm = false;
            IoC.Duties.Items = null;
            IoC.Duties.EmployeeItems = null;
        }

        /// <summary>
        /// Bring user to register new employee form
        /// </summary>
        private void NewEmployee()
        {
            IoC.Application.NewEmployeeFormVisible = true;
            IsRegisterPageForm = true;
        }

        /// <summary>
        /// When the attachment button is clicked show/hide the attachment content
        /// </summary>
        private void AttachmentButton ()
        {
            IsEditMode = false;
            SelectedDate = null;
            
            // Toggle menu visibility
            AttachmentMenuVisible ^= true;
        }

        /// <summary>
        /// Saves the changes employee properties to the server
        /// </summary>
        /// <returns>Returns true if successful, false otherwise</returns>
        public async Task<bool> UpdateEmployeeDetailAsync()
        {
            // Lock this command to ignore any other requests while processing
            return await RunCommandAsync( () => DetailIsSaving, async () =>
            {
                // Get the current known credentials
                var credentials = IoC.Settings;

                // Update the server with the details
                var result = await WebRequests.PostAsync<ApiResponse<UpdateEmployeeDto>>(
                    // TODO: Move URLs into better place
                    "http://localhost:5000/api/auth/update",
                    new UpdateEmployeeDto
                    {
                        FirstName = credentials.FirstName.OriginalText,
                        LastName = credentials.LastName.OriginalText,
                        Username = IoC.Settings.Identify.OriginalText,
                        Type =  IoC.Settings.Type.OriginalText,
                        Specialize = IoC.Settings.Specialize.OriginalText,
                        PwzNumber = IoC.Settings.PwdNumber.OriginalText
                    }, bearerToken: credentials.Token );

                // If the response has an error
                if (result.DisplayErrorIfFailedAsync( "Update first name" ))
                    return false;

                // Get employee data from result
                var employee = result.ServerResponse.Response;
                
                // Store the new employee first name to data store
                IoC.Settings.Identify.OriginalText = employee.Username;
                await IoC.Employees.LoadEmployees();

                return true;
            });
        }
    }
}
