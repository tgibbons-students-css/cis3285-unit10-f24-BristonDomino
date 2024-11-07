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
        private readonly ITradeDataProvider origProvider;

        public AdjustTradeDataProvider(ITradeDataProvider origProvider)
        {
            this.origProvider = origProvider;
        }

        public IEnumerable<string> GetTradeData()
        {
            // call orignal GetTradeData
            IEnumerable<string> lines = origProvider.GetTradeData();

            List<string> result = new List<string>();
            foreach (String line in lines)
            {
                String newLine = line.Replace("GBP", "EUR");
                result.Add(newLine);
            }

            return lines;
        }
    }

}
