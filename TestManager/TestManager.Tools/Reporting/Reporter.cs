using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using TestManager.Common;
using TestManager.Common.BO;
using TestManager.Common.Extensions;

namespace TestManager.Tools
{
    public static class Reporter
    {
        public static TimeSpan LifeTime { get; set; }

        public static void GenerateGraph(List<RunInfo> runsToReport)
        {
            GenerateHtmlGraph(runsToReport);
        }

        private static void GenerateHtmlGraph(List<RunInfo> logs)
        {
            string htmlTemplate =
                @"{
				label: '[Name]',
				backgroundColor: color(window.chartColors.[Color]).alpha(0.5).rgbString(),
				borderColor: window.chartColors.red,
				borderWidth: 1,
				data: [
					[RotationCount],
                    [LifeTime]
				]
			}";

            string htmlTemplatePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Bar Chart.html");
            var data = File.ReadAllText(htmlTemplatePath);

            List<string> templates = new List<string>();

            var lastFive = logs.TakeLast(5);

            for (int i = 0; i < lastFive.Count(); i++)
            {
                var temp = htmlTemplate
                    .Replace("[Name]", logs[i].LogName)
                    .Replace("[Color]", ((Colors)i).ToString().ToLower())
                    .Replace("[RotationCount]", logs[i].FullRotationCount.ToString())
                    .Replace("[LifeTime]", logs[i].LifeCycleTime.Seconds.ToString());
                templates.Add(temp);
            }

            data = data.Replace("[DataSets]", string.Join(",", templates));


            File.WriteAllText(htmlTemplatePath, data);
        }


        public static void CollectLifeTime(TimeSpan time)
        {
            LifeTime = time;
        }


    }

    internal enum Colors
    {
        Red,
        Orange,
        Yellow,
        Green,
        Blue
    }
}