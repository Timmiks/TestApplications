using System;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using TestManager.Common.Interfaces;

namespace TestManager.Common.Utils
{
    public class XmlReader : IReader
    {
        public object GetDataFrom(string location)
        {
            XDocument doc = XDocument.Load(location);
            return doc;
        }
    }
}