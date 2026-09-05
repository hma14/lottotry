using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lottotry.BusinessTier
{
    public class MatchingTicket
    {
        public string Ticket { get; set; }
        public List<int> MatchedNumbers { get; set; }
        public int Matches => MatchedNumbers.Count;
    }
}