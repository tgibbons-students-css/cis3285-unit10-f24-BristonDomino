using SingleResponsibilityPrinciple.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SingleResponsibilityPrinciple
{
    public class RestfulTradeDataProvider : ITradeDataProvider
    {
       private readonly string url;
       private readonly ILogger logger;
       private readonly HttpClient client = new HttpClient();

        public RestfulTradeDataProvider(string url, ILogger logger)
        {
            this.url = url;
            this.logger = logger;
        }

        public async IAsyncEnumerable<string> GetTradeDataAsync()
        {
            logger.LogInfo("Connecting to the Restful server using HTTP");

            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                using Stream stream = await response.Content.ReadAsStreamAsync();
                using StreamReader reader = new StreamReader(stream);

                while (!reader.EndOfStream)
                {
                    string line = await reader.ReadLineAsync();
                    yield return line;  // Yield each line as it’s read
                }
            }
            else
            {
                logger.LogWarning($"Failed to retrieve data. Status code: {response.StatusCode}");
                throw new Exception($"Error retrieving data from URL: {url}");
            }
        }

        public IEnumerable<string> GetTradeData()
        {
            Task<IAsyncEnumerable<string>> task = Task.FromResult(GetTradeDataAsync());
            List<string> tradeList = new List<string>();

            // Convert async enumerable to list for compatibility with synchronous method
            var enumerator = task.Result.GetAsyncEnumerator();
            while (enumerator.MoveNextAsync().Result)
            {
                tradeList.Add(enumerator.Current);
            }

            return tradeList;
        }
    }
}
