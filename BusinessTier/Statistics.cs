using System;

namespace BusinessTier
{
	/// <summary>
	/// Summary description for Statistics.
	/// </summary>
	public class Statistics
	{
		protected int num;
		protected int cnt;
		protected int drawNumber;
		protected int prevDrawNumber;
		protected string drawdate = " ";
    
		/** Creates a new instance of Statistics */
		public Statistics(int n, int c, int dn, string dd) 
		{
			num = n;
			cnt = c;
			drawNumber = dn;
			prevDrawNumber = 0;
			drawdate = dd;
		}
		// Default Constructor
		public Statistics() 
		{
			num = 0;
			cnt = 0;
			drawNumber = 0;
			prevDrawNumber = 0;
			drawdate = " ";
		}
		// Copy Constructor
		public Statistics(Statistics st) 
		{
			num = st.num;
			cnt = st.cnt;
			drawNumber = st.drawNumber;
			prevDrawNumber = st.prevDrawNumber;
			drawdate = st.drawdate;
		}
        public int Num
        {
            get 
            {
                return num;
            }
            set
            {
                num = value;
            }
        }
 
		public void increment() 
		{
			++cnt;
		}

        public int DrawNumber
        {
            get
            {
                return drawNumber;
            }
            set
            {
                drawNumber = value;
            }
        }

        public int PrevDrawNumber
        {
            get
            {
                return prevDrawNumber;
            }
            set
            {
                prevDrawNumber = value;
            }
        }

        public string DDate
        {
            get
            {
                return drawdate;
            }
            set
            {
                drawdate = value;
            }
        }
        public int Cnt
        {
            get
            {
                return cnt;
            }
            set
            {
                cnt = value;
            }
        }
		public int getDist(int currDrawno) 
		{
			return currDrawno - drawNumber;
		} 

		public int getPrevDist(int currDrawno) 
		{
			return currDrawno - prevDrawNumber - 1;
		} 
	}
}
