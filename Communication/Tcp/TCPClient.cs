﻿using Communication.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Communication.Tcp
{
    public class TcpClient : ITcpClient
    {
        private readonly ITcpClientListener _listener;
        public TcpClient(ITcpClientListener listener)
        {
            _listener = listener;
        }

        public bool Connect()
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

        public bool IsConnected()
        {
            throw new NotImplementedException();
        }

        public bool Send(byte[] buffer, int offset, int size)
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
