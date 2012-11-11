namespace Georgia
{
    /// <summary>
    /// Repair facility for the GA.
    /// </summary>
    /// <remarks>Repair ensures that Chromosomes remain valid solutions to the problem after recombination and mutation.  Implements the Repair() method that operates on a single Chromosome to repair it.  Care should be taken to avoid bias during repair.</remarks>
    public interface IRepairStrategy
    {
        IChromosome Repair(IChromosome c);
    }
}
