using System;
using System.Collections.Generic;
using System.Text;

namespace Communication.Core
{
    public interface IAgent : ISocket
    {
        bool Start();
        bool Stop();
        bool Connect(int connId);

    }
}
