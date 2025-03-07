using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureTestFunctionKatas.DataAccessLayer;

public interface ICosmosDbAccessor<T> where T : class
{
    Task<T> GetItemAsync(string id, string partitionKey);
    Task<IEnumerable<T>> GetItemsAsync(string query);
    Task AddItemAsync(T item, string partitionKey);
    Task UpdateItemAsync(string id, T item, string partitionKey);
    Task DeleteItemAsync(string id, string partitionKey);
}
