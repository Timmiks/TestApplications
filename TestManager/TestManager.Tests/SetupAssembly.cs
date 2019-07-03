using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestManager.Common;
using TestManager.Common.BO;
using TestManager.Common.Extensions;
using TestManager.Tools;

namespace TestManager
{
    [TestClass]
    public class SetupAssemblyCleanup
    {
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            Utility.GenerateVideoFolder();
            Utility.GenerateScreenshotsFolder();
            Utility.GenerateAppLogFolder();
            Utility.GenerateWebReportFolder();
        }


        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            Utility.KillAppProcesses();

            RunInfo runInfo = Utility.GenerateRunInfo(Reporter.LifeTime);
            List<RunInfo> runsToReport = Utility.GetStatistics(Global.StatisticFilePath, SourceType.XML).Add<RunInfo>(runInfo);
            Utility.AppendStatistics(runInfo, Global.StatisticFilePath, SourceType.XML);
            Reporter.GenerateGraph(runsToReport);

            Utility.OrganizeReportItems();
        }
    }
}
