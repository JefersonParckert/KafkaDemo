using Confluent.Kafka;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace KafkaDemoConsumer.Consumer
{
    public class ConsumerKafka
    {
        private readonly ConsumerConfig _consumerConfig;

        public ConsumerKafka()
        {
            _consumerConfig = new ConsumerConfig
            {
                GroupId = "test-consumer-group",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
        }
        public async Task ReadMessage()
        {
            using (var c = new ConsumerBuilder<Ignore, string>(_consumerConfig).Build())
            {
                c.Subscribe("DemoKafka");

                CancellationTokenSource cts = new CancellationTokenSource();
                Console.CancelKeyPress += (_, e) =>
                {
                    e.Cancel = true; // prevent the process from terminating.
                    cts.Cancel();
                };

                try
                {
                    while (true)
                    {
                        try
                        {
                            var cr = c.Consume(cts.Token);
                            Console.WriteLine($"Consumed message '{cr.Value}' at: '{cr.TopicPartitionOffset}'.");
                        }
                        catch (ConsumeException e)
                        {
                            Console.WriteLine($"Error occured: {e.Error.Reason}");
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    // Ensure the consumer leaves the group cleanly and final offsets are committed.
                    c.Close();
                }
            }
        }
    }
}