using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using VocabularyBooster.Core.GraphModel;
using VocabularyBooster.Data.Graph;
using VocabularyBooster.Service;
using Xunit;

namespace VocabularyBooster.Test.Integration
{
    [Collection(nameof(IntegrationCollectionDefinition))]
    public class WordServiceTest
    {
        private readonly TestHostFixture fixture;
        private readonly IWordService wordService;
        private readonly IUserService userService;

        public WordServiceTest(TestHostFixture fixture)
        {
            this.fixture = fixture;
            this.wordService = ServiceProviderAccessor.ServiceProvider.GetService<IWordService>();
            this.userService = ServiceProviderAccessor.ServiceProvider.GetService<IUserService>();
        }

        [Fact]
        public async Task AddOrUpdateWordTest()
        {
            var word = new Word()
            {
                Number = 456,
                Expression = "test",
                Definition = "a set of questions or exercises for measuring your knowledge of a subject, or your skill"
            };

            await this.wordService.AddOrUpdateWord(word);

            var returnedWord = await this.wordService.GetWord(word.Expression);
            returnedWord.Definition.ShouldBe(word.Definition);
        }

        [Fact]
        public async Task AddTextTest()
        {
            var text = new Text()
            {
                // Text after word Lemmatize
                Content = "if you have to sum up george washington 's life in one word , that word would have to be unforgettable . george 's story be one of travel and adventure , full of risk and , most of all , full of glory "
            };

            await this.wordService.AddText(text);
        }

        [Fact]
        public async Task MakeTextLearned()
        {
            var text = new Text()
            {
                // Text after word Lemmatize
                Content = "if you have to sum up george washington 's life in one word , that word would have to be unforgettable"
            };

            var textUuid = await this.wordService.AddText(text);

            var user = new User()
            {
                Email = "user@etfbl.net",
                FirstName = "FirstName",
                LastName = "LastName"
            };

            var userUuid = await this.userService.AddUser(user);

            await this.wordService.MakeTextLearned(userUuid: userUuid, textUuid: textUuid);
        }
    }
}
