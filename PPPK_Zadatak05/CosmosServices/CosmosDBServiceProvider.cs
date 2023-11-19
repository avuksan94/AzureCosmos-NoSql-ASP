using Microsoft.Azure.Cosmos;

namespace PPPK_Zadatak05.CosmosServices
{
    public static class CosmosDbServiceProvider
    {
        private const string DatabaseName = "pppkDatabase";

        private const string ContainerNamePerson = "PersonContainer";
        private const string ContainerNameItem = "ItemContainer";
        private const string Account = "https://pppkazurenosql.documents.azure.com:443/";
        private const string Key = "mJQ0NFL7F38isKOLYAGz3DRdGnQwShKVvW9dduwmXHvW1Bpf76yqm0Sy5uEbUV6L0Lc9s6oqsr2tACDbVCyeBg==";

        private static ICosmosDBPersonService _cosmosDBPersonService;
        private static ICosmosDBItemService _cosmosDBItemService;

        public static ICosmosDBPersonService CosmosDBPersonService { get => _cosmosDBPersonService; }
        public static ICosmosDBItemService CosmosDBItemService { get => _cosmosDBItemService; }

        public async static Task Init()
        {
            CosmosClient client = new CosmosClient(Account, Key);
            _cosmosDBPersonService = new CosmosDBPersonService(client, DatabaseName, ContainerNamePerson);
            _cosmosDBItemService = new CosmosDBItemService(client, DatabaseName, ContainerNameItem);
            DatabaseResponse database = await client.CreateDatabaseIfNotExistsAsync(DatabaseName);
            await database.Database.CreateContainerIfNotExistsAsync(ContainerNamePerson, "/id");
            await database.Database.CreateContainerIfNotExistsAsync(ContainerNameItem, "/id");
        }
    }
}
