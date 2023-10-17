using Inventec.Desktop.Common.Message;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Utility
{
    public class ActionWorkerUtil
    {
        public static void Execute(Action action)
        {
            Stopwatch overallStopWatch = new Stopwatch();
            System.ComponentModel.BackgroundWorker backgroundWorker1 = new BackgroundWorker();
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.ProgressChanged += delegate
            {
                double totalSeconds = overallStopWatch.Elapsed.TotalSeconds;
                if (totalSeconds >= 2)
                {
                    WaitingManager.Show();
                }
            };
            backgroundWorker1.DoWork += delegate
            {
                overallStopWatch.Start();
                if (action != null)
                    action();
            };
            backgroundWorker1.RunWorkerCompleted += delegate
            {
                WaitingManager.Hide();
                //MessageBox.Show("Completed");
            };
            backgroundWorker1.RunWorkerAsync();
        }

    }
}
