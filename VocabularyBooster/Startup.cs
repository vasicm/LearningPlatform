using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VocabularyBooster.Data.Graph;
using VocabularyBooster.Options;

namespace VocabularyBooster
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IContainer ApplicationContainer { get; protected set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            this.SetupDb(services);
            services
                .AddCustomOptions(this.Configuration)
                .AddControllersWithViews();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddCustomSwagger(this.Configuration)
                .AddCustomApiVersioning()
                .AddVersionedApiExplorer(x =>
                {
                    x.GroupNameFormat = "'v'VVV";
                    x.SubstituteApiVersionInUrl = true;
                });

            // autofac setup
            var builder = new ContainerBuilder();
            builder.Populate(services);
        }

        // ConfigureContainer is where you can register things directly
        // with Autofac. This runs after ConfigureServices so the things
        // here will override registrations made in ConfigureServices.
        // Don't build the container; that gets done for you by the factory.
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Register your own things directly with Autofac
            this.RegisterServices(builder);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            var enabledSpa = this.Configuration.GetSection(nameof(ApplicationSettings))[nameof(ApplicationSettings.EnabledSpa)] == "True";
            if (enabledSpa)
            {
                app.UseSpa(spa =>
                {
                    // To learn more about options for serving an Angular SPA from ASP.NET Core,
                    // see https://go.microsoft.com/fwlink/?linkid=864501
                    spa.Options.SourcePath = "ClientApp";

                    if (env.IsDevelopment())
                    {
                        spa.UseAngularCliServer(npmScript: "start");
                    }
                });
            }

            this.ConfigureOptionalServices(app);
        }

        protected virtual void SetupDb(IServiceCollection services)
        {
            services.AddSingleton<GraphDbContext>();
        }

        protected virtual void RegisterServices(ContainerBuilder builder)
        {
            // project service registrations with autofac
            builder.AddProjectServices();
        }

        protected virtual void ConfigureOptionalServices(IApplicationBuilder app)
        {
            // set default request culture
            var cultureInfo = new CultureInfo("en");
            var supportedCultures = new[]
            {
                cultureInfo,
                new CultureInfo("sl"),
                new CultureInfo("en"),
            };

            // Configure the Localization middleware
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(cultureInfo),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app
                .UseSwagger(c =>
                {
                    string prefix = this.Configuration.GetSection(nameof(ApplicationSettings))[nameof(ApplicationSettings.SwaggerPathPrefixToInsert)];
                    //change the path to include prefix
                    c.RouteTemplate = (prefix.StartsWith("/", StringComparison.InvariantCulture) ? prefix.Remove(0, 1) : prefix) + "/swagger/{documentName}/swagger.json";
                }).UseSwaggerUI(
                options =>
                {
                    // Set the Swagger UI browser document title.
                    options.DocumentTitle = typeof(Startup)
                        .Assembly
                        .GetCustomAttribute<AssemblyProductAttribute>()
                        .Product;
                    // Set the Swagger UI prefix
                    // if behind proxy we might need to insert prefix
                    string prefix = this.Configuration.GetSection(nameof(ApplicationSettings))[nameof(ApplicationSettings.SwaggerPathPrefixToInsert)];
                    if (string.IsNullOrEmpty(prefix) == false)
                    {
                        options.RoutePrefix =

                            (prefix.StartsWith("/", StringComparison.InvariantCulture) ? prefix.Remove(0, 1) : prefix) + "/swagger";
                        // Show the request duration in Swagger UI.
                        options.DisplayRequestDuration();
                    }

                    var provider = app.ApplicationServices.GetService<IApiVersionDescriptionProvider>();
                    foreach (ApiVersionDescription apiVersionDescription in provider
                        .ApiVersionDescriptions
                        .OrderByDescending(x => x.ApiVersion))
                    {
                        options.SwaggerEndpoint(
                            $"{apiVersionDescription.GroupName}/swagger.json",
                            $"Version {apiVersionDescription.ApiVersion}");
                    }
                });
        }
    }
}
