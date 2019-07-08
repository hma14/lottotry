using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DataAccessTier;
namespace BusinessTier
{
    public class CandidateLists
    {
        const int veryHot = 0;
        const int RAND_GEN_NUM = 20;
        protected int SEMI_HOT_MAX_SELECTED;
        protected int HOT_MAX_SELECTED;
        protected int VERY_HOT_MAX_SELECTED;
        protected int SEMI_COLD_MAX_SELECTED;
        protected int COLD_MAX_SELECTED;
        protected int VERY_COLD_MAX_SELECTED;

        protected int hotRangeMin;
        protected int hotRangeMax;

        protected int semiHotRangeMin;
        protected int semiHotRangeMax;

        protected int coldRangeMin;
        protected int coldRangeMax;

        protected int semiColdRangeMin;
        protected int semiColdRangeMax;

        protected int veryCold;

        protected List<SubStatistics> hotNumList;
        protected List<SubStatistics> semiHotNumList;
        protected List<SubStatistics> veryHotNumList;
        protected List<SubStatistics> coldNumList;
        protected List<SubStatistics> semiColdNumList;
        protected List<SubStatistics> veryColdNumList;
        protected List<SubStatistics> randNumList;

        protected List<SubStatistics> candidateNumList;
        protected List<SubStatistics> uniqueCandidateNumList;

        public CandidateLists(NumGen numGen) : this(20, 1, 5, 5, 5, 9, 9, 15, 20, 15, 9, 15, 3, 20, 3, numGen) { }

        public CandidateLists(int nHot, int hotMin, int hotMax,
                                int nSemiHot, int semiHotMin, int semiHotMax,
                                int nCold, int coldMin, int coldMax,
                                int nSemiCold, int semiColdMin, int semiColdMax,
                                int nVeryCold, int veryC,
                                int nVeryHot
                             )
        {
            HOT_MAX_SELECTED = nHot;
            hotRangeMin = hotMin;
            hotRangeMax = hotMax;
            SEMI_HOT_MAX_SELECTED = nSemiHot;
            semiHotRangeMin = semiHotMin;
            semiHotRangeMax = semiHotMax;
            COLD_MAX_SELECTED = nCold;
            coldRangeMin = coldMin;
            coldRangeMax = coldMax;
            SEMI_COLD_MAX_SELECTED = nSemiCold;
            semiColdRangeMin = semiColdMin;
            semiColdRangeMax = semiColdMax;
            VERY_COLD_MAX_SELECTED = nVeryCold;
            veryCold = veryC;
            VERY_HOT_MAX_SELECTED = nVeryHot;
        }

        public CandidateLists(int nHot, int hotMin, int hotMax,
                                int nSemiHot, int semiHotMin, int semiHotMax,
                                int nCold, int coldMin, int coldMax,
                                int nSemiCold, int semiColdMin, int semiColdMax,
                                int nVeryCold, int veryC,
                                int nVeryHot, NumGen numGen
                             )
        {
            HOT_MAX_SELECTED = nHot;
            hotRangeMin = hotMin;
            hotRangeMax = hotMax;
            SEMI_HOT_MAX_SELECTED = nSemiHot;
            semiHotRangeMin = semiHotMin;
            semiHotRangeMax = semiHotMax;
            COLD_MAX_SELECTED = nCold;
            coldRangeMin = coldMin;
            coldRangeMax = coldMax;
            SEMI_COLD_MAX_SELECTED = nSemiCold;
            semiColdRangeMin = semiColdMin;
            semiColdRangeMax = semiColdMax;
            VERY_COLD_MAX_SELECTED = nVeryCold;
            veryCold = veryC;
            VERY_HOT_MAX_SELECTED = nVeryHot;
            genLists(numGen);
        }

        virtual protected void genLists(NumGen numGen)
        {
            hotNumList = new List<SubStatistics>();
            semiHotNumList = new List<SubStatistics>();
            veryHotNumList = new List<SubStatistics>();
            coldNumList = new List<SubStatistics>();
            semiColdNumList = new List<SubStatistics>();
            veryColdNumList = new List<SubStatistics>();
            candidateNumList = new List<SubStatistics>();
            uniqueCandidateNumList = new List<SubStatistics>();
            randNumList = new List<SubStatistics>();

            SubStatistics[] stat = numGen.Stat;
            for (int i = 1; i <= numGen.ScaleLength; ++i)
            {
                if (stat[i].RelativeDist > semiHotRangeMin && stat[i].RelativeDist <= semiHotRangeMax)
                {
                    semiHotNumList.Add(stat[i]);
                }
                else if (stat[i].RelativeDist >= hotRangeMin && stat[i].RelativeDist <= HotRangeMax)
                {
                    hotNumList.Add(stat[i]);
                }
                else if (stat[i].RelativeDist == veryHot)
                {
                    veryHotNumList.Add(stat[i]);
                }
                else if (stat[i].RelativeDist > semiColdRangeMin && stat[i].RelativeDist <= semiColdRangeMax)
                {
                    semiColdNumList.Add(stat[i]);
                }
                else if (stat[i].RelativeDist > coldRangeMin && stat[i].RelativeDist <= coldRangeMax)
                {
                    coldNumList.Add(stat[i]);
                }
                else if (stat[i].RelativeDist > veryCold)
                {
                    veryColdNumList.Add(stat[i]);
                }
            }

            for (int i = 0; i < RAND_GEN_NUM; ++i)
            {
                randNumList.Add( stat[ RandomNumber(1, numGen.ScaleLength + 1) ] );
            }
        }

