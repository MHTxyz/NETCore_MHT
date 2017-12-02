using System;
using System.Threading;

namespace NETCoreExp
{
    class Program
    {
        static void Main(string[] args)
        {
            FinalizeDisposeBase finalizeDisposeBase = new FinalizeDisposeBase();
            Thread.Sleep(1000);
            finalizeDisposeBase.Dispose();

            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
