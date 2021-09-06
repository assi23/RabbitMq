using AnimesConsole.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;

namespace AnimesConsole
{
	public class Program
	{
		static void Main(string[] args)
		{
            var factory = new ConnectionFactory() { HostName = "192.168.0.140" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var anime = System.Text.Json.JsonSerializer.Deserialize<Animes>(body);
                    Console.WriteLine(" [x] Received {0}", anime.Name);
                };
                channel.BasicConsume(queue: "animes_queue",
                                                            autoAck: false,
                                                            consumer: consumer);
                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }

        }
    }
}
