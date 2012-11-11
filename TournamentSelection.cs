using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Georgia
{
    public class TournamentSelection : ISelectionStrategy
    {
        public int TournamentSize { get; set; }

        public TournamentSelection(int ts)
        {
            TournamentSize = ts;
        }

        #region ISelectionStrategy Members

        public IChromosome Select(IList<IChromosome> population)
        {
            if (TournamentSize > population.Count)
            {
                TournamentSize = population.Count;
            }
            List<IChromosome> tournament = new List<IChromosome>(TournamentSize);
            Random prng = RandomFactory.Instance();

            int index;
            for (int i = 0; i < TournamentSize; i++)
            {
                index = prng.Next(population.Count);
                tournament.Add(population[index]);
            }

            return tournament.Max();
        }

        #endregion
    }
}
