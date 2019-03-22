
using Communication.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Communication.Udp
{
    public class UdpServer : IUdpServer
    {
        private readonly IUdpClientListener _listener;
        public UdpServer(IUdpClientListener listener)
        {
            _listener = listener;
        }

        public bool Disconnect(int connId, bool isForce)
        {
            throw new NotImplementedException();
        }

        public int[] GetAllConnectionIDs()
        {
            throw new NotImplementedException();
        }

        public int GetConnectionCount()
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

        public bool Send(int connId, byte[] buffer, int offset, int size)
        {
            throw new NotImplementedException();
        }

        public bool SetHeartBeatInterval(int second)
        {
            throw new NotImplementedException();
        }

        public bool SetMaxConnectionCount(int count)
        {
            throw new NotImplementedException();
        }

        public bool SetSocketBufferSize(int size)
        {
            throw new NotImplementedException();
        }

        public bool SetSocketListenQueueLength(int length)
        {
            throw new NotImplementedException();
        }

        public bool SetWorkerThreadCount(int count)
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
