using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serialization;
using VocabularyBooster.Core;
using VocabularyBooster.FlashcardsService.Interface;
using VocabularyBooster.FlashcardsService.Interface.Model;
using VocabularyBooster.ServiceClient.Anki.Model;

namespace VocabularyBooster.ServiceClient.Anki
{
    public class AnkiFlashcardService : IFlashcardService
    {
        private const string FindCardsActionValue = "findCards";
        private const string CardsInfoActionValue = "cardsInfo";
        private const string CreateDeckActionValue = "createDeck";
        private const string DeleteDecksActionValue = "deleteDecks";
        private const string ChangeDeckActionValue = "changeDeck";

        private const string CardsParamValue = "cards";
        private const string QueryParamValue = "query";
        private const string DeckParamValue = "deck";
        private const string DecksParamValue = "decks";
        private const string CardsTooParamValue = "cardsToo";

        private readonly RestClient client;
        private readonly JsonSerializerSettings jsonSerializerSettings;
        private readonly AnkiApiOptions ankiApiOptions;

        public AnkiFlashcardService(AnkiApiOptions ankiApiOptions)
        {
            this.ankiApiOptions = ankiApiOptions;
            this.client = new RestClient(this.ankiApiOptions.BaseUri)
            {
                Timeout = -1
            };

            this.jsonSerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Include,
                ContractResolver = new FirstCharacterLowercaseContractResolver(),
            };
        }

        public async Task<List<long>> FindCards(string query)
        {
            return await this.ExecuteRequest<List<long>>(
                            FindCardsActionValue,
                            new Params().AddParameter(QueryParamValue, query));
        }

        public async Task<List<FlashCard>> GetCardsInfo(List<long> cardIdList)
        {
            return await this.ExecuteRequest<List<FlashCard>>(
                            CardsInfoActionValue,
                            new Params().AddParameter(CardsParamValue, cardIdList));
        }

        public async Task<long> CreateDeck(string deckName)
        {
            return await this.ExecuteRequest<long>(
                CreateDeckActionValue,
                new Params().AddParameter(DeckParamValue, deckName));
        }

        public async Task DeleteDecks(List<string> deckNameList, bool cardsToo)
        {
            await this.ExecuteRequest<object>(
                DeleteDecksActionValue,
                new Params()
                    .AddParameter(DecksParamValue, deckNameList)
                    .AddParameter(CardsTooParamValue, cardsToo));
        }

        public async Task ChangeDeck(string deckName, List<long> cardIdList)
        {
            await this.ExecuteRequest<object>(
                ChangeDeckActionValue,
                new Params()
                    .AddParameter(DeckParamValue, deckName)
                    .AddParameter(CardsParamValue, cardIdList));
        }

        private async Task<T> ExecuteRequest<T>(string action, Params prm)
        {
            var response = await this.ExecuteRequest<T>(new Request()
                                    {
                                        Action = action,
                                        Version = this.ankiApiOptions.Version,
                                        Params = prm
                                    });

            if (response?.Error != null)
            {
                throw new FlashcardServiceException(response.Error.ToString());
            }

            return (response != null) ? response.Result : default;
        }

        private async Task<Response<T>> ExecuteRequest<T>(Request request)
        {
            IRestResponse restResponse = await this.client.ExecuteAsync(
                                                            new RestRequest(Method.POST)
                                                                .AddHeader("Content-Type", ContentType.Json)
                                                                .AddParameter(
                                                                    ContentType.Json,
                                                                    JsonConvert.SerializeObject(request, this.jsonSerializerSettings),
                                                                    ParameterType.RequestBody));

            return JsonConvert.DeserializeObject<Response<T>>(restResponse.Content, this.jsonSerializerSettings);
        }
    }
}
