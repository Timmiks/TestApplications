using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Linq;
using TestManager.Common;
using TestManager.Tools;

namespace TestManager.TestSteps
{
    public class AppSteps
    {
        public static Stopwatch Timer;
        public void StartApplication(string applicationFullName)
        {
            Timer = new Stopwatch();
            Timer.Start();
            Process.Start(applicationFullName);
            Assert.IsTrue(Process.GetProcessesByName(Global.AppName.Split('.').First()).Any());
        }

        public void CheckAppWork(TimeSpan timeSpan)
        {
            Process p = Process.GetProcessesByName(Global.AppName.Split('.').First()).First();

            try
            {
                while (Timer.Elapsed.Seconds <= timeSpan.Seconds)
                {
                    Assert.IsFalse(Process.GetProcessesByName("WerFault").Any(process => process.MainWindowTitle.Equals(Global.AppName)),
                        "System error is shown");
                    Assert.IsFalse(p.HasExited);
                }
            }
            finally
            {
                Timer.Stop();
                Reporter.CollectLifeTime(Timer.Elapsed);
            }
        }
    }
}