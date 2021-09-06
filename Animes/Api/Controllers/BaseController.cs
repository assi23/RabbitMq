using Data.Models;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<M> : Controller where M : Base
    {
        [HttpPost]
        public void Post(M model)
        {
            var factory = new ConnectionFactory() { HostName = "192.168.0.140" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                        queue: "animes_queue",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                        );

                string message = System.Text.Json.JsonSerializer.Serialize(model);
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "",
                                                routingKey: "animes_queue",
                                                basicProperties: null,
                                                body: body);
            }

        }
    }
}
