using System;
using KafkaDemoProducer.Producer;
using System.Threading.Tasks;

namespace KafkaDemoProducer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Iniciando Processamento!");

            var producer = new ProducerKafka();

            await producer.SendMessageKafka($"Teste {DateTime.Now}");

            Console.WriteLine("Processamento finalizado!");
            Console.ReadKey();
        }
    }
}
