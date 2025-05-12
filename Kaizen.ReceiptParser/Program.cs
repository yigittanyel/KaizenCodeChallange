using Kaizen.ReceiptParser.Services;
using Kaizen.ReceiptParser.Utils;

namespace Kaizen.ReceiptParser
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var jsonPath = "response.json";
            var words = JsonLoader.Load(jsonPath);

            var processor = new ReceiptProcessor();
            var lines = processor.Process(words);

            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
        }
    }
}
