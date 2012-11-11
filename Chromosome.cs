using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Georgia
{

    /// <summary>
    /// GA's "Individual"
    /// </summary>
    /// <remarks>Chromosomes are complete individuals within the scope of a genetic algorithm.  They represent a possible solution to the problem.</remarks>
    public class Chromosome<T>: IChromosome, IEnumerable, IComparable, INotifyPropertyChanged where T: IComparable
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected T[] entries;
        private double? _fitness;

        public Chromosome() { }

        public Chromosome(int capacity)
        {
            entries = new T[capacity];
        }

        public Chromosome(T[] entries)
        {
            if (entries != null)
            {
                this.entries = entries;
            }
            else
            {
                throw new ArgumentNullException("Array argument may not be null");
            }
        }

        public object this[int index]
        {
            set
            {
                if (value.GetType() == typeof(T))
                {
                    entries[index] = (T) value;
                }
                else
                {
                    throw new ArgumentException("Incorrect class for assignment.");
                }
            }
            get
            {
                return entries[index];
            }
        }

        public int Count
        {
            get
            {
                return entries.Count();
            }
        }

        public double? Fitness
        {
            set
            {
                _fitness = value;
                NotifyPropertyChanged("Fitness");
            }
            get
            {
                return _fitness;
            }
        }

        public void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public int CompareTo(object obj)
        {
            if (obj is Chromosome<T> && obj != null)
            {
                Chromosome<T> other = (Chromosome<T>)obj;

                if (this.Count == other.Count)
                {
                    bool similar = true;
                    for (int i = 0; similar && i < Count; i++)
                    {
                        similar = (this.entries[i].CompareTo(other.entries[i]) == 0) ? true : false;
                    }
                    if (similar)
                    {
                        other.Fitness = this.Fitness;
                        return 0;
                    }
                    else
                    {
                        if (this.Fitness.HasValue && other.Fitness.HasValue)
                        {
                            return ((int)this.Fitness) - ((int)other.Fitness);
                        }
                        else
                        {
                            throw new ArgumentException("Both Chromosomes must have a Fitness value.");
                        }
                    }
                }
                else
                {
                    throw new ArgumentException("Chromosomes are of unequal length.");
                }
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        public virtual IChromosome GetCopy()
        {
            Chromosome<T> newChromosome = new Chromosome<T>(this.Count);
            newChromosome.entries = new T[this.Count];
            for (int i = 0; i < this.Count; i++)
            {
                newChromosome.entries[i] = this.entries[i];
            }
            return newChromosome;
        }

        #region IEnumerable<T> Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (entries != null) ? (entries as IEnumerable).GetEnumerator() : null;
        }

        #endregion

        public override string ToString()
        {
            string str = string.Empty;
            foreach (T s in this)
            {
                str += s + " ";
            }
            return str;
        }
    }
}
