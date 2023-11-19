using PPPK_Zadatak05.Models;

namespace PPPK_Zadatak05.CosmosServices
{
    public interface ICosmosDBItemService
    {
        Task<IEnumerable<Item>> GetItemsAsync(string queryString);
        Task<Item> GetItemAsync(string id);
        Task AddItemAsync(Item person);
        Task UpdateItemAsync(Item person);
        Task DeleteItemAsync(Item person);
    }
}
