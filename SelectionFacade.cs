using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Georgia
{

    /// <summary>
    /// Selection subsystem
    /// </summary>
    /// <remarks>A SelectionFacade manages the selection mechanism for the genetic algorithm.  Selection involves determining which Chromosomes are chosen for recombination and then calculating the fitness for chromosomes.</remarks>
    public class SelectionFacade
    {
        private ISelectionStrategy _selectionMechanism;
        private IFitnessStrategy _fitnessMechanism;

        public SelectionFacade() { }

        public SelectionFacade(ISelectionStrategy ss, IFitnessStrategy fs)
        {
            this._selectionMechanism = ss;
            this._fitnessMechanism = fs;
        }

        public void UpdateFitnessAll(IEnumerable<IChromosome> p)
        {
            List<IChromosome> needsEvaluated = new List<IChromosome>();
            foreach (IChromosome c in p)
            {
                if (!c.Fitness.HasValue)
                {
                    needsEvaluated.Add(c);
                }
            }
            if (needsEvaluated.Count > 0)
            {
                this.FitnessMechanism.EvaluatePool(needsEvaluated);
            }
        }

        public IList<IChromosome> Select(IList<IChromosome> population, int count)
        {

            UpdateFitnessAll(population);
            
            IChromosome[] r = new IChromosome[count];
            for (int i=0; i<count; i++) {
                r[i] = this.SelectionMechanism.Select(population);
            }
            return r;
        }

        public ISelectionStrategy SelectionMechanism
        {
            set
            {
                _selectionMechanism = value;
            }
            get { return _selectionMechanism; }
        }

        public IFitnessStrategy FitnessMechanism
        {
            set
            {
                _fitnessMechanism = value;
            }
            get { return _fitnessMechanism; }
        }

    }
}
