using Microsoft.Azure.Cosmos;
using SupportWebApp.Models;

namespace SupportWebApp.Services;

public class CosmosDbService
{
    private readonly Container _container;

    public CosmosDbService(IConfiguration configuration)
    {
        var connectionString = configuration["CosmosDb:ConnectionString"]
                               ?? throw new InvalidOperationException("CosmosDb connection string is missing.");

        var databaseName = configuration["CosmosDb:DatabaseName"]
                           ?? throw new InvalidOperationException("CosmosDb database name is missing.");

        var containerName = configuration["CosmosDb:ContainerName"]
                            ?? throw new InvalidOperationException("CosmosDb container name is missing.");

        var client = new CosmosClient(connectionString);
        _container = client.GetContainer(databaseName, containerName);
    }

    public async Task CreateSupportMessageAsync(SupportMessage message)
    {
        await _container.CreateItemAsync(
            message,
            new PartitionKey(message.Category));
    }
    
    public async Task<List<SupportMessage>> GetSupportMessagesAsync()
    {
        var messages = new List<SupportMessage>();

        var query = _container.GetItemQueryIterator<SupportMessage>(
            "SELECT * FROM c");

        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            messages.AddRange(response);
        }

        return messages;
    }
}