using System;
using KafkaDemoConsumer.Consumer;
using System.Threading.Tasks;

namespace KafkaDemoConsumer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Iniciando Processamento!");

            var consumer = new ConsumerKafka();

            await consumer.ReadMessage();

            Console.WriteLine("Processamento finalizado!");
            Console.ReadKey();
        }
    }
}
