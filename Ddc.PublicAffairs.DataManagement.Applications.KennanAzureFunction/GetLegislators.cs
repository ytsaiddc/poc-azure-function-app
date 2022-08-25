using System.Collections.Generic;
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
    public class GetLegislators
    {
        private readonly ILogger<GetLegislators> _logger;

        public GetLegislators(ILogger<GetLegislators> log)
        {
            _logger = log;
        }

        [FunctionName("GetLegislators")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "LegislatorService" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(IList<Legislator>), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "legislators")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var legislators = new List<Legislator>
            {
                new Legislator("123", "Joe", "Smith", "CA"),
                new Legislator("456", "Steve", "O", "MA"),
                new Legislator("789", "Craig", "David", "NY")
            };

            return new OkObjectResult(legislators);
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

