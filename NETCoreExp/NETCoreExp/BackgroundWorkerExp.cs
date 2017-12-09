using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;

namespace NETCoreExp
{
    class BackgroundWorkerExp
    {
        private BackgroundWorker backgroundWorker;

        public BackgroundWorkerExp()
        {
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
        }

        public void RunAsync()
        {
            if (backgroundWorker.IsBusy)
            {
                Console.WriteLine("backgroundWorker is running");
                return;
            }

            backgroundWorker.RunWorkerAsync("argument");

            //do
            //{
            //    Thread.Sleep(10 * 1000);
            //    Random random = new Random();
            //    int r = random.Next(0, 5);
            //    if (r % 5 == 0)
            //    {
            //        StopAsync();
            //        break;
            //    }
            //} while (true);

            //Timer timer = new Timer(new TimerCallback((state) => 
            //{
            //    Random random = new Random();
            //    int r = random.Next(0,5);
            //    if (r % 5 == 0)
            //    {
            //        StopAsync();
            //    }
            //}), "state", TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(10));
        }

        public void StopAsync()
        {
            if (backgroundWorker != null)
            {
                backgroundWorker.CancelAsync();
            }
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Console.WriteLine("error!");
                return;
            }

            if (e.Cancelled)
            {
                Console.WriteLine("cancelled!");
                return;
            }

            Console.WriteLine("Completed!");
            Console.WriteLine($"result : {e.Result}");
        }

        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Console.WriteLine($"progressPercentage : {e.ProgressPercentage}");
            Console.WriteLine($"userState : {e.UserState}");
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Console.WriteLine($"argument : {e.Argument}");

            for (int i = 0; i < 100; i++)
            {
                if (backgroundWorker.CancellationPending)
                {
                    e.Cancel = true;
                    backgroundWorker.Dispose();
                    return;
                }

                //TODO
                Thread.Sleep(1000);
                backgroundWorker.ReportProgress(i, $"userState{i}");
            }

            e.Result = "ok";
        }
    }
}
