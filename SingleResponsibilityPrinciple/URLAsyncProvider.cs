using SingleResponsibilityPrinciple.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleResponsibilityPrinciple
{
    public class URLAsyncProvider : ITradeDataProvider
    {
        private readonly ITradeDataProvider _baseProvider;

        public URLAsyncProvider(ITradeDataProvider baseProvider)
        {
            _baseProvider = baseProvider;
        }

        public IEnumerable<string> GetTradeData()
        {
            // Blocking call to GetTradeDataAsync, compatible with synchronous consumers
            List<string> resultList = new List<string>();
            var enumerator = GetTradeDataAsync().GetAsyncEnumerator();

            while (enumerator.MoveNextAsync().Result)
            {
                resultList.Add(enumerator.Current);
            }

            return resultList;
        }

        public async IAsyncEnumerable<string> GetTradeDataAsync()
        {
            // If _baseProvider supports IAsyncEnumerable, yield each line asynchronously
            await foreach (var trade in _baseProvider.GetTradeDataAsync())
            {
                yield return trade;
            }
        }
    }
}
