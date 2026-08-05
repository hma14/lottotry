using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lottotry.BusinessTier
{
    public class TicketScore
    {
        public List<int> Numbers { get; set; }
        public double RecentHits { get; set; }
        public double Overdue { get; set; }

        public double Distribution { get; set; }

        public double Consecutive { get; set; }

        public double OddEven { get; set; }

        public double HighLow { get; set; }

        public double Diversity { get; set; }

        public double HistoricalFrequency { get; set; }

        public double LastDigitDiversity { get; set; }

        public List<string> Reasons { get; set; }
            = new List<string>();

        public double Total =>
            Distribution +
            Consecutive +
            OddEven +
            HighLow +
            Diversity +
            RecentHits +
            Overdue + 
            HistoricalFrequency +
            LastDigitDiversity;
    }
}