using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

using DataAccessTier;
namespace BusinessTier
{
    enum ALGORITHMS
    {
        SEMI_HOT,
        HOT,
        MIX,
        RANGE
    }

    enum NUMBER_RANGE
    {
        ONES,
        TENS,
        TWENTIES,
        THIRTIES,
        FORTIES,
        FIFTIES
    }

    public class AutoDraw : CandidateLists
    {
        private NUMBER_RANGE distributeRange;
        private ALGORITHMS selectAlgorithm;
        private NumGen numgen = null;
        private DataAccessLayer dataAccessLayer;
        protected List<SubStatistics> onesList;
        protected List<SubStatistics> tensList;
        protected List<SubStatistics> twentiesList;
        protected List<SubStatistics> thirtiesList;
        protected List<SubStatistics> fortiesList;
        protected List<SubStatistics> fiftiesList;

        //CandidateLists candidateList;
        private int drawLength;

        public AutoDraw(Database db, int target,
                        int numHot, int hotMin, int hotMax,
                        int numSemiHot, int semiHotMin, int semiHotMax,
                        int numCold, int coldMin, int coldMax,
                        int numSemiCold, int semiColdMin, int semiColdMax,
                        int numVeryCold, int veryColdMin, int veryHot,
                        int algo, int range

            )
            : base(numHot, hotMin, hotMax,
                      numSemiHot, semiHotMin, semiHotMax,
                      numCold, coldMin, coldMax,
                      numSemiCold, semiColdMin, semiColdMax,
                      numVeryCold, veryColdMin,
                      veryHot)
        {
            // AutoDraw only class members
            //
            distributeRange = (NUMBER_RANGE)range;
            selectAlgorithm = (ALGORITHMS)algo;
            dataAccessLayer = new DataAccessLayer();

            //Dictionary<int, bool> targetDic = new Dictionary<int, bool>();
            int[] drawNumArray = new int[drawLength];

            if (target == 0)
            {
                target = dataAccessLayer.GetLastRow(db) + 1; // For next draw
                
            }
            numgen = new NumGen(db, target-1);
            //else
            //{
            //    target -= 1;
            //    numgen = new NumGen(db, target); // based on last draw's statistics
            //}
            genLists(numgen);
        }

