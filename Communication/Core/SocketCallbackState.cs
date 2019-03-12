using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Communication.Core
{
    public class SocketCallbackState
    {
        public int ConnId { get; set; }
        public Socket Socket { get; set; }  //负责通信的Socket
        public byte[] Buffer { get; set; }  //接收数据系统缓冲区
    }
}
