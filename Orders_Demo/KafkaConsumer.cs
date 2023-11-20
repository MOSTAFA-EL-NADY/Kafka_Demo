using Confluent.Kafka;
using Orders_Demo.Models;
using System.Text.Json;

namespace Orders_Demo

{
    public class KafkaConsumerHandler : BackgroundService
    {

        private readonly string topicName = "productTopic";

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // customize you configuration
            ConsumerConfig _consumerConfig = new ConsumerConfig
            {
                BootstrapServers = "kafka:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                ClientId = "Orders",
                GroupId = "my-group",

            };

            using (var consumer = new ConsumerBuilder<Ignore, string>(_consumerConfig)
                .SetErrorHandler((_, ex) => Console.WriteLine(ex.Reason))
                .Build())
            {
                // consumer now listen to that topic if any messages send by producer it will be consumed here 
                consumer.Subscribe(topicName);

                try
                {
                    while (true)
                    {
                        var consumerResult = consumer.Consume();
                        var prodcut = JsonSerializer.Deserialize<Product>(consumerResult.Message.Value);
                        // i have the product sent from product App ready to be processed
                        // write you implementation
                        Console.WriteLine($" Product Has Been received form PorductApp: => {prodcut.ToString()}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException.Message);
                    throw ex;
                }
                finally
                {
                    consumer.Close();
                }
            }
        }
    }

}


