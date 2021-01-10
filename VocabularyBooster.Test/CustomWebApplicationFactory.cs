using System;
using System.IO;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace VocabularyBooster.Test
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TestStartup>
    {
        protected override IHostBuilder CreateHostBuilder()
        {
            return new HostBuilder()
                .ConfigureAppConfiguration((context, builder) => { AppConfiguration(builder); })
                .UseContentRoot(Directory.GetCurrentDirectory())
                //.UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //webBuilder.UseSerilog();
                    webBuilder.UseStartup<TestStartup>();
                        //.UseSolutionRelativeContentRoot(".\\..\\..\\..\\..\\VocabularyBooster")
                })
                .UseServiceProviderFactory(new AutofacServiceProviderFactory());
        }

        //protected override IWebHostBuilder CreateWebHostBuilder()
        //{
        //    return WebHost.CreateDefaultBuilder(System.Array.Empty<string>())
        //        //.UseSerilog()
        //        .UseStartup<TestStartup>();
        //}

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (!string.IsNullOrEmpty(environmentName))
            {
                //throw new ArgumentException($"{nameof(CustomWebApplicationFactory<TStartup>)}.{nameof(this.ConfigureWebHost)} needs environment variable ASPNETCORE_ENVIRONMENT to set environment.");
                builder.UseEnvironment(environmentName);
            }
        }

        private static void AppConfiguration(IConfigurationBuilder config)
        {
            config.SetBasePath(Directory.GetCurrentDirectory())
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "appsettings.json", true)
                .AddJsonFile(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "appsettings.Local.json", true)
                .AddEnvironmentVariables();

            config.Build();
        }
    }
}
