using System.IO;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace VocabularyBooster
{
    public sealed class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseServiceProviderFactory(new AutofacServiceProviderFactory());

        // public static IHostBuilder CreateHostBuilder(string[] args)
        // {
        //     var hostBuilder = Host.CreateDefaultBuilder(args)
        //         .ConfigureAppConfiguration((context, builder) =>
        //         {
        //             // AppConfiguration(builder, args);
        //         })
        //         .UseContentRoot(Directory.GetCurrentDirectory())
        //         // .UseSerilog()
        //         .ConfigureWebHostDefaults(webBuilder =>
        //         {
        //             webBuilder
        //                 .UseKestrel(
        //                     (builderContext, options) =>
        //                     {
        //                         // Do not add the Server HTTP header.
        //                         options.AddServerHeader = false;
        //                         // Configure Kestrel from appsettings.json.
        //                         // options.Configure(builderContext.Configuration.GetSection(nameof(ApplicationOptions.Kestrel)));
        //                     })
        //                 .UseDefaultServiceProvider((context, options) =>
        //                     options.ValidateScopes = context.HostingEnvironment.IsDevelopment())
        //                 // .UseSerilog()
        //                 .UseStartup<Startup>();
        //         })
        //         .UseServiceProviderFactory(new AutofacServiceProviderFactory());
        //     return hostBuilder;
        // }
    }
}
