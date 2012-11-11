using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Georgia
{
    public class RandomFactory
    {
        private static Random prng;

        private RandomFactory() { }

        public static Random Instance()
        {
            if (prng == null)
            {
                TimeSpan t = (DateTime.UtcNow - new DateTime(1970, 1, 1));
                int epochTime = (int) t.TotalSeconds;
                prng = new Random(epochTime);
            }
            return prng;
        }
        
    }
}
