using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Data;

using DataAccessTier;
using BusinessTier;

namespace Lottotry.BusinessTier
{
    public class clsLotto : System.ComponentModel.Component
    {
        private static int lastRow = 0;
        private Dictionary<int, bool> targetrowDic;
        public ArrayList LottoArray = null;
        private Database db;
        public static ArrayList ncv = null;
        private string fromSite = " ";
        private DataAccessLayer dataAccessLayer = null;
        private NumGen numgen = null;
        private long databaseExecutionTime;

        static int RandomGen(int max)
        {
            return Util.rnd.Next(max);
        }

        public clsLotto(System.ComponentModel.IContainer container)
        {
            /// <summary>
            /// Required for Windows.Forms Class Composition Designer support
            /// </summary>
            container.Add(this);
            if (dataAccessLayer == null)
            {
                dataAccessLayer = new DataAccessLayer();
            }
            dataAccessLayer.OpenConnection();

            targetrowDic = new Dictionary<int, bool>();
        }

        public clsLotto(Database db, string from)
        {
            if (dataAccessLayer == null)
            {
                dataAccessLayer = new DataAccessLayer();
            }
            dataAccessLayer.OpenConnection();
            this.db = db;
            fromSite = from;
            databaseExecutionTime = 0;
            
            lastRow = dataAccessLayer.GetLastRow(db);
        }

        public long DatabaseExecutionTime
        {
            get
            {
                long tmp = databaseExecutionTime;
                databaseExecutionTime = 0;
                return tmp;
            }
        }

        public string highFreqDistribution(int start, int target, int distanceRange, out long dbExecTime)
        {
            string stmt = "";

            stmt += Util.CreateHTML_Header("Lotto Statistics 2\n", db);
            stmt += "<A href=" + fromSite + ">Back</A>\n";

            stmt += "<TABLE class=\"generated_tables\" border=\"1\">\n";

            // Draw table header
            stmt += "<TR>\n";

            for (int i = -1; i <= distanceRange; i++)
            {
                if (i == -1)
                {
                    stmt += "<TH style=\"color:Maroon; background-color:yellow; width:65px;font-size:small\">"
                        + "Draw\\Dist" + "</TH>\n";
                    continue;
                }
                if (i == 0)
                {
                    stmt += "<TH style=\"color:Maroon; width:120px;background-color:yellow;font-size:small\">"
                        + "Draw Date" + "</TH>\n";
                    continue;
                }
                stmt += "<TH style=\"width:65px; color:Maroon;background-color:#ffcccc\">" + i + "</TH>\n";
            }
            stmt += "<TH style=\"color:Maroon;background-color:#ffcccc\">" + "Total" + "</TH>\n";
            stmt += "</TR>\n";

            if (target == 0)
            {
                target = lastRow;
            }

            if (start == 0)
            {
                start = (target > Util.MAX_ROWS) ? target - Util.MAX_ROWS : 1;
            }

            numgen = new NumGen(db, start, target);
            dbExecTime = numgen.DatabaseExecutionTime;

            IEnumerator dno = numgen.DrawNo.GetEnumerator();
            IEnumerator ddate = numgen.DrawDate.GetEnumerator();         
            int cols = Util.getColumnnsOfLotto(db);

            // blow is exception only for Florida Lotto, not including bonus
            if (db == Database.FloridaLotto)
            {
                cols--;
            }
            IEnumerator[] en = new IEnumerator[cols];
            for (int i = 0; i < cols; i++)
            {
                en[i] = numgen.numberArray[i].GetEnumerator();
            }

            int loop = 0;
            int maxLoop = target - start + 1;

            while (dno.MoveNext() && loop < maxLoop)
            {
                loop++;
                ddate.MoveNext();
                for (int i = 0; i < cols; i++)
                {
                    en[i].MoveNext();
                }

                int[] a = new int[distanceRange + 1];
                for (int j = 0; j <= distanceRange; j++)
                {
                    a[j] = 0;
                }

                int total = 0;
                SubStatistics[] ss = new SubStatistics[Util.MAX_NUMBERS];
                for (int i = 0; i < cols; i++)
                {
                    ss[i] = (SubStatistics)en[i].Current;

                    if (ss[i].SavedDist <= distanceRange)
                    {
                        a[ss[i].SavedDist]++;
                        total++;
                    }
                }
               

                stmt += "<TR>\n";

                for (int i = -1; i <= distanceRange; i++)
                {
                    if (i == -1)
                    {
                        stmt += "<TD style=\"color:#ff3300; text-align:center;width:45px;\">"
                            + dno.Current + "</TD>\n";
                        continue;
                    }
                    if (i == 0)
                    {
                        stmt += "<TD style=\"color:#6960EC; width:120px;\">"
                            + ddate.Current + "</TD>\n";
                        continue;
                    }

                    if (a[i] != 0)
                    {
                        stmt += "<TD style=\"width:65px;\">" + a[i] + "</TD>\n";
                    }
                    else
                    {
                        stmt += "<TD style=\"background-color:#ffcc99; width:65px;\"></TD>\n";
                    }
                }
                stmt += "<TD style=\"width:45px; text-align:center; font-style:italic; color:#ff0033\">"
                    + total + "</TD>\n";
                stmt += "</TR>\n";
            }

            stmt += "<TR>\n";

            for (int i = -1; i <= distanceRange; i++)
            {
                if (i == -1)
                {
                    stmt += "<TH style=\"color:Maroon; background-color:yellow; width:65px;font-size:small \">" + "Draw\\Dist" + "</TH>\n";
                    continue;
                }
                if (i == 0)
                {
                    stmt += "<TH style=\"color:Maroon; width:120px; background-color:yellow; font-size:small\">" + "Draw Date" + "</TH>\n";
                    continue;
                }
                stmt += "<TH style=\"width:45px; color:Maroon;background-color:#ffcccc\">" + i + "</TH>\n";
            }
            stmt += "<TH bgcolor=\"ffcccc\">" + "Total" + "</TH>\n";
            stmt += "</TR>\n";

            stmt += "</TABLE>\n"
                + "<A href=" + fromSite + ">Back</A>\n";
            stmt += Util.CreateHTML_Tail();

            return stmt;
        }


        // Bubble Sort by frequence
        private void bubbleSort(Statistics[] st)
        {
            int i, j, n;
            n = st.Length;
            for (i = 0; i < n; i++)
            { /* n passes thru the array */
                /* fromSite start to the end of unsorted part */
                for (j = 1; j < (n - i); j++)
                {
                    /* If adjacent items out of order, swap */
                    if (st[j - 1].Cnt > st[j].Cnt)
                    {
                        Statistics t;
                        t = st[j - 1];
                        st[j - 1] = st[j];
                        st[j] = t;
                    }
                }
            }
        }

        // Bubble Sort by distance
        private void bubbleSortByDist(SubStatistics[] st)
        {
            int i, j, n;
            n = st.Length;
            for (i = 0; i < n; i++)
            { /* n passes thru the array */
                /* fromSite start to the end of unsorted part */
                for (j = 1; j < (n - i); j++)
                {
                    /* If adjacent items out of order, swap */
                    if (st[j - 1].RelativeDist > st[j].RelativeDist)
                    {
                        SubStatistics t;
                        t = st[j - 1];
                        st[j - 1] = st[j];
                        st[j] = t;
                    }
                }
            }
        }

        // Quicksort
        private void quicksort(Statistics[] a, int low, int high)
        {
            int pivot = 0;
            /* Termination condition! */
            if (high > low)
            {
                pivot = partition(a, low, high);
                quicksort(a, low, pivot - 1);
                quicksort(a, pivot + 1, high);
            }

        }
        // partition for Statistics count
        private int partition(Statistics[] a, int low, int high)
        {
            int left, right;
            Statistics pivot_item = a[low];
            left = low;
            right = high;
            while (left < right)
            {
                /* Move left while item < pivot */
                while (a[left].Cnt <= pivot_item.Cnt) left++;
                /* Move right while item > pivot */
                while (a[right].Cnt > pivot_item.Cnt) right--;
                if (left < right)
                {
                    Statistics t;
                    t = a[left];
                    a[left] = a[right];
                    a[right] = t;
                }
            }
            /* right is final position for the pivot */
            a[low] = a[right];
            a[right] = pivot_item;
            return right;
        }

