using System;
using System.CodeDom.Compiler;

namespace HomeTask2.NewIntTypes
{
    public class IntHash1: IComparable, IInt
    {
        public int Value { get; private set; }

        protected IntHash1(int value)
        {
            Value = value;
        }

        public static implicit operator IntHash1(int value)
        {
            return new IntHash1(value);
        }

        public override int GetHashCode()
        {
            return 101 * ((Value >> 24) + 101 * ((Value >> 16) + 101 * (Value >> 8))) + Value;
        }

        public int CompareTo(object obj)
        {
            int compareResult = -1;
            if (obj.GetHashCode() < this.GetHashCode())
                compareResult = -1;
            else if (obj.GetHashCode() > this.GetHashCode())
                compareResult = 1;
            else if (obj.GetHashCode() == this.GetHashCode())
                compareResult = 0;
            return compareResult;
        }
    }
}
