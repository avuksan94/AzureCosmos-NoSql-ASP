using Microsoft.Azure.Cosmos;
using PPPK_Zadatak05.Models;
using System.Collections.Concurrent;
using System.ComponentModel;

namespace PPPK_Zadatak05.CosmosServices
{
    public class CosmosDBItemService : ICosmosDBItemService
    {
        private readonly Microsoft.Azure.Cosmos.Container container;

        public CosmosDBItemService(CosmosClient cosmosClient, string databaseName, string containerName)
        {
            container = cosmosClient.GetContainer(databaseName, containerName);
        }

        public async Task AddItemAsync(Item item) => await container.CreateItemAsync(item, new PartitionKey(item.Id));
        public async Task DeleteItemAsync(Item item) => await container.DeleteItemAsync<Item>(item.Id, new PartitionKey(item.Id));
        public async Task<Item> GetItemAsync(string id) 
        {
            try
            {
                return await container.ReadItemAsync<Item>(id, new PartitionKey(id));
            }
            catch (CosmosException e) when (e.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(string queryString) {
            List<Item> items = new List<Item>();

            var query = container.GetItemQueryIterator<Item>(new QueryDefinition(queryString));
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                items.AddRange(response.ToList());
            }
            return items;
        }
        public async Task UpdateItemAsync(Item item) => await container.UpsertItemAsync(item, new PartitionKey(item.Id));
    }
}
