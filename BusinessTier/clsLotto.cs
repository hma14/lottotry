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

            Database database = Util.MapDbTable(db);
            lastRow = dataAccessLayer.GetLastRow(database);
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

            stmt += Util.CreateHTML_Header("Lotto Statistics 2", db);
            stmt += "<A href=" + fromSite + ">Back</A>";

            stmt += "<TABLE class=\"generated_tables\" border=\"1\">";

            // Draw table header
            stmt += "<TR>";

            int i = -1;
            int a1 = -1;
            int a2 = 0;
            int end = distanceRange;
            if (Util.IsDbInPicks(db))
            {
                i = -2;
                a1 = -2;
                a2 = -1;
                --end;
            }

            for (; i <= end; i++)
            {
                if (i == a1)
                {
                    stmt += "<TH style=\"color:Maroon; background-color:yellow; width:65px;font-size:small\">"
                        + "Draw\\Dist" + "</TH>";
                    continue;
                }
                if (i == a2)
                {
                    stmt += "<TH style=\"color:Maroon; width:120px;background-color:yellow;font-size:small\">"
                        + "Draw Date" + "</TH>";
                    continue;
                }
                stmt += "<TH style=\"width:65px; color:Maroon;background-color:#ffcccc\">" + i + "</TH>";
            }
            stmt += "<TH style=\"color:Maroon;background-color:#ffcccc\">" + "Total" + "</TH>";
            stmt += "</TR>";

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
            i = 0;
            for (; i < cols; i++)
            {
                en[i] = numgen.numberArray[i].GetEnumerator();
            }

            int loop = 0;
            int maxLoop = target - start + 1;

            while (dno.MoveNext() && loop < maxLoop)
            {
                loop++;
                ddate.MoveNext();
                i = 0;
                for (; i < cols; i++)
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
                i = 0;
                for (; i < cols; i++)
                {
                    ss[i] = (SubStatistics)en[i].Current;

                    if (ss[i].SavedDist <= distanceRange)
                    {
                        a[ss[i].SavedDist]++;
                        total++;
                    }
                }
               

                stmt += "<TR>";
                i = -1;
                for (; i <= distanceRange; i++)
                {
                    if (i == -1)
                    {
                        stmt += "<TD style=\"color:#ff3300; text-align:center;width:45px;\">"
                            + dno.Current + "</TD>";
                        continue;
                    }
                    if (i == 0)
                    {
                        stmt += "<TD style=\"color:#6960EC; width:120px;\">"
                            + ddate.Current + "</TD>";
                        continue;
                    }

                    if (a[i] != 0)
                    {
                        stmt += "<TD style=\"width:65px;\">" + a[i] + "</TD>";
                    }
                    else
                    {
                        stmt += "<TD style=\"background-color:#ffcc99; width:65px;\"></TD>";
                    }
                }
                if (total <= 2)
                {
                    stmt += "<TD style=\"width:45px; text-align:center; font-style:italic; color:#4662FC; font-weight:bold; background-color:#FCF646;\">"
                        + total + "</TD>";
                }
                else if (total == 3)
                {
                    stmt += "<TD style=\"width:45px; text-align:center; font-style:italic; color:#FCF646; font-weight:bold; background-color:#4662FC; \">"
                        + total + "</TD>";
                }
                else
                {
                    stmt += "<TD style=\"width:45px; text-align:center; font-style:italic; color:#ff0033\">"
                        + total + "</TD>";
                }
                stmt += "</TR>";
            }

            stmt += "<TR>";
            i = -1;
            a1 = -1;
            a2 = 0;
            end = distanceRange;
            if (Util.IsDbInPicks(db))
            {
                i = -2;
                a1 = -2;
                a2 = -1;
                --end;
            }
            for (; i <= end; i++)
            {
                if (i == a1)
                {
                    stmt += "<TH style=\"color:Maroon; background-color:yellow; width:65px;font-size:small \">" + "Draw\\Dist" + "</TH>";
                    continue;
                }
                if (i == a2)
                {
                    stmt += "<TH style=\"color:Maroon; width:120px; background-color:yellow; font-size:small\">" + "Draw Date" + "</TH>";
                    continue;
                }
                stmt += "<TH style=\"width:45px; color:Maroon;background-color:#ffcccc\">" + i + "</TH>";
            }
            stmt += "<TH bgcolor=\"ffcccc\">" + "Total" + "</TH>";
            stmt += "</TR>";

            stmt += "</TABLE>"
                + "<A href=" + fromSite + ">Back</A>";
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
            string stmt = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01 Transitional//EN\">"
                + "<HTML>"
                + "<HEAD>"
                + "<TITLE>" + "Lotto Table Contents" + "</TITLE>"
                + "</HEAD>"
                + "<BODY BGCOLOR=\"#FFFFFF\">"
                + "<CENTER>"
                + "<H2>Warning: <font color=\"#ff0080\">" + errMsg + "</font></H2>"
                + "<A href=" + fromSite + ">Back</A>"
                + "</CENTER>"
                + "</BODY>"
                + "</HTML>";

            return stmt;

        }

        private string createHtml_Segment(int start, int scale, int[][] arr)
        {
            string stmt = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01 Transitional//EN\">"
                + "<HTML>"
                + "<HEAD>"
                + "<TITLE>" + "Retrieve Result" + "</TITLE>"
                + "</HEAD>"
                + "<BODY BGCOLOR=\"#FFFFFF\">"
                + "<CENTER>";
            stmt += Util.LottoLogos(db);

            stmt += "</CENTER>"
                + "<A href=" + fromSite + ">Back</A>"
                + "<HR>"
                + "<TABLE class=\"generated_tables\" border=\"1\">"
                + "<TR>"
                + "<TH>Draw No.</TH>";
            for (int j = 0; j < scale; j++)
            {
                stmt += "<TH>Scale " + (j + 1) + "</TH>";
            }
            stmt += "</TR>";


            for (int i = 0; i < arr.Length; i++)
            {
                stmt += "<TR>"
                    + "<TD><font color=\"#ff00ff\">" + (start + i) + "</font></TD>";
                for (int j = 0; j < arr[i].Length; j++)
                {
                    if (arr[i][j] != 0)
                    {
                        stmt += "<TD><font color=\"#ff3300\"><B>" + arr[i][j] + "</B></font></TD>";
                    }
                    else
                    {
                        stmt += "<TD bgcolor=\"999966\"></TD>";
                    }
                }
                stmt += "</TR>";
            }

            stmt += "<TR>"
                + "<TH>Draw No.</TH>";
            for (int j = 0; j < scale; j++)
            {
                stmt += "<TH>Scale " + (j + 1) + "</TH>";
            }
            stmt += "</TR>";

            stmt += "</TABLE>"
                + "<A href=" + fromSite + ">Back</A>"
                + "</BODY></HTML>";

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
            stmt += "<A href=" + fromSite + ">Back</A>"
                + "<HR>"
                + "<TABLE class=\"generated_tables\" border=\"1\">"
                + "<TR>"
                + "<TH>Draw No</TH>"
                + "<TH>DrawDate</TH>";


            for (int i = 0; i < cols_no_bonus; ++i)
            {
                stmt += "<TH>No." + (i+1).ToString() + "</TH>";
            }
            if (db == Database.OZLottoTue)
            {
                stmt += "<TH>Supp1</TH><TH>Supp2</TH>";
            }
            else if (db == Database.SevenLotto)
                stmt += "<TH>Special</TH>";
            //else if (db == Database.FloridaLucky)
            //    stmt += "<TH>Lucky Ball</TH>";
            else
            {
                if (cols > cols_no_bonus)
                {
                    stmt += "<TH>Bonus</TH>";
                }
            }
            stmt += "</TR>";
            
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
                stmt += "<TR>"
                    + "<TD><font color=\"#ff00ff\">" 
                    + dno.Current.ToString() 
                    + "</font></TD>"
                    + "<TD><font style=\"color:maroon;font-size:small;\">" 
                    + ddate.Current.ToString() 
                    + "</font></TD>";

                for (int i = 0; i < cols; ++i)
                {     
                    //stmt += "<TD align=\"center\"><font color=\"#ff0000\"><font weight=\"bolder\">" 
                    stmt += "<td><font style=font-weight:bold;color:#ff0000;text-align:center>"
                        + ss[i].Num 
                        + "</font> (<font style=\"FONT-STYLE: italic\" color=\"#0066cc\">" 
                        + ss[i].SavedDist 
                        + "</font>)</td> ";
                }
                stmt += "</TR>";
            }
            

            // For bottom of table
            stmt += "<TR>" + "<TH>Draw No</TH>" + "<TH>DrawDate</TH>";
            for (int i = 0; i < cols_no_bonus; ++i)
            {
                stmt += "<TH>No." + (i + 1).ToString() + "</TH>";
            }
            if (db == Database.OZLottoTue)
            {
                stmt += "<TH>Supp1</TH><TH>Supp2</TH>";
            }
            else if (db == Database.SevenLotto)
                stmt += "<TH>Special</TH>";
            //else if (db == Database.FloridaLucky)
            //    stmt += "<TH>Lucky Ball</TH>";
            else
            {
                if (cols > cols_no_bonus)
                {
                    stmt += "<TH>Bonus</TH>";
                }
            }

            stmt += "</TR>";
            stmt += "</TABLE>" + "<A href=" + fromSite + ">Back</A>";
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

            stmt += "<A href=" + fromSite + ">Back</A>"
                + "<HR>"
                + "Display <em>COLD</em> numbers (distance >= 10) and getting colder from LEFT to RIGHT in this table: <br />"
                + "<TABLE class=\"generated_tables\" border=\"1\">"
                + "<TR>";

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
                    stmt += "<TH>Number</TH>";
                    continue;
                }

                if (st[i].RelativeDist >= 10)
                {
                    stmt += "<TH style=\"color:Red;background-color:#E0FFFF;font-style:bold;\">" + st[i].Num + "</TH>";
                }
            }
            stmt += "</TR><TR>";
            for (int i = 0; i < st.Length; i++)
            {
                if (i == 0)
                {
                    stmt += "<TD><B>Distance</B></TD>";
                    continue;
                }
                if (st[i].RelativeDist >= 10)
                {
                    stmt += "<TD style=\"color:#fff;background-color:#800517;font-style:bold;\">" + st[i].RelativeDist + "</TD>";
                }
            }
            stmt += "</TR><TR>";
            for (int i = 0; i < st.Length; i++)
            {
                if (i == 0)
                {
                    stmt += "<TD><B>Frequency</B></TD>";
                    continue;
                }
                if (st[i].RelativeDist >= 10)
                {
                    stmt += "<TD style=\"color:#fff;background-color:#2B547E;font-style:bold;\">" + st[i].Cnt + "</TD>";
                }
            }

            stmt += "</TR>";

            stmt += "</TABLE>";

            return stmt;

        }

        private string createShortDistTable(int dist, int scale, int start, int target, Database db, ref long dbExecTime)
        {

            string stmt = "";

            stmt += "<br /><HR>"
                + "Display <em>HOTTEST</em> numbers (distance < 5), the number on row getting colder from left to the right in this table: <br>"
                + "<table class=\"generated_tables\" border=\"1\">"
                + "<TR>";

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
                    stmt += "<TH>Number</TH>";
                    continue;
                }

                if (st[i].RelativeDist <= 4 && st[i].Num != 0)
                {
                    stmt += "<TH style=\"color:Red;background-color:#E0FFFF;font-style:bold;\">" + st[i].Num + "</TH>";
                }
            }
            stmt += "</TR><TR>";
            for (int i = -1; i < st.Length; i++)
            {
                if (i == -1)
                {
                    stmt += "<TD><B>Distance</B></TD>";
                    continue;
                }
                if (st[i].RelativeDist <= 4 && st[i].Num != 0)
                {
                    stmt += "<TD style=\"color:#fff;background-color:#800517;font-style:bold;\">" + st[i].RelativeDist + "</TD>";
                }
            }
            stmt += "</TR><TR>";
            for (int i = -1; i < st.Length; i++)
            {
                if (i == -1)
                {
                    stmt += "<TD><B>Frequency</B></TD>";
                    continue;
                }
                if (st[i].RelativeDist <= 4 && st[i].Num != 0)
                {
                    stmt += "<TD style=\"color:#fff;background-color:#2B547E;font-style:bold;\">" + st[i].Cnt + "</TD>";
                }
            }

            stmt += "</TR>";

            stmt += "</TABLE>";

            return stmt;

        }


        private string createFreqBand(int fragments)
        {
            // Mark the 49 numbers as scales (default is 7) with different colors 
            // for each scale

            string stmt = "";
            stmt += "<TR>";
            int n = fragments + 1;

            for (int i = -1; i <= numgen.ScaleLength; i++)
            {

                if (i == -1)
                {
                    stmt += "<TH class=\"tableheader\" style=\"font-size:small\">Draw No.\\Scales</TH>";
                    continue;
                }
                if (i == 0)
                {
                    stmt += "<TH class=\"tableheader\" style=\"font-size:small\">Draw Date</TH>";
                    continue;
                }

                if (i <= fragments)
                {
                    stmt += "<TH bgcolor=\"ffff66\"><font style=\"font-style: italic\">" + 1 + "</font>" + "." + i + "</TH>";
                }
                else if (i <= 2 * fragments)
                {
                    stmt += "<TH bgcolor=\"ffcccc\"><font style=\"font-style: italic\">" + 2 + "</font>" + "." + (i - fragments) + "</TH>";
                }
                else if (i <= 3 * fragments)
                {
                    stmt += "<TH bgcolor=\"ccff33\"><font style=\"font-style: italic\">" + 3 + "</font>" + "." + (i - 2 * fragments) + "</TH>";
                }
                else if (i <= 4 * fragments)
                {
                    stmt += "<TH bgcolor=\"ff99ff\"><font style=\"font-style: italic\">" + 4 + "</font>" + "." + (i - 3 * fragments) + "</TH>";
                }
                else if (i <= 5 * fragments)
                {
                    stmt += "<TH bgcolor=\"33cc99\"><font style=\"font-style: italic\">" + 5 + "</font>" + "." + (i - 4 * fragments) + "</TH>";
                }
                else if (i <= 6 * fragments)
                {
                    stmt += "<TH bgcolor=\"ffcc00\"><font style=\"font-style: italic\">" + 6 + "</font>" + "." + (i - 5 * fragments) + "</TH>";
                }
                else if (i <= 7 * fragments)
                {
                    stmt += "<TH bgcolor=\"cccc33\"><font style=\"font-style: italic\">" + 7 + "</font>" + "." + (i - 6 * fragments) + "</TH>"; ;
                }
                else if (i <= 8 * fragments)
                {
                    stmt += "<TH bgcolor=\"ffff66\"><font style=\"font-style: italic\">" + 8 + "</font>" + "." + (i - 7 * fragments) + "</TH>";
                }
                else if (i <= 9 * fragments)
                {
                    stmt += "<TH bgcolor=\"ffcccc\"><font style=\"font-style: italic\">" + 9 + "</font>" + "." + (i - 8 * fragments) + "</TH>";
                }
                else if (i <= 10 * fragments)
                {
                    stmt += "<TH bgcolor=\"ccff33\"><font style=\"font-style: italic\">" + 10 + "</font>" + "." + (i - 9 * fragments) + "</TH>";
                }
                else if (i <= 11 * fragments)
                {
                    stmt += "<TH bgcolor=\"ff99ff\"><font style=\"font-style: italic\">" + 11 + "</font>" + "." + (i - 10 * fragments) + "</TH>";
                }
                else if (i <= 12 * fragments)
                {
                    stmt += "<TH bgcolor=\"33cc99\"><font style=\"font-style: italic\">" + 12 + "</font>" + "." + (i - 11 * fragments) + "</TH>";
                }
                else if (i <= 13 * fragments)
                {
                    stmt += "<TH bgcolor=\"ffcc00\"><font style=\"font-style: italic\">" + 13 + "</font>" + "." + (i - 12 * fragments) + "</TH>";
                }
                else if (i <= 14 * fragments)
                {
                    stmt += "<TH bgcolor=\"cccc33\"><font style=\"font-style: italic\">" + 14 + "</font>" + "." + (i - 13 * fragments) + "</TH>";
                }
                else if (i <= 15 * fragments)
                {
                    stmt += "<TH bgcolor=\"ffff66\"><font style=\"font-style: italic\">" + 15 + "</font>" + "." + (i - 14 * fragments) + "</TH>";
                }
                else if (i <= 16 * fragments)
                {
                    stmt += "<TH bgcolor=\"ffcccc\"><font style=\"font-style: italic\">" + 16 + "</font>" + "." + (i - 15 * fragments) + "</TH>";
                }
                else if (i <= 17 * fragments)
                {
                    stmt += "<TH bgcolor=\"ccff33\"><font style=\"font-style: italic\">" + 17 + "</font>" + "." + (i - 16 * fragments) + "</TH>";
                }
            }
            stmt += "</TR>";
            return stmt;
        }

        private string createFreqBand4Pikcs(int fragments)
        {
            // Mark the 49 numbers as scales (default is 7) with different colors 
            // for each scale

            string stmt = "";
            stmt += "<TR>";
            //int n = fragments + 1;

            for (int i = -2; i < numgen.ScaleLength; i++)
            {

                if (i == -2)
                {
                    stmt += "<TH class=\"tableheader\" style=\"font-size:small\">Draw No.\\Scales</TH>";
                    continue;
                }
                if (i == -1)
                {
                    stmt += "<TH class=\"tableheader\" style=\"font-size:small\">Draw Date</TH>";
                    continue;
                }
                stmt += "<TH bgcolor=\"ffff66\"><font style=\"font-style: italic\">" + i + "</font></TH>";
                /*

                if (i <= fragments)
                {
                    stmt += "<TH bgcolor=\"ffff66\"><font style=\"font-style: italic\">" + i + "</font></TH>";
                }
                else if (i <= 2 * fragments)
                {
                    stmt += "<TH bgcolor=\"ffcccc\"><font style=\"font-style: italic\">" + 1 + "</font></TH>";
                }
                else if (i <= 3 * fragments)
                {
                    stmt += "<TH bgcolor=\"ccff33\"><font style=\"font-style: italic\">" + 2 + "</font></TH>";
                }
                else if (i <= 4 * fragments)
                {
                    stmt += "<TH bgcolor=\"ff99ff\"><font style=\"font-style: italic\">" + 3 + "</font></TH>";
                }
                else if (i <= 5 * fragments)
                {
                    stmt += "<TH bgcolor=\"33cc99\"><font style=\"font-style: italic\">" + 4 + "</font></TH>";
                }
                else if (i <= 6 * fragments)
                {
                    stmt += "<TH bgcolor=\"ffcc00\"><font style=\"font-style: italic\">" + 5 + "</font></TH>";
                }
                else if (i <= 7 * fragments)
                {
                    stmt += "<TH bgcolor=\"cccc33\"><font style=\"font-style: italic\">" + 6 + "</font></TH>"; ;
                }
                else if (i <= 8 * fragments)
                {
                    stmt += "<TH bgcolor=\"ffff66\"><font style=\"font-style: italic\">" + 7 + "</font></TH>";
                }
                else if (i <= 9 * fragments)
                {
                    stmt += "<TH bgcolor=\"ffcccc\"><font style=\"font-style: italic\">" + 8 + "</font></TH>";
                }
                else if (i <= 9 * fragments)
                {
                    stmt += "<TH bgcolor=\"ffcccc\"><font style=\"font-style: italic\">" + 9 + "</font></TH>";
                }
                */
                
            }
            stmt += "</TR>";
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
            if (fragments == 0) fragments = 1;

            string stmt = " ";
            stmt += Util.CreateHTML_Header("Lotto Statistics 3", db);
            stmt += "<A href=" + fromSite + ">Back</A>";

            if (numgen.ScaleLength < 20)
                stmt += "<table class=\"smallTable\" border=\"1\">";
            else if (numgen.ScaleLength < 40)
                stmt += "<table class=\"mediumTable\" border=\"1\">";
            else
                stmt += "<table class=\"tblRetrieveAllNumber\" border=\"1\">";

            SubStatistics[] st = new SubStatistics[numgen.ScaleLength];
            st = numgen.Stat;

            if (Util.IsDbInPicks(db))
            {
                stmt += createFreqBand4Pikcs(fragments);
            }
            else
            {
                stmt += createFreqBand(fragments);
            }
                
                
            for (int j = start; j <= target; j++)
            {
                numgen = new NumGen(db, 0, j, scale);
                dbExecTime += numgen.DatabaseExecutionTime;
                st = numgen.Stat;

                int i = 1;
                if (Util.IsDbInPicks(db))
                {
                    --i;
                }

                // Sort st with Frequency
                bubbleSort(st);

                stmt += "<TR>";

                stmt += "<TH style=\"color:#ff33ff; font-size:small\">" + j + "</font></TH>";

                
                // Only for output draw date for this table row
                for (; i < st.Length; i++)
                {
                    if (st[i].RelativeDist == 0)
                    {
                        stmt += "<TH style=\"color:#ff33ff; width:120px; font-size:small\">" + st[i].DDate + "</TH>";
                        break;
                    }
                }
                i = 1;
                if (Util.IsDbInPicks(db))
                {
                    --i;
                }
                for (; i < st.Length; i++)
                {
                    if (st[i].RelativeDist == 0)
                    {
                        stmt += "<TD bgcolor=\"#ffff00\"><font style=\"font-size: 14pt;FONT-STYLE: italic; TEXT-ALIGN: justify\" color=\"#ff0099\"><B>"
                                + st[i].Num
                                + "</B></font><br />"
                                + "<font color=\"#cc00cc\" size=\"2\">("
                                + st[i].SavedDist
                                + ")<br />"
                                + "<font color=\"#00bfff\" size=\"2\">("
                                + st[i].Cnt                              
                                + ")</font></TD>";
                    }
                    else
                    {
                        stmt += "<TD><font color=\"#3366ff\">"
                                + st[i].Num
                                + "</font><br />("
                                + "<font style=\"FONT-STYLE: italic\" color=\"#339900\" size=\"2\">"
                                + st[i].getDist(j)
                                + ")<br />"
                                + "<font color=\"#00bfff\" size=\"2\">("
                                + st[i].Cnt
                                + ")</font></TD>";
                    }
                }
                stmt += "</TR>";
            }
            if (Util.IsDbInPicks(db))
            {
                stmt += createFreqBand4Pikcs(fragments);
            }
            else
            {
                stmt += createFreqBand(fragments);
            }

            stmt += "</TABLE>";

            stmt += "<HR>" + "<A href=" + fromSite + ">Back</A>";

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
            stmt += "<A href=" + fromSite + ">Back</A>";
            if (numgen.ScaleLength < 20)
                stmt += "<table class=\"smallTable\" border=\"1\">";
            else if (numgen.ScaleLength < 40)
                stmt += "<table class=\"mediumTable\" border=\"1\">";
            else
                stmt += "<table class=\"tblRetrieveAllNumber\" border=\"1\">";
            SubStatistics[] st = new SubStatistics[numgen.ScaleLength];
            st = numgen.Stat;

            int i = -1;
            int end = st.Length;
            int a1 = -1;
            int a2 = 0;
            if (Util.IsDbInPicks(db))
            {
                i = -2;
                a1 = -2;
                a2 = -1;
            }
            // All numbers as scale
            for (; i < end; i++)
            {
                if (i == a1)
                {
                    stmt += "<TH class=\"tableheader\">Draw No.</TH>";
                    continue;
                }
                else if (i == a2)
                {
                    stmt += "<TH class=\"tableheader\">Draw Date</TH>";
                    continue;
                }
                else
                {
                    stmt += "<TH class=\"tableheader\"><font color=\"#ffffff\">" + i + "</font></TH>";
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

                stmt += "<TR>";

                stmt += "<TH><font color=\"#ff33ff\">" + j + "</font></TH>";
                i = 1;
                if (Util.IsDbInPicks(db))
                {
                    i = 0;
                }                 
                for (; i < st.Length; i++)
                {
                    if (st[i].RelativeDist == 0)
                    {
                        stmt += "<TH style=\"color:#ff33ff; font-size:small;\">" + st[i].DDate + "</TH>";
                        break;
                    }
                }
                i = 1;
                if (Util.IsDbInPicks(db))
                {
                    i = 0;
                }
                for (; i < st.Length; i++)
                {
                    if (st[i].RelativeDist == 0)
                    {
                        if (st[i].SavedDist >= 20)
                        {
                            stmt += "<TD bgcolor=\"#ff00ff\"><font style=\"font-size: 14pt;FONT-STYLE: italic; TEXT-ALIGN: justify\" color=\"#ffbf00\"><B>"
                                    + st[i].Num
                                    + "</B></font><br />"
                                    + "<font color=\"#ffff00\" size=\"2\"> (<B>"
                                    + st[i].SavedDist
                                    + ")<br />"
                                    + "<font color=\"#00bfff\" size=\"2\">("
                                    + st[i].Cnt
                                    + "</B>)</font></TD>";
                        }
                        else {
                            stmt += "<TD bgcolor=\"#ffff00\"><font style=\"font-size: 14pt;FONT-STYLE: italic; TEXT-ALIGN: justify\" color=\"#ff0099\"><B>"
                                    + st[i].Num
                                    + "</B></font><br />"
                                    + "<font color=\"#cc00cc\" size=\"2\">("
                                    + st[i].SavedDist
                                    + ")<br />"
                                    + "<font color=\"#00bfff\" size=\"2\">("
                                    + st[i].Cnt
                                    + ")</font></TD>";
                        }
                    }
                    else
                    {

                        if (st[i].RelativeDist % 7 == 0)
                        {
                            stmt += "<TD bgcolor=\"#ccff33\"><font style=\"font-size: 12pt\" color=\"#003333\"><B>" + st[i].Num;
                            stmt += "</B></font><br />("
                                    + "<font style=\"FONT-STYLE: italic\" color=\"#6600cc\" size=\"2\">"
                                    + st[i].getDist(j)
                                    + ")<br />"
                                    + "<font color=\"#ff0000\" size=\"2\">("
                                    + st[i].Cnt
                                    + ")</font></TD>";
                        }
                        else if (st[i].RelativeDist % 7 == 1)
                        {
                            stmt += "<TD bgcolor=\"#99cdff\"><font style=\"font-size: 12pt\" color=\"#003333\"><B>" + st[i].Num;
                            stmt += "</B></font><br />("
                                    + "<font style=\"FONT-STYLE: italic\" color=\"#6600cc\" size=\"2\">"
                                    + st[i].getDist(j)
                                    + ")<br />"
                                    + "<font color=\"#ff0000\" size=\"2\">("
                                    + st[i].Cnt
                                    + ")</font></TD>";
                        }
                        else if (st[i].RelativeDist % 7 == 2)
                        {
                            stmt += "<TD bgcolor=\"#cc99cc\"><font style=\"font-size: 12pt\" color=\"#003333\"><B>" + st[i].Num;
                            stmt += "</B></font><br />("
                                    + "<font style=\"FONT-STYLE: italic\" color=\"#6600cc\" size=\"2\">"
                                    + st[i].getDist(j)
                                    + ")<br />"
                                    + "<font color=\"#ff0000\" size=\"2\">("
                                    + st[i].Cnt
                                    + ")</font></TD>";
                        }
                        else if (st[i].RelativeDist % 7 == 3)
                        {
                            stmt += "<TD bgcolor=\"#ffcdff\"><font style=\"font-size: 12pt\" color=\"#003333\"><B>" + st[i].Num;
                            stmt += "</B></font><br />("
                                    + "<font style=\"FONT-STYLE: italic\" color=\"#6600cc\" size=\"2\">"
                                    + st[i].getDist(j)
                                    + ")<br />"
                                    + "<font color=\"#ff0000\" size=\"2\">("
                                    + st[i].Cnt
                                    + ")</font></TD>";
                        }
                        else if (st[i].RelativeDist % 7 == 4)
                        {
                            stmt += "<TD bgcolor=\"#ff99cc\"><font style=\"font-size: 12pt\" color=\"#003333\"><B>" + st[i].Num;
                            stmt += "</B></font><br />("
                                    + "<font style=\"FONT-STYLE: italic\" color=\"#6600cc\" size=\"2\">"
                                    + st[i].getDist(j)
                                    + ")<br />"
                                    + "<font color=\"#ff0000\" size=\"2\">("
                                    + st[i].Cnt
                                    + ")</font></TD>";
                        }
                        else if (st[i].RelativeDist % 7 == 5)
                        {
                            stmt += "<TD bgcolor=\"#cc9900\"><font style=\"font-size: 12pt\" color=\"#003333\"><B>" + st[i].Num;
                            stmt += "</B></font><br />("
                                    + "<font style=\"FONT-STYLE: italic\" color=\"#6600cc\" size=\"2\">"
                                    + st[i].getDist(j)
                                    + ")<br />"
                                    + "<font color=\"#ff0000\" size=\"2\">("
                                    + st[i].Cnt
                                    + ")</font></TD>";
                        }
                        else
                        {
                            stmt += "<TD bgcolor=\"#ffcccc\"><font style=\"font-size: 12pt\" color=\"#003333\"><B>" + st[i].Num;
                            stmt += "</B></font><br />("
                                    + "<font style=\"FONT-STYLE: italic\" color=\"#6600cc\" size=\"2\">"
                                    + st[i].getDist(j)
                                    + ")<br />"
                                    + "<font color=\"#ff0000\" size=\"2\">("
                                    + st[i].Cnt
                                    + ")</font></TD>";
                        }

                    }
                }
                stmt += "</TR>";
            }


            // All numbers as scale
            i = -1;
            end = st.Length;
            a1 = -1;
            a2 = 0;
            if (Util.IsDbInPicks(db))
            {
                i = -2;
                a1 = -2;
                a2 = -1;
            }
            for (; i < end; i++)
            {
                if (i == a1)
                {
                    stmt += "<TH class=\"tableheader\">Draw No.</TH>";
                    continue;
                }
                else if (i == a2)
                {
                    stmt += "<TH class=\"tableheader\">Draw Date</TH>";
                    continue;
                }
                else
                {
                    stmt += "<TH class=\"tableheader\"><font color=\"#ffffff\">" + i + "</font></TH>";
                }
            }

            stmt += "</TABLE>";

            stmt += "<HR>" + "<A href=" + fromSite + ">Back</A>";
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
            if (fragments == 0) fragments = 1;

            string stmt = " ";

            //stmt += "<A href=" + fromSite + ">Back</A>"
            stmt += "<br /><HR>";
            stmt += "Display whole numbers, fragmented into <em>scales</em>, for the <em>current draw</em> with <em>Frequcy</em> and <em>Distance</em> mapping to each numbers:"
                + "<br />"
                + "<table class=\"generated_tables\" border=\"1\">";


            SubStatistics[] st;
            st = numgen.Stat;

            // Sort st with Frequency
            bubbleSort(st);


            stmt += "<TR>";



            // Mark the 49 numbers as scales (default is 7) with different colors 
            // for each scale
            for (int i = 0; i < st.Length; i++)
            {

                if (i == 0)
                {
                    stmt += "<TH>Scales</TH>";
                    continue;
                }

                if (i <= fragments)
                {
                    stmt += "<TH bgcolor=\"ffff66\">" + 1 + "</TH>";
                }
                else if (i <= 2 * fragments)
                {
                    stmt += "<TH bgcolor=\"ffcccc\">" + 2 + "</TH>";
                }
                else if (i <= 3 * fragments)
                {
                    stmt += "<TH bgcolor=\"ccff33\">" + 3 + "</TH>";
                }
                else if (i <= 4 * fragments)
                {
                    stmt += "<TH bgcolor=\"ff99ff\">" + 4 + "</TH>";
                }
                else if (i <= 5 * fragments)
                {
                    stmt += "<TH bgcolor=\"33cc99\">" + 5 + "</TH>";
                }
                else if (i <= 6 * fragments)
                {
                    stmt += "<TH bgcolor=\"ffcc00\">" + 6 + "</TH>";
                }
                else if (i <= 7 * fragments)
                {
                    stmt += "<TH bgcolor=\"cccc33\">" + 7 + "</TH>"; ;
                }
                else if (i <= 8 * fragments)
                {
                    stmt += "<TH bgcolor=\"ffff66\">" + 8 + "</TH>";
                }
                else if (i <= 9 * fragments)
                {
                    stmt += "<TH bgcolor=\"ffcccc\">" + 9 + "</TH>";
                }
                else if (i <= 10 * fragments)
                {
                    stmt += "<TH bgcolor=\"ccff33\">" + 10 + "</TH>";
                }
                else if (i <= 11 * fragments)
                {
                    stmt += "<TH bgcolor=\"ff99ff\">" + 11 + "</TH>";
                }
                else if (i <= 12 * fragments)
                {
                    stmt += "<TH bgcolor=\"33cc99\">" + 12 + "</TH>";
                }
                else if (i <= 13 * fragments)
                {
                    stmt += "<TH bgcolor=\"ffcc00\">" + 13 + "</TH>";
                }
                else if (i <= 14 * fragments)
                {
                    stmt += "<TH bgcolor=\"cccc33\">" + 14 + "</TH>";
                }
                else if (i <= 15 * fragments)
                {
                    stmt += "<TH bgcolor=\"ffff66\">" + 15 + "</TH>";
                }
                else if (i <= 16 * fragments)
                {
                    stmt += "<TH bgcolor=\"ffcccc\">" + 16 + "</TH>";
                }
                else if (i <= 17 * fragments)
                {
                    stmt += "<TH bgcolor=\"ccff33\">" + 17 + "</TH>";
                }
            }
            stmt += "</TR><TR>";

            for (int i = 0; i < st.Length; i++)
            {
                if (i == 0)
                {
                    stmt += "<TH>Number</TH>";
                    continue;
                }
                if (st[i].RelativeDist == 0)
                {
                    stmt += "<TH style=\"background-color:#E0FFFF;\"><font style=\"FONT-STYLE: italic\" color=\"#ff0033\"><B>" 
                        + st[i].Num + "</B></font></TH>";
                }
                else
                {
                    stmt += "<TH style=\"background-color:#E0FFFF;\">" 
                        + st[i].Num + "</TH>";
                }
            }
            stmt += "</TR><TR>";
            for (int i = 0; i < st.Length; i++)
            {
                if (i == 0)
                {
                    stmt += "<TD><B>Frequency</B></TD>";
                    continue;
                }

                stmt += "<TD style=\"background-color:#C3FDB8;\"><font color=\"#ff0000\">" 
                    + st[i].Cnt + "</font></TD>";

            }
            stmt += "</TR><TR>";
            for (int i = 0; i < st.Length; i++)
            {
                if (i == 0)
                {
                    stmt += "<TD><B>Distance</B></TD>";
                    continue;
                }
                if (st[i].RelativeDist == 0)
                {
                    stmt += "<TD align=\"center\" style=\"background-color:#FFF8C6;\"><font color=\"#827839\">" 
                        + st[i].RelativeDist
                        + "</font><font style=\"FONT-STYLE: italic\" color=\"#254117\">"
                        + "(" + st[i].SavedDist + ")"
                        + "</font></TD>";
                }
                else
                {
                    stmt += "<TD align=\"center\" style=\"background-color:#FFF8C6;\"><font color=\"#ff0000\">" 
                        + st[i].RelativeDist + "</font></TD>";
                }
            }

            stmt += "</TR>";

            stmt += "</TABLE>";
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

            stmt += "<TR>";
            int i = 1;
            int end = st.Length;
            if (Util.IsDbInPicks(db))
            {
                i = 0;
            }
            for (; i < end; i++)
            {
                if (st[i].RelativeDist == 0)
                {
                    stmt += "<TH style=\"color:#ff00ff\">" 
                        + st[i].DrawNumber.ToString() + "</TH>";
                    stmt += "<TH style=\"color:#ff00ff;width=120px;font-size:small;\">" 
                        + st[i].DDate + "</TH>";
                    break;
                }
            }
            i = 1;
            end = st.Length;
            if (Util.IsDbInPicks(db))
            {
                i = 0;
            }
            for (; i < end; i++)
            {
                if (st[i].RelativeDist == 0)
                {
                    stmt += "<TD id=tdRelativeDist0 class=drawNumber>" + st[i].Num + "<br />";
                    stmt += "(" + "<font id=relativeDist0>" + st[i].SavedDist + "</font>)"
                        + "<br />"
                        + "<font color=\"#00bfff\" size=\"2\">("
                        + st[i].Cnt
                        + ")</TD>";
                }
                else
                {
                    stmt += "<TD id=numbers>" + st[i].Num + "<br />";
                    stmt += "(" + "<font id=tdRelativeDist>" + st[i].RelativeDist + "</font>)" 
                        +"<br />"
                        + "<font color=\"#00bfff\" size=\"2\">("
                        + st[i].Cnt
                        + ")</TD>";

                }
            }
            stmt += "</TR>";

            return stmt;

        }

        public string retrieveAllNumDist(int start, int target, out long dbExecTime)
        {
            numgen = new NumGen(db, target);
            string stmt = " ";
            stmt += Util.CreateHTML_Header("Statistics 6", db);
            stmt += "<A href=" + fromSite + ">Back</A>";
            if (numgen.ScaleLength < 20)
                stmt += "<table class=\"smallTable\" border=\"1\">";
            else if(numgen.ScaleLength < 40)
                stmt += "<table class=\"mediumTable\" border=\"1\">";
            else
                stmt += "<table class=\"tblRetrieveAllNumber\" border=\"1\">";
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


            stmt += "</TABLE>"
                + "<A href=" + fromSite + ">Back</A>";

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

            stmt += "<br /><HR>"
                + "Display <em>Distance</em> vs <em>Scale</em> matrix on this table:<br>"
                + "<em>Scales</em>, shown as columns of the table, fregmented based on <em>frequencies</em> of numbers. <br>"
                + "Each <em>scale</em> contains multiple numbers which's <em>distances</em> shown as rows of the table. <br>"
                + "<table class=\"generated_tables\" border=\"1\">"
                + "<TR>"
                + "<TH>Dist\\Scale</TH>";


            for (int i = 1; i <= numgen.Scale; i++)
            {

                switch (i)
                {
                    case 1:
                        stmt += "<TH width=\"80\" bgcolor=\"ffff66\">" + i + "</TH>";
                        break;
                    case 2:
                        stmt += "<TH width=\"80\" bgcolor=\"ffcccc\">" + i + "</TH>";
                        break;
                    case 3:
                        stmt += "<TH width=\"80\" bgcolor=\"ccff33\">" + i + "</TH>";
                        break;
                    case 4:
                        stmt += "<TH width=\"80\" bgcolor=\"ff99ff\">" + i + "</TH>";
                        break;
                    case 5:
                        stmt += "<TH width=\"80\" bgcolor=\"33cc99\">" + i + "</TH>";
                        break;
                    case 6:
                        stmt += "<TH width=\"80\" bgcolor=\"ffcc00\">" + i + "</TH>";
                        break;
                    case 7:
                        stmt += "<TH width=\"80\" bgcolor=\"cccc33\">" + i + "</TH>";
                        break;
                    case 8:
                        stmt += "<TH width=\"80\" bgcolor=\"ffff66\">" + i + "</TH>";
                        break;
                    case 9:
                        stmt += "<TH width=\"80\" bgcolor=\"ffcccc\">" + i + "</TH>";
                        break;
                    case 10:
                        stmt += "<TH width=\"80\" bgcolor=\"ccff33\">" + i + "</TH>";
                        break;
                    case 11:
                        stmt += "<TH width=\"80\" bgcolor=\"ff99ff\">" + i + "</TH>";
                        break;
                    case 12:
                        stmt += "<TH width=\"80\" bgcolor=\"33cc99\">" + i + "</TH>";
                        break;
                    case 13:
                        stmt += "<TH width=\"80\" bgcolor=\"ffcc00\">" + i + "</TH>";
                        break;
                    case 14:
                        stmt += "<TH width=\"80\" bgcolor=\"cccc33\">" + i + "</TH>";
                        break;
                    case 15:
                        stmt += "<TH width=\"80\" bgcolor=\"ffff66\">" + i + "</TH>";
                        break;
                    case 16:
                        stmt += "<TH width=\"80\" bgcolor=\"ffcccc\">" + i + "</TH>";
                        break;
                    case 17:
                        stmt += "<TH width=\"80\" bgcolor=\"ccff33\">" + i + "</TH>";
                        break;
                    case 18:
                        stmt += "<TH width=\"80\" bgcolor=\"ff99ff\">" + i + "</TH>";
                        break;
                    case 19:
                        stmt += "<TH width=\"80\" bgcolor=\"33cc99\">" + i + "</TH>";
                        break;
                    case 20:
                        stmt += "<TH width=\"80\" bgcolor=\"ffcc00\">" + i + "</TH>";
                        break;
                    case 21:
                        stmt += "<TH width=\"80\" bgcolor=\"cccc33\">" + i + "</TH>";
                        break;
                    case 22:
                        stmt += "<TH width=\"80\" bgcolor=\"ffff66\">" + i + "</TH>";
                        break;
                    case 23:
                        stmt += "<TH width=\"80\" bgcolor=\"ffcccc\">" + i + "</TH>";
                        break;
                    case 24:
                        stmt += "<TH width=\"80\" bgcolor=\"ccff33\">" + i + "</TH>";
                        break;
                    case 25:
                        stmt += "<TH width=\"80\" bgcolor=\"ff99ff\">" + i + "</TH>";
                        break;
                }
            }
            stmt += "</TR>";

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
                            stmt += "<TD bgcolor=\"ffcc99\">";
                        }
                    }
                }
                stmt += "</TR>";
            }


            stmt += "</TABLE>"
                + "<A href=" + fromSite + ">Back</A>";
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
                    if (num[i] >= 0 && num[i] < 10)
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
                    else if (num[i] > 50 && num[i] <= 60)
                    {
                        arr[ii][5]++;
                    }
                    else if (num[i] > 60 && num[i] < 70)
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
            stmt += "<A href=" + fromSite + ">Back</A>"
                + "<TABLE class=\"generated_tables\" border=\"1\" >"
                + "<TR>"
                + "<TH class=\"tableheader\">Draw No.</TH>"
                + "<TH class=\"tableheader\">Draw Date</TH>";
             
            if (Util.IsDbInPicks(db))
            {
                stmt += "<TH class=\"tableheader\">0 - " + (numbers-1).ToString() + "</TH>";
            }
            else if (numbers < 10)
            {
                stmt += "<TH class=\"tableheader\">1 - " + numbers.ToString() + "</TH>";
            }
            else if (numbers < 20)
            {
                stmt += "<TH class=\"tableheader\">1 - 9</TH>";
                stmt += "<TH class=\"tableheader\">10 - " + numbers.ToString() + "</TH>";
            }
            else if (numbers < 30)
            {
                stmt += "<TH class=\"tableheader\">1 - 9</TH>";
                stmt += "<TH class=\"tableheader\">10 - 19</TH>";
                stmt += "<TH class=\"tableheader\">20 - " + numbers.ToString() + "</TH>"; 
            }           
            else if (numbers < 40)
            {
                stmt += "<TH class=\"tableheader\">1 - 9</TH>";
                stmt += "<TH class=\"tableheader\">10 - 19</TH>";
                stmt += "<TH class=\"tableheader\">20 - 29</TH>";
                stmt += "<TH class=\"tableheader\">30 - " + numbers.ToString() + "</TH>"; 
            }
            else if (numbers <= 50)
            {
                stmt += "<TH class=\"tableheader\">1 - 9</TH>";
                stmt += "<TH class=\"tableheader\">10 - 19</TH>";
                stmt += "<TH class=\"tableheader\">20 - 29</TH>";
                stmt += "<TH class=\"tableheader\">30 - 39</TH>";
                stmt += "<TH class=\"tableheader\">40 - " + numbers.ToString() + "</TH>"; 
            }
            else if (numbers <= 60)
            {
                stmt += "<TH class=\"tableheader\">1 - 9</TH>";
                stmt += "<TH class=\"tableheader\">10 - 19</TH>";
                stmt += "<TH class=\"tableheader\">20 - 29</TH>";
                stmt += "<TH class=\"tableheader\">30 - 39</TH>";
                stmt += "<TH class=\"tableheader\">40 - 49</TH>";
                stmt += "<TH class=\"tableheader\">50 - " + numbers.ToString() + "</TH>"; 
            }
            else if (numbers < 70)
            {
                stmt += "<TH class=\"tableheader\">1 - 9</TH>";
                stmt += "<TH class=\"tableheader\">10 - 19</TH>";
                stmt += "<TH class=\"tableheader\">20 - 29</TH>";
                stmt += "<TH class=\"tableheader\">30 - 39</TH>";
                stmt += "<TH class=\"tableheader\">40 - 49</TH>";
                stmt += "<TH class=\"tableheader\">50 - 59</TH>";
                stmt += "<TH class=\"tableheader\">60 - " + numbers.ToString() + "</TH>";
            }
            else if (numbers < 80)
            {
                stmt += "<TH class=\"tableheader\">1 - 9</TH>";
                stmt += "<TH class=\"tableheader\">10 - 19</TH>";
                stmt += "<TH class=\"tableheader\">20 - 29</TH>";
                stmt += "<TH class=\"tableheader\">30 - 39</TH>";
                stmt += "<TH class=\"tableheader\">40 - 49</TH>";
                stmt += "<TH class=\"tableheader\">50 - 59</TH>";
                stmt += "<TH class=\"tableheader\">60 - 69</TH>";
                stmt += "<TH class=\"tableheader\">70 - " + numbers.ToString() + "</TH>";
            }
            stmt += "</TR>";

            int end = arr[0].Length;
            if (Util.IsDbInPicks(db))
            {
                end = 1;
            }

            for (int i = arr.Length - 1; i >= 0; i--)
            {
                stmt += "<TR>"
                    + "<TD style=\"color:#ff00ff\">" + (targetRow--) + "</font></TD>"
                    + "<TD style=\"color:#ff00ff; width:120px;\">" + ddate[i] + "</font></TD>";
                for (int j = 0; j < end; j++)
                {
                    if (arr[i][j] != 0)
                    {
                        if (arr[i][j] < 3)
                        {
                            stmt += "<TD><font color=\"#ff3300\"><B>" + arr[i][j] + "</B></font></TD>";
                        }
                        else
                        {
                            stmt += "<TD bgcolor=#A52A2A><font color=white><B>" + arr[i][j] + "</B></font></TD>";
                        }
                    }
                    else
                    {
                        stmt += "<TD bgcolor=\"999966\"></TD>";
                    }
                }
                stmt += "</TR>";
            }
            

            stmt += "<TR>"
                + "<TH class=\"tableheader\">Draw No.</TH>"
                + "<TH class=\"tableheader\">Draw Date</TH>";

            if (Util.IsDbInPicks(db))
            {
                stmt += "<TH class=\"tableheader\">0 - " + (numbers - 1).ToString() + "</TH>";
            }
            else if (numbers < 10)
            {
                stmt += "<TH class=\"tableheader\">1 - " + numbers.ToString() + "</TH>";
            }
            else if (numbers < 20)
            {
                stmt += "<TH class=\"tableheader\">1 - 9</TH>";
                stmt += "<TH class=\"tableheader\">10 - " + numbers.ToString() + "</TH>";
            }
            else if (numbers < 30)
            {
                stmt += "<TH class=\"tableheader\">1 - 9</TH>";
                stmt += "<TH class=\"tableheader\">10 - 19</TH>";
                stmt += "<TH class=\"tableheader\">20 - " + numbers.ToString() + "</TH>";
            }
            else if (numbers < 40)
            {
                stmt += "<TH class=\"tableheader\">1 - 9</TH>";
                stmt += "<TH class=\"tableheader\">10 - 19</TH>";
                stmt += "<TH class=\"tableheader\">20 - 29</TH>";
                stmt += "<TH class=\"tableheader\">30 - " + numbers.ToString() + "</TH>";
            }
            else if (numbers <= 50)
            {
                stmt += "<TH class=\"tableheader\">1 - 9</TH>";
                stmt += "<TH class=\"tableheader\">10 - 19</TH>";
                stmt += "<TH class=\"tableheader\">20 - 29</TH>";
                stmt += "<TH class=\"tableheader\">30 - 39</TH>";
                stmt += "<TH class=\"tableheader\">40 - " + numbers.ToString() + "</TH>";
            }
            else if (numbers <= 60)
            {
                stmt += "<TH class=\"tableheader\">1 - 9</TH>";
                stmt += "<TH class=\"tableheader\">10 - 19</TH>";
                stmt += "<TH class=\"tableheader\">20 - 29</TH>";
                stmt += "<TH class=\"tableheader\">30 - 39</TH>";
                stmt += "<TH class=\"tableheader\">40 - 49</TH>";
                stmt += "<TH class=\"tableheader\">50 - " + numbers.ToString() + "</TH>";
            }
            else if (numbers < 70)
            {
                stmt += "<TH class=\"tableheader\">1 - 9</TH>";
                stmt += "<TH class=\"tableheader\">10 - 19</TH>";
                stmt += "<TH class=\"tableheader\">20 - 29</TH>";
                stmt += "<TH class=\"tableheader\">30 - 39</TH>";
                stmt += "<TH class=\"tableheader\">40 - 49</TH>";
                stmt += "<TH class=\"tableheader\">50 - 59</TH>";
                stmt += "<TH class=\"tableheader\">60 - " + numbers.ToString() + "</TH>";
            }
            else if (numbers < 80)
            {
                stmt += "<TH class=\"tableheader\">1 - 9</TH>";
                stmt += "<TH class=\"tableheader\">10 - 19</TH>";
                stmt += "<TH class=\"tableheader\">20 - 29</TH>";
                stmt += "<TH class=\"tableheader\">30 - 39</TH>";
                stmt += "<TH class=\"tableheader\">40 - 49</TH>";
                stmt += "<TH class=\"tableheader\">50 - 59</TH>";
                stmt += "<TH class=\"tableheader\">60 - 69</TH>";
                stmt += "<TH class=\"tableheader\">70 - " + numbers.ToString() + "</TH>";
            }
            
            stmt += "</TR></TABLE>"
                + "<A href=" + fromSite + ">Back</A>";
            stmt += Util.CreateHTML_Tail();
            return stmt;
        }

    }

}