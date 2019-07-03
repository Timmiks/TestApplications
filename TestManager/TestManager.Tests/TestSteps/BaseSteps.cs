using System;
using System.Collections.Generic;
using System.Linq;

namespace TestManager.TestSteps
{
    public class BaseSteps
    {
        private readonly Lazy<AppSteps> appSteps = new Lazy<AppSteps>();
        public AppSteps App => appSteps.Value;
    }
}
