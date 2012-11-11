using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Georgia
{
    public abstract class RandomKPointMutation : IMutationStrategy
    {
        private int _numberOfMutations;
        public double MutationRate { get; set; }

        public RandomKPointMutation(double mutationRate, int numMutations)
        {
            _numberOfMutations = numMutations;
            MutationRate = mutationRate;
        }

        #region IMutationStrategy Members

        public IChromosome Mutate(IChromosome c)
        {
            Random prng = RandomFactory.Instance();
            IChromosome ret = c as IChromosome;

            int index;
            for (int i = 0; i < _numberOfMutations; i++)
            {
                index = prng.Next(0, ret.Count);
                ret = Perturb(ret, index);
            }
            return ret;
        }

        public abstract IChromosome Perturb(IChromosome c, int position);

        #endregion
    }
}
