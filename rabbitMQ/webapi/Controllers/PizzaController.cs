using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Queues;
using RabbitMQ.Client;
using webapi.Consumers;
using System.Threading.Tasks;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PizzaController : ControllerBase
    {
        private readonly PizzaClient pizzaClient;

        public PizzaController(PizzaClient pizzaClient)
        {
            this.pizzaClient = pizzaClient;
        }

        [HttpGet]
        public string Get([FromQuery]string pizza)
        {
            var response = this.pizzaClient.Call(pizza);
            return response;
        }
    }
}