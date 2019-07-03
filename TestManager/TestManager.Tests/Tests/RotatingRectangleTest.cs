using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestManager.Common;
using TestManager.Tools;

namespace TestManager.Tests
{
    [TestClass]
    public class RotatingRectangleTest : BaseTestSuite
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void BeforeTest()
        {
            Source.instance.NewFrame += VideoWriter.OnNewFrame;
            VideoWriter.RecordingName = $"Record_{TestContext.Properties["TestName"]}.mp4";
            VideoWriter.Open();
            Source.instance.Start();
        }

        [TestMethod]
        public void TestAppStart()
        {
            Do.App.StartApplication(Global.AppPath + Global.AppName);
        }

        [TestMethod]
        public void TestAppWork()
        {
            Do.App.CheckAppWork(TimeSpan.FromSeconds(45));
        }

        [TestCleanup]
        public void AfterTest()
        {
            Source.instance.Stop();
            VideoWriter.instance.Close();

            if (TestContext.CurrentTestOutcome != UnitTestOutcome.Passed)
            {
                Screenshot.TakeScreenshot($"Fail_{TestContext.Properties["TestName"]}");
            }
        }
    }
}
