using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Georgia
{
    /// <summary>
    /// Crossover mechanism
    /// </summary>
    /// <remarks>Implements the Recombine() function to perform genetic recombination of a pair of Chromosome objects.</remarks>
    public interface ICrossoverStrategy
    {
        IList<IChromosome> Recombine(IList<IChromosome> targets);
        int ChildrenProduced { get; set; }
    }
}
