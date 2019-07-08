using System;

namespace BusinessTier
{
	/// <summary>
	/// Summary description for SubStatistics.
	/// </summary>
	public class SubStatistics : Statistics
	{
		private int relativeDist;
		private int savedDist;

		// Default constructor
		public SubStatistics() : this(0, 0, 0, "", 0) {}
		

		public SubStatistics(int n, int c, int dn, string dd, int dist) 
            : base(n, c, dn, dd)
		{		
			RelativeDist = SavedDist = dist;
		}

		// Copy constructor
		public SubStatistics(SubStatistics ss)
		{
			num = ss.num;
			cnt = ss.cnt;
			drawNumber = ss.drawNumber;
			prevDrawNumber = ss.prevDrawNumber;
			drawdate = ss.drawdate;
			
			SavedDist = ss.savedDist;
			RelativeDist = ss.relativeDist;

		}

		public int RelativeDist
		{
			get
			{
				return relativeDist;
			}
            set
            {
                relativeDist = value;
            }
		}

		public int SavedDist
		{
			get
			{
				return savedDist;
			}
            set
            {
                savedDist = value;
            }
		}

		public void incrementDist()
		{
			relativeDist++;		
		}

		public void resetDist()
		{
            // Case relativeDist == 0 only happens on MegaMillions rearly, when draw number equals to
            // MegaBall number on same draw, this causes SavedDist of that number untracable,
            // To avoid this scenerio, made changes as below, it won't affect other lottos.
            //
            if (relativeDist != 0) 
            {
                SavedDist = relativeDist;               
            }
            RelativeDist = 0;
        }
	}
}
