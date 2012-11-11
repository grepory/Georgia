using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Georgia
{
    /// <summary>
    /// Reproductive facility of the genetic algorithm
    /// </summary>
    /// <remarks>Handles crossover, mutation and possibly repair mechanisms for the genetic algorithm.</remarks>
    public class ReproductionFacade
    {

        private ICrossoverStrategy _crossoverMechanism;
        private IRepairStrategy _repairMechanism;
        private IMutationStrategy _mutationMechanism;

        public ReproductionFacade() { }

        public ICrossoverStrategy CrossoverMechanism
        {
            set
            {
                _crossoverMechanism = value;
            }
            get
            {
                return _crossoverMechanism;
            }
        }

        public IRepairStrategy RepairMechanism
        {
            set
            {
                _repairMechanism = value;
            }
            get
            {
                return _repairMechanism;
            }
        }

        public IMutationStrategy MutationMechanism
        {
            set
            {
                _mutationMechanism = value;
            }
            get { return _mutationMechanism; }
        }
            

        public ReproductionFacade(ICrossoverStrategy cs, IRepairStrategy rs, IMutationStrategy ms) {
            this._crossoverMechanism = cs;
            this._repairMechanism = rs;
            this._mutationMechanism = ms;
        }

        public IList<IChromosome> Reproduce(IList<IChromosome> targets)
        {
            IList<IChromosome> newChromosomes = CrossoverMechanism.Recombine(targets);
            for (int i = 0; i < newChromosomes.Count; i++)
            {
                if (DoMutate())
                {
                    newChromosomes[i] = MutationMechanism.Mutate(newChromosomes[i]);
                }
                newChromosomes[i] = RepairMechanism.Repair(newChromosomes[i]);
            }
                
            return newChromosomes;
        }

        private bool DoMutate()
        {
            Random prng = RandomFactory.Instance();
            return (prng.NextDouble() < MutationMechanism.MutationRate) ? true : false;
        }
    }
}
