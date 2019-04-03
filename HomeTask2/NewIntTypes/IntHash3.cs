namespace HomeTask2.NewIntTypes
{
    public class IntHash3:IntHash1
    {
        
        private IntHash3(int value):base(value)
        {
        }

        public static implicit operator IntHash3(int value)
        {
            return new IntHash3(value);
        }

        public override int GetHashCode()
        {
            return Value;
        }
    }
}
