using System;
using TestManager.Common.Interfaces;

namespace TestManager.Common.Utils
{
    public static class ReaderFactory
    {
        public static IReader GetReader(SourceType configType)
        {
            switch (configType)
            {
                case SourceType.XML:
                    return new XmlReader();
                case SourceType.JSON:
                    throw new NotImplementedException();
                case SourceType.TextFile:
                    return new TextFileReader();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}