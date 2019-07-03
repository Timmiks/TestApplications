using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using TestManager.Common.BO;
using TestManager.Common.Utils;

namespace TestManager.Common
{
    public static class Utility
    {
        private const string FolderAppLogName = "AppLog";
        private const string FolderScreenshotsName = "Screenshots";
        private const string FolderVideoName = "Video";
        private const string FolderWebReportName = "WebReport";

        private static string lastLogPath;

        private static string assemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public static string GetApplicationPathFrom(string location, SourceType sourceType) => GetConfigInfo("AppPath", location, sourceType);

        public static string GetApplicationNameFrom(string location, SourceType sourceType) => GetConfigInfo("AppName", location, sourceType);

        public static void GenerateAppLogFolder() => GenerateFolder(FolderAppLogName);

        public static void GenerateScreenshotsFolder() => GenerateFolder(FolderScreenshotsName);

        public static void GenerateVideoFolder() => GenerateFolder(FolderVideoName);

        public static void GenerateWebReportFolder() => GenerateFolder(FolderWebReportName);

        private static void GenerateFolder(string folderName)
        {
            Directory.CreateDirectory(Path.Combine(assemblyLocation, folderName));
        }

        public static void OrganizeReportItems()
        {
            File.Copy(lastLogPath, Path.Combine(assemblyLocation, FolderAppLogName, Path.GetFileName(lastLogPath)));

            foreach(var file in Directory.GetFiles(assemblyLocation, "*.jpeg"))
            {
                File.Move(Path.Combine(assemblyLocation, Path.GetFileName(file)), 
                    Path.Combine(assemblyLocation, FolderScreenshotsName, Path.GetFileName(file)));
            }

            foreach (var file in Directory.GetFiles(assemblyLocation, "*.mp4"))
            {
                File.Move(Path.Combine(assemblyLocation, Path.GetFileName(file)),
                    Path.Combine(assemblyLocation, FolderVideoName, Path.GetFileName(file)));
            }

            Directory.Move(Path.Combine(assemblyLocation, "Bar Chart_files"), Path.Combine(assemblyLocation, FolderWebReportName, "Bar Chart_files"));
            File.Move(Path.Combine(assemblyLocation, "Bar Chart.html"), Path.Combine(assemblyLocation, FolderWebReportName, "Bar Chart.html"));
        }

        public static string GetStatisticFileFrom(string location, SourceType sourceType) => GetConfigInfo("RunStatPath", location, sourceType);

        public static void KillAppProcesses()
        {
            var appProc = Process.GetProcesses().Where(process => process.MainWindowTitle.Equals(Global.AppName));
            foreach (var proc in appProc)
            {
                proc.Kill();
            }
        }

        private static void FillRunInfoFromLog(ref RunInfo runInfo, string location, SourceType sourceType)
        {
            DirectoryInfo dir = new DirectoryInfo(location);
            lastLogPath = dir.GetFiles("Log_*.txt").OrderBy(f=> f.LastWriteTime).Last().FullName;


            var fileData = ReaderFactory.GetReader(sourceType).GetDataFrom(lastLogPath);
            if (fileData is string data)
            {
                runInfo.LogName = lastLogPath.Split('\\').Last();
                runInfo.FullRotationCount = data.Split(new string[] { "[notice ]" }, StringSplitOptions.None)
                    .Skip(1)
                    .Select(logEntry =>
                    {
                        int startIndex = logEntry.IndexOf("rotations count: ") + 17;
                        int endIndex = logEntry.IndexOf(";", startIndex);
                        return Convert.ToInt32(logEntry.Substring(startIndex, endIndex - startIndex));
                    })
                    .Max();
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private static string GetConfigInfo(string requestText, string location, SourceType sourceType)
        {
            var data = ReaderFactory.GetReader(sourceType).GetDataFrom(location);
            if (data is XDocument xml)
            {
                return xml.Root.Element(requestText).Value;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public static List<RunInfo> GetStatistics(string location, SourceType sourceType)
        {
            List<RunInfo> lastRunInfo = new List<RunInfo>();
            var data = ReaderFactory.GetReader(sourceType).GetDataFrom(location);
            if (data is XDocument xml)
            {
                var runInfos = xml.Root.Elements("Run");
                if(runInfos.Any())
                {
                    foreach (var runInfo in runInfos)
                    {
                        lastRunInfo.Add(new RunInfo()
                        {
                            LogName = runInfo.Element("LogName").Value,
                            FullRotationCount = Convert.ToInt32(runInfo.Element("FullRotations").Value),
                            LifeCycleTime = TimeSpan.FromSeconds(Convert.ToDouble(runInfo.Element("LifeTime").Value))
                        });
                    }
                }
                return lastRunInfo;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public static void AppendStatistics(RunInfo runInfo, string location, SourceType sourceType)
        {
            var data = ReaderFactory.GetReader(sourceType).GetDataFrom(location);
            if (data is XDocument xml)
            {
                xml.Root.Add(new XElement("Run",
                    new XElement("LogName", runInfo.LogName),
                    new XElement("FullRotations", runInfo.FullRotationCount),
                    new XElement("LifeTime", runInfo.LifeCycleTime.Seconds)));
            }
            else
            {
                throw new NotImplementedException();
            }
            WriterFactory.GetWriter(sourceType).WriteDataTo(data, location);
        }

        public static RunInfo GenerateRunInfo(TimeSpan appLifeTime)
        {
            RunInfo runInfo = new RunInfo();
            runInfo.LifeCycleTime = appLifeTime;
            FillRunInfoFromLog(ref runInfo, Global.AppPath, SourceType.TextFile);
            return runInfo;
        }
    }

    public enum SourceType
    {
        XML,
        JSON,
        TextFile
    }
}
   
