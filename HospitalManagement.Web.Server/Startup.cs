using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;
using HospitalManagement.Relational;

namespace HospitalManagement.Web.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add ApplicationDbContext to DI
            services.AddDbContext<DataContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString( "RelativeConnection" ) ));

            services.AddTransient<Seed>();

            // Adds scoped classes for things like UserManager, SignInManager, PasswordHashers etc...
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IGenericRepository, GenericRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            // Avoid required submit SSL certification
            services.AddHttpClient( "HttpClientWithSSLUntrusted" ).ConfigurePrimaryHttpMessageHandler( () => new HttpClientHandler
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback =
               ( httpRequestMessage, cert, cetChain, policyErrors ) =>
               {
                   return true;
               }
            } );

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider, Seed seeder)
        {
            // Store instance of the DI service provider so our application can access it anywhere
            IocContainer.Provider = serviceProvider as ServiceProvider;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            seeder.SeedEmployees();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
