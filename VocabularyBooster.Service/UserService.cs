using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Neo4j.Driver;
using VocabularyBooster.Core.GraphModel;
using VocabularyBooster.Data.Graph;
using VocabularyBooster.Data.Graph.Helper;

namespace VocabularyBooster.Service
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> logger;
        private readonly GraphDbContext graphDbContext;

        public UserService(ILogger<UserService> logger, GraphDbContext graphDbContext)
        {
            this.logger = logger;
            this.graphDbContext = graphDbContext;
        }

        public async Task<Guid> AddUser(User user)
        {
            return await this.graphDbContext.WriteTransaction(
                new ParameterDictionary()
                    .AddParameter("user", user),
                WordCypherQueries.AddUser,
                result => Guid.Parse(result.To<string>("userUuid")));
        }

        public async Task<User> GetUser(Guid userUuid)
        {
            return await this.graphDbContext.ReadTransactionSingle(
                new ParameterDictionary()
                    .AddParameter("userUuid", userUuid.ToString()),
                WordCypherQueries.GetUser,
                result => result.To<User>("user"));
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await this.graphDbContext.ReadTransactionSingle(
                new ParameterDictionary()
                    .AddParameter("email", email),
                WordCypherQueries.GetUserByEmail,
                result => result.To<User>("user"));
        }
    }
}