        // quicksort for int []
        private void quicksort(int[] a, int low, int high)
        {
            int pivot = 0;
            /* Termination condition! */
            if (high > low)
            {
                pivot = partition(a, low, high);
                quicksort(a, low, pivot - 1);
                quicksort(a, pivot + 1, high);
            }

        }
        // partition for Statistics count
        private int partition(int[] a, int low, int high)
        {
            int left, right;
            int pivot_item = a[low];
            left = low;
            right = high;
            while (left < right)
            {
                /* Move left while item < pivot */
                while (a[left] <= pivot_item) left++;
                /* Move right while item > pivot */
                while (a[right] > pivot_item) right--;
                if (left < right)
                {
                    int t;
                    t = a[left];
                    a[left] = a[right];
                    a[right] = t;
                }
            }
            /* right is final position for the pivot */
            a[low] = a[right];
            a[right] = pivot_item;
            return right;
        }

        private bool isInNCV(int m)
        {
            NumberCounter nc;
            IEnumerator en = ncv.GetEnumerator();
            while (en.MoveNext())
            {
                nc = (NumberCounter)en.Current;
                if (m == nc.Num)
                {
                    return true;
                }
            }
            return false;
        }

        private string errPage(string errMsg)
        {
            string stmt = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01 Transitional//EN\">\n"
                + "<HTML>\n"
                + "<HEAD>\n"
                + "<TITLE>" + "Lotto Table Contents" + "</TITLE>\n"
                + "</HEAD>\n"
                + "<BODY BGCOLOR=\"#FFFFFF\">\n"
                + "<CENTER>\n"
                + "<H2>Warning: <font color=\"#ff0080\">" + errMsg + "</font></H2>\n"
                + "<A href=" + fromSite + ">Back</A>\n"
                + "</CENTER>\n"
                + "</BODY>\n"
                + "</HTML>\n";

            return stmt;

        }

        private string createHtml_Segment(int start, int scale, int[][] arr)
        {
            string stmt = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01 Transitional//EN\">\n"
                + "<HTML>\n"
                + "<HEAD>\n"
                + "<TITLE>" + "Retrieve Result" + "</TITLE>\n"
                + "</HEAD>\n"
                + "<BODY BGCOLOR=\"#FFFFFF\">\n"
                + "<CENTER>\n";
            stmt += Util.LottoLogos(db);

            stmt += "</CENTER>\n"
                + "<A href=" + fromSite + ">Back</A>\n"
                + "<HR>\n"
                + "<TABLE class=\"generated_tables\" border=\"1\">\n"
                + "<TR>\n"
                + "<TH>Draw No.</TH>\n";
            for (int j = 0; j < scale; j++)
            {
                stmt += "<TH>Scale " + (j + 1) + "</TH>\n";
            }
            stmt += "</TR>\n";


            for (int i = 0; i < arr.Length; i++)
            {
                stmt += "<TR>\n"
                    + "<TD><font color=\"#ff00ff\">" + (start + i) + "</font></TD>\n";
                for (int j = 0; j < arr[i].Length; j++)
                {
                    if (arr[i][j] != 0)
                    {
                        stmt += "<TD><font color=\"#ff3300\"><B>" + arr[i][j] + "</B></font></TD>\n";
                    }
                    else
                    {
                        stmt += "<TD bgcolor=\"999966\"></TD>\n";
                    }
                }
                stmt += "</TR>\n";
            }

            stmt += "<TR>\n"
                + "<TH>Draw No.</TH>\n";
            for (int j = 0; j < scale; j++)
            {
                stmt += "<TH>Scale " + (j + 1) + "</TH>\n";
            }
            stmt += "</TR>\n";

            stmt += "</TABLE>\n"
                + "<A href=" + fromSite + ">Back</A>\n"
                + "</BODY></HTML>\n";

            return stmt;

        }

        private int[] convertToArray(ArrayList al, int type)
        {
            int[] arr = new int[al.Count];
            IEnumerator en = al.GetEnumerator();
            int i = 0;
            while (en.MoveNext())
            {
                if (type == 2)
                {
                    NumberCounter nc = new NumberCounter();
                    nc = (NumberCounter)en.Current;
                    arr[i++] = (int)nc.Cnt;
                }
                else
                {
                    arr[i++] = (int)en.Current;
                }
            }
            return arr;
        }

        private string[] convertToArray(ArrayList al)
        {
            string[] arr = new string[al.Count];
            IEnumerator en = al.GetEnumerator();
            int i = 0;
            while (en.MoveNext())
            {
                arr[i++] = (string)en.Current;
            }
            return arr;
        }

