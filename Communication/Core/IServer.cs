using System;
using System.Collections.Generic;
using System.Text;

namespace Communication.Core
{
    public interface IServer : ISocket
    {
        bool Start();

        bool Stop();

    }
}
