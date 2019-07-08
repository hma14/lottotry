using System;

namespace BusinessTier
{
	/// <summary>
	/// Summary description for ScaleDistMatrix.
	/// </summary>
	public class ScaleDistMatrix
	{
		private const int MaxItems = 10;
		private int scale;
		private int dist;
		private int index;
		private int [] valueArr;



		public ScaleDistMatrix()
		{
			scale = 0;
			dist = 0;
			index = 0;
			if (valueArr.Length == 0)
			{
				valueArr = new int[MaxItems];
			}

		}

		public ScaleDistMatrix(int s, int d)
		{
			scale = s;
			dist = d;
			index = 0;

			if (valueArr.Length == 0)
			{
				valueArr = new int[MaxItems];
			}
		}

		public void setScaleDistMatrix(int d, int s, int v)
		{
			scale = s;
			dist = d;
			index++;
			valueArr[index] = v;
		}


		public int [] getValueAt()
		{
			return valueArr;
		}


	}
}
