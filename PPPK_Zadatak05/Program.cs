using Microsoft.Azure.Cosmos;
using PPPK_Zadatak05.CosmosServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var cosmosDbConfig = builder.Configuration.GetSection("CosmosDb");
var cosmosClient = new CosmosClient(cosmosDbConfig["Account"], cosmosDbConfig["Key"]);
builder.Services.AddSingleton<CosmosClient>(cosmosClient);

builder.Services.AddSingleton<ICosmosDBPersonService>(sp =>
    new CosmosDBPersonService(sp.GetRequiredService<CosmosClient>(), "pppkDatabase", "PersonContainer"));
builder.Services.AddSingleton<ICosmosDBItemService>(sp =>
    new CosmosDBItemService(sp.GetRequiredService<CosmosClient>(), "pppkDatabase", "ItemContainer"));


var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Person}/{action=Index}/{id?}");

app.Run();
