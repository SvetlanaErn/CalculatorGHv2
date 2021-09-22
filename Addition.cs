using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CalculatorGHv2
{
    public static class Addition
    {
        public class TwoNumbers
        {
            public string n1;
            public string n2;
        }
        [FunctionName("Addition")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req, //   Route = .../api/Addition
            ILogger log)
        {

            log.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                TwoNumbers data = JsonConvert.DeserializeObject<TwoNumbers>(requestBody);
                float number1 = float.Parse(data.n1);
                float number2 = float.Parse(data.n2);
                return new OkObjectResult($"{number1}+{number2} = {(number1 + number2)}");
            }
            catch
            {
                return new BadRequestObjectResult("something went wrong");
            }
        }
    }
}
