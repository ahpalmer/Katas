using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AzureTestFunctionKatas;

public static class AzureFunctions
{
    [FunctionName("PythagoreanTheorem")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "PythagoreanTheorem/{sideA}/{sideB}")] HttpRequest req,
        double sideA,
        double sideB,
        ILogger log)
    {
        log.LogInformation("PythagoreanTheorem Function Processing request");

        // Change to Dependency Injection before you push
        PythagoreanTheorem pythagoreanTheorem = new PythagoreanTheorem();
        var answer = pythagoreanTheorem.Solve(sideA, sideB);
        log.LogInformation("Answer: {answer}", answer);

        return new OkObjectResult(answer.ToString());
    }
}
