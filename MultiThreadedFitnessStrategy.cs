using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace Georgia
{
    public abstract class MultiThreadedFitnessStrategy : IFitnessStrategy
    {
        protected List<IChromosome> Pool { get; set; }
        protected ManualResetEvent[] ResetEvents { get; set; }
        protected List<IChromosome> seen { get; set; }

        public MultiThreadedFitnessStrategy(int maxThreads)
        {
            ThreadPool.SetMaxThreads(maxThreads, maxThreads);
        }

        public void EvaluatePool(IList<IChromosome> pop)
        {
            if (seen == null)
            {
                seen = new List<IChromosome>(pop.Count);
                InitSeenList(pop.Count);
            }
            Trace.WriteLine("BEGIN: MultiThreadedFitnessStrategy.EvaluatePool()");
            Trace.Indent();
            Pool = pop.ToList<IChromosome>();
            DateTime start = DateTime.Now;
            ManualResetEvent signal = new ManualResetEvent(false);
            int threadCount = 0;
            for (int i = 0; i < Pool.Count; i++)
            {
                // We have to allocate a new int for each thread because of the
                // way .NET lambdas work
                int index = i;
                Trace.WriteLine(String.Format("Spawning thread {0}", i));
                Interlocked.Increment(ref threadCount);
                ThreadPool.QueueUserWorkItem(delegate
                {
                    try
                    {
                        Evaluate(index);
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
            Trace.WriteLine(String.Format("Evaluated all individuals in: {0}", DateTime.Now - start));
            Trace.Unindent();
            Trace.WriteLine("END: MultiThreadedFitnessStrategy.EvaluatePool()");
        }

        private void InitSeenList(int c)
        {
            for (int i = 0; i < c; i++)
            {
                seen.Add(null);
            }
        }

        public void Evaluate(int index)
        {
            int r = seen.FindIndex(delegate(IChromosome t)
            {
                try
                {
                    return t.CompareTo(Pool[index]) == 0;
                } catch (Exception e)
                {
                    return false;
                }
            });
            if (r != -1)
            {
                Trace.WriteLine(index + string.Format("Found match: {0} == {1}", Pool[index], seen[r]));
                Pool[index].Fitness = seen[r].Fitness;
            }
            else
            {
                Trace.WriteLine(index + "No match found for chromosome " + Pool[index] + " r = " + r);
                Evaluate(Pool[index]);
            }
            seen[index] = Pool[index];
        }

        #region IFitnessStrategy Members

        public abstract double Evaluate(IChromosome target);

        #endregion
    }
}
