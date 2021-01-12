using System;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Linq;

namespace Task_2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var sourceCurrency = "";
            var destinationCurrency = "";
            var amount = 0M;
            Console.WriteLine("Task 2. Currency convertor by Andrey Basystyi.");
            Console.WriteLine("Rules: length of currency must be 3 characters.\nAmount must be more than 0.");
            Console.Write("Source currency: ");
            while (string.IsNullOrEmpty(sourceCurrency = Console.ReadLine().ToUpper())
                   || sourceCurrency.Length != 3)
            {
                Console.Write("Invalid currency.\nEnter source currency: ");
            }
            Console.Write("Destination currency: ");
            while (string.IsNullOrEmpty(destinationCurrency = Console.ReadLine().ToUpper())
                   || destinationCurrency.Length != 3)
            {
                Console.Write("Invalid currency.\nEnter destination currency: ");
            }
            Console.Write("Amount: ");
            while (true)
            {
                try
                {
                    amount = decimal.Parse(Console.ReadLine());
                    if (amount <= 0)
                    {
                        throw new Exception();
                    }
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid amount.\nEnter amount: ");
                }
            }
            
            var uri = new Uri("https://bank.gov.ua");
            var httpClient = new HttpClient();
            httpClient.BaseAddress = uri;
            httpClient.Timeout = new TimeSpan(0, 0, 10);

            HttpResponseMessage response;
            try
            {
                response = await httpClient.GetAsync("/NBUStatService/v1/statdirectory/exchange?json");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to update exchange rates.");
                return;
            }

            var body = await response.Content.ReadAsStringAsync();
            File.WriteAllText("cache.json", body);
            var cache = "";
            try
            {
                cache = File.ReadAllText("cache.json");
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to open file: cache.json");
                return;
            }
            var jArray = JArray.Parse(cache).ToList();
            try
            {
                var sourceCachePair = jArray.Where(x => x["cc"].ToString() == sourceCurrency)
                              .Select(x => JsonConvert.DeserializeObject<CurrencyPair>(x.ToString()))
                              .First();
                var destinationCachePair = jArray.Where(x => x["cc"].ToString() == destinationCurrency)
                              .Select(x => JsonConvert.DeserializeObject<CurrencyPair>(x.ToString()))
                              .First();
                if (sourceCachePair.Currency == destinationCachePair.Currency)
                {
                    throw new Exception();
                }
                var date = jArray.Select(x => x["exchangedate"]).First().ToString();
                var rate = Math.Round(sourceCachePair.Rate / destinationCachePair.Rate, 2);

                Console.WriteLine($"{amount} {sourceCachePair.Currency} x {rate} = {Math.Round(amount * rate, 2)} " +
                                  $"{destinationCachePair.Currency} (from {date})");
            }
            catch (Exception)
            {
                Console.WriteLine($"Error: pair {sourceCurrency} and {destinationCurrency} not found.");
                return;
            }
        }
    }
}
