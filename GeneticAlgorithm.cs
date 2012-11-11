using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.ComponentModel;
using System.Data;
using System.Xml.Serialization;
using System.IO;

namespace Georgia
{
    [Serializable]
    public abstract class GeneticAlgorithm
    {
        [XmlElement]
        public int CurrentGeneration { get; set; }
        [XmlElement]
        public ReproductionFacade Reproduction { get; set; }
        [XmlElement]
        public SelectionFacade Selection { get; set; }
        [XmlElement]
        protected List<IChromosome> Population { get; set; }
        [XmlElement]
        protected int PopulationSize { get; set;  }
        [XmlElement]
        public double CrossoverRate { get; set; }

        public GeneticAlgorithm()
        {
            Population = new List<IChromosome>();
            CurrentGeneration = 1;
        }

        public GeneticAlgorithm(int pSize)
        {
            CurrentGeneration = 1;
            Population = new List<IChromosome>(pSize);
            PopulationSize = pSize;

        }

        public GeneticAlgorithm(int pSize, double cRate)
            : this(pSize)
        {
            CrossoverRate = cRate;
        }

        public GeneticAlgorithm(int pSize, double cRate, SelectionFacade sf, ReproductionFacade rf)
            : this(pSize, cRate)
        {
            Selection = sf;
            Reproduction = rf;
        }

        public abstract void InitPopulation();

        /// <summary>
        /// Process a single generation of the population
        /// </summary>
        public void DoGeneration()
        {
            Trace.WriteLine("BEGIN: GeneticAlgorithm.DoGeneration():");
            Trace.Indent();

            List<IChromosome> newPopulation = new List<IChromosome>();
            int matingPoolSize = (int)Math.Floor(PopulationSize * CrossoverRate);

            Trace.WriteLine(String.Format("matingPoolSize = {0}", matingPoolSize));

            for (int i = 0; i < matingPoolSize; i++)
            {
                // Select and remove the parents from the gene pool
                IList<IChromosome> parents = Selection.Select(Population, 2);
                Trace.WriteLine("Selected two parents for crossover.");
                Trace.WriteLine(String.Format("parents[0] = {0}", parents[0]));
                Trace.WriteLine(String.Format("parents[1] = {0}", parents[1]));

                // Reproduce and add children to gene pool
                IList<IChromosome> children = Reproduction.Reproduce(parents);
                Trace.WriteLine("Children:");
                foreach (IChromosome child in children)
                {
                    Trace.WriteLine(String.Format("{0}", child));
                }
                Trace.WriteLine("Reproduction complete.");


                newPopulation.Add(children[0]);
            }

            if (newPopulation.Count < PopulationSize)
            {
                Trace.WriteLine(String.Format("Re-using {0} individuals from previous generation.",
                    PopulationSize - newPopulation.Count));

                Population.Sort();
                // Why sort in ascending order?!  :(
                int carry = 1;
                while (newPopulation.Count < PopulationSize)
                {
                    newPopulation.Add(Population[Population.Count - carry]);
                    carry++;
                }
            }
            else if (newPopulation.Count > PopulationSize)
            {
                Console.WriteLine("Great, Scott!  How did we get here?");
                Console.WriteLine("In GeneticAlgorithm.DoGeneration(): newPopulation.Count > populationSize");
                Environment.Exit(-1);
            }

            Population = newPopulation;
            UpdateFitnessAll();
            Trace.Unindent();
            Trace.WriteLine("END: GeneticAlgorithm.DoGeneration()");
            CurrentGeneration++;
        }

        public void UpdateFitnessAll()
        {
            Selection.UpdateFitnessAll(Population);
        }

        public double GetAverageFitness()
        {
            return Population.Average(c => { if (c.Fitness.HasValue) { return c.Fitness.Value; } else { return 0; } });
        }

        public IChromosome GetMostFit()
        {
            return Population.Max();
        }

        public IChromosome GetLeastFit()
        {
            return Population.Min();
        }

        public List<IChromosome> GetPopulation()
        {
            return this.Population;
        }
    }
}
