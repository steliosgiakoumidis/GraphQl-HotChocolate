using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GraphQL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly AuthorRepository _authorRepository;

        public WeatherForecastController(AuthorRepository authorRepository, ILogger<WeatherForecastController> logger)
        {
            _authorRepository = authorRepository;
            _logger = logger;
        }

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var aa = _authorRepository.GetAllDAuthorsOnly();
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
        }
    }
}