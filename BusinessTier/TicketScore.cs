using BusinessTier;
using DataAccessTier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Web;

namespace Lottotry.BusinessTier
{
    public class TicketScore
    {
        private List<int> Ticket { get; set; }

        private double RecentHits { get; set; }
        private double Overdue { get; set; }
        private double OddEven { get; set; }
        private double HighLow { get; set; }
        private double Distribution { get; set; }
        private double Consecutive { get; set; }
        private double Diversity { get; set; }
        private int Start { get; set; }
        private int Target { get; set; }
        private Database Db { get; set; }
        private SubStatistics[] Stat { get; set; }
        private int MaxNumber { get; set; }
        private int NumbersPerDraw { get; set; }


        private double TotalScore { get; set; }


        public TicketScore(List<int> ticket,
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
            int zones = Ticket.Count;               // Fantasy5=5, Lotto6=6
            int zoneSize = MaxNumber / zones;

            bool[] used = new bool[zones];

            foreach (int n in Ticket)
            {
                int zone = Math.Min((n - 1) / zoneSize, zones - 1);
                used[zone] = true;
            }

            int covered = used.Count(x => x);

            Distribution = covered * 2;     // 0-10
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
        private void ScoreTotalScore()
        {
            TotalScore = RecentHits * 0.15
                    + Overdue * 0.15
                    + OddEven * 0.10
                    + HighLow * 0.10
                    + Distribution * 0.20
                    + Consecutive * 0.10
                    + Diversity * 0.20;
        }
        public TicketNumberScore GetTotalScore()
        {
            return new TicketNumberScore { Numbers = Ticket, TotalScore = TotalScore };
        }
    }
}