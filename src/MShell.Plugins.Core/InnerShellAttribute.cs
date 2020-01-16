using System;
using System.Collections.Generic;
using System.Text;

namespace MShell.Plugins.Core
{
    public class InnerShellAttribute : Attribute
    {
        public string Name { get; set; }
        public string EntryCommand { get; set; }
    }
}
