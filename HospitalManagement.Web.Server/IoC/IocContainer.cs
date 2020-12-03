using Microsoft.Extensions.DependencyInjection;

namespace HospitalManagement.Web.Server
{
    /// <summary>
    /// A shorthand access class to get DI services with nice clean short code
    /// </summary>
    public static class IoC
    {
        /// <summary>
        /// The scoped instance of the <see cref="ApplicationDbContext"/>
        /// </summary>
        public static DataContext ApplicationDbContext => IocContainer.Provider.GetService<DataContext>();
    }

    /// <summary>
    /// The dependency injection making user of the build in .Net Core service provider
    /// </summary>
    public static class IocContainer
    {
        /// <summary>
        /// The service provider for this application
        /// </summary>
        public static ServiceProvider Provider { get; set; }
    }
}
