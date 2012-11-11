using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Georgia
{
    public class FitnessEvent : EventArgs
    {
        public IChromosome Chromosome { get; set; }
        public double? Fitness { get; set; }

        public FitnessEvent(IChromosome c, double? f)
        {
            Chromosome = c;
            Fitness = f;
        }

        public override string ToString()
        {
            return String.Format("{0} :: {1}", this.Chromosome, this.Fitness);
        }
    }
}
