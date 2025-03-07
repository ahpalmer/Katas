using Microsoft.Azure.Cosmos;

namespace AzureTestFunctionKatas.DataAccessLayer;

public class CosmosDbAccessor<T> : ICosmosDbAccessor<T> where T : class
{
    private readonly Container _container;

    public CosmosDbAccessor(CosmosClient cosmosClient, string databaseId, string containerId)
    {
        _container = cosmosClient.GetContainer(databaseId, containerId);
    }

    public async Task<T?> GetItemAsync(string id, string partitionKey)
    {
        try
        {
            ItemResponse<T> response = await _container.ReadItemAsync<T>(id, new PartitionKey(partitionKey));
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public async Task<IEnumerable<T>> GetItemsAsync(string query)
    {
        List<T> results = new();
        using FeedIterator<T> iterator = _container.GetItemQueryIterator<T>(query);

        while (iterator.HasMoreResults)
        {
            FeedResponse<T> response = await iterator.ReadNextAsync();
            results.AddRange(response);
        }

        return results;
    }

    public async Task AddItemAsync(T item, string partitionKey)
    {
        await _container.CreateItemAsync(item, new PartitionKey(partitionKey));
    }

    public async Task UpdateItemAsync(string id, T item, string partitionKey)
    {
        await _container.UpsertItemAsync(item, new PartitionKey(partitionKey));
    }

    public async Task DeleteItemAsync(string id, string partitionKey)
    {
        await _container.DeleteItemAsync<T>(id, new PartitionKey(partitionKey));
    }
}
