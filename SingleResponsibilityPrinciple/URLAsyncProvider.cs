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
            Task<IEnumerable<string>> task = Task.Run(() => _baseProvider.GetTradeData());
            task.Wait();
            return task.Result;
        }
    }
}
