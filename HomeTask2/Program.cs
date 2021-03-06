﻿using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using HomeTask2.NewIntTypes;

namespace HomeTask2
{
    public class Test
    {
        const int Iterations = 1000000;
        const string ObjectName = "Some Object {0}";

        [Benchmark]
        public void TestAssociatedContainerWithHash1()
        {
            Console.WriteLine("Test an associated container of pairs: <int, object> with hash = 101 * ((x >> 24) + 101*((x >> 16)+101*(x >> 8)))+x");
            AssociatedContainer<IInt, object> container = new AssociatedContainer<IInt, object>();
            for (int i = 0; i < Iterations; i++)
            {
                IntHash1 integer = i;
                integer = integer.GetHashCode();
                try
                {
                    container.Add(integer, string.Format(ObjectName, i));
                }
                catch (ArgumentException) { }
            }
        }

        [Benchmark]
        public void TestAssociatedContainerWithHash2()
        {
            Console.WriteLine("Test an associated container of pairs: <int, object> with hash = ((x >> 16) ^ x) * 0x45d9f3b");
            AssociatedContainer<IInt, object> container2 = new AssociatedContainer<IInt, object>();
            for (int i = 0; i < Iterations; i++)
            {
                IntHash2 integer = i;
                integer = integer.GetHashCode();
                try
                {
                    container2.Add(integer, string.Format(ObjectName, i));
                }
                catch (ArgumentException) { }
            }
        }

        [Benchmark]
        public void TestAssociatedContainerWithHash3()
        {
            Console.WriteLine("Test an associated container of pairs: <int, object> with hash = x");
            AssociatedContainer<IInt, object> container0 = new AssociatedContainer<IInt, object>();
            for (int i = 0; i < Iterations; i++)
            {
                IntHash3 integer = i;
                integer = integer.GetHashCode();
                try
                {
                    container0.Add(integer, string.Format(ObjectName, i));
                }
                catch (ArgumentException) { }
            }
        }

        [Benchmark]
        public void TestAssociatedContainerWithCollisions()
        {
            Console.WriteLine("Test an associated container of pairs: <int, object> with bad hash");
            AssociatedContainer<IInt, object> container3 = new AssociatedContainer<IInt, object>();
            for (int i = 0; i < Iterations; i++)
            {
                IntHashCollision integer = i;
                integer = integer.GetHashCode();
                try
                {
                    container3.Add(integer, string.Format(ObjectName, i));
                }
                catch (ArgumentException) { }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Test>();
        }
    }
}
