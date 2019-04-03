namespace HomeTask2.NewIntTypes
{
    public class IntHash2:IntHash1
    {
        private IntHash2(int value):base(value)
        {
        }

        public static implicit operator IntHash2(int value)
        {
            return new IntHash2(value);
        }

        public override int GetHashCode()
        {
            return ((Value >> 16) ^ Value) * 0x45d9f3b;
        }
    }
}
