using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Georgia
{
    public interface IMutationStrategy
    {
        double MutationRate { get; set; }
        IChromosome Mutate(IChromosome c);
    }
}
