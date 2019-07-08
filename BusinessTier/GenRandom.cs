using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessTier
{
    public class GenRandom
    {
        private static int seedCounter = new Random(DateTime.Now.Millisecond).Next();

        [ThreadStatic]
        private static Random rng;

        public static Random Instance
        {
            get
            {
                if (rng == null)
                {
                    int seed = Interlocked.Increment(ref seedCounter);
                    //int seed = DateTime.Now.Millisecond;
                    rng = new Random(seed);
                }
                return rng;
            }
        }

    }
}