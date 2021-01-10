using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using VocabularyBooster.Options;
using Xunit;

namespace VocabularyBooster.Test
{
    public class TestHostFixture : ICollectionFixture<CustomWebApplicationFactory<TestStartup>>
    {
        public const string BaseUri = "http://localhost:54045";

        public TestHostFixture()
        {
            this.Factory = new CustomWebApplicationFactory<TestStartup>();
#pragma warning disable CA2000 // Dispose objects before losing scope
            this.Factory2 = new WebApplicationFactory<TestStartup>().WithWebHostBuilder(builder =>
#pragma warning restore CA2000 // Dispose objects before losing scope
            {
                builder.ConfigureServices(services => { });
            });
            this.ClientOptions = new WebApplicationFactoryClientOptions { BaseAddress = new Uri(BaseUri) };
            this.Client = this.Factory.CreateClient(this.ClientOptions);
            //this.Factory.CreateClient(this.ClientOptions);
            this.Server = this.Factory.Server;
            //this.Settings = GetApplicationSettings(GetAppSettingsPath());
        }

        public CustomWebApplicationFactory<TestStartup> Factory { get; }

        public WebApplicationFactory<TestStartup> Factory2 { get; }

        public WebApplicationFactoryClientOptions ClientOptions { get; }

        public HttpClient Client { get; }

        public TestServer Server { get; }

        //public ApplicationSettings Settings { get; }
    }
}
