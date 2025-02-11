﻿using Ninject;

namespace HospitalManagement.Core
{
    /// <summary>
    /// The IoC container for our application
    /// </summary>
    public static class IoC
    {
        #region Public Properties

        /// <summary>
        /// The kernel for our IoC container
        /// </summary>
        public static IKernel Kernel { get; private set; } = new StandardKernel();

        /// <summary>
        /// A shortcut to access the <see cref="ApplicationViewModel"/>
        /// </summary>
        public static ApplicationViewModel Application => Get<ApplicationViewModel>();

        /// <summary>
        /// A shortcut to access the <see cref="SettingsViewModel"/>
        /// </summary>
        public static SettingsViewModel Settings => Get<SettingsViewModel>();

        /// <summary>
        /// A shortcut to access the <see cref="RegisterViewModel"/>
        /// </summary>
        public static RegisterViewModel RegisterEmployee => Get<RegisterViewModel>();

        /// <summary>
        /// A shortcut to access the <see cref="EmployeeListDesignModel"/>
        /// </summary>
        public static EmployeeListDesignModel Employees => Get<EmployeeListDesignModel>();

        /// <summary>
        /// A shortcut to access the <see cref="DutyListDesignModel"/>
        /// </summary>
        public static DutyListDesignModel Duties => Get<DutyListDesignModel>();

        /// <summary>
        /// A shortcut to access the <see cref="DutyCalendarViewModel"/>
        /// </summary>
        public static DutyCalendarViewModel DutyCalendar => Get<DutyCalendarViewModel>();

        #endregion

        #region Construction

        /// <summary>
        /// Sets up the IoC container, binds all information required and is ready for use
        /// NOTE: Must be called as soon as your application starts up to ensure all
        ///              services can be found
        /// </summary>
        public static void Setup ()
        {
            // Bind all required view models
            BindViewModels();
        }

        /// <summary>
        /// Binds all singleton view models
        /// </summary>
        private static void BindViewModels ()
        {
            // Bind to a single instance of Application view model
            Kernel.Bind<ApplicationViewModel>().ToConstant( new ApplicationViewModel() );

            // Bind to a single instance of Settings view model
            Kernel.Bind<SettingsViewModel>().ToConstant( new SettingsViewModel() );

            // Bind to a single instance of Employee view model
            Kernel.Bind<EmployeeListDesignModel>().ToConstant ( new EmployeeListDesignModel() );
            
            // Bind to a single instance of Duty view model
            Kernel.Bind<DutyListDesignModel>().ToConstant ( new DutyListDesignModel() );

            // Bind to a single instance of Duty calendar view model
            Kernel.Bind<DutyCalendarViewModel>().ToConstant( new DutyCalendarViewModel() );
        }

        #endregion

        /// <summary>
        /// Get's a service from the IoC, of the specific type
        /// </summary>
        /// <typeparam name="T">The type to get</typeparam>
        /// <returns></returns>
        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }
    }
}
