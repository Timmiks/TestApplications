using Accord.Video;
using System.Drawing;

namespace TestManager.Tools
{
    public static class Source
    {
        public static ScreenCaptureStream instance { get; set; }
        static Source()
        {
            var region = new Rectangle(0, 0, 1920, 1080);
            instance = new ScreenCaptureStream(region, 250);
        }
    }
}
