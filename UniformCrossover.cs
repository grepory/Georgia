using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Georgia
{
    public class UniformCrossover : ICrossoverStrategy
    {

        public double ParentFavortism { get; set; }
        public int ChildrenProduced { get; set; }

        public UniformCrossover()
        {
        }

        public UniformCrossover(double favortism)
        {
            ParentFavortism = favortism;
        }

        public UniformCrossover(double favortism, int children)
            : this(favortism)
        {
            ChildrenProduced = children;
        }

        #region ICrossoverStrategy Members

        public IList<IChromosome> Recombine(IList<IChromosome> parents)
        {
            IList<IChromosome> children = new IChromosome[ChildrenProduced];
            Random prng = RandomFactory.Instance();
            int parent, count = parents.First().Count;

            for (int i=0; i<ChildrenProduced; i++)
            {
                children[i] = parents[i].GetCopy();

                for (int j = 0; j < count; j++)
                {
                    // TODO: Generalize to more than 2 parents... but what does ParentFavortism mean then?
                    parent = (prng.NextDouble() <= ParentFavortism) ? 0 : 1;
                    children[i][j] = parents[parent][j];
                }
            }

            return children;
        }

        #endregion

    }
}
