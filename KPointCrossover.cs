using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Georgia
{
    class KPointCrossover : ICrossoverStrategy
    {
        public int Points { get; set; }
        public int ChildrenProduced { get; set; }

        public KPointCrossover(int numPoints, int childrenProduced)
        {
            ChildrenProduced = childrenProduced;
            Points = numPoints;
        }

        public KPointCrossover(int numPoints)
        {
            Points = numPoints;
            ChildrenProduced = 2;
        }

        public KPointCrossover()
        {
            ChildrenProduced = 2;
            Points = 2;
        }

        #region ICrossoverStrategy Members

        public IList<IChromosome> Recombine(IList<IChromosome> targets)
        {
            IList<IChromosome> children = new List<IChromosome>();
            


            return children;
        }

        #endregion
    }
}
