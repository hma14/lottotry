using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

using DataAccessTier;

namespace BusinessTier
{


    /// <summary>
    /// Summary description for NumGen.
    /// </summary>
    public class NumGen
    {
        private int scale;
        private SubStatistics[] stat;
        private int currentDrawno;
        private string currentDrawdate;
        private int fregments;
        private ArrayList drawno;
        private ArrayList drawdate;
        private ArrayList[][] sdm;
        private long databaseExecutionTime;
        private Database database;
        public ArrayList[] numberArray;


        public NumGen(Database db, int startRow, int targetRow, int dist, int numScales)
        {
            database = db;
            databaseExecutionTime = createStat(db, startRow, targetRow);

            fregments = ScaleLength / numScales;
            if (ScaleLength % fregments > 0)
            {
                scale = ScaleLength / fregments + 1;
            }
            else
            {
                scale = ScaleLength / fregments;
            }

            // Init sdm (scale / distance matrix) ArrayList type of 2-dimention array 
            sdm = new ArrayList[dist][];

            for (int i = 0; i < sdm.Length; i++)
            {
                sdm[i] = new ArrayList[scale + 1];
                for (int j = 0; j < sdm[i].Length; j++)
                {
                    sdm[i][j] = new ArrayList();
                }
            }
            genScaleDistMatrix(dist);
        }

        public int ScaleLength
        {
            get
            {
                return Util.getTotalLottoNumbers(database);
            }

        }

        public SubStatistics[] Stat
        {
            get
            {
                return stat;
            }
        }

        public SubStatistics this[int index]
        {
            get
            {
                return stat[index];
            }
        }

        public NumGen(Database db, int startRow, int targetRow)
        {
            database = db;
            drawno = new ArrayList();
            drawdate = new ArrayList();
            numberArray = new ArrayList[Util.MAX_NUMBERS];
            int cols = Util.getColumnnsOfLotto(db);
            for (int i = 0; i < cols; i++)
            {
                numberArray[i] = new ArrayList();
            }
            databaseExecutionTime = createStat(db, startRow, targetRow);
            databaseExecutionTime += createNums(db, startRow, targetRow);
        }


        public NumGen(Database db, int targetRow)
        {
            database = db;
            int startRow = 1; // Always starts from first draw
            drawno = new ArrayList();
            drawdate = new ArrayList();
            numberArray = new ArrayList[Util.MAX_NUMBERS];
            int cols = Util.getColumnnsOfLotto(db);
            for (int i = 0; i < cols; i++)
            {
                numberArray[i] = new ArrayList();
            }

            databaseExecutionTime = createStat(db, startRow, targetRow);
            //databaseExecutionTime += createNums(db, startRow, targetRow);
        }

