using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Georgia
{
    /// <summary>
    /// Mechanism for determining the fitness of a chromosome
    /// </summary>
    /// <remarks>Implements the Evaluate() function to determine the fitness of a single Chromosome.</remarks>
    public interface IMultiThreadedFitnessStrategy : IFitnessStrategy
    {
        /// <summary>
        /// Evaluate the fitness of an individual chromosome.
        /// </summary>
        /// <returns>The integer representing the fitness of the individual.</returns>
        void Evaluate(int index);
        void EvaluatePool(ICollection<IChromosome> p);
    }
}
