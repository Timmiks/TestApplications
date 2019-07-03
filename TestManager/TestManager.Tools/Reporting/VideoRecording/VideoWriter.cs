using Accord.Video.FFMPEG;
using Accord.Video;
using System;
using System.IO;
using System.Reflection;

namespace TestManager.Tools
{
    public static class VideoWriter
    {
        public static VideoFileWriter instance { get; set; }
        private static int Width = 1920;
        private static int Height = 1080;
        private static int VIDEO_FRAME_PER_SECOND = 4;
        public static string RecordingName = "";
        private static int bitrate = Convert.ToInt32(Height * Width * VIDEO_FRAME_PER_SECOND * 0.07);
        static VideoWriter()
        {
            instance = new VideoFileWriter();
        }
        public static void Open()
        {
            instance.Open(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), RecordingName),
                Width, Height, VIDEO_FRAME_PER_SECOND, VideoCodec.H264, bitrate);
        }
        public static void OnNewFrame(object sender, NewFrameEventArgs e)
        {
            lock (instance)
            {
                try
                {
                    instance.WriteVideoFrame(e.Frame);
                }
                catch
                {
                    
                }
            }
        }

    }
}
