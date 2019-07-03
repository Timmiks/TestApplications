using System;
using TestManager.Common.Interfaces;

namespace TestManager.Common.Utils
{
    public static class WriterFactory
    {
        public static IWriter GetWriter(SourceType configType)
        {
            switch (configType)
            {
                case SourceType.XML:
                    return new XmlWriter();
                case SourceType.JSON:
                    throw new NotImplementedException();
                case SourceType.TextFile:
                    throw new NotImplementedException();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}