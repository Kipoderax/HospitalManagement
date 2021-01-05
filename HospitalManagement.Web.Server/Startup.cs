using System;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using AutoMapper;
using HospitalManagement.Relational;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace HospitalManagement.Web.Server
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add ApplicationDbContext to DI
            services.AddDbContext<DataContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString( "RelativeConnection" ) ));

            // Seed data to database if it is empty
            services.AddTransient<Seed>();
            
            // Add mappers between data models and api models
            services.AddAutoMapper();

            // Adds scoped classes for things like UserManager, SignInManager, PasswordHashers etc...
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IGenericRepository, GenericRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IDutyRepository, DutyRepository>();

            // Avoid required submit SSL certification
            services.AddHttpClient( "HttpClientWithSSLUntrusted" ).ConfigurePrimaryHttpMessageHandler( () => new HttpClientHandler
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback =
               ( httpRequestMessage, cert, cetChain, policyErrors ) => true
            } );

            // JWT authentication for Api requests
            services.AddAuthentication ( JwtBearerDefaults.AuthenticationScheme )
                .AddJwtBearer ( options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey ( Encoding.ASCII.GetBytes ( Configuration
                            .GetSection (
                                "AppSettings:Token" ).Value ) ),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                } );
            
            services.AddControllersWithViews();

            services.AddControllers().AddNewtonsoftJson ( opt =>
            {
                opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            } );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider, Seed seeder)
        {
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
