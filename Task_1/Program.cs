using System;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;

namespace Task_1
{
    class Program
    {
        static List<int> SimpleNumbers(Settings settings)
        {
            List<int> result = new List<int>();
            for (int i = (int)settings.PrimesFrom; i < settings.PrimesTo; i++)
            {
                bool flag = true;
                for (int j = 2; j <= Math.Sqrt(i) && flag; j++)
                {
                    if (i % j == 0) flag = false;
                }
                if (flag && i > 1)
                {
                    result.Add(i);
                }
            }
            return result;
        }

        static int Main(string[] args)
        {
            var options = new JsonSerializerOptions()
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            string duration = null;
            string errorMessage = null;
            bool success = true;
            int[] primes = null;
            int exitCode = 0;
            try {
                var clone = JsonSerializer.Deserialize<Settings>(File.ReadAllText("settings.json"), options);
                if (!clone.PrimesFrom.HasValue)
                {
                    throw new ArgumentNullException("PrimesFrom");
                }
                if (!clone.PrimesTo.HasValue)
                {
                    throw new ArgumentNullException("PrimesTo");
                }
                var durationTime = DateTime.Now.TimeOfDay;

                primes = SimpleNumbers(clone).ToArray();
                durationTime = DateTime.Now.TimeOfDay - durationTime;
                duration = durationTime.ToString().Split('.')[0];
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                success = false;
                duration = null;
                primes = null;
                exitCode = -1;
            }
            finally
            {
                var result = new Result(success, errorMessage, duration, primes);
                var json = JsonSerializer.Serialize(result, options);
                File.WriteAllText("result.json", json);
            }
            return exitCode;
        }
    }
}
