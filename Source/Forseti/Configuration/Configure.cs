using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Forseti.Configuration
{
    public class Configure : IConfigure
    {
        public IRunner Runner { get; private set; }


        public IConfigure With<T>() where T : IFramework
        {
            return this;
        }
    }
}