        protected override void genLists(NumGen numGen)
        {
            base.genLists(numGen);
            onesList = new List<SubStatistics>();
            tensList = new List<SubStatistics>();
            twentiesList = new List<SubStatistics>();
            thirtiesList = new List<SubStatistics>();
            fortiesList = new List<SubStatistics>();
            fiftiesList = new List<SubStatistics>();

            SubStatistics[] stat = numGen.Stat;
            for (int i = 0; i < veryHotNumList.Count; ++i)
            {
                if (veryHotNumList[i].Num >= 1 && veryHotNumList[i].Num < 10)
                    onesList.Add(veryHotNumList[i]);
                if (veryHotNumList[i].Num >= 10 && veryHotNumList[i].Num < 20)
                    tensList.Add(veryHotNumList[i]);
                if (veryHotNumList[i].Num >= 20 && veryHotNumList[i].Num < 30)
                    twentiesList.Add(veryHotNumList[i]);
                if (veryHotNumList[i].Num >= 30 && veryHotNumList[i].Num < 40)
                    thirtiesList.Add(veryHotNumList[i]);
                if (veryHotNumList[i].Num >= 40 && veryHotNumList[i].Num < 50)
                    fortiesList.Add(veryHotNumList[i]);
                if (veryHotNumList[i].Num >= 50 && veryHotNumList[i].Num < 60)
                    fiftiesList.Add(veryHotNumList[i]);
            }

            for (int i = 0; i < hotNumList.Count; ++i)
            {
                if (hotNumList[i].Num >= 1 && hotNumList[i].Num < 10)
                    onesList.Add(hotNumList[i]);
                if (hotNumList[i].Num >= 10 && hotNumList[i].Num < 20)
                    tensList.Add(hotNumList[i]);
                if (hotNumList[i].Num >= 20 && hotNumList[i].Num < 30)
                    twentiesList.Add(hotNumList[i]);
                if (hotNumList[i].Num >= 30 && hotNumList[i].Num < 40)
                    thirtiesList.Add(hotNumList[i]);
                if (hotNumList[i].Num >= 40 && hotNumList[i].Num < 50)
                    fortiesList.Add(hotNumList[i]);
                if (hotNumList[i].Num >= 50 && hotNumList[i].Num < 60)
                    fiftiesList.Add(hotNumList[i]);
            }

            for (int i = 0; i < semiHotNumList.Count; ++i)
            {
                if (semiHotNumList[i].Num >= 1 && semiHotNumList[i].Num < 10)
                    onesList.Add(semiHotNumList[i]);
                if (semiHotNumList[i].Num >= 10 && semiHotNumList[i].Num < 20)
                    tensList.Add(semiHotNumList[i]);
                if (semiHotNumList[i].Num >= 20 && semiHotNumList[i].Num < 30)
                    twentiesList.Add(semiHotNumList[i]);
                if (semiHotNumList[i].Num >= 30 && semiHotNumList[i].Num < 40)
                    thirtiesList.Add(semiHotNumList[i]);
                if (semiHotNumList[i].Num >= 40 && semiHotNumList[i].Num < 50)
                    fortiesList.Add(semiHotNumList[i]);
                if (semiHotNumList[i].Num >= 50 && semiHotNumList[i].Num < 60)
                    fiftiesList.Add(semiHotNumList[i]);
            }

            for (int i = 0; i < veryColdNumList.Count; ++i)
            {
                if (veryColdNumList[i].Num >= 1 && veryColdNumList[i].Num < 10)
                    onesList.Add(veryColdNumList[i]);
                if (veryColdNumList[i].Num >= 10 && veryColdNumList[i].Num < 20)
                    tensList.Add(veryColdNumList[i]);
                if (veryColdNumList[i].Num >= 20 && veryColdNumList[i].Num < 30)
                    twentiesList.Add(veryColdNumList[i]);
                if (veryColdNumList[i].Num >= 30 && veryColdNumList[i].Num < 40)
                    thirtiesList.Add(veryColdNumList[i]);
                if (veryColdNumList[i].Num >= 40 && veryColdNumList[i].Num < 50)
                    fortiesList.Add(veryColdNumList[i]);
                if (veryColdNumList[i].Num >= 50 && veryColdNumList[i].Num < 60)
                    fiftiesList.Add(veryColdNumList[i]);
            }

            for (int i = 0; i < coldNumList.Count; ++i)
            {
                if (coldNumList[i].Num >= 1 && coldNumList[i].Num < 10)
                    onesList.Add(coldNumList[i]);
                if (coldNumList[i].Num >= 10 && coldNumList[i].Num < 20)
                    tensList.Add(coldNumList[i]);
                if (coldNumList[i].Num >= 20 && coldNumList[i].Num < 30)
                    twentiesList.Add(coldNumList[i]);
                if (coldNumList[i].Num >= 30 && coldNumList[i].Num < 40)
                    thirtiesList.Add(coldNumList[i]);
                if (coldNumList[i].Num >= 40 && coldNumList[i].Num < 50)
                    fortiesList.Add(coldNumList[i]);
                if (coldNumList[i].Num >= 50 && coldNumList[i].Num < 60)
                    fiftiesList.Add(coldNumList[i]);
            }

            for (int i = 0; i < semiColdNumList.Count; ++i)
            {
                if (semiColdNumList[i].Num >= 1 && semiColdNumList[i].Num < 10)
                    onesList.Add(semiColdNumList[i]);
                if (semiColdNumList[i].Num >= 10 && semiColdNumList[i].Num < 20)
                    tensList.Add(semiColdNumList[i]);
                if (semiColdNumList[i].Num >= 20 && semiColdNumList[i].Num < 30)
                    twentiesList.Add(semiColdNumList[i]);
                if (semiColdNumList[i].Num >= 30 && semiColdNumList[i].Num < 40)
                    thirtiesList.Add(semiColdNumList[i]);
                if (semiColdNumList[i].Num >= 40 && semiColdNumList[i].Num < 50)
                    fortiesList.Add(semiColdNumList[i]);
                if (semiColdNumList[i].Num >= 50 && semiColdNumList[i].Num < 60)
                    fiftiesList.Add(semiColdNumList[i]);
            }
        }


