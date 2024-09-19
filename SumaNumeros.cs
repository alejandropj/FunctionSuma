using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FunctionSuma
{
    public class SumaNumeros
    {
        private readonly ILogger<SumaNumeros> _logger;

        public SumaNumeros(ILogger<SumaNumeros> logger)
        {
            _logger = logger;
        }

        [Function("SumaNumeros")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            string num1 = req.Query["num1"];
            string num2 = req.Query["num2"];

            if (string.IsNullOrEmpty(num1) || string.IsNullOrEmpty(num2) ||
            !double.TryParse(num1, out double num1Sum) || !double.TryParse(num2, out double num2Sum))
            {
                return new BadRequestObjectResult("Introduce 2 números válidos en num1 y num2. Por ejemplo: ?num1=5&num2=3");
            }

            double suma = num1Sum + num2Sum;
            return new OkObjectResult("Enhorabuena, tu suma es" + suma);
        }
    }
}
