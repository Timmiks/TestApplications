using System.IO;
using System.Reflection;

namespace TestManager.Common
{
    public static class Global
    {
        private static string appPath = "";
        private static string appName = "";
        private static string statisticFilePath = "";

        private static string ConfigName => "TestRunConfig.xml";
        private static string ConfigPath => Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), ConfigName);

        public static string AppPath
        {
            get
            {
                if(string.IsNullOrEmpty(appPath))
                {
                    appPath = Utility.GetApplicationPathFrom(ConfigPath, SourceType.XML);
                }
                return appPath;
            }
        }

        public static string AppName
        {
            get
            {
                if (string.IsNullOrEmpty(appName))
                {
                    appName = Utility.GetApplicationNameFrom(ConfigPath, SourceType.XML);
                }
                return appName;
            }
        }

        public static string StatisticFilePath
        {
            get
            {
                if (string.IsNullOrEmpty(statisticFilePath))
                {
                    statisticFilePath = Utility.GetStatisticFileFrom(ConfigPath, SourceType.XML);
                }
                return statisticFilePath;
            }
        }
    }
}
