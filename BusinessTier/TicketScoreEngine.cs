using BusinessTier;
using DataAccessTier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Web;

namespace Lottotry.BusinessTier
{
    public class TicketScoreEngine
    {
        private List<int> Ticket { get; set; }

        private double RecentHits { get; set; }
        private double Overdue { get; set; }
        private double OddEven { get; set; }
        private double HighLow { get; set; }
        private double Distribution { get; set; }
        private double Consecutive { get; set; }
        private double Diversity { get; set; }
        private double HistoricalFrequency { get; set; }
        private double LastDigitDiversity { get; set; }

        private int Start { get; set; }
        private int Target { get; set; }
        private Database Db { get; set; }
        private SubStatistics[] Stat { get; set; }
        private int MaxNumber { get; set; }
        private int NumbersPerDraw { get; set; }

        private double TotalScore { get; set; }


        public TicketScoreEngine(List<int> ticket,
                            Database db,
                            SubStatistics[] stat,
                            int start,
                            int target,
                            int maxNumber,
                            int numbersPerDraw,
                            double recentHits = 0,
                            double overdue = 0,
                            double oddEven = 0,
                            double highLow = 0,
                            double distribution = 0,
                            double consecutive = 0,
                            double diversity = 0,
                            double historicalFrequency = 0,
                            double lastDigitDiversity = 0,
                            double totalScore = 0)
        {
            Ticket = ticket;
            RecentHits = recentHits;
            Overdue = overdue;
            OddEven = oddEven;
            HighLow = highLow;
            Distribution = distribution;
            Consecutive = consecutive;
            Diversity = diversity;
            HistoricalFrequency = historicalFrequency;
            LastDigitDiversity = lastDigitDiversity;
            TotalScore = totalScore;

            Db = db;
            Stat = stat;
            Start = start;
            Target = target;
            MaxNumber = maxNumber;
            NumbersPerDraw = numbersPerDraw;

        }

        public void Run()
        {
            ScoreRecentHits();
            ScoreOverdue();
            ScoreOddEven();
            ScoreHighLow();
            ScoreDistribution();
            ScoreConsecutive();
            ScoreDiversity();
           
            ScoreLastDigitDiversity();
            ScoreHistoricalFrequency();

            ScoreTotalScore();
        }
        public double GetRecentHits()
        {
            return RecentHits;
        }
        public double GetOverdue()
        {
            return Overdue;
        }
        public double GetOddEven()
        {
            return OddEven;
        }
        public double GetHighLow()
        {
            return HighLow;
        }
        public double GetDistribution()
        {
            return Distribution;
        }
        public double GetConsecutive()
        {
            return Consecutive;
        }
        public double GetDiversity()
        {
            return Diversity;
        }
        public int GetNumbersPerDraw()
        {
            return NumbersPerDraw;
        }

