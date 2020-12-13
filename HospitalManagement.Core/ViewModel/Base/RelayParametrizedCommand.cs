using System;
using System.Windows.Input;

namespace HospitalManagement.Core
{
    public class RelayParametrizedCommand : ICommand
    {
        #region Private Members

        /// <summary>
        /// The action to run
        /// </summary>
        private Action<object> _action;

        #endregion

        #region Public Events

        /// <summary>
        /// The event thats fired when the <see cref="CanExecute(object)"/> value has changed
        /// </summary>
        public event EventHandler CanExecuteChanged = ( sender, e ) => { };

        #endregion

        #region Constructor

        /// <summary>
        /// One-parametrized constructor
        /// </summary>
        public RelayParametrizedCommand (Action<object> action)
        {
            _action = action;
        }

        #endregion

        #region Command Methods

        public bool CanExecute ( object parameter )
        {
            return true;
        }

        public void Execute ( object parameter )
        {
            _action( parameter );
        }

        #endregion
    }
}
