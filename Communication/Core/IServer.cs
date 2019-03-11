using System;
using System.Collections.Generic;
using System.Text;

namespace Communication.Core
{
    public interface IServer
    {
        bool Start();
        bool Stop();
    }
}
