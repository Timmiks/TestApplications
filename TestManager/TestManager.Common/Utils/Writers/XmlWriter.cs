using System.Xml.Linq;
using TestManager.Common.Interfaces;

namespace TestManager.Common.Utils
{
    public class XmlWriter : IWriter
    {
        public void WriteDataTo(object data, string location)
        {
            (data as XDocument).Save(location);
        }
    }
}
