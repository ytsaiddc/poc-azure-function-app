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
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Legislator), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "legislators/{id:int}")] HttpRequest req, string id)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            //from Legislator contract?
            var legislator = new Legislator(id, "Joe", "Smith", "CA");
            //var result = new { status = 200, data = legislator };
            return new OkObjectResult(legislator);
        }

        class Legislator
        {
            public string Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }

            public string State { get; set; }

            public Legislator(string id, string firstName, string lastName, string state)
            {
                Id = id;
                FirstName = firstName;
                LastName = lastName;
                State = state;
            }
        }
    }
}

