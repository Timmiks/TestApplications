using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;

namespace TestManager.Tools
{
    public static class Screenshot
    {
        public static readonly string SCREENSHOTS_FOLDER = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public static Bitmap GetScreenshot()
        {
            Bitmap printScreen;
            try
            {
                int screenLeft = SystemInformation.VirtualScreen.Left;
                int screenTop = SystemInformation.VirtualScreen.Top;
                int screenWidth = SystemInformation.VirtualScreen.Width;
                int screenHeight = SystemInformation.VirtualScreen.Height;

                printScreen = new Bitmap(screenWidth, screenHeight);
                using (Graphics g = Graphics.FromImage(printScreen))
                {
                    g.CopyFromScreen(screenLeft, screenTop, 0, 0, printScreen.Size);
                }
            }
            catch (Exception e)
            {
                printScreen = new Bitmap(1, 1);
            }
            return printScreen;
        }


        public static void TakeScreenshot(string fileName, string folderName)
        {
            Bitmap printScreen = GetScreenshot();
            try
            {
                printScreen.Save((Path.Combine(folderName, fileName + "." + ImageFormat.Jpeg)), ImageFormat.Jpeg);
            }
            catch
            {
            }
        }

        public static void TakeScreenshot(string fileName) => TakeScreenshot(fileName, SCREENSHOTS_FOLDER);

    }
}
