using System;
using System.Collections.Generic;

namespace Georgia
{
    /// <summary>
    /// Mechanism for determining the fitness of a chromosome
    /// </summary>
    /// <remarks>Implements the Evaluate() function to determine the fitness of a single Chromosome.</remarks>
    public interface IFitnessStrategy
    {
        /// <summary>
        /// Evaluate the fitness of an individual chromosome.
        /// </summary>
        /// <returns>The double representing the fitness of the individual.</returns>
        double Evaluate(IChromosome target);
        void EvaluatePool(IList<IChromosome> pool);
    }
}
