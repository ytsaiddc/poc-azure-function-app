using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace Ddc.PublicAffairs.DataManagement.Applications.KennanAzureFunction
{
    public class GetLegislator
    {
        private readonly ILogger<GetLegislator> _logger;

        public GetLegislator(ILogger<GetLegislator> log/*TODO: add legislator contract and service dependency*/)
        {
            _logger = log;
        }

        [FunctionName("GetLegislator")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "LegislatorService" })]
        [OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "The **Legislator Id** path parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "legislators/{id:int?}")] HttpRequest req, int? id)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            /*string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";
            */
            
            //from Legislator contract?
            var legislator = new { id = id, firstname = "Joe", lastname = "Smith", state = "CA" };
            //var result = new { status = 200, data = legislator };
            return new OkObjectResult(JsonConvert.SerializeObject(legislator));
        }
    }
}

