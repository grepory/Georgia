using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Georgia
{
    public class GenerationEvent : EventArgs
    {
        public int GenerationNumber { get; set; }
        public double AverageFitness { get; set; }
        public double MaxFitness { get; set; }
        public double MinFitness { get; set; }
        public IList<IChromosome> Population { get; set; }

        public GenerationEvent()
            : base()
        {
        }

        public GenerationEvent(int num, IList<IChromosome> pop, double avgFitness, double minFitness, double maxFitness)
        {
            GenerationNumber = num;
            AverageFitness = avgFitness;
            MinFitness = minFitness;
            MaxFitness = maxFitness;
            Population = pop;
        }

        public override string ToString()
        {
            string str = String.Format("Generation #: {0} --- Average Fitness: {1}\r\n", GenerationNumber, AverageFitness);
            foreach (IChromosome c in Population)
            {
                str += c + "\r\n";
            }
            return str;
        }
    }
}
