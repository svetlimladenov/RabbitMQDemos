using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Messages;
using Infrastucture;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Clients;

namespace WebApi.Controllers
{
    public class WeatherForecastController : ApiController
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IBus bus)
            :base(logger, bus)
        {
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get([FromQuery]string username)
        {
            var message = new GetWeatherMessage()
            {
                Username = username
            };

            Publish(message);

            return new List<WeatherForecast>();
        }
    }
}