        public bool isExist(int[] a)
        {
            IEnumerator en = LottoArray.GetEnumerator();
            while (en.MoveNext())
            {
                int[] b = (int[])en.Current;
                if (db == 0)
                {
                    if ((a[0] == b[0]) &&
                        (a[1] == b[1]) &&
                        (a[2] == b[2]) &&
                        (a[3] == b[3]) &&
                        (a[4] == b[4]) &&
                        (a[5] == b[5]))
                    {
                        return true;
                    }
                }
                else
                {
                    if (a[0] == b[0] &&
                        a[1] == b[1] &&
                        a[2] == b[2] &&
                        a[3] == b[3] &&
                        a[4] == b[4] &&
                        a[5] == b[5] &&
                        a[6] == b[6])
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        public string createHTML_NumDist(int start, int target, out long dbExecTime)
        {
            if (target == 0)
            {
                target = lastRow;
            }
            if (start == 0)
            {
                start = (target > Util.MAX_ROWS) ? (target - Util.MAX_ROWS) : 1;
            }
            numgen = new NumGen(db, start, target);
            dbExecTime = numgen.DatabaseExecutionTime;
            int cols = Util.getColumnnsOfLotto(db);
            int cols_no_bonus = Util.getColumnnsOfLotto_no_bonus(db);
            string stmt = " ";

            stmt += Util.CreateHTML_Header("Lotto Draw Number and Distance Table", db);

            // Top of the table
            stmt += "<A href=" + fromSite + ">Back</A>\n"
                + "<HR>\n"
                + "<TABLE class=\"generated_tables\" border=\"1\">\n"
                + "<TR>\n"
                + "<TH>Draw No</TH>\n"
                + "<TH>DrawDate</TH>\n";


            for (int i = 0; i < cols_no_bonus; ++i)
            {
                stmt += "<TH>No." + (i+1).ToString() + "</TH>\n";
            }
            if (db == Database.OZLottoTue)
            {
                stmt += "<TH>Supp1</TH><TH>Supp2</TH>\n";
            }
            else if (db == Database.SevenLotto)
                stmt += "<TH>Special</TH>\n";
            else if (db == Database.FloridaLucky)
                stmt += "<TH>Lucky Ball</TH>\n";
            else
            {
                if (cols > cols_no_bonus)
                {
                    stmt += "<TH>Bonus</TH>\n";
                }
            }
            stmt += "</TR>\n";
            
            IEnumerator dno = numgen.DrawNo.GetEnumerator();
            IEnumerator ddate = numgen.DrawDate.GetEnumerator();
            IEnumerator[] en = new IEnumerator[cols];

            for (int i = 0; i < cols; ++i)
            {
                en[i] = numgen.numberArray[i].GetEnumerator();
            }
            int loop = 0;
            int maxLoop = target - start + 1;
            SubStatistics [] ss = new SubStatistics[cols];
            while (dno.MoveNext() && loop < maxLoop)
            {
                loop++;
                ddate.MoveNext();
                for (int i = 0; i < cols; ++i)
                {
                    en[i].MoveNext();
                    ss[i] = (SubStatistics)en[i].Current;
                }
                stmt += "<TR>\n"
                    + "<TD><font color=\"#ff00ff\">" 
                    + dno.Current.ToString() 
                    + "</font></TD>\n"
                    + "<TD><font style=\"color:maroon;font-size:small;\">" 
                    + ddate.Current.ToString() 
                    + "</font></TD>\n";

                for (int i = 0; i < cols; ++i)
                {     
                    //stmt += "<TD align=\"center\"><font color=\"#ff0000\"><font weight=\"bolder\">" 
                    stmt += "<td><font style=font-weight:bold;color:#ff0000;text-align:center>"
                        + ss[i].Num 
                        + "</font> (<font style=\"FONT-STYLE: italic\" color=\"#0066cc\">" 
                        + ss[i].SavedDist 
                        + "</font>)</td>\n ";
                }
                stmt += "</TR>\n";
            }
            

            // For bottom of table
            stmt += "<TR>\n" + "<TH>Draw No</TH>\n" + "<TH>DrawDate</TH>\n";
            for (int i = 0; i < cols_no_bonus; ++i)
            {
                stmt += "<TH>No." + (i + 1).ToString() + "</TH>\n";
            }
            if (db == Database.OZLottoTue)
            {
                stmt += "<TH>Supp1</TH><TH>Supp2</TH>\n";
            }
            else if (db == Database.SevenLotto)
                stmt += "<TH>Special</TH>\n";
            else if (db == Database.FloridaLucky)
                stmt += "<TH>Lucky Ball</TH>\n";
            else
            {
                if (cols > cols_no_bonus)
                {
                    stmt += "<TH>Bonus</TH>\n";
                }
            }

            stmt += "</TR>\n";
            stmt += "</TABLE>\n" + "<A href=" + fromSite + ">Back</A>\n";
            stmt += Util.CreateHTML_Tail();
            return stmt;
        }

        public string createHTML_ScaleDistMatrix(
                                                 int target,
                                                 int dist,
                                                 int scale,
                                                 out long dbExecTime)
        {
            string stmt = " ";
            stmt += Util.CreateHTML_Header("Lotto Statistics 5", db);

            int start = 1;
            dbExecTime = 0;
            stmt += createLongDistTable(dist, scale, start, target, db, ref dbExecTime);
            stmt += createShortDistTable(dist, scale, start, target, db, ref  dbExecTime);

            stmt += createAllNumTable(dist, scale, start, target, db, ref dbExecTime);

            stmt += createDistScaleTable(dist, scale, start, target, db, ref dbExecTime);

            return stmt;
        }

        private string createLongDistTable(int dist, int scale, int start, int target, Database db, ref long dbExecTime)
        {

            string stmt = "";

            stmt += "<A href=" + fromSite + ">Back</A>\n"
                + "<HR>\n"
                + "Display <em>COLD</em> numbers (distance >= 10) and getting colder from LEFT to RIGHT in this table: <br />\n"
                + "<TABLE class=\"generated_tables\" border=\"1\">\n"
                + "<TR>\n";

            if (target == 0)
            {
                target = lastRow;
            }
            if (start == 0)
            {
                start = (target > Util.MAX_ROWS) ? (target - Util.MAX_ROWS) : 1;
            }
            numgen = new NumGen(db, start, target, dist, scale);
            dbExecTime += numgen.DatabaseExecutionTime;

            SubStatistics[] st;
            st = numgen.Stat;

            // Sort by Distance 
            //
            Util.selection_sort(st, target);


            for (int i = 0; i < st.Length; i++)
            {
                if (i == 0)
                {
                    stmt += "<TH>Number</TH>\n";
                    continue;
                }

                if (st[i].RelativeDist >= 10)
                {
                    stmt += "<TH style=\"color:Red;background-color:#E0FFFF;font-style:bold;\">" + st[i].Num + "</TH>\n";
                }
            }
            stmt += "</TR><TR>\n";
            for (int i = 0; i < st.Length; i++)
            {
                if (i == 0)
                {
                    stmt += "<TD><B>Distance</B></TD>\n";
                    continue;
                }
                if (st[i].RelativeDist >= 10)
                {
                    stmt += "<TD style=\"color:#fff;background-color:#800517;font-style:bold;\">" + st[i].RelativeDist + "</TD>\n";
                }
            }
            stmt += "</TR><TR>\n";
            for (int i = 0; i < st.Length; i++)
            {
                if (i == 0)
                {
                    stmt += "<TD><B>Frequency</B></TD>\n";
                    continue;
                }
                if (st[i].RelativeDist >= 10)
                {
                    stmt += "<TD style=\"color:#fff;background-color:#2B547E;font-style:bold;\">" + st[i].Cnt + "</TD>\n";
                }
            }

            stmt += "</TR>\n";

            stmt += "</TABLE>\n";

            return stmt;

        }

        private string createShortDistTable(int dist, int scale, int start, int target, Database db, ref long dbExecTime)
        {

            string stmt = "";

            stmt += "<br /><HR>\n"
                + "Display <em>HOTTEST</em> numbers (distance < 5), the number on row getting colder from left to the right in this table: <br>\n"
                + "<table class=\"generated_tables\" border=\"1\">\n"
                + "<TR>\n";

            if (target == 0)
            {
                target = lastRow;
            }
            if (start == 0)
            {
                start = (target > Util.MAX_ROWS) ? (target - Util.MAX_ROWS) : 1;
            }
            numgen = new NumGen(db, start, target, dist, scale);
            dbExecTime += numgen.DatabaseExecutionTime;

            SubStatistics[] st;
            st = numgen.Stat;

            // Sort by Distance
            //
            Util.selection_sort(st, target);


            for (int i = -1; i < st.Length; i++)
            {
                if (i == -1)
                {
                    stmt += "<TH>Number</TH>\n";
                    continue;
                }

                if (st[i].RelativeDist <= 4 && st[i].Num != 0)
                {
                    stmt += "<TH style=\"color:Red;background-color:#E0FFFF;font-style:bold;\">" + st[i].Num + "</TH>\n";
                }
            }
            stmt += "</TR><TR>\n";
            for (int i = -1; i < st.Length; i++)
            {
                if (i == -1)
                {
                    stmt += "<TD><B>Distance</B></TD>\n";
                    continue;
                }
                if (st[i].RelativeDist <= 4 && st[i].Num != 0)
                {
                    stmt += "<TD style=\"color:#fff;background-color:#800517;font-style:bold;\">" + st[i].RelativeDist + "</TD>\n";
                }
            }
            stmt += "</TR><TR>\n";
            for (int i = -1; i < st.Length; i++)
            {
                if (i == -1)
                {
                    stmt += "<TD><B>Frequency</B></TD>\n";
                    continue;
                }
                if (st[i].RelativeDist <= 4 && st[i].Num != 0)
                {
                    stmt += "<TD style=\"color:#fff;background-color:#2B547E;font-style:bold;\">" + st[i].Cnt + "</TD>\n";
                }
            }

            stmt += "</TR>\n";

            stmt += "</TABLE>\n";

            return stmt;

        }


        private string createFreqBand(int fragments)
        {
            // Mark the 49 numbers as scales (default is 7) with different colors 
            // for each scale

            string stmt = "";
            stmt += "<TR>\n";
            int n = fragments + 1;

            for (int i = -1; i <= numgen.ScaleLength; i++)
            {

                if (i == -1)
                {
                    stmt += "<TH class=\"tableheader\" style=\"font-size:small\">Draw No.\\Scales</TH>\n";
                    continue;
                }
                if (i == 0)
                {
                    stmt += "<TH class=\"tableheader\" style=\"font-size:small\">Draw Date</TH>\n";
                    continue;
                }

                if (i <= fragments)
                {
                    stmt += "<TH bgcolor=\"ffff66\"><font style=\"font-style: italic\">" + 1 + "</font>" + "." + i + "</TH>\n";
                }
                else if (i <= 2 * fragments)
                {
                    stmt += "<TH bgcolor=\"ffcccc\"><font style=\"font-style: italic\">" + 2 + "</font>" + "." + (i - fragments) + "</TH>\n";
                }
                else if (i <= 3 * fragments)
                {
                    stmt += "<TH bgcolor=\"ccff33\"><font style=\"font-style: italic\">" + 3 + "</font>" + "." + (i - 2 * fragments) + "</TH>\n";
                }
                else if (i <= 4 * fragments)
                {
                    stmt += "<TH bgcolor=\"ff99ff\"><font style=\"font-style: italic\">" + 4 + "</font>" + "." + (i - 3 * fragments) + "</TH>\n";
                }
                else if (i <= 5 * fragments)
                {
                    stmt += "<TH bgcolor=\"33cc99\"><font style=\"font-style: italic\">" + 5 + "</font>" + "." + (i - 4 * fragments) + "</TH>\n";
                }
                else if (i <= 6 * fragments)
                {
                    stmt += "<TH bgcolor=\"ffcc00\"><font style=\"font-style: italic\">" + 6 + "</font>" + "." + (i - 5 * fragments) + "</TH>\n";
                }
                else if (i <= 7 * fragments)
                {
                    stmt += "<TH bgcolor=\"cccc33\"><font style=\"font-style: italic\">" + 7 + "</font>" + "." + (i - 6 * fragments) + "</TH>\n"; ;
                }
                else if (i <= 8 * fragments)
                {
                    stmt += "<TH bgcolor=\"ffff66\"><font style=\"font-style: italic\">" + 8 + "</font>" + "." + (i - 7 * fragments) + "</TH>\n";
                }
                else if (i <= 9 * fragments)
                {
                    stmt += "<TH bgcolor=\"ffcccc\"><font style=\"font-style: italic\">" + 9 + "</font>" + "." + (i - 8 * fragments) + "</TH>\n";
                }
                else if (i <= 10 * fragments)
                {
                    stmt += "<TH bgcolor=\"ccff33\"><font style=\"font-style: italic\">" + 10 + "</font>" + "." + (i - 9 * fragments) + "</TH>\n";
                }
                else if (i <= 11 * fragments)
                {
                    stmt += "<TH bgcolor=\"ff99ff\"><font style=\"font-style: italic\">" + 11 + "</font>" + "." + (i - 10 * fragments) + "</TH>\n";
                }
                else if (i <= 12 * fragments)
                {
                    stmt += "<TH bgcolor=\"33cc99\"><font style=\"font-style: italic\">" + 12 + "</font>" + "." + (i - 11 * fragments) + "</TH>\n";
                }
                else if (i <= 13 * fragments)
                {
                    stmt += "<TH bgcolor=\"ffcc00\"><font style=\"font-style: italic\">" + 13 + "</font>" + "." + (i - 12 * fragments) + "</TH>\n";
                }
                else if (i <= 14 * fragments)
                {
                    stmt += "<TH bgcolor=\"cccc33\"><font style=\"font-style: italic\">" + 14 + "</font>" + "." + (i - 13 * fragments) + "</TH>\n";
                }
                else if (i <= 15 * fragments)
                {
                    stmt += "<TH bgcolor=\"ffff66\"><font style=\"font-style: italic\">" + 15 + "</font>" + "." + (i - 14 * fragments) + "</TH>\n";
                }
                else if (i <= 16 * fragments)
                {
                    stmt += "<TH bgcolor=\"ffcccc\"><font style=\"font-style: italic\">" + 16 + "</font>" + "." + (i - 15 * fragments) + "</TH>\n";
                }
                else if (i <= 17 * fragments)
                {
                    stmt += "<TH bgcolor=\"ccff33\"><font style=\"font-style: italic\">" + 17 + "</font>" + "." + (i - 16 * fragments) + "</TH>\n";
                }
            }
            stmt += "</TR>\n";
            return stmt;
        }


        public string createAllNumAllDrawsTable(int scale, int start, int target, out long dbExecTime)
        {
            if (target == 0)
            {
                target = lastRow;
            }
            if (start == 0)
            {
                start = (target > Util.MAX_ROWS) ? target - Util.MAX_ROWS : 1;
            }
            numgen = new NumGen(db, start, target, scale);
            dbExecTime = numgen.DatabaseExecutionTime;

            //int fragments = Convert.ToInt32(Convert.ToDouble((double)numgen.ScaleLength / (double)scale) + 0.5);
            int fragments = numgen.ScaleLength / scale;

            string stmt = " ";
            stmt += Util.CreateHTML_Header("Lotto Statistics 3", db);
            stmt += "<A href=" + fromSite + ">Back</A>\n";

            if (numgen.ScaleLength < 20)
                stmt += "<table class=\"smallTable\" border=\"1\">\n";
            else if (numgen.ScaleLength < 40)
                stmt += "<table class=\"mediumTable\" border=\"1\">\n";
            else
                stmt += "<table class=\"tblRetrieveAllNumber\" border=\"1\">\n";

            SubStatistics[] st = new SubStatistics[numgen.ScaleLength];
            st = numgen.Stat;

            stmt += createFreqBand(fragments);

            for (int j = start; j <= target; j++)
            {
                numgen = new NumGen(db, 0, j, scale);
                dbExecTime += numgen.DatabaseExecutionTime;
                st = numgen.Stat;

                // Sort st with Frequency
                bubbleSort(st);

                stmt += "<TR>\n";

                stmt += "<TH style=\"color:#ff33ff; font-size:small\">" + j + "</font></TH>\n";


                // Only for output draw date for this table row
                for (int i = 1; i < st.Length; i++)
                {
                    if (st[i].RelativeDist == 0)
                    {
                        stmt += "<TH style=\"color:#ff33ff; width:120px; font-size:small\">" + st[i].DDate + "</TH>\n";
                        break;
                    }
                }

                for (int i = 1; i < st.Length; i++)
                {
                    if (st[i].RelativeDist == 0)
                    {
                        stmt += "<TD bgcolor=\"#ffff00\"><font style=\"font-size: 14pt;FONT-STYLE: italic; TEXT-ALIGN: justify\" color=\"#ff0099\"><B>"
                                + st[i].Num
                                + "</B></font>"
                                + "<font color=\"#cc00cc\">("
                                + st[i].SavedDist
                                + ")</font></TD>\n";
                    }
                    else
                    {
                        stmt += "<TD><font color=\"#3366ff\">"
                                + st[i].Num
                                + "</font>("
                                + "<font style=\"FONT-STYLE: italic\" color=\"#339900\">"
                                + st[i].getDist(j)
                                + ")</font></TD>\n";
                    }
                }
                stmt += "</TR>\n";
            }
            stmt += createFreqBand(fragments);

            stmt += "</TABLE>\n";

            stmt += "<HR>" + "<A href=" + fromSite + ">Back</A>\n";

            stmt += Util.CreateHTML_Tail();

            return stmt;
        }

        public string createAllNumAllDrawsTableByDist(int start, int target, out long dbExecTime)
        {
            if (target == 0)
            {
                target = lastRow;
            }
            if (start == 0)
            {
                start = (target > Util.MAX_ROWS) ? (target - Util.MAX_ROWS) : 1;
            }

            numgen = new NumGen(db, start, target);
            dbExecTime = numgen.DatabaseExecutionTime;

            string stmt = " ";

            stmt += Util.CreateHTML_Header("Lotto Statistics 4", db);
            stmt += "<A href=" + fromSite + ">Back</A>\n";
            if (numgen.ScaleLength < 20)
                stmt += "<table class=\"smallTable\" border=\"1\">\n";
            else if (numgen.ScaleLength < 40)
                stmt += "<table class=\"mediumTable\" border=\"1\">\n";
            else
                stmt += "<table class=\"tblRetrieveAllNumber\" border=\"1\">\n";
            SubStatistics[] st = new SubStatistics[numgen.ScaleLength];
            st = numgen.Stat;

            // All numbers as scale
            for (int i = -1; i < st.Length; i++)
            {
                if (i == -1)
                {
                    stmt += "<TH class=\"tableheader\">Draw No.</TH>";
                    continue;
                }
                else if (i == 0)
                {
                    stmt += "<TH class=\"tableheader\">Draw Date</TH>";
                    continue;
                }
                else
                {
                    stmt += "<TH class=\"tableheader\"><font color=\"#ff33ff\">" + i + "</font></TH>\n";
                }
            }

            for (int j = start; j <= target; j++)
            {
                numgen = new NumGen(db, 0, j);
                dbExecTime += numgen.DatabaseExecutionTime;
                st = numgen.Stat;

                // Sort st with Relative Distance
                bubbleSortByDist(st);
                //quicksort(st, 0, st.Length);

                stmt += "<TR>\n";

                stmt += "<TH><font color=\"#ff33ff\">" + j + "</font></TH>\n";

                for (int i = 1; i < st.Length; i++)
                {
                    if (st[i].RelativeDist == 0)
                    {
                        stmt += "<TH style=\"color:#ff33ff; font-size:small;\">" + st[i].DDate + "</TH>\n";
                        break;
                    }
                }

                for (int i = 1; i < st.Length; i++)
                {
                    if (st[i].RelativeDist == 0)
                    {
                        stmt += "<TD bgcolor=\"#ffff00\"><font style=\"font-size: 14pt;FONT-STYLE: italic; TEXT-ALIGN: justify\" color=\"#ff0099\"><B>"
                                + st[i].Num
                                + "</B></font>"
                                + "<font color=\"#cc00cc\">("
                                + st[i].SavedDist
                                + ")</font></TD>\n";
                    }
                    else
                    {

                        if (st[i].RelativeDist % 7 == 0)
                        {
                            stmt += "<TD bgcolor=\"#ccff33\"><font style=\"font-size: 12pt\" color=\"#003333\"><B>" + st[i].Num;
                            stmt += "</B></font>("
                                    + "<font style=\"FONT-STYLE: italic\" color=\"#6600cc\">"
                                    + st[i].getDist(j)
                                    + ")</font></TD>\n";
                        }
                        else if (st[i].RelativeDist % 7 == 1)
                        {
                            stmt += "<TD bgcolor=\"#99cdff\"><font style=\"font-size: 12pt\" color=\"#003333\"><B>" + st[i].Num;
                            stmt += "</B></font>("
                                    + "<font style=\"FONT-STYLE: italic\" color=\"#6600cc\">"
                                    + st[i].getDist(j)
                                    + ")</font></TD>\n";
                        }
                        else if (st[i].RelativeDist % 7 == 2)
                        {
                            stmt += "<TD bgcolor=\"#cc99cc\"><font style=\"font-size: 12pt\" color=\"#003333\"><B>" + st[i].Num;
                            stmt += "</B></font>("
                                    + "<font style=\"FONT-STYLE: italic\" color=\"#6600cc\">"
                                    + st[i].getDist(j)
                                    + ")</font></TD>\n";
                        }
                        else if (st[i].RelativeDist % 7 == 3)
                        {
                            stmt += "<TD bgcolor=\"#ffcdff\"><font style=\"font-size: 12pt\" color=\"#003333\"><B>" + st[i].Num;
                            stmt += "</B></font>("
                                    + "<font style=\"FONT-STYLE: italic\" color=\"#6600cc\">"
                                    + st[i].getDist(j)
                                    + ")</font></TD>\n";
                        }
                        else if (st[i].RelativeDist % 7 == 4)
                        {
                            stmt += "<TD bgcolor=\"#ff99cc\"><font style=\"font-size: 12pt\" color=\"#003333\"><B>" + st[i].Num;
                            stmt += "</B></font>("
                                    + "<font style=\"FONT-STYLE: italic\" color=\"#6600cc\">"
                                    + st[i].getDist(j)
                                    + ")</font></TD>\n";
                        }
                        else if (st[i].RelativeDist % 7 == 5)
                        {
                            stmt += "<TD bgcolor=\"#cc9900\"><font style=\"font-size: 12pt\" color=\"#003333\"><B>" + st[i].Num;
                            stmt += "</B></font>("
                                    + "<font style=\"FONT-STYLE: italic\" color=\"#6600cc\">"
                                    + st[i].getDist(j)
                                    + ")</font></TD>\n";
                        }
                        else
                        {
                            stmt += "<TD bgcolor=\"#ffcccc\"><font style=\"font-size: 12pt\" color=\"#003333\"><B>" + st[i].Num;
                            stmt += "</B></font>("
                                    + "<font style=\"FONT-STYLE: italic\" color=\"#6600cc\">"
                                    + st[i].getDist(j)
                                    + ")</font></TD>\n";
                        }

                    }
                }
                stmt += "</TR>\n";
            }

            // All numbers as scale
            for (int i = -1; i < st.Length; i++)
            {
                if (i == -1)
                {
                    stmt += "<TH class=\"tableheader\">Draw No.</TH>";
                    continue;
                }
                else if (i == 0)
                {
                    stmt += "<TH class=\"tableheader\">Draw Date</TH>";
                    continue;
                }
                else
                {
                    stmt += "<TH class=\"tableheader\"><font color=\"#ff33ff\">" + i + "</font></TH>\n";
                }
            }

            stmt += "</TABLE>\n";

            stmt += "<HR>" + "<A href=" + fromSite + ">Back</A>\n";
            stmt += Util.CreateHTML_Tail();


            return stmt;
        }


        private string createAllNumTable(int dist, int scale, int start, int target, Database db, ref long dbExecTime)
        {
            if (target == 0)
            {
                target = lastRow;
            }
            if (start == 0)
            {
                start = (target > Util.MAX_ROWS) ? (target - Util.MAX_ROWS) : 1;
            }
            numgen = new NumGen(db, start, target, dist, scale);
            dbExecTime += numgen.DatabaseExecutionTime;

            int fragments = numgen.ScaleLength / scale;

            string stmt = " ";

            //stmt += "<A href=" + fromSite + ">Back</A>\n"
            stmt += "<br /><HR>\n";
            stmt += "Display whole numbers, fragmented into <em>scales</em>, for the <em>current draw</em> with <em>Frequcy</em> and <em>Distance</em> mapping to each numbers:\n"
                + "<br />\n"
                + "<table class=\"generated_tables\" border=\"1\">\n";


            SubStatistics[] st;
            st = numgen.Stat;

            // Sort st with Frequency
            bubbleSort(st);


            stmt += "<TR>\n";



            // Mark the 49 numbers as scales (default is 7) with different colors 
            // for each scale
            for (int i = 0; i < st.Length; i++)
            {

                if (i == 0)
                {
                    stmt += "<TH>Scales</TH>\n";
                    continue;
                }

                if (i <= fragments)
                {
                    stmt += "<TH bgcolor=\"ffff66\">" + 1 + "</TH>\n";
                }
                else if (i <= 2 * fragments)
                {
                    stmt += "<TH bgcolor=\"ffcccc\">" + 2 + "</TH>\n";
                }
                else if (i <= 3 * fragments)
                {
                    stmt += "<TH bgcolor=\"ccff33\">" + 3 + "</TH>\n";
                }
                else if (i <= 4 * fragments)
                {
                    stmt += "<TH bgcolor=\"ff99ff\">" + 4 + "</TH>\n";
                }
                else if (i <= 5 * fragments)
                {
                    stmt += "<TH bgcolor=\"33cc99\">" + 5 + "</TH>\n";
                }
                else if (i <= 6 * fragments)
                {
                    stmt += "<TH bgcolor=\"ffcc00\">" + 6 + "</TH>\n";
                }
                else if (i <= 7 * fragments)
                {
                    stmt += "<TH bgcolor=\"cccc33\">" + 7 + "</TH>\n"; ;
                }
                else if (i <= 8 * fragments)
                {
                    stmt += "<TH bgcolor=\"ffff66\">" + 8 + "</TH>\n";
                }
                else if (i <= 9 * fragments)
                {
                    stmt += "<TH bgcolor=\"ffcccc\">" + 9 + "</TH>\n";
                }
                else if (i <= 10 * fragments)
                {
                    stmt += "<TH bgcolor=\"ccff33\">" + 10 + "</TH>\n";
                }
                else if (i <= 11 * fragments)
                {
                    stmt += "<TH bgcolor=\"ff99ff\">" + 11 + "</TH>\n";
                }
                else if (i <= 12 * fragments)
                {
                    stmt += "<TH bgcolor=\"33cc99\">" + 12 + "</TH>\n";
                }
                else if (i <= 13 * fragments)
                {
                    stmt += "<TH bgcolor=\"ffcc00\">" + 13 + "</TH>\n";
                }
                else if (i <= 14 * fragments)
                {
                    stmt += "<TH bgcolor=\"cccc33\">" + 14 + "</TH>\n";
                }
                else if (i <= 15 * fragments)
                {
                    stmt += "<TH bgcolor=\"ffff66\">" + 15 + "</TH>\n";
                }
                else if (i <= 16 * fragments)
                {
                    stmt += "<TH bgcolor=\"ffcccc\">" + 16 + "</TH>\n";
                }
                else if (i <= 17 * fragments)
                {
                    stmt += "<TH bgcolor=\"ccff33\">" + 17 + "</TH>\n";
                }
            }
            stmt += "</TR><TR>\n";

            for (int i = 0; i < st.Length; i++)
            {
                if (i == 0)
                {
                    stmt += "<TH>Number</TH>\n";
                    continue;
                }
                if (st[i].RelativeDist == 0)
                {
                    stmt += "<TH style=\"background-color:#E0FFFF;\"><font style=\"FONT-STYLE: italic\" color=\"#ff0033\"><B>" 
                        + st[i].Num + "</B></font></TH>\n";
                }
                else
                {
                    stmt += "<TH style=\"background-color:#E0FFFF;\">" 
                        + st[i].Num + "</TH>\n";
                }
            }
            stmt += "</TR><TR>\n";
            for (int i = 0; i < st.Length; i++)
            {
                if (i == 0)
                {
                    stmt += "<TD><B>Frequency</B></TD>\n";
                    continue;
                }

                stmt += "<TD style=\"background-color:#C3FDB8;\"><font color=\"#ff0000\">" 
                    + st[i].Cnt + "</font></TD>\n";

            }
            stmt += "</TR><TR>\n";
            for (int i = 0; i < st.Length; i++)
            {
                if (i == 0)
                {
                    stmt += "<TD><B>Distance</B></TD>\n";
                    continue;
                }
                if (st[i].RelativeDist == 0)
                {
                    stmt += "<TD align=\"center\" style=\"background-color:#FFF8C6;\"><font color=\"#827839\">" 
                        + st[i].RelativeDist
                        + "</font><font style=\"FONT-STYLE: italic\" color=\"#254117\">"
                        + "(" + st[i].SavedDist + ")"
                        + "</font></TD>\n";
                }
                else
                {
                    stmt += "<TD align=\"center\" style=\"background-color:#FFF8C6;\"><font color=\"#ff0000\">" 
                        + st[i].RelativeDist + "</font></TD>\n";
                }
            }

            stmt += "</TR>\n";

            stmt += "</TABLE>\n";
            stmt += Util.CreateHTML_Tail(databaseExecutionTime);

            return stmt;

        }

        private string createAllNumRow(int target, Database db)
        {

            numgen = new NumGen(db, target);
            databaseExecutionTime += numgen.DatabaseExecutionTime;

            string stmt = " ";

            SubStatistics[] st;
            st = numgen.Stat;

            stmt += "<TR>\n";

            for (int i = 1; i < st.Length; i++)
            {
                if (st[i].RelativeDist == 0)
                {
                    stmt += "<TH style=\"color:#ff00ff\">" 
                        + st[i].DrawNumber.ToString() + "</TH>\n";
                    stmt += "<TH style=\"color:#ff00ff;width=120px;font-size:small;\">" 
                        + st[i].DDate + "</TH>\n";
                    break;
                }
            }



            for (int i = 1; i < st.Length; i++)
            {
                if (st[i].RelativeDist == 0)
                {
                    stmt += "<TD id=tdRelativeDist0 class=drawNumber>" + st[i].Num + "\n";
                    stmt += "( " + "<font id=relativeDist0>" + st[i].SavedDist + "</font> )" + "</TD>\n";
                }
                else
                {
                    stmt += "<TD id=numbers>" + st[i].Num + "\n";
                    stmt += "( " + "<font id=tdRelativeDist>" + st[i].RelativeDist + "</font> )" + "</TD>\n";
                    
                }
            }
            stmt += "</TR>\n";

            return stmt;

        }

        public string retrieveAllNumDist(int start, int target, out long dbExecTime)
        {
            numgen = new NumGen(db, target);
            string stmt = " ";
            stmt += Util.CreateHTML_Header("Statistics 6", db);
            stmt += "<A href=" + fromSite + ">Back</A>\n";
            if (numgen.ScaleLength < 20)
                stmt += "<table class=\"smallTable\" border=\"1\">\n";
            else if(numgen.ScaleLength < 40)
                stmt += "<table class=\"mediumTable\" border=\"1\">\n";
            else
                stmt += "<table class=\"tblRetrieveAllNumber\" border=\"1\">\n";
            dbExecTime = numgen.DatabaseExecutionTime;
            if (target == 0)
            {
                target = lastRow;
            }

            if (start == 0)
            {
                start = (target > Util.MAX_ROWS) ? target - Util.MAX_ROWS : 1;
            }
            for (int i = start; i <= target; i++)
            {
                stmt += createAllNumRow(i, db);
            }


            stmt += "</TABLE>\n"
                + "<A href=" + fromSite + ">Back</A>\n";

            stmt += Util.CreateHTML_Tail();

            return stmt;

        }

        private string createDistScaleTable(int dist, int scale, int start, int target, Database db, ref long dbExecTime)
        {

            if (target == 0)
            {
                target = lastRow;
            }
            if (start == 0)
            {
                start = (target > Util.MAX_ROWS) ? (target - Util.MAX_ROWS) : 1;
            }
            numgen = new NumGen(db, start, target, dist, scale);
            dbExecTime += numgen.DatabaseExecutionTime;

            string stmt = " ";

            stmt += "<br /><HR>\n"
                + "Display <em>Distance</em> vs <em>Scale</em> matrix on this table:<br>\n"
                + "<em>Scales</em>, shown as columns of the table, fregmented based on <em>frequencies</em> of numbers. <br>\n"
                + "Each <em>scale</em> contains multiple numbers which's <em>distances</em> shown as rows of the table. <br>\n"
                + "<table class=\"generated_tables\" border=\"1\">\n"
                + "<TR>\n"
                + "<TH>Dist\\Scale</TH>\n";


            for (int i = 1; i <= numgen.Scale; i++)
            {

                switch (i)
                {
                    case 1:
                        stmt += "<TH width=\"80\" bgcolor=\"ffff66\">" + i + "</TH>\n";
                        break;
                    case 2:
                        stmt += "<TH width=\"80\" bgcolor=\"ffcccc\">" + i + "</TH>\n";
                        break;
                    case 3:
                        stmt += "<TH width=\"80\" bgcolor=\"ccff33\">" + i + "</TH>\n";
                        break;
                    case 4:
                        stmt += "<TH width=\"80\" bgcolor=\"ff99ff\">" + i + "</TH>\n";
                        break;
                    case 5:
                        stmt += "<TH width=\"80\" bgcolor=\"33cc99\">" + i + "</TH>\n";
                        break;
                    case 6:
                        stmt += "<TH width=\"80\" bgcolor=\"ffcc00\">" + i + "</TH>\n";
                        break;
                    case 7:
                        stmt += "<TH width=\"80\" bgcolor=\"cccc33\">" + i + "</TH>\n";
                        break;
                    case 8:
                        stmt += "<TH width=\"80\" bgcolor=\"ffff66\">" + i + "</TH>\n";
                        break;
                    case 9:
                        stmt += "<TH width=\"80\" bgcolor=\"ffcccc\">" + i + "</TH>\n";
                        break;
                    case 10:
                        stmt += "<TH width=\"80\" bgcolor=\"ccff33\">" + i + "</TH>\n";
                        break;
                    case 11:
                        stmt += "<TH width=\"80\" bgcolor=\"ff99ff\">" + i + "</TH>\n";
                        break;
                    case 12:
                        stmt += "<TH width=\"80\" bgcolor=\"33cc99\">" + i + "</TH>\n";
                        break;
                    case 13:
                        stmt += "<TH width=\"80\" bgcolor=\"ffcc00\">" + i + "</TH>\n";
                        break;
                    case 14:
                        stmt += "<TH width=\"80\" bgcolor=\"cccc33\">" + i + "</TH>\n";
                        break;
                    case 15:
                        stmt += "<TH width=\"80\" bgcolor=\"ffff66\">" + i + "</TH>\n";
                        break;
                    case 16:
                        stmt += "<TH width=\"80\" bgcolor=\"ffcccc\">" + i + "</TH>\n";
                        break;
                    case 17:
                        stmt += "<TH width=\"80\" bgcolor=\"ccff33\">" + i + "</TH>\n";
                        break;
                    case 18:
                        stmt += "<TH width=\"80\" bgcolor=\"ff99ff\">" + i + "</TH>\n";
                        break;
                    case 19:
                        stmt += "<TH width=\"80\" bgcolor=\"33cc99\">" + i + "</TH>\n";
                        break;
                    case 20:
                        stmt += "<TH width=\"80\" bgcolor=\"ffcc00\">" + i + "</TH>\n";
                        break;
                    case 21:
                        stmt += "<TH width=\"80\" bgcolor=\"cccc33\">" + i + "</TH>\n";
                        break;
                    case 22:
                        stmt += "<TH width=\"80\" bgcolor=\"ffff66\">" + i + "</TH>\n";
                        break;
                    case 23:
                        stmt += "<TH width=\"80\" bgcolor=\"ffcccc\">" + i + "</TH>\n";
                        break;
                    case 24:
                        stmt += "<TH width=\"80\" bgcolor=\"ccff33\">" + i + "</TH>\n";
                        break;
                    case 25:
                        stmt += "<TH width=\"80\" bgcolor=\"ff99ff\">" + i + "</TH>\n";
                        break;
                }
            }
            stmt += "</TR>\n";

            for (int d = 0; d < dist; d++)
            {
                stmt += "<TR>";
                for (int s = 0; s <= numgen.Scale; s++)
                {
                    if (s == 0)
                    {
                        stmt += "<TD  align=\"center\"><font style=\"FONT-STYLE: italic\" color=\"#ff0033\"><B>" + (d) + "</B></font></TD>";
                    }
                    else
                    {

                        ArrayList al = numgen.getScaleDistMatrix(d, s);
                        if (al.Count != 0)
                        {
                            IEnumerator cell = al.GetEnumerator();

                            stmt += "<TD align=\"center\">";
                            while (cell.MoveNext())
                            {
                                stmt += (int)cell.Current + ", ";
                            }
                            stmt += "</TD>";
                        }
                        else
                        {
                            // Blank cell
                            stmt += "<TD bgcolor=\"ffcc99\">\n";
                        }
                    }
                }
                stmt += "</TR>";
            }


            stmt += "</TABLE>\n"
                + "<A href=" + fromSite + ">Back</A>\n";
            stmt += Util.CreateHTML_Tail();

            return stmt;

        }

        public string classifyLottoNumbers(int startRow, int targetRow, out long dbExecTime)
        {
            if (targetRow == 0)
            {
                targetRow = lastRow;
            }

            if (startRow == 0)
            {
                startRow = (targetRow < Util.MAX_ROWS) ? 1 : targetRow - Util.MAX_ROWS;
            }

            numgen = new NumGen(db, startRow, targetRow);
            dbExecTime = numgen.DatabaseExecutionTime;

            int cols = Util.getColumnnsOfLotto(db);
            int t = targetRow - startRow + 1;


            // initialize arr, size of second dimention of arr is 5:
            // 1 - 9, 10 - 19, 20 - 29, 30 - 39, 40 - 50
            //
            int[][] arr = new int[t][];
            string[] drawDateArray = new string[t];
            int ranges = Util.getLottoNumberRanges(db);
            for (int i = 0; i < t; i++)
            {
                arr[i] = new int[ranges];
                for (int j = 0; j < arr[i].Length; j++)
                {
                    arr[i][j] = 0;
                }
            }
            IEnumerator ddate = numgen.DrawDate.GetEnumerator();
            IEnumerator [] n = new IEnumerator[cols];
            for (int k = 0; k < cols; k++)
            {
                n[k] = numgen.numberArray[k].GetEnumerator();
            }

            int ii = 0;
            SubStatistics [] ss = new SubStatistics[cols];
            int[] num = new int[cols];

            while (n[0].MoveNext() && ii < t)
            {
                ddate.MoveNext();
                ss[0] = (SubStatistics)n[0].Current;
                num[0] = ss[0].Num;
                for (int k = 1; k < cols; ++k)
                {
                    n[k].MoveNext();
                    ss[k] = (SubStatistics)n[k].Current;
                    num[k] = ss[k].Num;                   
                }

                drawDateArray[ii] = (string)ddate.Current;
                for (int i = 0; i < num.Length; i++)
                {
                    if (num[i] >= 1 && num[i] < 10)
                    {
                        arr[ii][0]++;
                    }
                    else if (num[i] >= 10 && num[i] < 20)
                    {
                        arr[ii][1]++;
                    }                   
                    else if (num[i] >= 20 && num[i] < 30)
                    {
                        arr[ii][2]++;
                    }
                    else if (num[i] >= 30 && num[i] < 40)
                    {
                        arr[ii][3]++;
                    }
                    else if (num[i] >= 40 && num[i] <= 50)
                    {
                        arr[ii][4]++;
                    }
                    else if (num[i] > 50 && num[i] < 60)
                    {
                        arr[ii][5]++;
                    }
                    else if (num[i] >= 60 && num[i] < 70)
                    {
                        arr[ii][6]++;
                    }
                    else if (num[i] >= 70)
                    {
                        arr[ii][7]++;
                    }                    
                }
                ii++;
            }
            return createHtml_Classify(targetRow, drawDateArray, arr);
        }


        private string createHtml_Classify(int targetRow, string[] ddate, int[][] arr)
        {

            int firstDrawNumber = (targetRow < Util.MAX_ROWS) ? 0 : targetRow - Util.MAX_ROWS;
            string stmt = "";
            int numbers = Util.getTotalLottoNumbers(db);

            stmt += Util.CreateHTML_Header("Lotto Statistics 1", db);
            //stmt += "<p style=\"color:green\">Database execution time: " + DatabaseExecutionTime.ToString() + " ms</p>";
            stmt += "<A href=" + fromSite + ">Back</A>\n"
                + "<TABLE class=\"generated_tables\" border=\"1\" >\n"
                + "<TR>\n"
                + "<TH class=\"tableheader\">Draw No.</TH>\n"
                + "<TH class=\"tableheader\">Draw Date</TH>\n";
             
            if (numbers < 10)
            {
                stmt += "<TH class=\"tableheader\">1 - " + numbers.ToString() + "</TH>\n";
            }
            else if (numbers < 20)
            {
                stmt += "<TH class=\"tableheader\">1 - 9</TH>\n";
                stmt += "<TH class=\"tableheader\">10 - " + numbers.ToString() + "</TH>\n";
            }
            else if (numbers < 30)
            {
                stmt += "<TH class=\"tableheader\">1 - 9</TH>\n";
                stmt += "<TH class=\"tableheader\">10 - 19</TH>\n";
                stmt += "<TH class=\"tableheader\">20 - " + numbers.ToString() + "</TH>\n"; 
            }           
            else if (numbers < 40)
            {
                stmt += "<TH class=\"tableheader\">1 - 9</TH>\n";
                stmt += "<TH class=\"tableheader\">10 - 19</TH>\n";
                stmt += "<TH class=\"tableheader\">20 - 29</TH>\n";
                stmt += "<TH class=\"tableheader\">30 - " + numbers.ToString() + "</TH>\n"; 
            }
            else if (numbers <= 50)
            {
                stmt += "<TH class=\"tableheader\">1 - 9</TH>\n";
                stmt += "<TH class=\"tableheader\">10 - 19</TH>\n";
                stmt += "<TH class=\"tableheader\">20 - 29</TH>\n";
                stmt += "<TH class=\"tableheader\">30 - 39</TH>\n";
                stmt += "<TH class=\"tableheader\">40 - " + numbers.ToString() + "</TH>\n"; 
            }
            else if (numbers < 60)
            {
                stmt += "<TH class=\"tableheader\">1 - 9</TH>\n";
                stmt += "<TH class=\"tableheader\">10 - 19</TH>\n";
                stmt += "<TH class=\"tableheader\">20 - 29</TH>\n";
                stmt += "<TH class=\"tableheader\">30 - 39</TH>\n";
                stmt += "<TH class=\"tableheader\">40 - 49</TH>\n";
                stmt += "<TH class=\"tableheader\">50 - " + numbers.ToString() + "</TH>\n"; 
            }
            else if (numbers < 70)
            {
                stmt += "<TH class=\"tableheader\">1 - 9</TH>\n";
                stmt += "<TH class=\"tableheader\">10 - 19</TH>\n";
                stmt += "<TH class=\"tableheader\">20 - 29</TH>\n";
                stmt += "<TH class=\"tableheader\">30 - 39</TH>\n";
                stmt += "<TH class=\"tableheader\">40 - 49</TH>\n";
                stmt += "<TH class=\"tableheader\">50 - 59</TH>\n";
                stmt += "<TH class=\"tableheader\">60 - " + numbers.ToString() + "</TH>\n";
            }
            else if (numbers < 80)
            {
                stmt += "<TH class=\"tableheader\">1 - 9</TH>\n";
                stmt += "<TH class=\"tableheader\">10 - 19</TH>\n";
                stmt += "<TH class=\"tableheader\">20 - 29</TH>\n";
                stmt += "<TH class=\"tableheader\">30 - 39</TH>\n";
                stmt += "<TH class=\"tableheader\">40 - 49</TH>\n";
                stmt += "<TH class=\"tableheader\">50 - 59</TH>\n";
                stmt += "<TH class=\"tableheader\">60 - 69</TH>\n";
                stmt += "<TH class=\"tableheader\">70 - " + numbers.ToString() + "</TH>\n";
            }
            stmt += "</TR>\n";

            for (int i = arr.Length - 1; i >= 0; i--)
            {
                stmt += "<TR>\n"
                    + "<TD style=\"color:#ff00ff\">" + (targetRow--) + "</font></TD>\n"
                    + "<TD style=\"color:#ff00ff; width:120px;\">" + ddate[i] + "</font></TD>\n";
                for (int j = 0; j < arr[i].Length; j++)
                {
                    if (arr[i][j] != 0)
                    {
                        if (arr[i][j] < 3)
                        {
                            stmt += "<TD><font color=\"#ff3300\"><B>" + arr[i][j] + "</B></font></TD>\n";
                        }
                        else
                        {
                            stmt += "<TD bgcolor=#A52A2A><font color=white><B>" + arr[i][j] + "</B></font></TD>\n";
                        }
                    }
                    else
                    {
                        stmt += "<TD bgcolor=\"999966\"></TD>\n";
                    }
                }
                stmt += "</TR>\n";
            }

            stmt += "<TR>\n"
                + "<TH class=\"tableheader\">Draw No.</TH>\n"
                + "<TH class=\"tableheader\">Draw Date</TH>\n";

            if (numbers < 10)
            {
                stmt += "<TH class=\"tableheader\">1 - " + numbers.ToString() + "</TH>\n";
            }
            else if (numbers < 20)
            {
                stmt += "<TH class=\"tableheader\">1 - 9</TH>\n";
                stmt += "<TH class=\"tableheader\">10 - " + numbers.ToString() + "</TH>\n";
            }
            else if (numbers < 30)
            {
                stmt += "<TH class=\"tableheader\">1 - 9</TH>\n";
                stmt += "<TH class=\"tableheader\">10 - 19</TH>\n";
                stmt += "<TH class=\"tableheader\">20 - " + numbers.ToString() + "</TH>\n";
            }
            else if (numbers < 40)
            {
                stmt += "<TH class=\"tableheader\">1 - 9</TH>\n";
                stmt += "<TH class=\"tableheader\">10 - 19</TH>\n";
                stmt += "<TH class=\"tableheader\">20 - 29</TH>\n";
                stmt += "<TH class=\"tableheader\">30 - " + numbers.ToString() + "</TH>\n";
            }
            else if (numbers <= 50)
            {
                stmt += "<TH class=\"tableheader\">1 - 9</TH>\n";
                stmt += "<TH class=\"tableheader\">10 - 19</TH>\n";
                stmt += "<TH class=\"tableheader\">20 - 29</TH>\n";
                stmt += "<TH class=\"tableheader\">30 - 39</TH>\n";
                stmt += "<TH class=\"tableheader\">40 - " + numbers.ToString() + "</TH>\n";
            }
            else if (numbers < 60)
            {
                stmt += "<TH class=\"tableheader\">1 - 9</TH>\n";
                stmt += "<TH class=\"tableheader\">10 - 19</TH>\n";
                stmt += "<TH class=\"tableheader\">20 - 29</TH>\n";
                stmt += "<TH class=\"tableheader\">30 - 39</TH>\n";
                stmt += "<TH class=\"tableheader\">40 - 49</TH>\n";
                stmt += "<TH class=\"tableheader\">50 - " + numbers.ToString() + "</TH>\n";
            }
            else if (numbers < 70)
            {
                stmt += "<TH class=\"tableheader\">1 - 9</TH>\n";
                stmt += "<TH class=\"tableheader\">10 - 19</TH>\n";
                stmt += "<TH class=\"tableheader\">20 - 29</TH>\n";
                stmt += "<TH class=\"tableheader\">30 - 39</TH>\n";
                stmt += "<TH class=\"tableheader\">40 - 49</TH>\n";
                stmt += "<TH class=\"tableheader\">50 - 59</TH>\n";
                stmt += "<TH class=\"tableheader\">60 - " + numbers.ToString() + "</TH>\n";
            }
            else if (numbers < 80)
            {
                stmt += "<TH class=\"tableheader\">1 - 9</TH>\n";
                stmt += "<TH class=\"tableheader\">10 - 19</TH>\n";
                stmt += "<TH class=\"tableheader\">20 - 29</TH>\n";
                stmt += "<TH class=\"tableheader\">30 - 39</TH>\n";
                stmt += "<TH class=\"tableheader\">40 - 49</TH>\n";
                stmt += "<TH class=\"tableheader\">50 - 59</TH>\n";
                stmt += "<TH class=\"tableheader\">60 - 69</TH>\n";
                stmt += "<TH class=\"tableheader\">70 - " + numbers.ToString() + "</TH>\n";
            }
            stmt += "</TR></TABLE>\n"
                + "<A href=" + fromSite + ">Back</A>\n";
            stmt += Util.CreateHTML_Tail();
            return stmt;
        }

    }

}