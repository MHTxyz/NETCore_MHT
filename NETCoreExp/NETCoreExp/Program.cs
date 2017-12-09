using System;
using System.Threading;

namespace NETCoreExp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var randomNumberGenerator = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                byte[] bytes = new byte[1024];
                randomNumberGenerator.GetBytes(bytes);
                string s = BitConverter.ToString(bytes);
            }
            
            
            

            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }

        private void Test()
        {
            #region DisposeExp
            FinalizeDisposeBase finalizeDisposeBase = new FinalizeDisposeBase();
            Thread.Sleep(1000);
            finalizeDisposeBase.Dispose();
            #endregion

            #region BackgroundWorkerExp
            BackgroundWorkerExp workerExp = new BackgroundWorkerExp();
            workerExp.RunAsync();
            do
            {
                Thread.Sleep(10 * 1000);
                Random random = new Random();
                int r = random.Next(0, 10);
                if (r % 2 == 0)
                {
                    workerExp.StopAsync();
                    break;
                }
            } while (true);
            #endregion


        }
    }
}
