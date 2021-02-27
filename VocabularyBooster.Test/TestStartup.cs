using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VocabularyBooster.Data.Graph;
using VocabularyBooster.FlashcardsService.Interface;
using VocabularyBooster.Service;
using VocabularyBooster.ServiceClient.Anki;

namespace VocabularyBooster.Test
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
            : base(configuration, hostingEnvironment)
        {
        }

        protected override void SetupDb(IServiceCollection services)
        {
            services.AddSingleton<GraphDbContext>();
        }

        protected override void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<WordService>().As<IWordService>();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<AnkiFlashcardService>().As<IFlashcardService>();
        }

        protected override void ConfigureOptionalServices(IApplicationBuilder app, IWebHostEnvironment env)
        {
            ServiceProviderAccessor.ServiceProvider = app?.ApplicationServices;
        }
    }
}
