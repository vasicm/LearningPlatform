using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using VocabularyBooster.Core.GraphModel;
using VocabularyBooster.Service;

namespace VocabularyBooster.Controllers
{
    [Route(CommonRoute.BaseApiRoute + "/v{api-version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing",
            "Bracing",
            "Chilly",
            "Cool",
            "Mild",
            "Warm",
            "Balmy",
            "Hot",
            "Sweltering",
            "Scorching",
        };

        private readonly ILogger<WeatherForecastController> logger;
        private readonly IWordService wordService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWordService wordService)
        {
            this.logger = logger;
            this.wordService = wordService;
        }

        [HttpGet]
        [SwaggerOperation(OperationId = nameof(Get))]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)],
            })
            .ToArray();
        }
    }
}
