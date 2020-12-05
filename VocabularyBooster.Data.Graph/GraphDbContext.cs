using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Neo4j.Driver;

namespace VocabularyBooster.Data.Graph
{
    public sealed class GraphDbContext : IDisposable
    {
        public GraphDbContext(GraphDbOptions options)
        {
            this.Driver = GraphDatabase.Driver(new Uri(options.Uri), AuthTokens.Basic(options.Username, options.Password));
        }

        public IDriver Driver { get; }

        public void Dispose()
        {
            this.Driver?.Dispose();
        }


        /// <summary>
        /// Execute the cypher query with parameters.
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="query"></param>
        /// <param name="operation"></param>
        /// <returns>True if at least one object has been created/updated.</returns>
        public async Task<T> WriteTransaction<T>(Dictionary<string, object> parameters, string query, Func<IRecord, T> operation = null)
        {
            return await this.WriteTransactionAsync(parameters, query, async result =>
            {
                IRecord record = await result.SingleAsync();
                if (operation != null)
                {
                    return operation(record);
                }

                return default;
            });
        }

        /// <summary>
        /// Execute the cypher query with parameters.
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="query"></param>
        /// <returns>True if at least one object has been created/updated.</returns>
        public async Task<bool> WriteTransaction(Dictionary<string, object> parameters, string query)
        {
            return await this.WriteTransactionAsync(parameters, query, async result => (await result.ConsumeAsync()).Counters.ContainsUpdates);
        }

        /// <summary>
        /// Execute the cypher query with parameters.
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="query"></param>
        /// <param name="operation"></param>
        /// <returns>True if at least one object has been created/updated.</returns>
        public async Task<T> WriteTransactionAsync<T>(Dictionary<string, object> parameters, string query, Func<IResultCursor, Task<T>> operation = null)
        {
            bool status;
            T value = default(T);

            IAsyncSession session = this.Driver.AsyncSession();
            {
                status = await session.WriteTransactionAsync(async tx =>
                {
                    IResultCursor result = await tx.RunAsync(query, parameters);
                    if (operation != null)
                    {
                        value = await operation(result);
                    }

                    return (await result.ConsumeAsync()).Counters.ContainsUpdates;
                });
            }

            return value;
        }

        /// <summary>
        /// Executes one more cypher queries in a single transaction.
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="queries"></param>
        /// <returns>True if at least one of the queries has created/updated some object.</returns>
        public async Task<bool> WriteTransactions(Dictionary<string, object> parameters, string[] queries)
        {
            var status = false;

            IAsyncSession session = this.Driver.AsyncSession();
            {
                status = await session.WriteTransactionAsync(async tx =>
                {
                    bool updates = false;
                    foreach (string query in queries)
                    {
                        IResultCursor result = await tx.RunAsync(query, parameters);
                        if ((await result.ConsumeAsync()).Counters.ContainsUpdates)
                        {
                            updates = true;
                        }
                    }

                    return updates;
                });
            }

            return status;
        }

        public async Task<List<T>> ReadTransaction<T>(Dictionary<string, object> parameters, string statementText, Func<IRecord, T> operation)
        {
            var contents = new List<T>();

            IAsyncSession session = this.Driver.AsyncSession();
            {
                await session.ReadTransactionAsync(async tx =>
                {
                    IResultCursor result = await tx.RunAsync(statementText, parameters);

                    contents = await result.ToListAsync(operation);
                });
            }

            return contents;
        }

        public async Task<T> ReadTransactionSingle<T>(Dictionary<string, object> parameters, string statementText, Func<IRecord, T> operation)
        {
            var contents = default(T);

            IAsyncSession session = this.Driver.AsyncSession();
            {
                await session.ReadTransactionAsync(async tx =>
                {
                    IResultCursor result = await tx.RunAsync(statementText, parameters);

                    contents = await result.SingleAsync(operation);
                });
            }

            return contents;
        }
    }
}