        public List<SubStatistics> GenCandidateList()
        {
            candidateNumList.Clear();
            uniqueCandidateNumList.Clear();

            // Set Candidate list
            // Add 5 Very Cold numbers to Candidate list
            int veryColdIndex;
            for (int i = 0; i < (veryColdNumList.Count < VERY_COLD_MAX_SELECTED ? veryColdNumList.Count : VERY_COLD_MAX_SELECTED); ++i)
            {
                veryColdIndex = RandomNumber(0, veryColdNumList.Count);
                candidateNumList.Add(veryColdNumList.ElementAt(veryColdIndex));
            }
            // Add 5 semi Cold numbers
            int semiColdIndex;
            for (int i = 0; i < (semiColdNumList.Count < SEMI_COLD_MAX_SELECTED ? semiColdNumList.Count : SEMI_COLD_MAX_SELECTED); ++i)
            {
                semiColdIndex = RandomNumber(0, semiColdNumList.Count);
                candidateNumList.Add(semiColdNumList.ElementAt(semiColdIndex));
            }
            // Add 5 Cold numbers
            int coldIndex;
            for (int i = 0; i < (coldNumList.Count < COLD_MAX_SELECTED ? coldNumList.Count : COLD_MAX_SELECTED); ++i)
            {
                coldIndex = RandomNumber(0, coldNumList.Count);
                candidateNumList.Add(coldNumList.ElementAt(coldIndex));
            }
            // Add 10 Hot numbers
            int hotIndex;
            for (int i = 0; i < (hotNumList.Count < HOT_MAX_SELECTED ? hotNumList.Count : HOT_MAX_SELECTED); ++i)
            {
                hotIndex = RandomNumber(0, hotNumList.Count);
                candidateNumList.Add(hotNumList.ElementAt(hotIndex));
            }
           // Add 10 Semi Hot numbers
            int semiHotIndex;
            for (int i = 0; i < (semiHotNumList.Count < SEMI_HOT_MAX_SELECTED ? semiHotNumList.Count : SEMI_HOT_MAX_SELECTED); ++i)
            {
                semiHotIndex = RandomNumber(0, semiHotNumList.Count);
                candidateNumList.Add(semiHotNumList.ElementAt(semiHotIndex));
            }
            // Add very Hot numbers
            int veryHotIndex;
            for (int i = 0; i < (veryHotNumList.Count < VERY_HOT_MAX_SELECTED ? veryHotNumList.Count : VERY_HOT_MAX_SELECTED); ++i)
            {
                veryHotIndex = RandomNumber(0, veryHotNumList.Count);
                candidateNumList.Add(veryHotNumList.ElementAt(veryHotIndex));
            }

            candidateNumList.AddRange(randNumList);
            uniqueCandidateNumList = RemoveDuplicates(candidateNumList);
            return uniqueCandidateNumList;
        }

        public List<SubStatistics> RemoveDuplicates(List<SubStatistics> inputList)
        {
            Dictionary<SubStatistics, int> dic = new Dictionary<SubStatistics, int>();
            List<SubStatistics> finalList = new List<SubStatistics>();
            foreach (SubStatistics t in inputList)
            {
                if (!dic.ContainsKey(t))
                {
                    dic.Add(t, 0);
                    finalList.Add(t);
                }
            }
            return finalList;
        }


        public int RandomNumber(int min, int max)
        {
            
            return Util.rnd.Next(min, max);
        }

        public int HotRangeMin
        {
            get
            {
                return hotRangeMin;
            }
            set
            {
                if (value > 1 && value <= 4)
                {
                    hotRangeMin = value;
                }
            }
        }
        public int HotRangeMax
        {
            get
            {
                return hotRangeMax;
            }
            set
            {
                if (value > 4 && value <= 6)
                {
                    hotRangeMax = value;
                }
            }
        }
        public int SemiHotRangeMin
        {
            get
            {
                return semiHotRangeMin;
            }
            set
            {
                if (value > 6 && value <= 8)
                {
                    semiHotRangeMin = value;
                }
            }
        }
        public int SemiHotRangeMax
        {
            get
            {
                return semiHotRangeMax;
            }
            set
            {
                if (value > 8 && value <= 10)
                {
                    semiHotRangeMax = value;
                }
            }
        }
        public int VeryHot
        {
            get
            {
                return veryHot;
            }
        }
        public int SemiColdRangeMin
        {
            get
            {
                return semiColdRangeMin;
            }
            set
            {
                if (value > 10 && value <= 13)
                {
                    semiColdRangeMin = value;
                }
            }
        }
        public int SemiColdRangeMax
        {
            get
            {
                return semiColdRangeMax;
            }
            set
            {
                if (value > 13 && value <= 15)
                {
                    semiColdRangeMax = value;
                }
            }
        }

        public int ColdRangeMin
        {
            get
            {
                return coldRangeMin;
            }
            set
            {
                if (value > 15 && value <= 18)
                {
                    coldRangeMin = value;
                }
            }
        }
        public int ColdRangeMax
        {
            get
            {
                return coldRangeMax;
            }
            set
            {
                if (value > 18 && value <= 20)
                {
                    coldRangeMax = value;
                }
            }
        }
        public int VeryCold
        {
            get
            {
                return veryCold;
            }
            set
            {
                if (value > 20)
                {
                    veryCold = value;
                }
            }
        }

    }
}