        private void initDictionary(Dictionary<int, bool> dic)
        {

            for (int i = 1; i <= numgen.ScaleLength; ++i)
            {
                dic[i] = false;
            }
        }


        public int getNumFromList(Dictionary<int, int> dic, List<SubStatistics> list)
        {
            int idx = 0, count = 0;
            while (count < list.Count)
            {
                idx = Util.rnd.Next(0, list.Count);

                if (!dic.ContainsKey(idx))
                {
                    dic.Add(idx, 0);
                    return list[idx].Num;
                }
                count++;
            }
            return getNumFromList(dic, hotNumList);


        }

        public static int getNumFromGenList(Dictionary<int, int> dic, List<SubStatistics> list)
        {
            int idx = 0, count = 0;
            while (count < list.Count)
            {
                idx = Util.rnd.Next(0, list.Count);

                if (!dic.ContainsKey(idx))
                {
                    dic.Add(idx, 0);
                    return list[idx].Num;
                }
                count++;
            }
            return -1;

        }

        private int selectAListFromTwo(Dictionary<int, int> dic, List<SubStatistics> list1, List<SubStatistics> list2)
        {
            int n = RandomNumber(0, 10);
            if (((n & 1) == 0) && (list1.Count > 0))
                return getNumFromList(dic, list1);
            else
            {
                if (list2.Count > 0)
                    return getNumFromList(dic, list2);
                else
                    return getNumFromList(dic, hotNumList); // Gueranteed veotNumList is not empty
            }
        }

