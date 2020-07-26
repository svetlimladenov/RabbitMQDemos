using Infrastucture;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{
    [ApiController]   
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        private readonly ILogger logger;

        public ApiController(ILogger logger, IBus bus)
        {
            this.logger = logger;
            Bus = bus;
        }

        public IBus Bus { get; }

        protected void Publish(string exchange, string message)
        {
            this.logger.LogInformation($"Published to {exchange}, message - {message}");
            this.Bus.Publish(exchange, message);
        }
    }
}