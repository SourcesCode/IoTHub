using Communication.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Communication.Udp
{
    public class UdpClient : IUdpClient
    {
        private readonly IUdpClientListener _listener;
        public UdpClient(IUdpClientListener listener)
        {
            _listener = listener;
        }

        public int ReceiveBufferSize { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int SendBufferSize { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool Connected { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Socket Client { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool Connect(string remoteIp, int remotePort)
        {
            throw new NotImplementedException();
        }

        public bool Disconnect()
        {
            throw new NotImplementedException();
        }

        public int GetConnectionID()
        {
            throw new NotImplementedException();
        }

        public EndPoint GetLocalEndPoint()
        {
            throw new NotImplementedException();
        }

        public EndPoint GetRemoteEndPoint()
        {
            throw new NotImplementedException();
        }

        public bool Send(byte[] buffer, int offset, int size)
        {
            throw new NotImplementedException();
        }
    }
}
