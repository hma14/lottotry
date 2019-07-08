using System;

namespace BusinessTier
{
	/// <summary>
	/// Summary description for NumberCounter.
	/// </summary>
	public class NumberCounter
	{
		private int cnt = 0;
		private int num;

		public NumberCounter()
		{}
		public NumberCounter(int c, int n) {cnt = c; num = n;}
        public int Cnt
        {
            get
            {
                return cnt;
            }
        }
        public int Num
        {
            get
            {
                return num;
            }
        }

		public int getCnt() { return cnt;}
		public int getNum() { return num;}
		public void increment() { cnt++;}
	}
}
