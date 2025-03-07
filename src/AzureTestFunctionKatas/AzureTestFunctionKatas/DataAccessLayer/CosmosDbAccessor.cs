using Microsoft.Azure.Cosmos;
using System.Data;

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
        // Get ETag using reflection (or use a more specific approach for your models)
        var etagProperty = typeof(T).GetProperty("ETag");
        string etag = etagProperty?.GetValue(item) as string;

        // Create request options with the ETag if available
        ItemRequestOptions requestOptions = null;
        if (!string.IsNullOrEmpty(etag))
        {
            requestOptions = new ItemRequestOptions
            {
                IfMatchEtag = etag // This is the key for concurrency control
            };
        }

        // Update with ETag condition
        try
        {
            await _container.UpsertItemAsync(item, new PartitionKey(partitionKey), requestOptions);
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
        {
            // Handle the case where someone else updated the document
            throw new ConcurrencyException($"The item with id {id} was modified by another process", ex);
        }

        //await _container.UpsertItemAsync(item, new PartitionKey(partitionKey));
    }

    public async Task DeleteItemAsync(string id, string partitionKey)
    {
        await _container.DeleteItemAsync<T>(id, new PartitionKey(partitionKey));
    }
}

// 3. Define a custom exception for concurrency issues
public class ConcurrencyException : Exception
{
    public ConcurrencyException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