        public void ScoreRecentHits()
        {
            RecentHits = Ticket.Count(x => Stat[x].RelativeDist <= 5 && Stat[x].Cnt >= 2);
        }
        private void ScoreOverdue()
        {
            Overdue = Ticket.Count(n => Stat[n].RelativeDist > 10 & Stat[n].Cnt < 2);
        }
        private void ScoreOddEven() {
            int even = Ticket.Count(n => n % 2 == 0);
            double ideal = Ticket.Count / 2.0;
            double difference = Math.Abs(even - ideal);
            OddEven = Math.Max(0, 2 - difference);
        }
        private void ScoreHighLow()
        {
            int high = Ticket.Count(n => n > MaxNumber / 2);
            double ideal = Ticket.Count / 2.0;
            double difference = Math.Abs(high - ideal);
            HighLow = Math.Max(0, 2 - difference);
        }
        private void ScoreDistribution() {
            int zones = 7;
            int zoneSize = (int)Math.Ceiling((float) MaxNumber / zones);

            int[] counts = new int[zones];

            foreach (int n in Ticket)
            {
                int zone = Math.Min((n - 1) / zoneSize, zones - 1);
                counts[zone]++;
            }

            double score = 0;

            foreach (int count in counts)
            {
                switch (count)
                {
                    case 0:
                        score -= 2;      // Empty zone
                        break;

                    case 1:
                        score += 3;      // Perfect
                        break;

                    case 2:
                        score += 2;      // Still good
                        break;

                    default:
                        score -= count;  // Too many in one zone
                        break;
                }
            }
            Distribution = score;

        }
        private void ScoreConsecutive() {
            int consecutive = 0;
            for (int i = 0; i < Ticket.Count - 1; i++)
            {
                if (Ticket[i + 1] == Ticket[i] + 1)
                {
                    consecutive++;
                }
            }

            switch (consecutive)
            {
                case 0:
                    Consecutive = 10;
                    break;
                case 1:
                    Consecutive = 8;
                    break;
                case 2:
                    Consecutive = 3;
                    break;
                case 3:
                    Consecutive = 2;
                    break;
                default: Consecutive = 0;
                    break;
            }
        }
        private void ScoreDiversity() {
            int zones = 4;
            int zoneSize = (int)Math.Ceiling((double) MaxNumber / zones);

            int[] zoneCounts = new int[zones];

            foreach (int n in Ticket)
            {
                int zone = Math.Min((n - 1) / zoneSize, zones - 1);
                zoneCounts[zone]++;
            }

            double score = 0;

            foreach (int count in zoneCounts)
            {
                switch (count)
                {
                    case 0:
                        score -= 1.0;   // Empty zone
                        break;

                    case 1:
                        score += 2.0;   // Ideal
                        break;

                    case 2:
                        score += 1.0;   // Acceptable
                        break;

                    default:
                        score -= (count - 2);   // Penalize clustering
                        break;
                }
            }
            Diversity = score;
        }

        private void ScoreHistoricalFrequency()
        {
            double score = 0;
            foreach (int n in Ticket)
            {
                score += Stat[n].Cnt;
            }
            HistoricalFrequency = score;
        }
        private void ScoreLastDigitDiversity()
        {
            int[] lastDigitCounts = new int[10];
            foreach (int n in Ticket)
            {
                int lastDigit = n % 10;
                lastDigitCounts[lastDigit]++;
            }
            double score = 0;
            foreach (int count in lastDigitCounts)
            {
                switch (count)
                {
                    case int c when c == NumbersPerDraw - 2:
                        score -= 1.0;   // dup = 4
                        break;
                    case int c when c == NumbersPerDraw - 1:
                        score -= 2.0;   // dup = 5
                        break;
                    case int c when c == NumbersPerDraw:
                        score -= 3.0;   // dup = 6
                        break;
                    default:
                        //score += (NumbersPerDraw - count - NumbersPerDraw  / 2.0);   
                        score += (3 - count) / 2;
                        break;
                }
            }
            LastDigitDiversity = score;
        }
        private void BiildReasons(TicketScore ts)
        {
            if (ts.Consecutive >= 8 * 0.1)
                ts.Reasons.Add("No consecutive numbers");

            if (ts.Diversity >= 2 * 0.15)
                ts.Reasons.Add("Excellent zone coverage");

            if (ts.OddEven == Math.Ceiling(Ticket.Count / 2.0))
                ts.Reasons.Add("Balanced odd/even");

            if (ts.HighLow == Math.Ceiling(Ticket.Count / 2.0))
                ts.Reasons.Add("Balanced low/high");

        }

        private void ScoreTotalScore()
        {
            TotalScore = Distribution * 0.15
                    + Consecutive * 0.10
                    + OddEven * 0.10
                    + HighLow * 0.10
                    + Diversity * 0.15
                    + RecentHits * 0.10
                    + Overdue * 0.10
                    + HistoricalFrequency * 0.05
                    + LastDigitDiversity * 0.10;

        }
        public TicketScore GetTotalScore()
        {
            TicketScore ticketScore = new TicketScore
            {
                Numbers = Ticket,

                Distribution = Distribution * 0.15,              
                Consecutive = Consecutive * 0.10,
                OddEven = OddEven * 0.10,
                HighLow = HighLow * 0.10,                            
                Diversity = Diversity * 0.15,
                RecentHits = RecentHits * 0.10,
                Overdue = Overdue * 0.10,
                HistoricalFrequency = HistoricalFrequency * 0.05,                
                LastDigitDiversity = LastDigitDiversity * 0.10,
                
            };
            BiildReasons(ticketScore);
            return ticketScore;
        }
    }
}