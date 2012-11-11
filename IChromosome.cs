using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.ComponentModel;

namespace Georgia
{
    public interface IChromosome : IEnumerable, IComparable, INotifyPropertyChanged
    {
        double? Fitness { get; set; }
        IChromosome GetCopy();
        int Count { get; }
        object this[int index] { get; set; }
    }
}
