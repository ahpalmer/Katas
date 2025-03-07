using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using AzureTestFunctionKatas.Services;

namespace AzureTestFunctionKatas.Controllers;

public class AzureFunctions
{
    private readonly IPythagoreanTheoremService _pythagoreanTheoremService;
    private readonly ILogger _logger;

    public AzureFunctions(
        IPythagoreanTheoremService pythagoreanTheoremService,
        ILogger<AzureFunctions> logger)
    {
        _pythagoreanTheoremService = pythagoreanTheoremService;
        _logger = logger;
    }

    [Function("PythagoreanTheorem")]
    public HttpResponseData Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "PythagoreanTheorem/{sideA}/{sideB}")] HttpRequestData req,
        double sideA,
        double sideB)
    {
        _logger.LogInformation("PythagoreanTheorem Function Processing request");
        // Change to Dependency Injection before you push
        var answer = _pythagoreanTheoremService.Solve(sideA, sideB);
        _logger.LogInformation("Answer: {answer}", answer);

        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
        response.WriteString(answer.ToString());

        return response;
    }
}
