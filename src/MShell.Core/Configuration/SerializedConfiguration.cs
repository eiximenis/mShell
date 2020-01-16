using System;
using System.Collections.Generic;
using System.Text;

namespace MShell.Core.Configuration
{
    public class SerializedConfiguration
    {
        public List<InnerShellConfiguration> InnerShells { get; set; }

        public SerializedConfiguration()
        {
            InnerShells = new List<InnerShellConfiguration>();
        }
    }
}
