using System;
using System.IO;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft;
using System.Threading.Tasks;
using System.Linq;

namespace Task_2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var initialCurrency = "";
            var finalCurrency = "";
            var value = 0M;
            Console.WriteLine("Task 2. Currency convertor by Andrey Basystyi.");
            Console.WriteLine("Rules: length of currency must be 3 characters.\nValue must be more than 0.");
            Console.Write("Enter initial currency -> ");
            while (string.IsNullOrEmpty(initialCurrency = Console.ReadLine().ToUpper()) || initialCurrency.Length != 3)
            {
                Console.Write("Invalid currency.\nEnter initial currency -> ");
            }
            Console.Write("Enter final currency -> ");
            while (string.IsNullOrEmpty(finalCurrency = Console.ReadLine().ToUpper()) || finalCurrency.Length != 3)
            {
                Console.Write("Invalid currency.\nEnter initial currency -> ");
            }
            Console.Write("Enter value -> ");
            while (true)
            {
                try
                {
                    value = decimal.Parse(Console.ReadLine());
                    if (value <= 0)
                    {
                        throw new Exception();
                    }
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid value.\nEnter value -> ");
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

            /*var jarray = JArray.Parse(body).ToList();
            Console.WriteLine(body);
            Console.WriteLine(jarray.Find(x => x["cc"].ToString() == finalCurrency)["rate"]);*/
            //var jobj = JObject.Parse(body);
            //var clone = JsonConvert.DeserializeObject<CurrencyPare>(body);
            
            /*foreach(var i in jarray)
            {
                Console.WriteLine($"{i["cc"]}: {i["rate"]}");
            }*/
        }
    }
}
