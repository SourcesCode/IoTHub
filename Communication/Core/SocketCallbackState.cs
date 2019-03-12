using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Communication.Core
{
    public class SocketCallbackState
    {
        private Socket _socket;  //负责通信的Socket
        private byte[] _buffer = new byte[1024];  //接收数据系统缓冲区
    }
}
