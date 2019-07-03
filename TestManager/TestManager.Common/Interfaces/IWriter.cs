using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestManager.Common.Interfaces
{
    public interface IWriter
    {
        void WriteDataTo(object data, string location);
    }
}
