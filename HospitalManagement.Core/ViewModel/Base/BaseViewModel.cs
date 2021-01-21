using PropertyChanged;
using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace HospitalManagement.Core
{
    /// <summary>
    /// A base view model that fires Property Changed events as needed
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region Protected Members

        /// <summary>
        /// A globaly lock for property checks so prevent locking on different instances of expressions.
        /// Considering how fast this check will always be it isn't an issue to globally lock all callers.
        /// </summary>
        protected object _propertyValueCheckLock = new object();
        
        /// <summary>
        /// True if user get success result from web request, false otherwise
        /// </summary>
        public bool Success { get; set; }
        
        /// <summary>
        /// Let know user what is wrong
        /// </summary>
        public string ErrorMessage { get; set; }
        
        /// <summary>
        /// Let know user everything is fine
        /// </summary>
        public string SuccessMessage { get; set; }

        #endregion
        
        /// <summary>
        /// The event that is fired when any child property changes its value
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        /// <summary>
        /// Call this to fire a <see cref="PropertyChanged"/> event
        /// </summary>
        /// <param name="name"></param>
        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #region Command Helper

        /// <summary>
        /// Runs a command if the updating flag is not set
        /// If the flag is true (the function is already running) the the action is not run
        /// If the flag is false (indicating no running function) then the action is run
        /// Once the action is finished if it was run, then the flag is reset to false
        /// </summary>
        /// <param name="updatingFlag">The boolean property flag defining if the command is already running</param>
        /// <param name="action">The action to run if the command is not already running</param>
        /// <returns></returns>
        protected async Task RunCommandAsync(Expression<Func<bool>> updatingFlag, Func<Task> action)
        {
            // Lock to ensure isngle access to check
            lock (_propertyValueCheckLock)
            {
                // Check if the flag property is true (meaning the function is already running)
                if (updatingFlag.GetPropertyValue())
                    return;

                // Set the property flag to true to indicate we are running
                updatingFlag.SetPropertyValue( true );
            }

            try
            {
                // Run the passed in action
                await action();
            }
            finally
            {
                // Set the property flag back to false now it's finished
                updatingFlag.SetPropertyValue( false );
            }
        }

        /// <summary>
        /// Runs a command if the updating flag is not set
        /// If the flag is true (the function is already running) the the action is not run
        /// If the flag is false (indicating no running function) then the action is run
        /// Once the action is finished if it was run, then the flag is reset to false
        /// </summary>
        /// <param name="updatingFlag">The boolean property flag defining if the command is already running</param>
        /// <param name="action">The action to run if the command is not already running</param>
        /// <param name="defaultValue">The type the action returns</param>
        /// <returns></returns>
        protected async Task<T> RunCommandAsync<T> ( Expression<Func<bool>> updatingFlag, Func<Task<T>> action, T defaultValue = default )
        {
            // Lock to ensure isngle access to check
            lock (_propertyValueCheckLock)
            {
                // Check if the flag property is true (meaning the function is already running)
                if (updatingFlag.GetPropertyValue())
                    return defaultValue;

                // Set the property flag to true to indicate we are running
                updatingFlag.SetPropertyValue( true );
            }

            try
            {
                // Run the passed in action
                return await action();
            }
            finally
            {
                // Set the property flag back to false now it's finished
                updatingFlag.SetPropertyValue( false );
            }
        }

        #endregion
    }
}
