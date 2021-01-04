using Confluent.Kafka;
using System.Threading.Tasks;
using System;

namespace KafkaDemoProducer.Producer
{
    public class ProducerKafka
    {
        public async Task SendMessageKafka(string message)
        {
            var config = new ProducerConfig { BootstrapServers = "localhost:9092" };

            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    var result = await producer.ProduceAsync("DemoKafka", new Message<Null, string> { Value = message });
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                }
            }
        }
    }
}