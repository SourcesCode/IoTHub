using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Communication.Core
{
    public class RemoteEndPoint
    {
        public EndPoint EndPoint { get; set; }
        public Socket Socket { get; private set; }

    }
}
