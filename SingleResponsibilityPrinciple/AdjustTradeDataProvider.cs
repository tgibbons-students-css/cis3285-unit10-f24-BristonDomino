using SingleResponsibilityPrinciple.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleResponsibilityPrinciple
{
    public class AdjustTradeDataProvider : ITradeDataProvider
    {
        private ITradeDataProvider _origProvider;

        public AdjustTradeDataProvider(ITradeDataProvider origProvider)
        {
            _origProvider = origProvider;
        }

        public IEnumerable<string> GetTradeData()
        {
            // call orignal GetTradeData
            IEnumerable<string> lines = _origProvider.GetTradeData();

            List<string> result = new List<string>();
            foreach (string line in lines)
            {
                string newLine = line.Replace("GBP", "EUR");
                result.Add(newLine);
            }

            return result;
        }
    }

}
