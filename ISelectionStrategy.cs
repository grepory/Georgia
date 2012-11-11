using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Georgia
{
    /// <summary>
    /// Selection mechanism for the GA.
    /// </summary>
    /// <remarks>Implements the Select() method to select individual(s) for recombination.</remarks>
    public interface ISelectionStrategy
    {
        /// <summary>
        /// Use a method of selecting the population for reproduction.
        /// </summary>
        /// <returns>The population used for reproduction.</returns>
        IChromosome Select(IList<IChromosome> population);
    }
}
