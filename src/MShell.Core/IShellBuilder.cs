using System;
using System.Collections.Generic;
using System.Text;

namespace MShell.Core
{
    public interface IShellBuilder
    {
        IShellBuilder UseConfiguration(Action<IConfigurationProvider> configAction);
        Shell Build();
    }
}
