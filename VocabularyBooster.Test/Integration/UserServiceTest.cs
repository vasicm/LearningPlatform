using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using Shouldly;
using VocabularyBooster.Core.GraphModel;
using VocabularyBooster.Service;
using Xunit;

namespace VocabularyBooster.Test.Integration
{
    [Collection(nameof(IntegrationCollectionDefinition))]
    public class UserServiceTest
    {
        private readonly TestHostFixture fixture;
        private readonly IUserService userService;

        public UserServiceTest(TestHostFixture fixture)
        {
            this.fixture = fixture;
            this.userService = ServiceProviderAccessor.ServiceProvider.GetService<IUserService>();
        }

        [Fact]
        public void StemInDefault()
        {
            var client = new RestClient("http://localhost:8765/");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\r\n    \"action\": \"cardsInfo\",\r\n    \"version\": 6,\r\n    \"params\": {\r\n        \"cards\": [\r\n        1399117062404\r\n    ]\r\n    }\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            response.Content.ShouldNotBeNull();
        }

        [Fact]
        public async Task AddUserTest()
        {
            var user = new User
            {
                Email = "vasic.marko@mail.ru",
                FirstName = "Marko",
                LastName = "Vasic"
            };

            var userUuid = await this.userService.AddUser(user);

            var retUser = await this.userService.GetUser(userUuid);

            retUser.Email.ShouldBe(user.Email);
            retUser.FirstName.ShouldBe(user.FirstName);
            retUser.LastName.ShouldBe(user.LastName);
        }
    }
}