using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureTestFunctionKatas.DataAccessLayer;

public class CosmosDbAccessor : ICosmosDbAccessor
{
    public CosmosDbAccessor(IConfiguration configuration)
    {
        _configuration = configuration;
    }
}
