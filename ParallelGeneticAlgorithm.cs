using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.ComponentModel;

namespace Georgia
{
    public abstract class ParallelGeneticAlgorithm : GeneticAlgorithm
    {
        public ParallelGeneticAlgorithm() : base() { }

        public ParallelGeneticAlgorithm(int pSize) : base(pSize) { }

        public ParallelGeneticAlgorithm(int pSize, double cRate)
            : base(pSize, cRate) { }

        public ParallelGeneticAlgorithm(int pSize, double cRate, SelectionFacade sf, ReproductionFacade rf) :
            base(pSize, cRate, sf, rf) { }

        public new void DoGeneration()
        {
            Trace.WriteLine("BEGIN: GeneticAlgorithm.DoGeneration():");
            Trace.Indent();

            List<IChromosome> newPopulation = new List<IChromosome>(PopulationSize);
            int matingPoolSize = (int)Math.Floor(PopulationSize * CrossoverRate);

            Trace.WriteLine(String.Format("matingPoolSize = {0}", matingPoolSize));

            UpdateFitnessAll();

            ManualResetEvent signal = new ManualResetEvent(false);
            int threadCount = 0;
            for (int i = 0; i < matingPoolSize; i++)
            {
                int j = i;
                Interlocked.Increment(ref threadCount);
                ThreadPool.QueueUserWorkItem(delegate
                {
                    try
                    {
                        // Select parents from the gene pool
                        IList<IChromosome> parents = Selection.Select(Population, 2);

                        Trace.WriteLine("Selected two parents for crossover.");
                        Trace.WriteLine(String.Format("parents[0] = {0}", parents[0]));
                        Trace.WriteLine(String.Format("parents[1] = {0}", parents[1]));

                        // Reproduce and add children to the new gene pool
                        IList<IChromosome> children = Reproduction.Reproduce(parents);
                        lock (newPopulation)
                        {
                            Trace.WriteLine("Children:");
                            foreach (IChromosome child in children)
                            {
                                newPopulation.Add(child);
                                Trace.WriteLine(String.Format("{0}", child));
                            }
                        }

                        Trace.WriteLine("Reproduction complete.");
                    }
                    finally
                    {
                        if (Interlocked.Decrement(ref threadCount) == 0)
                        {
                            signal.Set();
                        }
                    }
                });
            }

            signal.WaitOne();

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
        }
    }
}
