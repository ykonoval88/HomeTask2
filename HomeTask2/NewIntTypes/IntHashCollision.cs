using System;

namespace HomeTask2.NewIntTypes
{
    public class IntHashCollision : IntHash1
    {
        private readonly int _hash;

        private IntHashCollision(int value):base(value)
        {
            Random rand = new Random();
            _hash = rand.Next(1, 20000);
        }

        public static implicit operator IntHashCollision(int value)
        {
            return new IntHashCollision(value);
        }

        public override int GetHashCode()
        {
            return _hash;
        }
    }
}
