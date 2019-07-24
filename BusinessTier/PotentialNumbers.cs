using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

using DataAccessTier;
using BusinessTier;

namespace BusinessTier
{
    public class PotentialNumbers
    {
        private static int lastRow = 0;
        private long databaseExecutionTime;
        private DataAccessLayer dataAccessLayer;
        private Database db;
        private string fromSite = " ";
        private static bool flag = false;
        private string colorValue = "Red";
        private string fontStyle = "normal";

        private NumGen numgen = null;
        CandidateLists candidateList;
        private List<SubStatistics> GeneratedList;
        int[] drawNumArray;



        private const string ResponseStatment = "No generated draws matches target draw! <br />"
                + "This is because the scope for those variables may too small.<br />"
                + "Click 'Tune Variables' button to calibrate scope of numbers to be selected. <br />"
                + "Or you may continue to click 'Submit' button to re-generate.";


        public PotentialNumbers(Database database, string from)
        {
            dataAccessLayer = new DataAccessLayer();
            lastRow = dataAccessLayer.GetLastRow(database);
            fromSite = from;
            db = database;
        }

        public void genTargetDrawArray(Database db, int target)
        {
            if (target == 0)
            {
                target = lastRow;
            }
            numgen = new NumGen(db, target - 1);
            int drawLength = Util.getColumnnsOfLotto_no_bonus(db);

            //Dictionary<int, bool> targetDic = new Dictionary<int, bool>();
            drawNumArray = new int[drawLength];
            //databaseExecutionTime = dbNumGenMap[db].DatabaseExecutionTime;

            try
            {
                // Retrieve target draw from database
                SqlDataReader reader = dataAccessLayer.SpGetTargetDraw(db, target);
                while (reader.Read())
                {
                    for (int i = 0; i < drawLength; ++i)
                    {
                        // Skip drawnumber and drawdate columns
                        drawNumArray[i] = (int)reader[i+2];
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public string genPotentialDraws()
        {

            Dictionary<int, bool> GeneratedDic = new Dictionary<int, bool>();

            int counter = 0;
            const long MAX_GEN = 500;
            while (counter < MAX_GEN)
            {
                counter++;

                // Generate Candidate List
                GeneratedList = candidateList.GenCandidateList();
                GeneratedDic.Clear();
                Util.initDictionary(GeneratedDic, numgen.ScaleLength);

                for (int j = 0; j < GeneratedList.Count; ++j)
                {
                    int key = GeneratedList[j].Num;
                    GeneratedDic[key] = true;
                }

                // Test if the target draw numbers are among the random generated numbers
                if (Util.amongNumbers(GeneratedDic, drawNumArray))
                {
                    return displayPotentialNumbers(counter, GeneratedDic, drawNumArray, GeneratedList, numgen.ScaleLength);
                }
            }

            flag = !flag;
            if (flag)
            {
                colorValue = "Maroon";
                fontStyle = "normal";
            }
            else
            {
                colorValue = "Red";
                fontStyle = "italic";
            }

            string stmt = "";
            stmt = "<font style=font-style:" + fontStyle + "; color=" + colorValue + ">";
            stmt += ResponseStatment;
            return stmt;
        }


        public void genPotentialNumbers(int target,
                                          int numHot, int hotMin, int hotMax,
                                          int numSemiHot, int semiHotMin, int semiHotMax,
                                          int numCold, int coldMin, int coldMax,
                                          int numSemiCold, int semiColdMin, int semiColdMax,
                                          int numVeryCold, int veryColdMin, int veryHot
                                         )
        {

            if (target == 0)
            {
                target = lastRow + 1;
            }
            numgen = new NumGen(db, target - 1);
            //databaseExecutionTime = numgen.DatabaseExecutionTime;


            candidateList = new CandidateLists(numHot, hotMin, hotMax,
                                                numSemiHot, semiHotMin, semiHotMax,
                                                numCold, coldMin, coldMax,
                                                numSemiCold, semiColdMin, semiColdMax,
                                                numVeryCold, veryColdMin, veryHot,
                                                numgen
                                              );

            GeneratedList = new List<SubStatistics>();
            GeneratedList.Clear();

            // Generate new Candidate List
            GeneratedList = candidateList.GenCandidateList();

        }

        public string PredictNextDraws(int sumMin, int sumMax, int odds)
        {
            int drawLength = Util.getColumnnsOfLotto_no_bonus(db);

            //Dictionary<int, bool> targetDic = new Dictionary<int, bool>();
            int[] drawNumArray = new int[drawLength];

            Dictionary<int, bool> GeneratedDic = new Dictionary<int, bool>();
            Util.initDictionary(GeneratedDic, numgen.ScaleLength);

            for (int j = 0; j < GeneratedList.Count; ++j)
            {
                int key = GeneratedList[j].Num;
                GeneratedDic[key] = true;
            }

            Dictionary<int, int> dic = new Dictionary<int, int>();

            bool regen = false;
            string stmt = "";
            int outerCounter = 0;
            do
            {
                outerCounter++;
                if (GeneratedList.Count < drawNumArray.Length) break; // avoid looping forever

                int index = 0, i = 0;
                int innerCounter = 0;

                while (innerCounter < 100)
                {
                    innerCounter++;
                    index = candidateList.RandomNumber(0, GeneratedList.Count);
                    if (!dic.ContainsKey(index))
                    {
                        if (i < drawLength)
                        {
                            if (i == 0 && ((GeneratedList[index].SavedDist >= 10 && GeneratedList[index].RelativeDist <= 1) || 
                                (GeneratedList[index].SavedDist <= 10 && GeneratedList[index].RelativeDist <= 5)))
                            {
                                dic.Add(index, 0);
                                drawNumArray[i] = GeneratedList[index].Num;
                                i++;
                            }
                            else if (i == 1 && ((GeneratedList[index].SavedDist > 10 && GeneratedList[index].RelativeDist == 0) || 
                                GeneratedList[index].RelativeDist == 0))
                            {
                                dic.Add(index, 0);
                                drawNumArray[i] = GeneratedList[index].Num;
                                i++;
                            }
                            else if (i == 2 && ((GeneratedList[index].SavedDist < 10 && GeneratedList[index].RelativeDist < 3) || 
                                (GeneratedList[index].SavedDist < 10 && GeneratedList[index].RelativeDist <= 5)))
                            {
                                dic.Add(index, 0);
                                drawNumArray[i] = GeneratedList[index].Num;
                                i++;
                            }
                            else if (i == 3 && GeneratedList[index].SavedDist <= 5 && GeneratedList[index].RelativeDist <= 10)
                            {
                                dic.Add(index, 0);
                                drawNumArray[i] = GeneratedList[index].Num;
                                i++;
                            }
                            else if (i == 4 && ((GeneratedList[index].SavedDist >= 10 && GeneratedList[index].RelativeDist < 10) || 
                                (GeneratedList[index].SavedDist >= 10 && GeneratedList[index].RelativeDist >= 10)))
                            {
                                dic.Add(index, 0);
                                drawNumArray[i] = GeneratedList[index].Num;
                                i++;
                            }
                            else if (i == 5 && ((GeneratedList[index].SavedDist >= 10 && GeneratedList[index].RelativeDist >= 10) || 
                                (GeneratedList[index].SavedDist < 10 && GeneratedList[index].RelativeDist >= 10)))
                            {
                                dic.Add(index, 0);
                                drawNumArray[i] = GeneratedList[index].Num;
                                i++;
                            }
                            else if ((i == drawNumArray.Length - 1) && (GeneratedList[index].SavedDist >= 10 && GeneratedList[index].RelativeDist <= 5))
                            {
                                dic.Add(index, 0);
                                drawNumArray[i] = GeneratedList[index].Num;
                                i++;
                            }

                            if (i == drawNumArray.Length)
                            {
                                dic.Clear();
                                break;
                            }
                        }
                    }
                }

                // Verify sum of the array to match input sum range
                int sum = Util.getSum(drawNumArray);
                if (sum < sumMin || sum > sumMax || Util.numOdds(drawNumArray) != odds)
                {
                    regen = true;
                }
                else
                    regen = false;
            } while (regen && outerCounter < 100);

            // Sort array
            Util.selection_sort(drawNumArray);
            stmt += Util.DisplayPotentialNumbers(GeneratedDic, drawNumArray, GeneratedList, db, numgen.ScaleLength);
            stmt += Util.Distribution(numgen.Stat, drawNumArray, db);

            return stmt;
        }


        public string genNumbersToMatchTargetDraw(int target)
        {
            if (target == 0)
            {
                target = lastRow;
            }
            numgen = new NumGen(db, target);
            //numgen = new NumGen(db, target - 1);
            databaseExecutionTime = numgen.DatabaseExecutionTime;

            int drawLength = numgen.ScaleLength;
            Dictionary<int, bool> targetDic = new Dictionary<int, bool>();
            int cols = Util.getColumnnsOfLotto(db);
            try
            {
                // Retrieve target draw from database
                SqlDataReader reader = dataAccessLayer.SpGetTargetDraw(db, target);
                while (reader.Read())
                {
                    for (int i = 0; i < cols; ++i)
                    {
                        // set to Dictionary and skip drawnumber and drawdate columns
                        int key = (int)reader[i+2];
                        targetDic[key] = true;
                    }
                }
            }
            catch
            {
                throw;
            }

            CandidateLists candidateList = new CandidateLists(numgen);
            Dictionary<int, bool> GeneratedDic = new Dictionary<int, bool>();
            string stmt = "";

            // Generate 6 draw numbers from Generated List
            int[] drawNumArray = new int[drawLength];

            int gen_group_number_counter = 0, gen_draw_number_counter = 0;
            const long MAX_GEN = 50;
            const int MAX_OUT_LOOP = 1000;
            while (gen_group_number_counter < MAX_OUT_LOOP)
            {
                // Generate Candidate List
                List<SubStatistics> GeneratedList = candidateList.GenCandidateList();
                GeneratedDic.Clear();
                Util.initDictionary(GeneratedDic, numgen.ScaleLength);

                for (int j = 0; j < GeneratedList.Count; ++j)
                {
                    int key = GeneratedList[j].Num;
                    GeneratedDic[key] = true;
                }

                if (!Util.amongNumbers(targetDic, GeneratedDic))
                {
                    stmt += "Last draw has not been completely contained inside generated candidate list";
                    return stmt;
                    //continue;
                }

                Dictionary<int, int> dic = new Dictionary<int, int>();

                gen_group_number_counter++;
                gen_draw_number_counter = 0;
                while (gen_draw_number_counter < MAX_GEN)
                {
                    int i = 0;
                    dic.Clear();
                    while (i < drawNumArray.Length)
                    {
                        drawNumArray[i++] = AutoDraw.getNumFromGenList(dic, GeneratedList);
                    }

                    // Sort array                   
                    Util.selection_sort(drawNumArray);
                    gen_draw_number_counter++;

                    // Test if selected numbers are hit the target draw
                    if (Util.processMatch(targetDic, drawNumArray))
                    {
                        // build output table
                        return genOutput(gen_group_number_counter, gen_draw_number_counter, drawNumArray);
                    }
                    // else random generate another set of draw numbers and test again   
                }
                // else random generate another group of potential numbers which may contains all next draw numbers
            } // Last while

            int totalGens = gen_group_number_counter * gen_draw_number_counter;

            flag = !flag;
            if (flag)
            {
                colorValue = "Green";
                fontStyle = "normal";
            }
            else
            {
                colorValue = "Red";
                fontStyle = "italic";
            }

            stmt = "<font style=font-style:" + fontStyle + "; color=" + colorValue + ">";
            stmt += "After " + totalGens + " times of generating, ";
            stmt += ResponseStatment;
            return stmt;
        }


        private string genOutput(int gen_group_number_counter, int gen_draw_number_counter, int[] arr)
        {
            string stmt = "";

            //stmt += Util.CreateHTML_Header("", db);
            stmt += "<HR>\n"
                + "<TABLE border=1 align=center>\n"
                + "<TR>\n";
            stmt += "<TH bgcolor=#FFFFF0><font color=#F6358A>" + "Loops for Generating Group Numbers</TH>\n";
            stmt += "<TH bgcolor=#FFFFF0><font color=#F6358A>" + "Loops for Generating Draw Numbers</TH>\n";
            stmt += "<TH bgcolor=#FFFFF0><font color=#F6358A>" + "Total Loops</TH>\n";
            for (int i = 1; i <= arr.Length; i++)
            {
                stmt += "<TH bgcolor=#FFFFF0><font color=#F6358A>No. " + i.ToString() + "</TH>";
            }
            stmt += "</TR>\n";

            stmt += "<TR>\n";
            stmt += "<TD><font color=#FFFFF0>" + gen_group_number_counter.ToString() + "</TD>";
            stmt += "<TD><font color=#FFFFF0>" + gen_draw_number_counter.ToString() + "</TD>";

            int totalLoops = gen_group_number_counter * gen_draw_number_counter;
            stmt += "<TD><font color=#FFFFF0>" + totalLoops.ToString() + "</TD>";
            for (int i = 0; i < arr.Length; i++)
            {
                stmt += "<TD width=50> <FONT style=FONT-STYLE: italic; font-weight:bold color=#FFFF00>" + arr[i].ToString() + "</TD>";
            }
            stmt += "</TR>";

            stmt += "</TABLE>\n";
            //stmt += Util.CreateHTML_Tail();

            return stmt;
        }


        public string distribution(Database db, int startRow, int targetRow, out long dbExecTime)
        {
            if (targetRow == 0)
            {
                targetRow = lastRow;
            }

            if (startRow == 0)
            {
                startRow = (targetRow < Util.MAX_ROWS) ? 1 : targetRow - Util.MAX_ROWS;
            }

            Dictionary<int, string> drawNumber = new Dictionary<int, string>();
            Dictionary<int, string> drawDate = new Dictionary<int, string>();
            Dictionary<int, int> sumRange = new Dictionary<int, int>();

            int TotalOdds = 0, TotalEvens = 0;

            numgen = new NumGen(db, startRow, targetRow);

            int drawLength = Util.getColumnnsOfLotto(db);
            int range = targetRow - startRow + 1;
            int[,] drawNumArray = new int[range, drawLength];

            dbExecTime = numgen.DatabaseExecutionTime;

            try
            {
                dataAccessLayer.OpenConnection();
                SqlDataReader reader = dataAccessLayer.SelectAllOnRangeOfDrawNo(db, startRow, targetRow);

                int i = 0;
                while (reader.Read() && i < range)
                {
                    drawNumber[i] = reader["DrawNumber"].ToString();
                    drawDate[i] = reader["DrawDate"].ToString();

                    for (int k = 0; k < drawLength; ++k)
                    {
                        drawNumArray[i, k] = int.Parse(reader[k+2].ToString());
                    }
                    i++;
                }
            }
            catch (SqlException exp)
            {
                throw exp;
            }
            finally
            {
                dataAccessLayer.CloseConnection();
            }

            string stmt = "";
            stmt += Util.CreateHTML_Header("Statistics 7", db);
            stmt += "<A href=" + fromSite + ">Back</A>"
                + "<TABLE border=1 align=center>\n";

            // <TH> part for the top of table
            stmt += "<TH class=tableheader >Draw No.</TH>";
            stmt += "<TH class=tableheader>Draw Date</TH>";
            string bonusColumn = Util.getBonusColName(db);
            if (drawLength > 2)
            {

                int j = 1;
                for (int i = 0; i < drawLength; ++i)
                {
                    if (bonusColumn == "Supp" && i >= drawLength - 2)
                    {
                        stmt += "<TH class=tableheader>" + bonusColumn + j.ToString() + "</TH>";
                        j++;
                    }
                    else if ((i == drawLength - 1) && (bonusColumn != null))
                    {
                        stmt += "<TH class=tableheader>" + bonusColumn + "</TH>";
                    }
                    else
                    {
                        stmt += "<TH class=tableheader>No. " + (i + 1).ToString() + "</TH>";
                    }
                }
            }
            else
            {
                for (int i = 0; i < drawLength; ++i)
                {
                    if (drawLength > 1)
                    {
                        stmt += "<TH class=tableheader>" + bonusColumn + (i + 1).ToString() + "</TH>";
                    }
                    else
                    {
                        stmt += "<TH class=tableheader>" + bonusColumn + "</TH>";
                    }
                }
            }
            stmt += "<TH class=tableheader>Sum</TH>";
            stmt += "<TH class=tableheader>Freq. of Sum</TH>";
            stmt += "<TH class=tableheader>Odd/Even</TH>";
            stmt += "<TH class=tableheader>Total Odd/Even</TH>";
            stmt += "</TR>";
            
            int[] arr = new int[drawLength];
            int oddNum = 0, evenNum = 0, sum = 0, rangeSum = 0;

            
            SubStatistics[] stat = numgen.Stat;

            int totalLottoNumbers = Util.getTotalLottoNumbers(db);
            int columnNumbers = Util.getColumnnsOfLotto(db);
            if (totalLottoNumbers > 40 && columnNumbers > 3)
            {
                Util.initSumRangeDic(sumRange, Util.LargeNumberRange.Length);
            }
            else
            {
                Util.initSumRangeDic(sumRange, Util.NumberRange.Length);
            }

            // <TD> part
            for (int i = 0; i < drawNumber.Count; ++i)
            {
                stmt += "<TR>";
                stmt += "<TD style=color:#FF00FF >" + drawNumber[i] + "</TD>"
                    + "<TD style=color:#FF00FF;width:120px;font-size:small>" + drawDate[i] + "</TD>";
                for (int j = 0; j < drawLength; ++j)
                {
                    stmt += "<TD style=color:#FF0000>" + drawNumArray[i, j];
                    arr[j] = drawNumArray[i, j];
                }
                int drawLength_no_bonus = Util.getColumnnsOfLotto_no_bonus(db);
                sum = Util.getSum(arr, drawLength_no_bonus);
                stmt += "<TD style=color:#0000FF>" + sum.ToString() + "</TD>";
                if (totalLottoNumbers > Util.SMALL_LOTTO_NUMBERS && columnNumbers > Util.SMALL_COLUMN_NUMBERS)
                {
                    rangeSum = Util.getSumLargeRange(sum, ref sumRange);
                }
                else
                {
                    rangeSum = Util.getSumRange(sum, ref sumRange);
                }
                
                stmt += "<TD style=color:#008000>" + rangeSum.ToString() + "</TD>";
                oddNum = Util.numOdds(arr);
                evenNum = arr.Length - oddNum;
                TotalOdds += oddNum;
                TotalEvens += evenNum;
                stmt += "<TD style=color:#F52887>" + oddNum.ToString() + "/<font style=color:#8467D7>" + evenNum.ToString() + "</TD>";
                stmt += "<TD style=color:#F52887>" + TotalOdds.ToString() + "/<font style=color:#8467D7>" + TotalEvens.ToString() + "</TD>";

                stmt += "</TR>";
            }

            stmt += "<TR>";
            // <TH> part for the bottom of table
            stmt += "<TH class=tableheader>Draw No.</TH>";
            stmt += "<TH class=tableheader>Draw Date</TH>";

            
            if (drawLength > 2)
            {
                
                int j = 1;
                for (int i = 0; i < drawLength; ++i)
                {
                    if (bonusColumn == "Supp" && i >= drawLength - 2)
                    {
                        stmt += "<TH class=tableheader>" + bonusColumn + j.ToString() + "</TH>";
                        j++;
                    }                   
                    else if ((i == drawLength - 1) && (bonusColumn != null))
                    {
                            stmt += "<TH class=tableheader>" + bonusColumn + "</TH>";
                    }
                    else
                    {
                        stmt += "<TH class=tableheader>No. " + (i + 1).ToString() + "</TH>";
                    }
                }
            }
            else
            {
                for (int i = 0; i < drawLength; ++i)
                {
                    if (drawLength > 1)
                    {
                        stmt += "<TH class=tableheader>" + bonusColumn + (i + 1).ToString() + "</TH>";
                    }
                    else
                    {
                        stmt += "<TH class=tableheader>" + bonusColumn + "</TH>";
                    }
                }
            }
            stmt += "<TH class=tableheader>Sum</TH>";
            stmt += "<TH class=tableheader>Freq. of Sum</TH>";
            stmt += "<TH class=tableheader>Odd/Even</TH>";
            stmt += "<TH class=tableheader>Total Odd/Even</TH>";
            stmt += "</TR>";
            stmt += "</TABLE>";
            if (bonusColumn != null)
            {
                stmt += "<div class='fromSite'><a href=" + fromSite + ">Back</a><em>Sum</em> is not including <em>Bonus, Extra, Stars, Supp ...</em> number</div>";
            }
            else
            {
                stmt += "<div class='fromSite'><a href=" + fromSite + ">Back</a></div><br/>";
            }

            stmt += Util.CreateHTML_Tail();


            return stmt;

        }

        private string displayPotentialNumbers(int counter, Dictionary<int, bool> dic, int[] arr, List<SubStatistics> list, int len)
        {
            string stmt = "<div id=\"gen\"><h2>Number of Generating: " + counter.ToString() + "</h2></div>";
            return stmt + Util.DisplayPotentialNumbers(dic, arr, list, db, len);
        }




        public Dictionary<int,int> getChartData(Database db, int startRow, int targetRow)
        {
            if (targetRow == 0)
            {
                targetRow = lastRow;
            }

            if (startRow == 0)
            {
                startRow = (targetRow < Util.MAX_ROWS) ? 1 : targetRow - Util.MAX_ROWS;
            }

            Dictionary<int, string> drawNumber = new Dictionary<int, string>();
            Dictionary<int, string> drawDate = new Dictionary<int, string>();
            Dictionary<int, int> sumRange = new Dictionary<int, int>();

         

            numgen = new NumGen(db, startRow, targetRow);

            int drawLength = Util.getColumnnsOfLotto_no_bonus(db);
            int range = targetRow - startRow + 1;
            int[,] drawNumArray = new int[range, drawLength];

            databaseExecutionTime = numgen.DatabaseExecutionTime;

            try
            {
                dataAccessLayer.OpenConnection();
                SqlDataReader reader = dataAccessLayer.SelectAllOnRangeOfDrawNo(db, startRow, targetRow);

                int i = 0;
                while (reader.Read() && i < range)
                {
                    drawNumber[i] = reader["DrawNumber"].ToString();
                    drawDate[i] = reader["DrawDate"].ToString();

                    for (int k = 0; k < drawLength; ++k)
                    {
                        drawNumArray[i, k] = int.Parse(reader[k+2].ToString());
                    }
                    i++;
                }
            }
            catch (SqlException exp)
            {
                throw exp;
            }
            finally
            {
                dataAccessLayer.CloseConnection();
            }

 

            int[] arr = new int[drawLength];
            int totalLottoNumbers = Util.getTotalLottoNumbers(db);
            int columnNumbers = Util.getColumnnsOfLotto(db);
            if (totalLottoNumbers > Util.SMALL_LOTTO_NUMBERS && columnNumbers > Util.SMALL_COLUMN_NUMBERS)
            {
                Util.initSumRangeDic(sumRange, Util.LargeNumberRange.Length);
            }
            else
            {
                Util.initSumRangeDic(sumRange, Util.NumberRange.Length);
            }

            for (int i = 0; i < range; ++i)
            {               
                for (int j = 0; j < drawLength; ++j)
                {              
                    arr[j] = drawNumArray[i, j];
                }
                int sum = Util.getSum(arr);

                if (totalLottoNumbers > Util.SMALL_LOTTO_NUMBERS && columnNumbers > Util.SMALL_COLUMN_NUMBERS)
                {
                    Util.getSumLargeRange(sum, ref sumRange);
                }
                else
                {
                    Util.getSumRange(sum, ref sumRange);
                }
            }

            return sumRange;
        }
    }
}