        public NumGen(Database db, int startRow, int targetRow, int numScales)
        {
            database = db;
            //fregments = Convert.ToInt32(Convert.ToDouble((double)ScaleLength / (double)numScales) + 0.5);
            fregments = ScaleLength / numScales;
            if (ScaleLength % fregments > 0)
            {
                scale = ScaleLength / fregments + 1;
            }
            else
            {
                scale = ScaleLength / fregments;
            }

            drawno = new ArrayList();
            drawdate = new ArrayList();
            numberArray = new ArrayList[Util.MAX_NUMBERS];
            int cols = Util.getColumnnsOfLotto(db);
            for (int i = 0; i < cols; i++)
            {
                numberArray[i] = new ArrayList();
            }

            databaseExecutionTime = createStat(db, startRow, targetRow);
            //databaseExecutionTime += createNums(db, startRow, targetRow);
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

        public int Scale
        {
            get
            {
                return scale;
            }
        }

        public int Fregments
        {
            get
            {
                return fregments;
            }
        }

        public bool amongNumbers(int i, int[] a)
        {
            for (int j = 0; j < a.Length; j++)
            {
                if (i == a[j])
                {
                    return true;
                }
            }
            return false;
        }


        public long createStat(Database db, int startRow, int targetRow)
        {
            stat = new SubStatistics[ScaleLength + 1];
            for (int i = 0; i < stat.Length; i++)
            {
                stat[i] = new SubStatistics(i, 0, 0, " ", 0);
            }
            DataAccessLayer dataAccessLayer = new DataAccessLayer();
            SqlDataReader reader =
                    dataAccessLayer.SelectAllOnRangeOfDrawNo(db, startRow, targetRow);
            int cols = Util.getColumnnsOfLotto(db);
            while (reader.Read() == true)
            {
                // Increase the internal distCnt for all numbers (1 - 49)
                increment();
                int draw_no = reader.GetInt32(0);
                string draw_date = reader.GetString(1);                     
                int[] no = new int[cols];
                for (int i = 0; i < cols; i++)
                {
                    no[i] = (int)reader.GetSqlInt32(i+2);

                    stat[no[i]].increment();
                    stat[no[i]].DDate = draw_date;
                    stat[no[i]].PrevDrawNumber = stat[no[i]].DrawNumber;
                    stat[no[i]].DrawNumber = draw_no;
                    stat[no[i]].resetDist();
                    
                }
                currentDrawno = draw_no;
                currentDrawdate = draw_date;
            }
            return dataAccessLayer.CloseConnection(reader);
        }



        private long createNums(Database db, int startRow, int targetRow)
        {
            DataAccessLayer dataAccessLayer = new DataAccessLayer();
            SqlDataReader reader = dataAccessLayer.SelectAllOnRangeOfDrawNo(db, startRow, targetRow);
            int draw_no;
            string draw_date;
            int[] no = new int[Util.MAX_NUMBERS];
            SubStatistics[] ss = new SubStatistics[Util.MAX_NUMBERS];

            while (reader.Read())
            {
                // Increase distance for all numbers, ex. 1 - 49
                increment();
                draw_no = (int)reader.GetSqlInt32(0);
                draw_date = reader.GetString(1);
                drawno.Add(draw_no);
                drawdate.Add(draw_date);

                int cols = Util.getColumnnsOfLotto(db);
                for (int i = 0; i < cols; i++)
                {
                    no[i] = (int)reader.GetSqlInt32(i + 2);
                    stat[no[i]].resetDist();
                    ss[i] = new SubStatistics(stat[no[i]]);
                    numberArray[i].Add(ss[i]);
                }
            }
            return dataAccessLayer.CloseConnection(reader);
        }

        private void increment()
        {
            for (int i = 1; i < stat.Length; i++)
            {
                // stat[0] contains nothing
                stat[i].incrementDist();
            }
        }

        private int getDistFromNum(int n)
        {
            for (int i = 1; i < stat.Length; i++)
            {
                if (stat[i].Num == n)
                {
                    return stat[i].RelativeDist;
                }
            }
            return -1;
        }


        private void genScaleDistMatrix(int dist)
        {

            Util.bubbleSort(stat); // sort statistics on Frequency
            int s = 0;

            for (int i = 1; i < stat.Length; i++)
            {
                if (i <= fregments)
                {
                    s = 1;
                }
                else if (i <= 2 * fregments)
                {
                    s = 2;
                }
                else if (i <= 3 * fregments)
                {
                    s = 3;
                }
                else if (i <= 4 * fregments)
                {
                    s = 4;
                }
                else if (i <= 5 * fregments)
                {
                    s = 5;
                }
                else if (i <= 6 * fregments)
                {
                    s = 6;
                }
                else if (i <= 7 * fregments)
                {
                    s = 7;
                }
                else if (i <= 8 * fregments)
                {
                    s = 8;
                }
                else if (i <= 9 * fregments)
                {
                    s = 9;
                }
                else if (i <= 10 * fregments)
                {
                    s = 10;
                }
                else if (i <= 11 * fregments)
                {
                    s = 11;
                }
                else if (i <= 12 * fregments)
                {
                    s = 12;
                }
                else if (i <= 13 * fregments)
                {
                    s = 13;
                }
                else if (i <= 14 * fregments)
                {
                    s = 14;
                }
                else if (i <= 15 * fregments)
                {
                    s = 15;
                }
                else if (i <= 16 * fregments)
                {
                    s = 16;
                }
                else if (i <= 17 * fregments)
                {
                    s = 17;
                }
                else
                {
                    s = 18;
                }

                int d = stat[i].RelativeDist;

                if (d < dist)
                {
                    sdm[d][s].Add(stat[i].Num);
                }
            }
        }

        public ArrayList getScaleDistMatrix(int d, int s)
        {
            return sdm[d][s];
        }

        public ArrayList DrawNo
        {
            get
            {
                return drawno;
            }
        }
        public ArrayList DrawDate
        {
            get
            {
                return drawdate;
            }
        }
    }
}
