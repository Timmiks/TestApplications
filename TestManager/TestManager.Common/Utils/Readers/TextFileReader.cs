using System.IO;
using System.Reflection;
using TestManager.Common.Interfaces;

namespace TestManager.Common.Utils
{
    internal class TextFileReader : IReader
    {
        public object GetDataFrom(string location)
        {
            using (var stream = File.Open(location, FileMode.Open, FileAccess.ReadWrite, FileShare.Read))
            {
                StreamReader reader = new StreamReader(stream);
                return reader.ReadToEnd();
            }
        }
    }
}