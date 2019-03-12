using Communication.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Communication.Tcp
{
    public class TcpServer : ITcpServer
    {
        private readonly ITcpServerListener _listener;
        public TcpServer(ITcpServerListener listener)
        {
            _listener = listener;
        }
        public bool Close()
        {

            throw new NotImplementedException();
        }

        public bool Disconnect(int connId, bool isForce)
        {
            throw new NotImplementedException();
        }

        public int[] GetAllConnectionIDs()
        {
            throw new NotImplementedException();
        }

        public bool GetConnectionCount()
        {
            throw new NotImplementedException();
        }

        public EndPoint GetLocalEndPoint(int connId)
        {
            throw new NotImplementedException();
        }

        public EndPoint GetRemoteEndPoint(int connId)
        {
            throw new NotImplementedException();
        }

        public bool IsConnected()
        {
            throw new NotImplementedException();
        }

        public bool Receive(int connId, byte[] buffer, int offset, int size)
        {
            throw new NotImplementedException();
        }

        public bool Send(int connId, byte[] buffer, int offset, int size)
        {
            throw new NotImplementedException();
        }

        public bool Start()
        {
            throw new NotImplementedException();
        }

        public bool Stop()
        {
            throw new NotImplementedException();
        }
    }
}