        public string PlayNextDraw(Database db)
        {

            drawLength = Util.getColumnnsOfLotto_no_bonus(db);

            int[] drawNumberArray = new int[drawLength];
            Dictionary<int, int> dic = new Dictionary<int, int>();
            

            List<SubStatistics> GeneratedList = new List<SubStatistics>();
            GeneratedList.Clear();

            // Generate new Candidate List
            GeneratedList = GenCandidateList(); //candidateList.GenCandidateList();


            Dictionary<int, bool> GeneratedDic = new Dictionary<int, bool>();
            initDictionary(GeneratedDic);

            for (int j = 0; j < GeneratedList.Count; ++j)
            {
                int key = GeneratedList[j].Num;
                GeneratedDic[key] = true;
            }

            string stmt = "";

            switch (selectAlgorithm)
            {
                case ALGORITHMS.HOT:
                    {
                        int i = 0;
                        dic.Clear();
                        if (drawLength == 1)
                        {
                            drawNumberArray[i++] = getNumFromList(dic, hotNumList);
                        }
                        else if (drawLength == 2)
                        {
                            drawNumberArray[i++] = getNumFromList(dic, hotNumList);    
                            drawNumberArray[i++] = getNumFromList(dic, semiHotNumList);
                        }
                        else if (drawLength == 3)
                        {
                            drawNumberArray[i++] = getNumFromList(dic, hotNumList);
                            drawNumberArray[i++] = getNumFromList(dic, semiHotNumList);
                            drawNumberArray[i++] = getNumFromList(dic, hotNumList);
                        }
                        else if (drawLength == 4)
                        {
                            drawNumberArray[i++] = getNumFromList(dic, hotNumList);
                            drawNumberArray[i++] = getNumFromList(dic, semiHotNumList);
                            drawNumberArray[i++] = getNumFromList(dic, hotNumList);
                            drawNumberArray[i++] = getNumFromList(dic, hotNumList);
                        }
                        else if (drawLength == 5)
                        {
                            drawNumberArray[i++] = getNumFromList(dic, hotNumList);
                            drawNumberArray[i++] = getNumFromList(dic, semiHotNumList);
                            drawNumberArray[i++] = getNumFromList(dic, hotNumList);
                            drawNumberArray[i++] = getNumFromList(dic, hotNumList);
                            drawNumberArray[i++] = getNumFromList(dic, hotNumList);
                        }
                        else if (drawLength == 6)
                        {
                            drawNumberArray[i++] = getNumFromList(dic, hotNumList);
                            drawNumberArray[i++] = getNumFromList(dic, semiHotNumList);
                            drawNumberArray[i++] = getNumFromList(dic, hotNumList);
                            drawNumberArray[i++] = getNumFromList(dic, hotNumList);
                            drawNumberArray[i++] = getNumFromList(dic, hotNumList);
                            drawNumberArray[i++] = selectAListFromTwo(dic, semiHotNumList, semiColdNumList);
                        }
                        else if (drawLength == 7)
                        {
                            drawNumberArray[i++] = getNumFromList(dic, hotNumList);
                            drawNumberArray[i++] = getNumFromList(dic, semiHotNumList);
                            drawNumberArray[i++] = getNumFromList(dic, hotNumList);
                            drawNumberArray[i++] = getNumFromList(dic, hotNumList);
                            drawNumberArray[i++] = getNumFromList(dic, hotNumList);
                            drawNumberArray[i++] = selectAListFromTwo(dic, semiHotNumList, semiColdNumList);
                            drawNumberArray[i] = selectAListFromTwo(dic, semiColdNumList, veryColdNumList);
                        }
                    }
                    break;
                case ALGORITHMS.MIX:
                    {

                        int i = 0;
                        dic.Clear();
                        if (drawLength == 1)
                        {
                            drawNumberArray[i++] = getNumFromList(dic, hotNumList);
                        }
                        else if (drawLength == 2)
                        {
                            drawNumberArray[i++] = getNumFromList(dic, semiHotNumList);
                            drawNumberArray[i++] = getNumFromList(dic, coldNumList);
                        }
                        else if (drawLength == 3)
                        {
                            drawNumberArray[i++] = getNumFromList(dic, semiHotNumList);
                            drawNumberArray[i++] = getNumFromList(dic, coldNumList);
                            drawNumberArray[i++] = getNumFromList(dic, veryHotNumList);
                        }
                        else if (drawLength == 4)
                        {
                            drawNumberArray[i++] = getNumFromList(dic, semiHotNumList);
                            drawNumberArray[i++] = getNumFromList(dic, coldNumList);
                            drawNumberArray[i++] = getNumFromList(dic, veryHotNumList);
                            drawNumberArray[i++] = getNumFromList(dic, hotNumList);
                        }
                        else if (drawLength == 5)
                        {
                            drawNumberArray[i++] = getNumFromList(dic, semiHotNumList);
                            drawNumberArray[i++] = getNumFromList(dic, coldNumList);
                            drawNumberArray[i++] = getNumFromList(dic, veryHotNumList);
                            drawNumberArray[i++] = getNumFromList(dic, hotNumList);
                            drawNumberArray[i++] = getNumFromList(dic, hotNumList);
                        }
                        else if (drawLength == 6)
                        {
                            drawNumberArray[i++] = getNumFromList(dic, semiHotNumList);
                            drawNumberArray[i++] = getNumFromList(dic, coldNumList);
                            drawNumberArray[i++] = getNumFromList(dic, veryHotNumList);
                            drawNumberArray[i++] = getNumFromList(dic, hotNumList);
                            drawNumberArray[i++] = getNumFromList(dic, hotNumList);
                            drawNumberArray[i++] = getNumFromList(dic, semiHotNumList);
                        }
                        else if (drawLength == 7)
                        {
                            drawNumberArray[i++] = getNumFromList(dic, semiHotNumList);
                            drawNumberArray[i++] = getNumFromList(dic, coldNumList);
                            drawNumberArray[i++] = getNumFromList(dic, veryHotNumList);
                            drawNumberArray[i++] = getNumFromList(dic, hotNumList);
                            drawNumberArray[i++] = getNumFromList(dic, hotNumList);
                            drawNumberArray[i++] = getNumFromList(dic, semiHotNumList);
                            drawNumberArray[i] = getNumFromList(dic, veryColdNumList);
                        }
                    }
                    break;
                case ALGORITHMS.RANGE:
                    {
                        int i = 0;
                        dic.Clear();

                        if (drawLength == 1)
                        {
                            drawNumberArray[i++] = getNumFromList(dic, hotNumList);
                        }
                        else if (drawLength == 2)
                        {
                            drawNumberArray[i++] = getNumFromList(dic, semiHotNumList);
                            drawNumberArray[i++] = getNumFromList(dic, hotNumList);
                        }
                        else
                        {
                            switch (distributeRange)
                            {
                                case NUMBER_RANGE.ONES:
                                    
                                    if (drawLength == 3)
                                    {
                                        drawNumberArray[i++] = getNumFromList(dic, onesList);
                                        drawNumberArray[i++] = getNumFromList(dic, onesList);
                                        drawNumberArray[i++] = getNumFromList(dic, onesList);
                                    }
                                    else if (drawLength == 4)
                                    {
                                        drawNumberArray[i++] = getNumFromList(dic, onesList);
                                        drawNumberArray[i++] = getNumFromList(dic, onesList);
                                        drawNumberArray[i++] = getNumFromList(dic, onesList);
                                        drawNumberArray[i++] = getNumFromList(dic, hotNumList);
                                    }
                                    else if (drawLength == 5)
                                    {
                                        drawNumberArray[i++] = getNumFromList(dic, onesList);
                                        drawNumberArray[i++] = getNumFromList(dic, onesList);
                                        drawNumberArray[i++] = getNumFromList(dic, onesList);
                                        drawNumberArray[i++] = getNumFromList(dic, hotNumList);
                                        drawNumberArray[i++] = getNumFromList(dic, semiHotNumList);
                                    }
                                    else if (drawLength == 6)
                                    {
                                        drawNumberArray[i++] = getNumFromList(dic, onesList);
                                        drawNumberArray[i++] = getNumFromList(dic, onesList);
                                        drawNumberArray[i++] = getNumFromList(dic, onesList);
                                        drawNumberArray[i++] = getNumFromList(dic, hotNumList);
                                        drawNumberArray[i++] = getNumFromList(dic, semiHotNumList);
                                        drawNumberArray[i++] = getNumFromList(dic, coldNumList);
                                    }
                                    else if (drawLength == 7)
                                    {
                                        drawNumberArray[i++] = getNumFromList(dic, onesList);
                                        drawNumberArray[i++] = getNumFromList(dic, onesList);
                                        drawNumberArray[i++] = getNumFromList(dic, onesList);
                                        drawNumberArray[i++] = getNumFromList(dic, hotNumList);
                                        drawNumberArray[i++] = getNumFromList(dic, semiHotNumList);
                                        drawNumberArray[i++] = getNumFromList(dic, coldNumList);
                                        drawNumberArray[i] = getNumFromList(dic, veryHotNumList);
                                    }
                                    break;
                                case NUMBER_RANGE.TENS:

                                    if (drawLength == 3)
                                    {
                                        drawNumberArray[i++] = getNumFromList(dic, tensList);
                                        drawNumberArray[i++] = getNumFromList(dic, tensList);
                                        drawNumberArray[i++] = getNumFromList(dic, tensList);
                                    }
                                    else if (drawLength == 4)
                                    {
                                        drawNumberArray[i++] = getNumFromList(dic, tensList);
                                        drawNumberArray[i++] = getNumFromList(dic, tensList);
                                        drawNumberArray[i++] = getNumFromList(dic, tensList);
                                        drawNumberArray[i++] = getNumFromList(dic, hotNumList);
                                    }
                                    else if (drawLength == 5)
                                    {
                                        drawNumberArray[i++] = getNumFromList(dic, tensList);
                                        drawNumberArray[i++] = getNumFromList(dic, tensList);
                                        drawNumberArray[i++] = getNumFromList(dic, tensList);
                                        drawNumberArray[i++] = getNumFromList(dic, hotNumList);
                                        drawNumberArray[i++] = getNumFromList(dic, semiHotNumList);
                                    }
                                    else if (drawLength == 6)
                                    {
                                        drawNumberArray[i++] = getNumFromList(dic, tensList);
                                        drawNumberArray[i++] = getNumFromList(dic, tensList);
                                        drawNumberArray[i++] = getNumFromList(dic, tensList);
                                        drawNumberArray[i++] = getNumFromList(dic, hotNumList);
                                        drawNumberArray[i++] = getNumFromList(dic, semiHotNumList);
                                        drawNumberArray[i++] = getNumFromList(dic, coldNumList);
                                    }
                                    else if (drawLength == 7)
                                    {
                                        drawNumberArray[i++] = getNumFromList(dic, tensList);
                                        drawNumberArray[i++] = getNumFromList(dic, tensList);
                                        drawNumberArray[i++] = getNumFromList(dic, tensList);
                                        drawNumberArray[i++] = getNumFromList(dic, hotNumList);
                                        drawNumberArray[i++] = getNumFromList(dic, semiHotNumList);
                                        drawNumberArray[i++] = getNumFromList(dic, coldNumList);
                                        drawNumberArray[i] = getNumFromList(dic, veryHotNumList);
                                    }

                                    break;
                                case NUMBER_RANGE.TWENTIES:

                                    if (drawLength == 3)
                                    {
                                        drawNumberArray[i++] = getNumFromList(dic, twentiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, twentiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, twentiesList);
                                    }
                                    else if (drawLength == 4)
                                    {
                                        drawNumberArray[i++] = getNumFromList(dic, twentiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, twentiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, twentiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, hotNumList);
                                    }
                                    else if (drawLength == 5)
                                    {
                                        drawNumberArray[i++] = getNumFromList(dic, twentiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, twentiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, twentiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, hotNumList);
                                        drawNumberArray[i++] = getNumFromList(dic, semiHotNumList);
                                    }
                                    else if (drawLength == 6)
                                    {
                                        drawNumberArray[i++] = getNumFromList(dic, twentiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, twentiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, twentiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, hotNumList);
                                        drawNumberArray[i++] = getNumFromList(dic, semiHotNumList);
                                        drawNumberArray[i++] = getNumFromList(dic, coldNumList);
                                    }
                                    else if (drawLength == 7)
                                    {
                                        drawNumberArray[i++] = getNumFromList(dic, twentiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, twentiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, twentiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, hotNumList);
                                        drawNumberArray[i++] = getNumFromList(dic, semiHotNumList);
                                        drawNumberArray[i++] = getNumFromList(dic, coldNumList);
                                        drawNumberArray[i] = getNumFromList(dic, veryHotNumList);
                                    }
                                    break;
                                case NUMBER_RANGE.THIRTIES:

                                    if (drawLength == 3)
                                    {
                                        drawNumberArray[i++] = getNumFromList(dic, thirtiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, thirtiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, thirtiesList);
                                    }
                                    else if (drawLength == 4)
                                    {
                                        drawNumberArray[i++] = getNumFromList(dic, thirtiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, thirtiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, thirtiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, hotNumList);
                                    }
                                    else if (drawLength == 5)
                                    {
                                        drawNumberArray[i++] = getNumFromList(dic, thirtiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, thirtiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, thirtiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, hotNumList);
                                        drawNumberArray[i++] = getNumFromList(dic, semiHotNumList);
                                    }
                                    else if (drawLength == 6)
                                    {
                                        drawNumberArray[i++] = getNumFromList(dic, thirtiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, thirtiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, thirtiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, hotNumList);
                                        drawNumberArray[i++] = getNumFromList(dic, semiHotNumList);
                                        drawNumberArray[i++] = getNumFromList(dic, coldNumList);
                                    }
                                    else if (drawLength == 7)
                                    {
                                        drawNumberArray[i++] = getNumFromList(dic, thirtiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, thirtiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, thirtiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, hotNumList);
                                        drawNumberArray[i++] = getNumFromList(dic, semiHotNumList);
                                        drawNumberArray[i++] = getNumFromList(dic, coldNumList);
                                        drawNumberArray[i] = getNumFromList(dic, veryHotNumList);
                                    }
                                    break;
                                case NUMBER_RANGE.FORTIES:

                                    if (drawLength == 3)
                                    {
                                        drawNumberArray[i++] = getNumFromList(dic, fortiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, fortiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, fortiesList);
                                    }
                                    else if (drawLength == 4)
                                    {
                                        drawNumberArray[i++] = getNumFromList(dic, fortiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, fortiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, fortiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, hotNumList);
                                    }
                                    else if (drawLength == 5)
                                    {
                                        drawNumberArray[i++] = getNumFromList(dic, fortiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, fortiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, fortiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, hotNumList);
                                        drawNumberArray[i++] = getNumFromList(dic, semiHotNumList);
                                    }
                                    else if (drawLength == 6)
                                    {
                                        drawNumberArray[i++] = getNumFromList(dic, fortiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, fortiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, fortiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, hotNumList);
                                        drawNumberArray[i++] = getNumFromList(dic, semiHotNumList);
                                        drawNumberArray[i++] = getNumFromList(dic, coldNumList);
                                    }
                                    else if (drawLength == 7)
                                    {
                                        drawNumberArray[i++] = getNumFromList(dic, fortiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, fortiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, fortiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, hotNumList);
                                        drawNumberArray[i++] = getNumFromList(dic, semiHotNumList);
                                        drawNumberArray[i++] = getNumFromList(dic, coldNumList);
                                        drawNumberArray[i] = getNumFromList(dic, veryHotNumList);
                                    }
                                    break;
                                case NUMBER_RANGE.FIFTIES:

                                    if (drawLength == 3)
                                    {
                                        drawNumberArray[i++] = getNumFromList(dic, fiftiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, fiftiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, fiftiesList);
                                    }
                                    else if (drawLength == 4)
                                    {
                                        drawNumberArray[i++] = getNumFromList(dic, fiftiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, fiftiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, fiftiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, hotNumList);
                                    }
                                    else if (drawLength == 5)
                                    {
                                        drawNumberArray[i++] = getNumFromList(dic, fiftiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, fiftiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, fiftiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, hotNumList);
                                        drawNumberArray[i++] = getNumFromList(dic, semiHotNumList);
                                    }
                                    else if (drawLength == 6)
                                    {
                                        drawNumberArray[i++] = getNumFromList(dic, fiftiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, fiftiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, fiftiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, hotNumList);
                                        drawNumberArray[i++] = getNumFromList(dic, semiHotNumList);
                                        drawNumberArray[i++] = getNumFromList(dic, coldNumList);
                                    }
                                    else if (drawLength == 7)
                                    {
                                        drawNumberArray[i++] = getNumFromList(dic, fiftiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, fiftiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, fiftiesList);
                                        drawNumberArray[i++] = getNumFromList(dic, hotNumList);
                                        drawNumberArray[i++] = getNumFromList(dic, semiHotNumList);
                                        drawNumberArray[i++] = getNumFromList(dic, coldNumList);
                                        drawNumberArray[i] = getNumFromList(dic, veryHotNumList);
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    break;

                case ALGORITHMS.SEMI_HOT:
                    {
                        int i = 0;
                        dic.Clear();
                                                
                        if (drawLength == 1)
                        {
                            drawNumberArray[i++] = getNumFromList(dic, semiHotNumList);
                        }
                        else if (drawLength == 2)
                        {
                            drawNumberArray[i++] = getNumFromList(dic, semiHotNumList);
                            drawNumberArray[i++] = getNumFromList(dic, semiHotNumList);
                        }
                        else
                        {
                            if (drawLength == 3)
                            {
                                drawNumberArray[i++] = getNumFromList(dic, semiHotNumList);
                                drawNumberArray[i++] = getNumFromList(dic, semiHotNumList);
                                drawNumberArray[i++] = selectAListFromTwo(dic, hotNumList, semiColdNumList);
                            }
                            else if (drawLength == 4)
                            {
                                drawNumberArray[i++] = getNumFromList(dic, semiHotNumList);
                                drawNumberArray[i++] = getNumFromList(dic, semiHotNumList);
                                drawNumberArray[i++] = selectAListFromTwo(dic, hotNumList, semiColdNumList);
                                drawNumberArray[i++] = selectAListFromTwo(dic, veryHotNumList, veryColdNumList);
                            }
                            else if (drawLength == 5)
                            {
                                drawNumberArray[i++] = getNumFromList(dic, semiHotNumList);
                                drawNumberArray[i++] = getNumFromList(dic, semiHotNumList);
                                drawNumberArray[i++] = selectAListFromTwo(dic, hotNumList, semiColdNumList);
                                drawNumberArray[i++] = selectAListFromTwo(dic, veryHotNumList, veryColdNumList);
                                drawNumberArray[i++] = getNumFromList(dic, semiHotNumList);
                                
                            }
                            else if (drawLength == 6)
                            {
                                drawNumberArray[i++] = getNumFromList(dic, semiHotNumList);
                                drawNumberArray[i++] = getNumFromList(dic, semiHotNumList);
                                drawNumberArray[i++] = selectAListFromTwo(dic, hotNumList, semiColdNumList);
                                drawNumberArray[i++] = selectAListFromTwo(dic, veryHotNumList, veryColdNumList);
                                drawNumberArray[i++] = selectAListFromTwo(dic, veryHotNumList, hotNumList);
                                drawNumberArray[i++] = getNumFromList(dic, semiHotNumList);

                            }
                            else if (drawLength == 7)
                            {
                                drawNumberArray[i++] = getNumFromList(dic, semiHotNumList);
                                drawNumberArray[i++] = getNumFromList(dic, semiHotNumList);
                                drawNumberArray[i++] = selectAListFromTwo(dic, hotNumList, semiColdNumList);
                                drawNumberArray[i++] = selectAListFromTwo(dic, veryHotNumList, veryColdNumList);
                                drawNumberArray[i++] = selectAListFromTwo(dic, veryHotNumList, hotNumList);
                                drawNumberArray[i++] = getNumFromList(dic, semiHotNumList);
                                drawNumberArray[i] = selectAListFromTwo(dic, semiColdNumList, veryColdNumList);
                            }
                        }
                    }
                    break;

                default:
                    break;
            }

            // Sort array
            Util.qsort(drawNumberArray);


            stmt += Util.DisplayPotentialNumbers(GeneratedDic, drawNumberArray, GeneratedList, db, numgen.ScaleLength);
            stmt += Util.Distribution(numgen.Stat, drawNumberArray, db);
            return stmt;

        }
    }
}