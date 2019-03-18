using Communication.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Communication.Tcp
{
    public class TcpServer : ITcpServer
    {
        private readonly ITcpServerListener _listener;
        public TcpServer(ITcpServerListener listener)
        {
            //OnPrepareConnect += listener.OnPrepareConnect;
            //OnConnected += listener.OnConnected;
            OnHandShake += listener.OnHandShake;
            OnAccept += listener.OnAccept;
            OnSend += listener.OnSend;
            OnReceive += listener.OnReceive;
            OnDisconnected += listener.OnDisconnected;
            OnShutdown += listener.OnShutdown;
        }

        //public event Func<TcpServer, int, Socket, EventResult> OnPrepareConnect;
        //public event Func<TcpServer, int, EventResult> OnConnected;
        public event Func<TcpServer, int, EventResult> OnHandShake;
        public event Func<TcpServer, int, Socket, EventResult> OnAccept;
        public event Func<TcpServer, int, byte[], int, int, EventResult> OnSend;
        public event Func<TcpServer, int, byte[], int, int, EventResult> OnReceive;
        public event Func<TcpServer, int, EventResult> OnDisconnected;
        public event Func<TcpServer, EventResult> OnShutdown;

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
