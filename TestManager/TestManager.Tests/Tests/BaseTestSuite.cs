using System;
using System.Linq;
using TestManager.TestSteps;

namespace TestManager
{
    public class BaseTestSuite
    {
        private readonly Lazy<BaseSteps> steps = new Lazy<BaseSteps>();

        protected BaseSteps Do
        {
            get
            {
                return steps.Value;
            }
        }
    }
}
